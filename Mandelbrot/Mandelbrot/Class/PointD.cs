namespace Mandelbrot
{
    // Struct als Point of PointF maar dan met doubles, handig omdat doubles een dubbel zo hoge precisie kunnen hebben
    public struct PointD
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointD(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public uint GetMandelBrotNumber(uint maxIterate, out double c_result)
        {
            // Wat ingelezen op de formule, schijnt dat je een reel coordinaat (this.) en een imaginair coordinaat gebruikt vandaar imaginary
            PointD imaginary = new PointD(0, 0);
            // Imaginary in het kwadraat
            PointD imaginary2 = new PointD(0, 0);
            double compare = 0;

            uint counter = 0;

            // < 4 omdat 4 het kwadraat van 2 is, betere performance
            while (counter < maxIterate && compare < 4)
            {
                // Schrijf de nieuwe imaginaire X en Y, ofwel a en b in de formule
                imaginary = new PointD(imaginary2.X - imaginary2.Y + this.X, 2 * imaginary.X * imaginary.Y + this.Y);
                // Reken het kwadraat uit omdat we die voor de compare variabele en de volgende iteratie nodig hebben (efficienter)
                imaginary2 = imaginary * imaginary;
                compare = imaginary2.X + imaginary2.Y;

                counter++;
            }
            // Geef het laatste resultaat terug
            c_result = compare;
            return counter;
        }

        // Om bovenstaande methode iets te versimpelen
        public static PointD operator * (PointD p1, PointD p2)
        {
            return new PointD(p1.X * p2.X, p1.Y * p2.Y);
        }
    }
}
