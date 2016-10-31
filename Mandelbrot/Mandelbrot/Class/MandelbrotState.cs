using Mandelbrot.Class;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Mandelbrot
{
    // Verzameling van variabelen om de status van de mandelbrot aan te geven
    public class MandelbrotState
    {
        public uint MaxIterations { get; private set; }
        public double Scale { get; private set; }
        public PointD Center { get; private set; }
        public ColorPallet.ColorScheme ColorScheme { get; private set; }

        public MandelbrotState(uint MaxIterations, double Scale, PointD Center)
        {
            this.MaxIterations = MaxIterations;
            this.Scale = Scale;
            this.Center = Center;
        }

        public Bitmap ToImage(Size size, uint iterations)
        {
            // Haal een tabel op die de "echte" coordinaten voor iedere X en Y geeft
            PointD[,] coordinates = this.GetTransposedPointsArray(size);
            // Reken voor iedere coordinaat de kleur uit met de mandelbrot formule
            Color[,] colorResults = this.GetMandelbrotColorArray(size, coordinates, iterations);

            if (size.Width > 0 && size.Height > 0)
            {
                // Teken de kleuren in een bitmap
                Bitmap mandelbrot = this.ColorArrayToBmp(size, colorResults);
                // Teken de informatie van de status in de bitmap
                mandelbrot = this.DrawMandelbrotStateToBmp(mandelbrot);
                return mandelbrot;
            }
            return new Bitmap(size.Width, size.Height);
        }

        // Gegeven een grootte, een schaal en een coordinaat berekent deze functie de echte coordinaten voor iedere pixel
        private PointD[,] GetTransposedPointsArray(Size size)
        {
            double deductWidth = (size.Width / 2) * this.Scale;
            double deductHeight = (size.Height / 2) * this.Scale;

            PointD[,] transposedPoints = new PointD[size.Width, size.Height];
            // Een array is een reference-type en hiermee thread-safe zolang je niet naar dezelfde waarden in de array schrijft. 
            // Parallel.for is een for loop die de verzameling verdeelt over meerdere threads
            Parallel.For(0, size.Width, x =>
            {
                for (int y = 0; y < size.Height; y++)
                {
                    transposedPoints[x, y] = new PointD(x * this.Scale - deductWidth + this.Center.X,
                                                        y * this.Scale - deductHeight + this.Center.Y);
                }
            });

            return transposedPoints;
        }

        // Gegeven de coordinaten reken de mandelbrot uit en geef de kleuren terug
        private Color[,] GetMandelbrotColorArray(Size size, PointD[,] coordinates, uint iterations)
        {
            ColorPallet pallet = new ColorPallet(this.ColorScheme);
            Color[,] colorResults = new Color[size.Width, size.Height];
            // Een array is een reference-type en hiermee thread-safe zolang je niet naar dezelfde waarden in de array schrijft. 
            // Parallel.for is een for loop die de verzameling verdeelt over meerdere threads
            Parallel.For(0, size.Width, x =>
            {
                for (int y = 0; y < size.Height; y++)
                {
                    double c_result = 0;
                    // Reken voor dit coordinaat het aantal iteraties en het eindresultaat uit
                    uint it_result = coordinates[x, y].GetMandelBrotNumber(iterations, out c_result);
                    // Geef de kleur terug gegeven het aantal iteraties en het eindresultaat
                    colorResults[x, y] = pallet.GetColor(it_result, c_result);
                }
            });
            return colorResults;
        }

        private Bitmap ColorArrayToBmp(Size size, Color[,] colors)
        {
            Bitmap b = new Bitmap(size.Width, size.Height);

            BitmapData bmd = b.LockBits(new Rectangle(0, 0, size.Width, size.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int s = bmd.Stride;
            IntPtr i = bmd.Scan0;
            int bytes = Math.Abs(s) * b.Height;

            byte[] rgbs = new byte[bytes];
            Parallel.For(0, size.Width, x =>
            {
                for (int y = 0; y < size.Height; y++)
                {
                    rgbs[(x * 3) + y * s] = colors[x, y].B;
                    rgbs[(x * 3) + y * s + 1] = colors[x, y].G;
                    rgbs[(x * 3) + y * s + 2] = colors[x, y].R;
                }
            });

            Marshal.Copy(rgbs, 0, i, bytes);
            b.UnlockBits(bmd);

            return b;
        }

        private Bitmap DrawMandelbrotStateToBmp(Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            Font f = new Font("Tahoma", 8);
            g.DrawString("X: " + this.Center.X, f, Brushes.Yellow, new PointF(0, 0));
            g.DrawString("Y: " + this.Center.Y, f, Brushes.Yellow, new PointF(0, 10));
            g.DrawString("Scale: " + this.Scale, f, Brushes.Yellow, new PointF(0, 20));
            g.DrawString("Iterations: " + this.MaxIterations, f, Brushes.Yellow, new PointF(0, 30));
            return bmp;
        }
    }
}