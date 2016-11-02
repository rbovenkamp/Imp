using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SchetsEditor.Historie
{
    class PlaatjeObject : ISchetsObject
    {
        public Bitmap Bitmap;
        private const string bitmapSerialisatieNaam = "bitmap";
        private const string breedteSerialisatieNaam = "breedte";
        private const string hoogteSerialisatieNaam = "hoogte";

        public PlaatjeObject(Bitmap bmp)
        {
            this.Bitmap = bmp;
        }

        public string Serialiseer()
        {
            string geserialiseerd = breedteSerialisatieNaam + ":" + Bitmap.Width + "\\";
            geserialiseerd += hoogteSerialisatieNaam + ":" + Bitmap.Height + "\\";
            geserialiseerd += bitmapSerialisatieNaam + ":" + BitmapToString(Bitmap);
            return geserialiseerd;
        }

        public string BitmapToString(Bitmap bmp)
        {
            string[] pixels = new string[bmp.Width * bmp.Height];
            for (int x = 0; x < bmp.Width; x ++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    pixels[x + bmp.Width * y] = c.B + "," + c.G + "," + c.R;
                }
            }

            return String.Join(";", pixels);
        }

        public static ISchetsObject VanSerialisatie(string serialisatie)
        {
            Bitmap bmp = null;

            int? breedte = null;
            int? hoogte = null;

            string[] instellingen = serialisatie.Split('\\');
            foreach (string instelling in instellingen)
            {
                string[] naamEnWaarde = instelling.Split(':');
                switch (naamEnWaarde[0])
                {
                    case bitmapSerialisatieNaam:
                        schrijfStringNaarBitmap(naamEnWaarde[1], bmp);
                        break;
                    case hoogteSerialisatieNaam:
                        hoogte = int.Parse(naamEnWaarde[1]);
                        if (breedte != null)
                            bmp = new Bitmap(breedte ?? 0, hoogte ?? 0);
                        break;
                    case breedteSerialisatieNaam:
                        breedte = int.Parse(naamEnWaarde[1]);
                        if (hoogte != null)
                            bmp = new Bitmap(breedte ?? 0, hoogte ?? 0);
                        break;
                }
            }

            return new PlaatjeObject(bmp);
        }

        private static void schrijfStringNaarBitmap(string bmpString, Bitmap bmp)
        {
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var length = bmd.Stride * bmd.Height;

            byte[] rgbs = new byte[length];
            int currentRgbPos = 0;

            char[] buffer = new char[3];
            int currentBufferPos = 0;

            foreach (char c in bmpString)
            {
                if (c == ',' || c==';')
                {
                    rgbs[currentRgbPos] = (byte)int.Parse(new string(buffer, 0, currentBufferPos));
                    currentBufferPos = 0;
                    currentRgbPos ++;
                }
                else
                {
                    buffer[currentBufferPos] = c;
                    currentBufferPos++;
                }
            }

            Marshal.Copy(rgbs, 0, bmd.Scan0, length);
            bmp.UnlockBits(bmd);
        }

        public void Teken(Graphics g)
        {
            g.DrawImage(this.Bitmap, 0, 0, this.Bitmap.Width, this.Bitmap.Height);
        }

        public bool RaaktCirkel(Point locatie, int radius)
        {
            return false;
        }
    }
}
