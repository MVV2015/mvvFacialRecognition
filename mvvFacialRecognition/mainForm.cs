using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Neurotec.Biometrics;
using Neurotec.Biometrics.Gui;
using Neurotec.Devices;
using Neurotec.Images;
using Neurotec.IO;
using Neurotec.Licensing;


namespace mvvFacialRecognition
{

    public partial class mainForm : Form
    {
        bool useNLView = true;
        bool isLive = false;
        bool fullScreen = false;
        bool boundingBoxOn = true;
        bool matchNow = false;
        static int frameRate = 25; // Set video frame rate here.
        static int framesBetweenSample = 1000 / frameRate; // one second before match attempt
        List<string> userIdList = new List<string>();
        Pen p;
        Bitmap capturedImage = null;
        Bitmap bmp = null;
        Thread myThread;
        Thread matchingThread;
        dbInterface myDdInterface = new dbInterface();

        const string Components = "Biometrics.FaceExtraction,Devices.Cameras,Biometrics.FaceMatching";
        NCamera camera = null;
        NImage currentImage = null;
        NGrayscaleImage grayscaleImage = null;
        NleFace thisFace;
        NLExtractor templateExtractor = new NLExtractor();

        // maybe we can try using this instread of NLAttributes[]
        NLAttributes details;// for NLExtractor and NleFace

        NBiometricStatus extractionStatus = NBiometricStatus.None;
        NLTemplate facialTemplate = null;


        public mainForm()
        {
            InitializeComponent();
            verifyLicense();
            operationsTabControl.SelectedIndexChanged += new EventHandler(operationsTabControl_SelectedIndexChanged);
            //userIdDropDown.SelectionChangeCommitted += new EventHandler(userIdSelected);
            NDeviceManager devMan = new NDeviceManager(NDeviceType.Camera, true, false, System.Threading.SynchronizationContext.Current);

            checkForCamera(devMan);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            // start the video feed
            if (camera != null)
            {
                // Start live feed
                isLive = true;
                myThread = new Thread(getLiveVideo);
                myThread.Start();
                mainFeedPictureBox.Image = null;
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
// rework thread calls
                isLive = false;
                if (matchingThread!=null && matchingThread.IsAlive)
                { matchingThread.Abort(); }

                if (myThread!=null && myThread.IsAlive)
                {
                    myThread.Resume();
                    myThread.Abort(); 
                }

                NLicense.ReleaseComponents(Components);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void checkForCamera(NDeviceManager devMan)
        {
            int count = devMan.Devices.Count();

            if (count > 0)
            {
                camera = (NCamera)devMan.Devices[0];
// get or set video format?                
            }
            else
            {
                MessageBox.Show("No cameras found");
                // decide what to do if there is not camera
            }
        }

        private void getLiveVideo()
        {  // what if the camera is disconnected during feed?
            verifyLicense();
            templateExtractor.TemplateSize = NleTemplateSize.Large;
            templateExtractor.DetectAllFeaturePoints = false;
            // False, will only detect eyes.
            templateExtractor.FavorLargestFace = true;
            // Extract details only on the largest face

            NleDetectionDetails[] detectionDetails;
            camera.StartCapturing();

            //create graphics object
            p = new Pen(currentView.FaceRectangleColor, 1);
            var timer = new System.Diagnostics.Stopwatch();
            int timeSpan;
            int elapsed = 0;
            int frameDelay = 1000 / frameRate;
            int matchDelay = 0; // When this equals framesBetweenSample, will attempt match

            while (isLive == true)
            {
                timer.Start();
                currentImage = camera.GetFrame();
                currentImage.FlipHorizontally();
                bmp = currentImage.ToBitmap();
                Draw drawfeatures = new Draw();
                // create grayscale image for template operations
                grayscaleImage = currentImage.ToGrayscale();

                if (useNLView)
                {
                    if (currentView.Visible == false)
                    {
                        currentView.Invoke(new Action(() => currentView.Visible = true));
                    }

                    // remove mainFeedPictureBox to invisible
                    if (mainFeedPictureBox.Visible == true)
                    {
                        if (mainFeedPictureBox.InvokeRequired)
                        {
                            mainFeedPictureBox.Invoke(new Action(() => mainFeedPictureBox.Visible = false));
                        }
                        else
                        { mainFeedPictureBox.Visible = false; }
                    }
                    // Set image to currentView
                    currentView.Invoke(new Action(() => currentView.Image = bmp));

                    if (templateExtractor.DetectFace(grayscaleImage, out thisFace))
                    //if (detectDetails(grayscaleImage, out detectionDetails))
                    {
                        currentView.DetectionDetails = detectDetails(grayscaleImage);
                        if(matchNow)
                        { matchDelay++; }
                         
                        //    // Pause to see bounding box
                        //    Thread.Sleep(250);
                    }
                }

                if (!useNLView)
                {
                    // make currentView invisible
                    if (currentView.Visible == true)
                    {
                        currentView.Invoke(new Action(() => currentView.Visible = false));
                    }
                    // make mainFeedPictureBox visible
                    if (mainFeedPictureBox.Visible == false)
                    {
                        mainFeedPictureBox.Invoke(new Action(() => mainFeedPictureBox.Visible = true));
                    }

                    if (templateExtractor.DetectFace(grayscaleImage, out thisFace))
                    {
                        if (matchNow)
                        { matchDelay++; }

                        Point locConfText = new Point(mainFeedPictureBox.Bottom - thisFace.Rectangle.Bottom, mainFeedPictureBox.Left + thisFace.Rectangle.Left);
                        if (boundingBoxOn)
                        {
                            // We can only draw the rectangle and confidence score with a NleFace object. 
                            // If we want more we have to use NlView.
                            bmp = drawfeatures.drawFaceRectangle(thisFace, bmp, (int)thisFace.Confidence, p);
                        }
                    }

                    // display image on pictureBox
                    mainFeedPictureBox.Invoke(new Action(() => mainFeedPictureBox.Image = bmp));

                    //// pause to see bounding box
                    //Thread.Sleep(250);
                }
                if (matchNow && (matchDelay == framesBetweenSample))
                {
                    // Should I shut off face detection while matching?
                    matchingThread = new Thread(attemptMatch);
                    matchingThread.Start();
                    myThread.Suspend();
                    //Can I abort this thread here or can I do it at the end of the matching method?
                    //matchingThread.Abort();
                    matchDelay = 0;
                }

                timer.Stop();
                elapsed = (Int32)timer.ElapsedMilliseconds;
                timeSpan = frameDelay - elapsed;
                if (timeSpan < 0)
                {
                    timeSpan = 0;
                }
                Thread.Sleep(timeSpan);
                timer.Reset();

            }
            camera.StopCapturing();
            //mainFeedPictureBox.Image = null;
            myThread.Abort();
        }

        private void attemptMatch()
        {
            try
            {
                // use new objects to avoid conflicts
                NLView insetView = new NLView();
                NLExtractor viewExtractor = new NLExtractor();
                NleFace snipFace = new NleFace();
                NleDetectionDetails[] theseDetails = new NleDetectionDetails[1];
                Draw makeImage = new Draw();
                p = new Pen(currentView.FaceRectangleColor, 1);

                // Detect all features
                if (viewExtractor.DetectAllFeaturePoints == false)
                {
                    viewExtractor.DetectAllFeaturePoints = true;
                }
                viewExtractor.DetectFace(grayscaleImage, out snipFace);
                theseDetails[0] = viewExtractor.DetectFacialFeatures(grayscaleImage, snipFace);
                insetView.DetectionDetails = theseDetails;
                //insetView.DetectionDetails = detectDetails(grayscaleImage);
                //enrollmentPictureBox.Image = (Bitmap) currentView.Image.Clone();
                //.DrawToBitmap(enrollmentPictureBox.Image, croppedFace);
                //enrollmentPictureBox.Image = faceSnip;

                // put new pictureBox on form with detection features.
                enrollmentPictureBox.Invoke(new Action(() => enrollmentPictureBox.Height = 
                    (int)(insetView.DetectionDetails[0].Face.Rectangle.Height * 1.5)));

                enrollmentPictureBox.Invoke(new Action(() => enrollmentPictureBox.Width = 
                    (int)(insetView.DetectionDetails[0].Face.Rectangle.Width * 1.5)));
                Image faceSnip = makeImage.snipFace(bmp, insetView.DetectionDetails, p);
                enrollmentPictureBox.Invoke(new Action(() => enrollmentPictureBox.Image = faceSnip));
                enrollmentPictureBox.Invoke(new Action(() => enrollmentPictureBox.Visible = true));

                // check for match
                if(identify(grayscaleImage))
                {
                    // add second pictureBox with file image?
                    // Play linked video
                }
                else
                {
                    MessageBox.Show("Subject not registered.");
                    // continue matching? add yes or no buttons
                }
 
                // If identification successful, play video and stop all other processes.
                // Extractor methods pg 2355
                matchNow = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
            finally
            {
                if(templateExtractor!=null)
                {
                    templateExtractor.Dispose();
                }
                matchingThread.Abort();
            }
        }

        private bool identify(NGrayscaleImage grayscaleImage)
        {
            verifyLicense();
            cryptography decrypt = new cryptography();
            NMatcher templateMatcher = null;
            int numRecords = 0;

            try
            {
                NBuffer probeTemplateBuffer = null;
                byte[] probeTemplateArray = null;
                try
                {
                    grayscaleImage = currentImage.ToGrayscale();
                    if (!createTemplate())
                    {
                        return false;
                    }

                    probeTemplateBuffer = facialTemplate.Save();
                    probeTemplateArray = probeTemplateBuffer.ToByteArray();
                }
                catch (IOException ex)
                {
                    MessageBox.Show("error reading input file {0}: " + ex);
                    return false;
                }

                // extract gallery templates
                numRecords = myDdInterface.numOfRecords();
                List<byte[]> dbaseTemplates = new List<byte[]>();

                for (int i = 0; i < numRecords; i++)
                {
                    try
                    {
                        byte[] tempArray = myDdInterface.getTemplateFromKey(i + 1);
                        dbaseTemplates.Add(tempArray);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("error reading reference image " + i + ": " + ex);
                        return false;
                    }
                }

                templateMatcher = new NMatcher();
                templateMatcher.IdentifyStart(probeTemplateArray);
                try
                {
                    int keyNum = 0;
                    int highScore = 0;
                    for (int i = 1; i <= numRecords; i++)
                    {
                        int score = templateMatcher.IdentifyNext(dbaseTemplates[i - 1]);
                        if (score > highScore)
                        {
                            highScore = score;
                            keyNum = i;
                        }
                    }
                    if (keyNum == 0)
                    {
                        MessageBox.Show("No Match found");
                        return false;
                    }
                    else
                    {//use assigned values for getting info from sending thread

                        //identifyUserIdTxtBox.Text = myDdInterface.userIdFromKey(keyNum);
                        //imagePictureBox.Image = myDdInterface.getImageFromId(identifyUserIdTxtBox.Text);
                        //identifyFNameTxtBox.Text = myDdInterface.getFName(identifyUserIdTxtBox.Text);
                        //identifyLNameTxtBox.Text = myDdInterface.getLName(identifyUserIdTxtBox.Text);
                        //idTabNameGroupBox.Visible = true;
                        //MessageBox.Show("Image identified as " + identifyFNameTxtBox.Text + " " + identifyLNameTxtBox.Text + "\nWith a score of: " + highScore);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
                finally
                {
                    templateMatcher.IdentifyEnd();
                    facialTemplate = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
            finally
            {
                NLicense.ReleaseComponents(Components);

                if (templateExtractor != null)
                {
                    templateExtractor.Dispose();
                }
                if (templateMatcher != null)
                {
                    templateMatcher.Dispose();
                }
            }
        }

        private NleDetectionDetails[] detectDetails(NGrayscaleImage grayscaleImage)
        {
            templateExtractor.MaxRecordsPerTemplate = 1;
            // See how many faces are in the image
            NleFace[] facesInIMage = templateExtractor.DetectFaces(grayscaleImage);
            // Get details on the faces
            NleDetectionDetails[] _detectionDetails = new NleDetectionDetails[facesInIMage.Length];
            if (facesInIMage.Length == 0 || grayscaleImage == null)
            {
                _detectionDetails = null;
                return _detectionDetails;
            }
            for (int i = 0; i < _detectionDetails.Length; i++)
            {
                _detectionDetails[i] = templateExtractor.DetectFacialFeatures(grayscaleImage, facesInIMage[i]);
            }
            return _detectionDetails;
        }

        public void verifyLicense()
        {
            while (!NLicense.ObtainComponents("/local", 5000, Components))
            {

                DialogResult dr = MessageBox.Show("Please insert the license key, wait for it to authenticate and then click 'Retry'.", "Retry?", MessageBoxButtons.RetryCancel);
                if (dr == DialogResult.Cancel)
                {
                    // get this to close out the app!!!
                    this.Close();
                }
            }
        }

        private void operationsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (operationsTabControl.SelectedIndex == 0)
            {
                //enrolleeUserId.Text = string.Empty;
                //enrollFName.Text = string.Empty;
                //enrollLName.Text = string.Empty;
                //enrollUserIdGoupBox.Visible = false;
            }
            else if (operationsTabControl.SelectedIndex == 1)
            {
                //verifyFNameTextBox.Text = string.Empty;
                //verifyLNameTxtBox.Text = string.Empty;
                //verifyTabNameGroupBox.Visible = false;
            }
        }

        //// Why doesn't this work after selecting radio button?
        //        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        //        {
        //            if (e.KeyData == Keys.Escape)
        //            {
        //                if (fullScreen == true)
        //                {
        //                    fullScreen = false;
        //                    // resize the form
        //                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        //                    this.WindowState = FormWindowState.Normal;
        //                    // resize display boxes
        //                    // Image Size = {Width = 640 Height = 480}
        //                    mainFeedPictureBox.Location = new Point(100, 110);
        //                    mainFeedPictureBox.Width = 480;
        //                    mainFeedPictureBox.Height = 360;

        //                    currentView.Location = new Point(100, 110);
        //                    currentView.Width = 480;
        //                    currentView.Height = 360;
        //                    currentView.Zoom = (float).75;

        //                    operationsTabControl.Visible = true;

        //                }
        //                else
        //                {
        //                    //fullScreen = true;
        //                    // maximize the form
        //                    this.WindowState = FormWindowState.Maximized;
        //                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

        //                    // enlarge display boxes
        //                    //Screen dimentions = {Width = 1456 Height = 916}
        //                    mainFeedPictureBox.Location = new Point(0, 0);
        //                    mainFeedPictureBox.Height = this.Height;
        //                    mainFeedPictureBox.Width = this.Width;

        //                    currentView.Location = new Point(0, 0);
        //                    currentView.Height = 900;
        //                    currentView.Width = 1200;
        //                    //currentView.Height = this.Height;
        //                    //currentView.Width = this.Width;
        //                    currentView.Zoom = 2;

        //                    operationsTabControl.Visible = false;
        //                }
        //            }
        //        }
        #region image display options

        private void bitmapSelectButton_CheckedChanged(object sender, EventArgs e)
        {
            if (bitmapSelectButton.Checked)
            {
                useNLView = false;
            }
        }

        private void NlViewSelectButton_CheckedChanged(object sender, EventArgs e)
        {
            if (NlViewSelectButton.Checked)
            {
                useNLView = true;
            }
        }

        private void drawEyesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (currentView.ShowEyes == true)
            {
                currentView.ShowEyes = false;
            }
            else
            {
                currentView.ShowEyes = true;
            }
        }

        private void faceConfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (currentView.ShowFaceConfidence == true)
            {
                currentView.ShowFaceConfidence = false;
            }
            else
            {
                currentView.ShowFaceConfidence = true;
            }
        }

        private void showEyeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (currentView.ShowEyesConfidence == false)
            {
                currentView.ShowEyesConfidence = true;
            }
            else
            {
                currentView.ShowEyesConfidence = false;
            }
        }

        private void detectAllcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (templateExtractor.DetectAllFeaturePoints == false)
            {
                templateExtractor.DetectAllFeaturePoints = true;
            }
            else
            {
                templateExtractor.DetectAllFeaturePoints = false;
                currentView.DetectionDetails = null;
            }
        }

        private void markNoseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (currentView.ShowNose == true)
            {
                currentView.ShowNose = false;
            }
            else
            {
                currentView.ShowNose = true;
            }
        }

        private void NoseConfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (currentView.ShowNoseConfidence == true)
            {
                currentView.ShowNoseConfidence = false;
            }
            else
            {
                currentView.ShowNoseConfidence = true;
            }
        }

        private void markMouthheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (currentView.ShowMouth == true)
            {
                currentView.ShowMouth = false;
            }
            else
            {
                currentView.ShowMouth = true;
            }
        }

        private void showMouthConfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (currentView.ShowMouthConfidence == true)
            {
                currentView.ShowMouthConfidence = false;
            }
            else
            {
                currentView.ShowMouthConfidence = true;
            }
        }
        #endregion

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            //// Specify video extentions
            //selectFileDialog.Filter = "Video|*.mpg";
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileLoc = selectFileDialog.FileName;
                // Store the file location in the database.
            }
        }

        private void identifyButton_Click(object sender, EventArgs e)
        {
            if (matchNow == false)
                matchNow = true;
        }

        //private Image currentView_Paint(object sender, PaintEventArgs e)
        //{
        //    Image _currentViewImage = currentView.Image;
        //    return _currentViewImage;
        //}

        private void currentView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!fullScreen)
            {
                fullScreen = true;
                // resize display boxes
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                // Image Size = {Width = 640 Height = 480}
                mainFeedPictureBox.Location = new Point(0, 0);
                mainFeedPictureBox.Width = 1280;
                mainFeedPictureBox.Height = 960;

                currentView.Location = new Point(0, 0);
                currentView.Width = 1280;
                currentView.Height = 960;
                currentView.Zoom = (float)2;
            }
            else
            {
                fullScreen = false;
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

                mainFeedPictureBox.Location = new Point(50, 100);
                mainFeedPictureBox.Width = 640;
                mainFeedPictureBox.Height = 480;

                currentView.Location = new Point(50, 100);
                currentView.Width = 640;
                currentView.Height = 480;
                currentView.Zoom = (float)1;
            }
        }
//// outline for video file playback        
//        bool isUrl = false;
//            int baseFrameIndex = 0;
//            NMediaSource mediaSource = null;
//            NMediaReader mediaReader = null;
//            try
//            {
//                // Obtain license.
//                if (!NLicense.ObtainComponents("/local", 5000, components))
//                {
//                    Console.WriteLine("Could not obtain licenses for components: {0}", components);
//                    return;
//                }

//                // create media source
//                mediaSource = isUrl ? NMediaSource.FromUrl(args[0]) : NMediaSource.FromFile(args[0]);

//                mediaReader = new NMediaReader(mediaSource, NMediaType.Video, true);

//                NImage image = null;
//                mediaReader.Start();
//                while ((image = mediaReader.ReadVideoSample()) != null)
//                {
//                    // Feed video to currentView
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex);
//            }
//            finally
//            {
//                NLicense.ReleaseComponents(components);
//                if (mediaReader != null)
//                {
//                    mediaReader.Stop();
//                    mediaReader.Dispose();
//                }

//                if (mediaSource != null)
//                {
//                    mediaSource.Dispose();
//                }
//            }
    }
}
