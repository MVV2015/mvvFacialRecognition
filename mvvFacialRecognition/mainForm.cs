using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Neurotec.Biometrics;
using Neurotec.Devices;
using Neurotec.Images;
using Neurotec.IO;
using Neurotec.Licensing;


namespace mvvFacialRecognition
{

	public partial class mainForm : Form
	{
		const string Components = "Biometrics.FaceExtraction,Devices.Cameras,Biometrics.FaceMatching";
		string defaultVideoFile = Path.Combine (Environment.CurrentDirectory, "videoFiles\\Salaries.mp4");
		// These are used when editing profile information to make sure the user is not duplicating another entry
		string registeredUserFName = null;
		string registeredUserLName = null;
		string registeredUserId = null;

		static int frameRate = 25; // Set video frame rate here.
		static string [] accessLevels = new string [] { "User", "Admin" };
		bool isLive = false;
		bool fullScreen = false;
		bool boundingBoxOn = true;
		bool matchNow = false;
		bool autoDetect = false;
		int tabIndex = 0; // track which tab is selected.
		int registeredUserKey;
		string videoFileLoc;
		List<string> userIdList = new List<string> ();
		System.Drawing.Pen myPen;
		Bitmap globalInsetFaceBmp;
		Thread myThread;

		dbInterface myDdInterface = new dbInterface ();// Close if not in existence

		NCamera camera;
		NImage currentImage;
		NLTemplate facialTemplate;

		public mainForm ()
		{
			InitializeComponent ();
			operationsTabControl.SelectedIndexChanged += new EventHandler (operationsTabControl_SelectedIndexChanged);
			videoPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler (videoPlayer_playStateChanged);
			NDeviceManager devMan = new NDeviceManager (NDeviceType.Camera, true, false, System.Threading.SynchronizationContext.Current);
			checkForCamera (devMan);
		}

		private void mainForm_Load (object sender, EventArgs e)
		{
			// start the video feed
			if (camera != null) {
				isLive = true;
				myThread = new Thread (getLiveVideo);
				myThread.Start ();
				mainFeedPictureBox.Image = null;//????
				getUserNameList ();
				accessLevelDropDown.DataSource = accessLevels;
			}
		}

		private void mainForm_FormClosing (object sender, FormClosingEventArgs e)
		{
			try {
				isLive = false;
				if (myThread != null) {
					myThread.Abort ();
				}
				NLicense.ReleaseComponents (Components);
			} catch (Exception ex) {
				MessageBox.Show (ex.ToString ());
			} finally {
				Application.Exit ();
			}
		}

		private void checkForCamera (NDeviceManager _devMan)
		{
			int count = _devMan.Devices.Count ();

			if (count > 0) {
				camera = (NCamera)_devMan.Devices [0];
			} else {
				MessageBox.Show ("No cameras found");
				Application.Exit ();
			}
		}

		private void getLiveVideo ()
		{
			// what if the camera is disconnected during feed?
			verifyLicense ();
			NLExtractor liveExtractor = new NLExtractor ();
			NleFace [] theseFaces = null;
			List<NleDetectionDetails> faceDetails = new List<NleDetectionDetails> ();
			liveExtractor.DetectAllFeaturePoints = false; // False, will only detect eyes.
			NGrayscaleImage liveGrayImage;
			Bitmap displayBmp;
			Draw drawfeatures = new Draw ();
			myPen = new System.Drawing.Pen (System.Drawing.Color.Blue, 2);
			System.Drawing.Point faceConfLoc = new System.Drawing.Point ();
			System.Drawing.Point rEye = new System.Drawing.Point ();
			System.Drawing.Point lEye = new System.Drawing.Point ();
			var timer = new System.Diagnostics.Stopwatch ();
			int timeSpan;
			int elapsed = 0;
			int frameDelay = 1000 / frameRate;
			int autoDetectDelay = 0;
			int largestFaceNumber = 0;
			camera.StartCapturing ();

			while (isLive == true) {
				// this loop only draws on the live display box. Largest face is detected elsewhere.
				try {
					currentImage = camera.GetFrame ();
					if (currentImage != null) {
						currentImage.FlipHorizontally ();
						// create grayscale image for extractor operations
						liveGrayImage = currentImage.ToGrayscale ();
						displayBmp = currentImage.ToBitmap ();
						theseFaces = liveExtractor.DetectFaces (liveGrayImage);
						int largestFaceWidth = 0;
						int liveFaceCount = theseFaces.Count ();
						if (liveFaceCount > 0) {
							if (faceDetails.Count () != 0) {
								faceDetails.Clear ();
							}

							for (int i = 0; i < theseFaces.Length; i++) {
								faceDetails.Add (liveExtractor.DetectFacialFeatures (liveGrayImage, theseFaces [i]));
								faceConfLoc.X = faceDetails [i].Face.Rectangle.Left;
								faceConfLoc.Y = faceDetails [i].Face.Rectangle.Bottom;
								rEye.X = faceDetails [i].RightEyeCenter.X;
								rEye.Y = faceDetails [i].RightEyeCenter.Y;
								lEye.X = faceDetails [i].LeftEyeCenter.X;
								lEye.Y = faceDetails [i].LeftEyeCenter.Y;

								if (boundingBoxOn) {
									displayBmp = drawfeatures.drawFaceRectangle (faceDetails [i], displayBmp, myPen);
								}
								if (faceConfCheckBox.Checked) {
									displayBmp = drawfeatures.faceConfidence (displayBmp, (int)faceDetails [i].Face.Confidence, faceConfLoc, myPen);
								}
								if (drawEyesCheckBox.Checked) {
									displayBmp = drawfeatures.connect (displayBmp, rEye, lEye, myPen);
								}
								if (showEyeCheckBox.Checked) {
									displayBmp = drawfeatures.confidence (displayBmp, faceDetails [i].LeftEyeCenter.Confidence, lEye, myPen);
									displayBmp = drawfeatures.confidence (displayBmp, faceDetails [i].RightEyeCenter.Confidence, rEye, myPen);
								}

								if (faceDetails [i].Face.Rectangle.Width > largestFaceWidth) {
									largestFaceNumber = i;
								}
								globalInsetFaceBmp = drawfeatures.snipFace (currentImage.ToBitmap (), faceDetails [largestFaceNumber]);//make face clipping here

							}
							if (autoDetect) {
								autoDetectDelay++;
							}

						}
						liveGrayImage.Dispose ();
						currentImage.Dispose ();

						if (matchNow || autoDetectDelay == 50) {
							autoDetectDelay = 0;
							attemptMatch ();
						}
						// display image on pictureBox
						if (mainFeedPictureBox.InvokeRequired) {
							mainFeedPictureBox.Invoke (new Action (() => mainFeedPictureBox.Image = displayBmp));
						} else {
							mainFeedPictureBox.Image = displayBmp;
						}
						timer.Stop ();
						elapsed = (Int32)timer.ElapsedMilliseconds;
						timeSpan = frameDelay - elapsed;
						if (timeSpan < 0) {
							timeSpan = 0;
						}
						Thread.Sleep (timeSpan);
						timer.Reset ();
						theseFaces = null;
					}
				} catch (Exception ex) {
					// do nothing
				}
			}
			camera.StopCapturing ();
		}

		private void attemptMatch ()
		{
			try {
				int sleepTime = 110;
				Bitmap myInset = globalInsetFaceBmp;
				Draw makeImage = new Draw ();
				Action updateFace = new Action (() => insetPictureBox.Image = myInset);
				NleDetectionDetails theseDetails;
				theseDetails = detectDetails ();
				if (theseDetails == null) {
					return;
				}

				// set feature points
				System.Drawing.Point rightEye = new System.Drawing.Point (theseDetails.RightEyeCenter.X, theseDetails.RightEyeCenter.Y);
				System.Drawing.Point leftEye = new System.Drawing.Point (theseDetails.LeftEyeCenter.X, theseDetails.LeftEyeCenter.Y);
				System.Drawing.Point eyeMid = new System.Drawing.Point ((rightEye.X + leftEye.X) / 2, (rightEye.Y + leftEye.Y) / 2);
				System.Drawing.Point nose = new System.Drawing.Point (theseDetails.NoseTip.X, theseDetails.NoseTip.Y);
				System.Drawing.Point mouth = new System.Drawing.Point (theseDetails.MouthCenter.X, theseDetails.MouthCenter.Y);
				System.Drawing.Point TLCorner = new System.Drawing.Point (theseDetails.Face.Rectangle.Left, theseDetails.Face.Rectangle.Top);
				System.Drawing.Point TRCorner = new System.Drawing.Point (theseDetails.Face.Rectangle.Right, theseDetails.Face.Rectangle.Top);
				System.Drawing.Point BLCorner = new System.Drawing.Point (theseDetails.Face.Rectangle.Left, theseDetails.Face.Rectangle.Bottom);
				System.Drawing.Point BRCorner = new System.Drawing.Point (theseDetails.Face.Rectangle.Right, theseDetails.Face.Rectangle.Bottom);

				insetPictureBox.Invoke (updateFace);
				if (!insetPictureBox.Visible) {
					insetPicVisToggle ();
				}
				// Add rectangle
				myInset = makeImage.drawInsetRectangle (theseDetails, myInset, myPen);
				insetPictureBox.Invoke (updateFace);
				Thread.Sleep (sleepTime);
				// Connect eyes
				myInset = makeImage.connect (myInset, rightEye, leftEye, myPen);
				insetPictureBox.Invoke (updateFace);
				Thread.Sleep (sleepTime);
				// midEyes to mouth
				myInset = makeImage.connect (myInset, eyeMid, mouth, myPen);
				insetPictureBox.Invoke (updateFace);
				Thread.Sleep (sleepTime);
				// right eye to mouth
				myInset = makeImage.connect (myInset, rightEye, mouth, myPen);
				insetPictureBox.Invoke (updateFace);
				Thread.Sleep (sleepTime);
				// left eye to mouth
				myInset = makeImage.connect (myInset, leftEye, mouth, myPen);
				insetPictureBox.Invoke (updateFace);
				Thread.Sleep (sleepTime);
				// top right corner to left eye
				myInset = makeImage.connect (myInset, leftEye, TRCorner, myPen);
				insetPictureBox.Invoke (updateFace);
				Thread.Sleep (sleepTime);
				// top left corner to right eye
				myInset = makeImage.connect (myInset, rightEye, TLCorner, myPen);
				insetPictureBox.Invoke (updateFace);
				Thread.Sleep (sleepTime);
				// bottom left corner to nose
				myInset = makeImage.connect (myInset, nose, BLCorner, myPen);
				insetPictureBox.Invoke (updateFace);
				Thread.Sleep (sleepTime);
				// bottom right corner to nose
				myInset = makeImage.connect (myInset, nose, BRCorner, myPen);
				insetPictureBox.Invoke (updateFace);
				Thread.Sleep (sleepTime);

				// check for match
				int primeKey = identify ();
				if (primeKey == 0) {
					if (insetPictureBox.Visible) {
						insetPicVisToggle ();
					}
				}
				matchNow = false;
			} catch (Exception ex) {
				//MessageBox.Show(ex.ToString()); 
			}
		}

		private int identify ()
		{
			verifyLicense ();
			bool largeTemplate = true;
			cryptography decrypt = new cryptography ();
			NMatcher templateMatcher = null;
			int maxKeyNum = 0;
			int primKey = 0;

			try {
				NBuffer probeTemplateBuffer = null;
				byte [] probeTemplateArray = null;
				try {
					NleDetectionDetails details; // unused
					if (!createTemplate (globalInsetFaceBmp, largeTemplate, out details)) {
						return primKey;
					}
					probeTemplateBuffer = facialTemplate.Save ();
					probeTemplateArray = probeTemplateBuffer.ToByteArray ();
				} catch (IOException ex) {
					MessageBox.Show ("error reading input file {0}: " + ex);
					return primKey;
				}

				// extract gallery templates
				// This code allows for searching a database where rows (users) have been deleted, 
				// causing primaryKeys to not be sequential.
				int row = 1;
				maxKeyNum = myDdInterface.maxPrimaryKey ();
				if (maxKeyNum == 0) {
					MessageBox.Show ("There are no registered users.\nPlease enroll and try again.");
					return maxKeyNum;
				}
				List<byte []> dbaseTemplates = new List<byte []> ();
				List<int [,]> rowKeyList = new List<int [,]> ();// list of row/primary keys

				for (int i = 0; i < maxKeyNum; i++) {
					try {
						byte [] tempArray = myDdInterface.getTemplateFromKey (i + 1);
						if (tempArray != null)// skip deleted records
						{
							int [,] rowKeyRef = new int [1, 2];
							dbaseTemplates.Add (tempArray);
							// store row/primary key reference
							rowKeyRef [0, 0] = row;
							rowKeyRef [0, 1] = i + 1;
							rowKeyList.Add (rowKeyRef);
							row++;
						}
					} catch (IOException ex) {
						MessageBox.Show ("error reading reference template " + i + ": " + ex);
						return primKey;
					}
				}

				templateMatcher = new NMatcher ();
				templateMatcher.IdentifyStart (probeTemplateArray);

				try {
					int numOfTemplates = dbaseTemplates.Count ();
					int score = 0;
					int highScore = 0;
					for (int i = 0; i < numOfTemplates; i++) {
						score = templateMatcher.IdentifyNext (dbaseTemplates [i]);
						if (score > highScore) {
							highScore = score;
							// get primary key based on row number
							int [,] tempRef = new int [1, 2];
							tempRef = rowKeyList [i];
							primKey = tempRef [0, 1];
						}
					}
					if (primKey == 0) {
						MessageBox.Show ("Sorry, Unable to Identify.\nTry again.");
						return primKey;
					} else {
						string fName = myDdInterface.getFName (primKey);
						string lName = myDdInterface.getLName (primKey);
						Bitmap storedBmp = myDdInterface.getImageFromId (myDdInterface.userIdFromKey (primKey));// Check for potential errors referencing Primary key.
						if (enrolledImagePictureBox.InvokeRequired) {
							enrolledImagePictureBox.Invoke (new Action (() => enrolledImagePictureBox.Image = storedBmp));
							enrolledImageNameLabel.Invoke (new Action (() => enrolledImageNameLabel.Text = (fName + " " + lName)));
						} else {
							enrolledImagePictureBox.Image = storedBmp;
							enrolledImageNameLabel.Text = (fName + " " + lName);
						}
						if (!enrolledImagePictureBox.Visible) {
							enrolledPicVisToggle ();
						}
						if (tabIndex == 0)// play video only on main tab
						{
							playVideoFile (primKey);
						}
						return primKey;
					}
				} catch (Exception ex) {
					MessageBox.Show ("" + ex);
				} finally {
					templateMatcher.IdentifyEnd ();
					facialTemplate = null;
				}
				return primKey;
			} catch (Exception ex) {
				MessageBox.Show ("" + ex);
				return primKey;
			} finally {
				NLicense.ReleaseComponents (Components);
				if (templateMatcher != null) {
					templateMatcher.Dispose ();
				}
			}
		}

		private void playVideoFile (int primaryKey)
		{
			enrolledPicVisToggle ();
			string greetingVideo = Path.Combine (Environment.CurrentDirectory, "videoFiles\\" + myDdInterface.getVideoLoc (primaryKey));
			videoPlayer.URL = greetingVideo;
			if (fullScreen) {
				videoPlayer.Invoke (new Action (() => videoPlayer.Visible = true));
				videoPlayer.Invoke (new Action (() => videoPlayer.Height = this.Height));
				videoPlayer.Invoke (new Action (() => videoPlayer.Width = this.Width));
			} else {
				videoPlayer.Invoke (new Action (() => videoPlayer.Visible = true));
				videoPlayer.Invoke (new Action (() => videoPlayer.Height = (this.Height - 40)));
				videoPlayer.Invoke (new Action (() => videoPlayer.Width = (this.Width - 20)));
			}
			videoPlayer.Ctlcontrols.play ();
			isLive = false;
		}

		private void videoPlayer_playStateChanged (object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
		{
			if (e.newState == 8)// end of file
			{
				videoPlayer.close ();
				videoPlayer.Visible = false;
				enrolledPicVisToggle ();
				isLive = true;
				myThread = new Thread (getLiveVideo);
				myThread.Start ();
			}
		}

		#region enroll tab methods

		private void enrollButton_Click (object sender, EventArgs e)
		{
			camera.StopCapturing ();
			myThread.Abort ();
			enroll (globalInsetFaceBmp);
		}

		private void enroll (Bitmap globalBitmap)
		{
			NleDetectionDetails detDetails = null;
			string destination = null;
			string permissionLevel = "User";
			bool lgTemplate = true;
			Draw clipping = new Draw ();
			if (firstNameTextBox.Text == "" || lastNameTextBox.Text == "") {
				MessageBox.Show ("Please fill in the first and last name fields.");
				return;
			}
			// Strip extra spaces from name entries.
			string firstName = RemoveWhitespace (firstNameTextBox.Text);
			string lastName = RemoveWhitespace (lastNameTextBox.Text);

			if (!myDdInterface.userExists (firstName, lastName)) {
				string enrolleeId = createUserId (firstName, lastName);

				if (!createTemplate (globalBitmap, lgTemplate, out detDetails)) {
					myThread = new Thread (getLiveVideo);
					myThread.Start ();
					videoFileLoc = null;
					videoFileTxt.Text = null;
					return;
				}

				if (videoFileLoc == null) {
					MessageBox.Show ("The default video will play when you are recognized.\n You can change it later.");
					videoFileLoc = defaultVideoFile;
				} else {
					destination = Path.Combine (Environment.CurrentDirectory, "videoFiles\\" + videoFileTxt.Text);
					if (!File.Exists (destination))// Check to see if the file is already there
					{
						File.Copy (videoFileLoc, destination);
					}
				}
				string videoFileName = Path.GetFileName (videoFileLoc);
				if (!myDdInterface.adminExists ()) {
					permissionLevel = "Admin";
				}

				// insert user info into database
				myDdInterface.insertEntry (firstName, lastName, permissionLevel, enrolleeId, globalBitmap, facialTemplate, videoFileName);
				facialTemplate = null;
				// set the enrolled image
				if (enrolledImagePictureBox.InvokeRequired) {
					enrolledImagePictureBox.Invoke (new Action (() => enrolledImagePictureBox.Image = globalBitmap));
					enrolledImageNameLabel.Invoke (new Action (() => enrolledImageNameLabel.Text = (firstNameTextBox.Text + " " + lastNameTextBox.Text)));
				} else {
					enrolledImagePictureBox.Image = globalBitmap;
					enrolledImageNameLabel.Text = (firstName + " " + lastName);
				}
				// make it visible
				if (!enrolledImagePictureBox.Visible) {
					enrolledPicVisToggle ();
				}
				videoFileLoc = null;
				videoFileTxt.Text = null;
				profileFNameTxt.Text = null;
				profileLNameTxt.Text = null;
				myThread = new Thread (getLiveVideo);
				myThread.Start ();
				MessageBox.Show ("Enrollment Successful. Your userId is " + enrolleeId);
			} else {
				MessageBox.Show ("That user is already enrolled");
				myThread = new Thread (getLiveVideo);
				myThread.Start ();
				return;
			}
		}

		private string createUserId (string fName, string lName)
		{
			string enrolleeId = fName [0] + lName;
			// Check to see if userId already exists
			if (myDdInterface.userIdExists (enrolleeId)) {
				// if it does, append a number.
				Random rnd = new Random ();
				int num = rnd.Next (1, 100);
				string enrolleeId2 = enrolleeId + num.ToString ();
				// Double check for unused userId
				while (myDdInterface.userIdExists (enrolleeId2)) {
					enrolleeId2 = enrolleeId + rnd.Next (1, 100);
				}
				enrolleeId = enrolleeId2;
			}
			return enrolleeId;
		}

		static string RemoveWhitespace (string input)
		{
			StringBuilder output = new StringBuilder (input.Length);

			for (int index = 0; index < input.Length; index++) {
				if (!char.IsWhiteSpace (input, index)) {
					output.Append (input [index]);
				}
			}

			return output.ToString ();
		}

		#endregion

		#region options tab methods

		private void userIdDropDown_SelectionChangeCommitted (object sender, EventArgs e)
		{
			if (userIdDropDown.SelectedValue.ToString () == "Select User") {
				return;
			}
			int accessorPrimeKey = identify ();
			if (accessorPrimeKey == 0) { return; }
			string registeredUserToEdit = userIdDropDown.SelectedValue.ToString ();
			registeredUserLName = registeredUserToEdit.Substring (registeredUserToEdit.IndexOf (' ') + 1);
			registeredUserFName = registeredUserToEdit.Remove (registeredUserToEdit.Length - registeredUserLName.Length - 1);
			registeredUserKey = myDdInterface.getKeyFromName (registeredUserFName, registeredUserLName);
			bool registeredUserIsAdmin = myDdInterface.isAdmin (registeredUserKey);
			string accessorsName = (myDdInterface.getFName (accessorPrimeKey) + " " + myDdInterface.getLName (accessorPrimeKey));
			bool accessingUserAdmin = myDdInterface.isAdmin (accessorPrimeKey);

			if (registeredUserToEdit.ToString () == accessorsName.ToString ()) {
				profileUserId.Text = myDdInterface.userIdFromKey (accessorPrimeKey);
				profileFNameTxt.Text = myDdInterface.getFName (accessorPrimeKey);
				profileLNameTxt.Text = myDdInterface.getLName (accessorPrimeKey);
				if (accessingUserAdmin) {
					accessLevelDropDown.Enabled = true;
					accessLevelDropDown.SelectedIndex = 1;
				} else {
					accessLevelDropDown.Enabled = false;
					accessLevelDropDown.SelectedIndex = 0;
				}
				profileUserVideo.Text = Path.GetFileName (myDdInterface.getVideoLoc (accessorPrimeKey));
				videoFileLoc = profileUserVideo.Text;
			} else if (accessingUserAdmin) {// use this to set access level on tab
				profileUserId.Text = myDdInterface.userIdFromKey (registeredUserKey);
				profileFNameTxt.Text = myDdInterface.getFName (registeredUserKey);
				profileLNameTxt.Text = myDdInterface.getLName (registeredUserKey);
				if (registeredUserIsAdmin) {
					accessLevelDropDown.SelectedIndex = 1;
				} else {
					accessLevelDropDown.SelectedIndex = 0;
				}
				accessLevelDropDown.Enabled = true;
				profileUserVideo.Text = Path.GetFileName (myDdInterface.getVideoLoc (registeredUserKey));
				videoFileLoc = profileUserVideo.Text;
			} else {
				MessageBox.Show ("You are only allowed to access your own profile");
			}
			registeredUserId = profileUserId.Text;
		}

		private void profileUpdateUserBtn_Click (object sender, EventArgs e)
		{
			string fName = RemoveWhitespace (profileFNameTxt.Text);
			string lName = RemoveWhitespace (profileLNameTxt.Text);
			string accessLevel = accessLevelDropDown.SelectedValue.ToString ();
			string enrolleeId = profileUserId.Text;
			string fileDestination = Path.Combine (Environment.CurrentDirectory, "videoFiles\\" + profileUserVideo.Text);
			string fileName = Path.GetFileName (fileDestination);

			if (myDdInterface.userExists (fName, lName) && ((registeredUserLName != lName) || (registeredUserFName != fName))) {
				MessageBox.Show ("That User Name is already registered.\nPlease choose a different one.");
				return;
			}
			if (myDdInterface.userIdExists (enrolleeId) && (registeredUserId != enrolleeId)) {
				MessageBox.Show ("That userID is already registered.\nPlease choose a different one.");
				return;
			}
			DialogResult result = MessageBox.Show ("The user information will be recorded as:\nUser Id: " + enrolleeId + "\nFirst Name: " + fName + "\nLastName: " + lName + "\nVideo: " + fileName + "\nWith an access level of: " + accessLevel, "Confirm Changes", MessageBoxButtons.OKCancel);
			if (result == DialogResult.OK) {
				if (!File.Exists (fileDestination))// Check to see if the file is already there
				{
					File.Copy (videoFileLoc, fileDestination);
				}
				myDdInterface.updateEntry (registeredUserKey, fName, lName, accessLevel, enrolleeId, fileName);
				// Clear all fields
				registeredUserKey = 0;
				getUserNameList ();
				userIdDropDown.SelectedIndex = 0;
				profileUserId.Text = null;
				profileFNameTxt.Text = null;
				profileLNameTxt.Text = null;
				profileUserVideo.Text = null;
				accessLevelDropDown.SelectedIndex = 0;
				videoFileLoc = null;
				accessLevelDropDown.Enabled = false;
			}
		}

		#endregion

		#region Neurotec operations

		private bool createTemplate (Bitmap enrollmentBmp, bool largeTemplate, out NleDetectionDetails detDetails)
		{
			NLExtractor templateExtractor = new NLExtractor ();
			if (largeTemplate) {
				templateExtractor.TemplateSize = NleTemplateSize.Large;
			} else {
				templateExtractor.TemplateSize = NleTemplateSize.Medium;
			}
			NImage enrollmentImage = NImage.FromBitmap (enrollmentBmp);
			NGrayscaleImage enrollmentGrayscale = enrollmentImage.ToGrayscale ();
			NleDetectionDetails _detDetails = null;

			try {
				verifyLicense ();
				NleExtractionStatus extractionStatus;
				facialTemplate = templateExtractor.Extract (enrollmentGrayscale, out _detDetails, out extractionStatus);

				if (extractionStatus != NleExtractionStatus.TemplateCreated) {
					MessageBox.Show ("Face Template Extraction Failed!\nPlease try again.\n" + extractionStatus.ToString ());
					detDetails = _detDetails;
					return false;
				} else {
					detDetails = _detDetails;
					return true;
				}
			} catch (Exception ex) {
				MessageBox.Show ("" + ex);
				detDetails = null;
				return false;
			} finally {
				NLicense.ReleaseComponents (Components);
				if (templateExtractor != null) {
					templateExtractor.Dispose ();
				}
			}
		}

		private NleDetectionDetails detectDetails ()
		{
			NGrayscaleImage grayImage = NImage.FromBitmap (globalInsetFaceBmp).ToGrayscale ();
			NleFace _faceInImage;
			NLExtractor myExtractor = new NLExtractor ();
			NleDetectionDetails _detectionDetails = new NleDetectionDetails ();
			if (myExtractor.DetectAllFeaturePoints == false) {
				myExtractor.DetectAllFeaturePoints = true;
			}

			if (grayImage == null || !myExtractor.DetectFace (grayImage, out _faceInImage)) {
				_detectionDetails = null;
				return _detectionDetails;
			}
			_detectionDetails = myExtractor.DetectFacialFeatures (grayImage, _faceInImage);
			return _detectionDetails;
		}

		public void verifyLicense ()
		{
			if (!NLicense.ObtainComponents ("/local", 5000, Components)) {
				if (this.InvokeRequired) {
					this.Invoke (new Action (() => MessageBox.Show (this, "Please insert the license key and try again.")));
					this.Invoke (new Action (() => { this.Close (); }));
				} else {
					MessageBox.Show (this, "Please insert the license key and try again.");
					this.Close ();
				}
				if (myThread != null) {
					myThread.Abort ();
				}
			}
		}

		#endregion

		#region event handlers

		private void operationsTabControl_SelectedIndexChanged (object sender, EventArgs e)
		{
			registeredUserFName = null;
			registeredUserLName = null;
			registeredUserId = null;
			if (enrolledImageBox.Visible) {
				enrolledPicVisToggle ();
			}
			if (insetPictureBox.Visible) {
				insetPicVisToggle ();
			}

			if (operationsTabControl.SelectedIndex == 0)// Main
			{
				// Set default state of controls
				tabIndex = 0;
			} else if (operationsTabControl.SelectedIndex == 1)// Enroll
			  {
				tabIndex = 1;
				firstNameTextBox.Text = String.Empty;
				lastNameTextBox.Text = String.Empty;
				if (videoFileLoc != null) {
					videoFileLoc = null;
				}
				videoFileTxt.Text = null;
				profileUserId.Text = string.Empty;
			} else if (operationsTabControl.SelectedIndex == 2)// Profile
			  {
				tabIndex = 2;
				accessLevelDropDown.Enabled = false;
				profileUserVideo.Text = null;
				if (videoFileLoc != null) {
					videoFileLoc = null;
				}
				profileUserId.Text = null;
				profileFNameTxt.Text = null;
				profileLNameTxt.Text = null;

				// repopulate userId dropdown
				getUserNameList ();

			} else if (operationsTabControl.SelectedIndex == 3)// About
			  {
				tabIndex = 3;
			}
		}

		private void getUserNameList ()
		{
			userIdList.Clear ();
			userIdList = myDdInterface.populateUserIdList ();
			userIdDropDown.DataSource = userIdList;
		}

		private void drawRectangleCheckbox_CheckedChanged (object sender, EventArgs e)
		{
			if (boundingBoxOn) {
				boundingBoxOn = false;
			} else {
				boundingBoxOn = true;
			}
		}

		private void identifyButton_Click (object sender, EventArgs e)
		{
			if (matchNow == false)
				matchNow = true;
			if (enrolledImageBox.Visible) {
				enrolledPicVisToggle ();
			}
			if (insetPictureBox.Visible) {
				insetPicVisToggle ();
			}
		}

		private void mainFeedPictureBox_Click (object sender, EventArgs e)
		{
			if (!fullScreen) {
				fullScreen = true;
				// resize display boxes
				this.WindowState = FormWindowState.Maximized;
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

				// Image Size = {Width = 640 Height = 480}
				mainFeedPictureBox.Location = new System.Drawing.Point (0, 0);
				mainFeedPictureBox.Width = 1280;
				mainFeedPictureBox.Height = 960;
			} else {
				fullScreen = false;
				this.WindowState = FormWindowState.Normal;
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

				mainFeedPictureBox.Location = new System.Drawing.Point (100, 80);
				mainFeedPictureBox.Width = 700;
				mainFeedPictureBox.Height = 525;
			}
		}

		private void profileDeleteButton_Click (object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show ("You are about to permanently delete the user " + userIdDropDown.Text + "\nAre You Sure?", "", MessageBoxButtons.YesNo);
			if (result == DialogResult.No) {
				return;
			}
			string userToDelete = userIdDropDown.SelectedValue.ToString ();
			string userLName = userToDelete.Substring (userToDelete.IndexOf (' ') + 1);
			string userFName = userToDelete.Remove (userToDelete.Length - userLName.Length - 1);
			int userPrimKey = myDdInterface.getKeyFromName (userFName, userLName);
			if (myDdInterface.deleteUser (userPrimKey)) {
				//MessageBox.Show("User sucessfully removed.");
				userIdDropDown.SelectedIndex = 0;
			} else {
				MessageBox.Show ("could not delete user.");
			}
			getUserNameList ();
			profileUserId.Text = null;
			profileFNameTxt.Text = null;
			profileLNameTxt.Text = null;
			profileUserVideo.Text = null;
		}

		private void selectFileButton_Click (object sender, EventArgs e)
		{
			if (selectFileDialog.ShowDialog () == DialogResult.OK) {
				videoFileTxt.Text = selectFileDialog.SafeFileName;
				videoFileLoc = selectFileDialog.FileName;
			}
		}

		private void changeVideoButton_Click (object sender, EventArgs e)
		{
			if (updateFileDialog.ShowDialog () == DialogResult.OK) {
				videoFileLoc = updateFileDialog.FileName;
				profileUserVideo.Text = updateFileDialog.SafeFileName;
			}
		}

		#endregion

		private void insetPicVisToggle ()
		{
			if (insetPictureBox.InvokeRequired)
				if (insetPictureBox.Visible) {
					insetPictureBox.Invoke (new Action (() => insetPictureBox.Visible = false));
				} else {
					insetPictureBox.Invoke (new Action (() => insetPictureBox.Visible = true));
				} else
				if (insetPictureBox.Visible) {
				insetPictureBox.Visible = false;
			} else {
				insetPictureBox.Visible = true;
			}
		}

		private void enrolledPicVisToggle ()
		{
			if (enrolledImageBox.InvokeRequired) {
				if (enrolledImagePictureBox.Visible) {
					enrolledImageBox.Invoke (new Action (() => enrolledImageBox.Visible = false));
				} else {
					enrolledImageBox.Invoke (new Action (() => enrolledImageBox.Visible = true));
				}
			} else if (enrolledImagePictureBox.Visible) {
				enrolledImageBox.Visible = false;
			} else {
				enrolledImageBox.Visible = true;
			}
		}

		private void autoIdentifyButton_Click (object sender, EventArgs e)
		{
			if (autoDetect) {
				autoDetect = false;
				autoIdentifyButton.Text = "OFF";
			} else {
				autoDetect = true;
				autoIdentifyButton.Text = "ON";
			}
		}
	}
}
