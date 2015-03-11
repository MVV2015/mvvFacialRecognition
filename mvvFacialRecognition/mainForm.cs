﻿using System;
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
        bool fullScreen = true;
        bool boundingBoxOn = true;
        static int frameRate = 25; // Set video frame rate here.
        static int framesBetweenSample = 2000 / frameRate; // two seconds between match attempts
        List<string> userIdList = new List<string>();
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
            templateExtractor.DetectAllFeaturePoints = true;
            // False, will only detect eyes.
            templateExtractor.FavorLargestFace = true;
            // Extract details only on the largest face

            NleDetectionDetails[] detectionDetails;
            camera.StartCapturing();
            
            //create graphics object
            Brush b = new SolidBrush(currentView.FaceRectangleColor);
            Pen p = new Pen(b,1);
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
                Draw drawfeatures = new Draw(bmp, p);
                // create grayscale image for template operations
                grayscaleImage = currentImage.ToGrayscale();

                //if (fullScreen)
                //{
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

                        //if (templateExtractor.DetectFace(grayscaleImage, out thisFace))
                        if (detectDetails(grayscaleImage, out detectionDetails))
                        {
                            currentView.DetectionDetails = detectionDetails;
                            matchDelay++;
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
                            matchDelay++;
                            //currentView.DetectionDetails[0] = templateExtractor.DetectFacialFeatures(grayscaleImage, thisFace);
                            Point locConfText = new Point(mainFeedPictureBox.Bottom - thisFace.Rectangle.Bottom, mainFeedPictureBox.Left + thisFace.Rectangle.Left);
                            if (boundingBoxOn)
                            {
                                // We can only draw the rectangle and confidence score with a NleFace object. 
                                // If we want more we have to use NlView.
                                bmp = drawfeatures.drawFaceRectangle(thisFace, bmp, (int)thisFace.Confidence);
                            }
                        }

                        // display image on pictureBox
                        mainFeedPictureBox.Invoke(new Action(() => mainFeedPictureBox.Image = bmp));

                        //// pause to see bounding box
                        //Thread.Sleep(250);
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

        private bool detectDetails(NGrayscaleImage grayscaleImage, out NleDetectionDetails[] detectionDetails)
        {
            if (grayscaleImage ==null)
            {
                detectionDetails = null;
                return false;
            }
            templateExtractor.MaxRecordsPerTemplate = 1;
            // See how many faces are in the image
            NleFace[] facesInIMage = templateExtractor.DetectFaces(grayscaleImage);
            // Get details on the faces
            NleDetectionDetails[] _detectionDetails = new NleDetectionDetails[facesInIMage.Length];
            if (facesInIMage.Length == 0)
            {
                detectionDetails = null;
                return false;
            }
            for (int i = 0; i < _detectionDetails.Length; i++)
            {
                _detectionDetails[i] = templateExtractor.DetectFacialFeatures(grayscaleImage, facesInIMage[i]);
            }
            detectionDetails = _detectionDetails;
            return true;
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

// Why doesn't this work after selecting radio button?
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
                    // Image Size = {Width = 640 Height = 480}
                    mainFeedPictureBox.Location = new Point(100, 110);
                    mainFeedPictureBox.Width = 480;
                    mainFeedPictureBox.Height = 360;

                    currentView.Location = new Point(100, 110);
                    currentView.Width = 480;
                    currentView.Height = 360;
                    currentView.Zoom = (float).75;

                    operationsTabControl.Visible = true;
                    
                }
                else
                {
                    fullScreen = true;
                    // maximize the form
                    this.WindowState = FormWindowState.Maximized;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                    // enlarge display boxes
                    //Screen dimentions = {Width = 1456 Height = 916}
                    mainFeedPictureBox.Location = new Point(0, 0);
                    mainFeedPictureBox.Height = this.Height;
                    mainFeedPictureBox.Width = this.Width;

                    currentView.Location = new Point(0, 0);
                    currentView.Height = 900;
                    currentView.Width = 1200;
                    //currentView.Height = this.Height;
                    //currentView.Width = this.Width;
                    currentView.Zoom = 2;

                    operationsTabControl.Visible = false;
                }
            }
            else if (e.KeyData == Keys.B)
            {
                if (boundingBoxOn)
                {
                    boundingBoxOn = false;
                }
                else
                { 
                    boundingBoxOn = true;
                }
            }
            else if (e.KeyData == Keys.N)
            {
                if (useNLView)
                { useNLView = false; }
                else
                { 
                    useNLView = true;
                }
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

        private void drawEyesButton_Click(object sender, EventArgs e)
        {
            if (currentView.ShowEyes == true)
            {
                drawEyesButton.Checked = false;
                currentView.ShowEyes = false;
            }
            else
            {
                drawEyesButton.Checked = true;
                currentView.ShowEyes = true;
            }
        }

        private void faceConfButton_Click(object sender, EventArgs e)
        {
            if (currentView.ShowFaceConfidence == true)
            {
                currentView.ShowFaceConfidence = false;
                faceConfButton.Checked = false;
            }
            else
            {
                currentView.ShowFaceConfidence = true;
                faceConfButton.Checked = true;
            }
        }

        private void eyeConfidenceButton_Click(object sender, EventArgs e)
        {
            if (currentView.ShowEyesConfidence == false)
            {
                currentView.ShowEyesConfidence = true;
                eyeConfidenceButton.Checked = true;
            }
            else
            {
                currentView.ShowEyesConfidence = false;
                eyeConfidenceButton.Checked = false;
            }
        }

        private void markNoseButton_Click(object sender, EventArgs e)
        {
            if (currentView.ShowNose == true)
            {
                markNoseButton.Checked = false;
                currentView.ShowNose = false;
            }
            else
            {
                markNoseButton.Checked = true;
                currentView.ShowNose = true;
            }
        }

        private void markMouthButton_Click(object sender, EventArgs e)
        {
            if(currentView.ShowMouth == true)
            {
                currentView.ShowMouth = false;
                markMouthButton.Checked = false;
            }
            else
            {
                currentView.ShowMouth = true;
                markMouthButton.Checked = true;
            }
        }

        private void noseConfButton_Click(object sender, EventArgs e)
        {
            if (currentView.ShowNoseConfidence ==  true)
            {
                currentView.ShowNoseConfidence = false;
                noseConfButton.Checked = false;
            }
            else
            {
                currentView.ShowNoseConfidence = true;
                noseConfButton.Checked = true;
            }
        }

        private void mouthConfButton_Click(object sender, EventArgs e)
        {
            if (currentView.ShowMouthConfidence ==  true)
            {
                currentView.ShowMouthConfidence = false;
                mouthConfButton.Checked = false;
            }
            else
            {
                currentView.ShowMouthConfidence = true;
                mouthConfButton.Checked = true;
            }
        }

        private void detectAllFeaturesButton_Click(object sender, EventArgs e)
        {
            if (templateExtractor.DetectAllFeaturePoints == false)
            {
                detectAllFeaturesButton.Checked = true;
                templateExtractor.DetectAllFeaturePoints = true;
            }
            else
            {
                detectAllFeaturesButton.Checked = false;
                templateExtractor.DetectAllFeaturePoints = false;
                currentView.DetectionDetails = null;
            }
        }
    }
}
