using System.Drawing;

using Neurotec.Biometrics;

namespace mvvFacialRecognition
{

	class Draw
    {
        Graphics g;
        Font myFont = new Font("Microsoft Sans Serif",(float)12, FontStyle.Bold);
        public Draw()
        {
        }

        internal Bitmap drawFaceRectangle(NleDetectionDetails thisFace, Bitmap myImage, Pen p)
        {
            g = Graphics.FromImage(myImage);
            g.DrawRectangle(p, thisFace.Face.Rectangle.X, thisFace.Face.Rectangle.Y, thisFace.Face.Rectangle.Width, thisFace.Face.Rectangle.Height);
            g.Dispose();
            return myImage;
        }

        internal Bitmap drawInsetRectangle(NleDetectionDetails thisFace, Bitmap myImage, Pen p)
        {
            // This is a duplicate of drawFaceRectangle() above. It is here because of occational crossthreading conflicts. (needs to be an async method)
            g = Graphics.FromImage(myImage);
            g.DrawRectangle(p, thisFace.Face.Rectangle.X, thisFace.Face.Rectangle.Y, thisFace.Face.Rectangle.Width, thisFace.Face.Rectangle.Height);
            g.Dispose();
            return myImage;
        }

        internal Bitmap snipFace(Bitmap myImage, NleDetectionDetails nleDetectionDetails)
        {
            int _width = (int)(nleDetectionDetails.Face.Rectangle.Width * 2);
            int _height = (int)(nleDetectionDetails.Face.Rectangle.Height * 2);
            Bitmap snippedImage = new Bitmap(_width, _height);
            g = Graphics.FromImage(snippedImage);
            Rectangle destination = new Rectangle(0, 0, _width, _height);
            Rectangle source = new Rectangle((nleDetectionDetails.Face.Rectangle.X - _width / 4),
                (nleDetectionDetails.Face.Rectangle.Y - _height / 4), _width, _height);
            g.DrawImage(myImage, destination, source, GraphicsUnit.Pixel);
            g.Dispose();
            return snippedImage;
        }

        internal Bitmap connect(Bitmap fullImage, Point point1, Point point2, Pen P)
        {            
            g = Graphics.FromImage(fullImage);
            g.DrawLine(P, point1, point2);
            g.Dispose();
            return fullImage;
        }

        internal Bitmap confidence(Bitmap liveBmp, int score, Point myPoint, Pen myPen)
        {
            g = Graphics.FromImage(liveBmp);
            g.DrawString(score.ToString(), myFont, myPen.Brush, myPoint);
            g.Dispose();
            return liveBmp;
        }

        internal Bitmap faceConfidence(Bitmap liveBmp, int score, Point myPoint, Pen myPen)
        {
            g = Graphics.FromImage(liveBmp);
            g.DrawString("Conf "+ score.ToString(), myFont, myPen.Brush, myPoint);
            g.Dispose();
            return liveBmp;
        }
    }
}
