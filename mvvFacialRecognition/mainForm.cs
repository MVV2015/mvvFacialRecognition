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
        bool useNLView = false;
        const string Components = "Biometrics.FaceExtraction,Devices.Cameras,Biometrics.FaceMatching";
        dbInterface myDdInterface = new dbInterface();
        Draw getfeatures = new Draw();
        bool isLive = false;
        bool markEyes = true; 
        bool fullScreen = true;
        bool boundingBoxOn = true;
        static int frameRate = 25; // Set video frame rate here.
        static int framesBetweenSample = 2000/frameRate; // two seconds between match attempts

        NCamera camera = null;
        NImage currentImage = null;
        NGrayscaleImage grayscaleImage = null;
        NleFace thisFace;
        NLExtractor templateExtractor = new NLExtractor();
        NLAttributes details;// for NLExtractor and NleFace
        NBiometricStatus extractionStatus = NBiometricStatus.None;
        NLTemplate facialTemplate = null;
        Bitmap capturedImage = null;
        Bitmap bmp = null;
        Thread myThread;
        Thread matchingThread;
        //ManualResetEvent pauseCapture = new ManualResetEvent(true);
        List<string> userIdList = new List<string>();
        
            
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
            // set initial form state
            operationsTabControl.Visible = false;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            // Set location and size of bitmap display box
            mainFeedPictureBox.Location = new Point(0, 0);
            mainFeedPictureBox.Height = this.Height;
            mainFeedPictureBox.Width = this.Width;

            // Set location and size of currentView
            currentView.Location = new Point(0, 0);
            currentView.Height = this.Height;
            currentView.Width = this.Width;

            fullScreen = true;

            // start the video feed
            if (camera != null)
            {
                // Start live feed
                isLive = true;
                myThread = new Thread(getLiveVideo);
                myThread.Start();
                //startStopCameraButton.Text = "Stop Live Feed";
                mainFeedPictureBox.Image = null;
            }
        }

        ~mainForm()
        {
            try
            {
                isLive = false;
                NLicense.ReleaseComponents(Components);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to release license. " + ex.ToString());
            }
        }

        private void checkForCamera(NDeviceManager devMan)
        {
            int count = devMan.Devices.Count();

            if (count > 0)
            {
                camera = (NCamera)devMan.Devices[0];
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
            camera.StartCapturing();
            
            //create graphics object
            Brush b = new SolidBrush(currentView.FaceRectangleColor);
            Pen p = new Pen(b,2);
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
                // create grayscale image for template operations
                grayscaleImage = currentImage.ToGrayscale();
                //if (fullScreen)
                //{
                if (useNLView)
                {
                    if (currentView.Visible == false)
                    {
                        currentView.Invoke(new Action(() => currentView.Visible = true));
                        //Action act1 = () => { currentView.Visible = true; };
                        //this.currentView.Invoke(act1);
                    }

                    // remove mainFeedPictureBox to invisible
                    if (mainFeedPictureBox.Visible == true)
                    {
                        mainFeedPictureBox.Invoke(new Action(() => mainFeedPictureBox.Visible = false));
                        //Action act3 = () => { mainFeedPictureBox.Visible = false; };
                        //this.mainFeedPictureBox.Invoke(act3);
                    }

                    if (templateExtractor.DetectFace(grayscaleImage, out thisFace))
                    {
                        matchDelay++;
                        currentView.DetectionDetails = detectDetails(grayscaleImage);
                        //if (boundingBoxOn)
                        //{
                        //    Action act3 = () => currentView.DrawToBitmap(bmp,thisFace.Rectangle);
                        //    currentView.BeginInvoke(act3);
                        //    //bmp = getfeatures.drawFaceRectangle(thisFace, bmp, p);
                        //}
                        Thread.Sleep(500);
                    }

                    // Set image to currentView
                    currentView.Invoke(new Action(() =>currentView.Image=bmp));
                    //Action act2 = () => { currentView.Image = bmp; };
                    //this.currentView.Invoke(act2);

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
                        //Action act3 = () => { mainFeedPictureBox.Visible = true; };
                        //this.mainFeedPictureBox.Invoke(act3);
                    }

                    if (templateExtractor.DetectFace(grayscaleImage, out thisFace))
                    {
                        matchDelay++;
                        if (boundingBoxOn)
                        {
                            bmp = getfeatures.drawFaceRectangle(thisFace, bmp, p);
                        }
                    }

                    // display image on pictureBox
                    mainFeedPictureBox.Invoke(new Action(() => mainFeedPictureBox.Image = bmp));
                }
                if (matchDelay == framesBetweenSample)
                {
                    // Should I shut off face detection while matching?
                    matchingThread = new Thread(attemptMatch);
                    matchingThread.Start();
                    //Can I abort this thread here or can I do it at the end of the matching method?
                    matchingThread.Abort();
                    matchDelay = 0;
                }
                //}
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
            // put new pictureBox on form with detection features.
            // add second pictureBox with file image
            // If identification successful, play video and stop all other processes.
            //templateExtractor.DetectAllFeaturePoints = true;
            //details = templateExtractor.DetectFacialFeatures(grayscaleImage, thisFace);
            // Extractor methods pg 2355
        }

        private NleDetectionDetails[] detectDetails(NGrayscaleImage grayscaleImage)
        {
            if (grayscaleImage ==null)
            {
                return null;
            }
            templateExtractor.MaxRecordsPerTemplate = 1;
            // See how many faces are in the image
            NleFace[] facesInIMage = templateExtractor.DetectFaces(grayscaleImage);
            // Get details on the faces
            NleDetectionDetails[] detectionDetails = new NleDetectionDetails[facesInIMage.Length];
            if (facesInIMage.Length == 0)
            {
                detectionDetails = null;
                return detectionDetails;
            }
            for (int i = 0; i < detectionDetails.Length; i++)
            {
                detectionDetails[i] = templateExtractor.DetectFacialFeatures(grayscaleImage, facesInIMage[i]);
            }
            return detectionDetails;
        }

        public void verifyLicense()
        {
            while (!NLicense.ObtainComponents("/local", 5000, Components))
            {

                DialogResult dr = MessageBox.Show("Please insert the license key, wait for it to authenticate and then click 'Retry'.", "Retry?", MessageBoxButtons.RetryCancel);
                if (dr == DialogResult.Cancel)
                {
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


        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if (fullScreen == true)
                {
                    fullScreen = false;
                    // resize the form
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
                    this.WindowState = FormWindowState.Normal;
                    // resize display boxes
                    mainFeedPictureBox.Location = new Point(100, 100);
                    mainFeedPictureBox.Height = 250;
                    mainFeedPictureBox.Width = 320;

                    currentView.Location = new Point(100, 110);
                    currentView.Height = 250;
                    currentView.Width = 320;

                    operationsTabControl.Visible = true;
                    
                }
                else
                {
                    fullScreen = true;
                    // maximize the form
                    this.WindowState = FormWindowState.Maximized;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                    // enlarge display boxes
                    mainFeedPictureBox.Location = new Point(0, 0);
                    mainFeedPictureBox.Height = this.Height;
                    mainFeedPictureBox.Width = this.Width;

                    currentView.Location = new Point(0, 0);
                    currentView.Height = this.Height;
                    currentView.Width = this.Width;
                    currentView.Zoom = 1;

                    operationsTabControl.Visible = false;
                }
            }
            if (e.KeyData == Keys.B)
            {
                if (boundingBoxOn)
                { boundingBoxOn = false; }
                else
                { boundingBoxOn = true; }
            }

        }

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
                this.useNLView = true;
            }
        }
    }
}
