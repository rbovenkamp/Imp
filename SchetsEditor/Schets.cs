using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SchetsEditor
{
    public class Schets
    {
        public Bitmap Bitmap { get; private set; }

        public Schets()
        {
            Bitmap = new Bitmap(1, 1);
        }
        public Schets(Bitmap bmp)
        {
            Bitmap = bmp;
        }

        public Graphics BitmapGraphics
        {
            get { return Graphics.FromImage(Bitmap); }
        }

        public void VeranderAfmeting(Size sz)
        {
            if (sz.Width > Bitmap.Size.Width || sz.Height > Bitmap.Size.Height)
            {
                Bitmap nieuw = new Bitmap( Math.Max(sz.Width,  Bitmap.Size.Width)
                                         , Math.Max(sz.Height, Bitmap.Size.Height)
                                         );
                Graphics gr = Graphics.FromImage(nieuw);
                gr.FillRectangle(Brushes.White, 0, 0, sz.Width, sz.Height);
                gr.DrawImage(Bitmap, 0, 0);
                Bitmap = nieuw;
            }
        }
        public void Teken(Graphics gr)
        {
            gr.DrawImage(Bitmap, 0, 0);
        }
        public void Schoon()
        {
            Graphics gr = Graphics.FromImage(Bitmap);
            gr.FillRectangle(Brushes.White, 0, 0, Bitmap.Width, Bitmap.Height);
        }
        public void Roteer()
        {
            Bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }
    }
}
