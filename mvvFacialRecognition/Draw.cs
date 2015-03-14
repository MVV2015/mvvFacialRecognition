using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neurotec.Biometrics;
using Neurotec.Biometrics.Gui;

namespace mvvFacialRecognition
{
    
    class Draw
    {
        //Bitmap myImage;
        Graphics g;
        Font myFont = new Font("Microsoft Sans Serif",(float)8);

        public Draw()
        {
        }

        internal Bitmap drawFaceRectangle(NleFace thisFace, Bitmap myImage, int confScore, Pen p)
        {
            g = Graphics.FromImage(myImage);
            g.DrawRectangle(p, thisFace.Rectangle.X, thisFace.Rectangle.Y, thisFace.Rectangle.Width, thisFace.Rectangle.Height);
            g.DrawString(("Confidence Score: " + confScore), myFont, p.Brush, thisFace.Rectangle.Left, thisFace.Rectangle.Bottom);
            return myImage;
        }
        //Image iSource = Image.FromFile(@"c:\users\tamer\desktop\a.jpg");//load the orginal file
        //    int width = 100;//determine the width of the area will be copied
        //    int height = 100;//determine the height of the area will be copied 
        //    Image iNew = new Bitmap(width, height);//create a new image
        //    Graphics g = Graphics.FromImage(iNew);//create graphics object
        //    Rectangle destRect = new Rectangle(0, 0, iNew.Width, iNew.Height);//create destination rectangle
        //    Rectangle srcRect = new Rectangle(50, 100, width, height);//get from (50,100) to (150,200). source rectangle
        //    g.DrawImage(iSource, destRect, srcRect, GraphicsUnit.Pixel);//draw image
        //    iNew.Save(@"c:\users\tamer\desktop\a2.jpg");//save new image
        //    g.Dispose();//dispose graphics

        internal Image snipFace(Bitmap bmp, NleDetectionDetails[] nleDetectionDetails, Pen p)
        {
            int _width = (int)(nleDetectionDetails[0].Face.Rectangle.Width * 2);
            int _height = (int)(nleDetectionDetails[0].Face.Rectangle.Height * 2);
            Image snippedImage = new Bitmap(_width, _height);
            g = Graphics.FromImage(snippedImage);
            Rectangle destination = new Rectangle(0, 0, _width, _height);
            Rectangle source = new Rectangle((nleDetectionDetails[0].Face.Rectangle.X - _width / 4),
                (nleDetectionDetails[0].Face.Rectangle.Y - _height / 4), _width, _height);
            g.DrawImage(bmp, destination, source, GraphicsUnit.Pixel);
            return snippedImage;
        }
    }
}
