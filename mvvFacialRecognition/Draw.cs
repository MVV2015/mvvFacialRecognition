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
        Graphics g;
        Pen p;
        public Draw()
        { }

        internal Bitmap drawFaceRectangle(NleFace thisFace, Bitmap myImage, Pen myPen)
        {
            p = myPen;
            g = Graphics.FromImage(myImage);
            g.DrawRectangle(p, thisFace.Rectangle.X, thisFace.Rectangle.Y, thisFace.Rectangle.Width, thisFace.Rectangle.Height);
            return myImage;
        }
    }


}
