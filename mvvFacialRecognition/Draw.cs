using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neurotec.Biometrics;

namespace mvvFacialRecognition
{
    
    class Draw
    {
        Bitmap myImage;
        Graphics g;
        Pen p;
        Font myFont = new Font("Microsoft Sans Serif",(float)8);

        public Draw(Bitmap currentViewImage, Pen myPen)
        {
            myImage = currentViewImage;
            g = Graphics.FromImage(myImage);
            p = myPen;
        }

        internal Bitmap drawFaceRectangle(NleFace thisFace, Bitmap myImage, int confScore)
        {
            g.DrawRectangle(p, thisFace.Rectangle.X, thisFace.Rectangle.Y, thisFace.Rectangle.Width, thisFace.Rectangle.Height);
            g.DrawString(("Confidence Score: " + confScore), myFont, p.Brush, thisFace.Rectangle.Left, thisFace.Rectangle.Bottom);
            return myImage;
        }
    }
}
