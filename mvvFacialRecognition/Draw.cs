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

        public Draw(Bitmap currentViewImage, Pen myPen)
        {
            myImage = currentViewImage;
            g = Graphics.FromImage(myImage);
            p = myPen;
        }

        internal Bitmap drawFaceRectangle(NleFace thisFace, Bitmap myImage, Pen myPen)
        {
            g.DrawRectangle(p, thisFace.Rectangle.X, thisFace.Rectangle.Y, thisFace.Rectangle.Width, thisFace.Rectangle.Height);
            
            return myImage;
        }

        //internal Bitmap markEyes(NleFace thisFace, Bitmap bmp, Pen p)
        //{
        //    g.DrawLine(p,thisFace.);
        //}
    }


}
