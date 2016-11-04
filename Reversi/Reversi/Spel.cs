using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public class Spel : UserControl
    {
        public int VakjesBreedte { get; private set; }
        public int VakjesHoogte { get; private set; }

        public Steentje[,] BordRepresentatie { get; private set; }
        public Dictionary<Point, List<Point>> MogelijkeZetten { get; private set; }
        public List<Speler> Spelers { get; private set; } = new List<Speler>();
        public bool isSimulatie { get; private set; }

        private int aantalDodeZetten = 0;
        private const int lijnDikte = 4;

        private Speler _spelerAanZet;
        public Speler SpelerAanZet
        {
            get
            {
                return _spelerAanZet;
            }
            private set
            {
                _spelerAanZet = value;
                if (_spelerAanZet != null)
                {
                    onNieuweBeurt(this, new EventArgs());
                }
            }
        }

        private bool _hintsAanUit = true;
        public bool HintsAanUit
        {
            get
            {
                return _hintsAanUit;
            }
            set
            {
                _hintsAanUit = value;
                this.Invalidate();
            }
        }

        public event EventHandler onNieuweBeurt;
        public event EventHandler onSpelerGewonnen;

        public Spel(int vakjesBreedte, int vakjesHoogte, Speler speler1, Speler speler2, bool isSimulatie = false)
        {
            this.BackColor = Color.Transparent;

            this.VakjesBreedte = vakjesBreedte;
            this.VakjesHoogte = vakjesHoogte;

            BordRepresentatie = new Steentje[this.VakjesBreedte, this.VakjesHoogte];

            this.Spelers.Add(speler1);
            this.Spelers.Add(speler2);

            Point middenLB = new Point(vakjesBreedte / 2 - 1, vakjesHoogte / 2 - 1);

            BordRepresentatie[middenLB.X, middenLB.Y] = new Steentje(speler1);
            BordRepresentatie[middenLB.X + 1, middenLB.Y] = new Steentje(speler2);
            BordRepresentatie[middenLB.X, middenLB.Y + 1] = new Steentje(speler2);
            BordRepresentatie[middenLB.X + 1, middenLB.Y + 1] = new Steentje(speler1);

            this.ResizeRedraw = true;
            this.DoubleBuffered = true;
            this.Paint += Spel_Paint;
            this.Resize += Spel_Resize;
            this.onNieuweBeurt += Spel_onNieuweBeurt;

            this.isSimulatie = isSimulatie;
            if (!this.isSimulatie)
            {
                speler1.BindSpel(this);
                speler2.BindSpel(this);
            }
            this.SpelerAanZet = speler1;
        }

        private void Spel_onNieuweBeurt(object sender, EventArgs e)
        {
            this.GenereerMogelijkeZetten();
            this.Invalidate();

            if (this.MogelijkeZetten.Count == 0 && this.SpelerAanZet != null)
            {
                aantalDodeZetten++;
                // Een dode zet is als een speler niets kan doen
                if (aantalDodeZetten < 2)
                {
                    if (!isSimulatie)
                    {
                        DialogResult test = MessageBox.Show(SpelerAanZet.Naam + " kan niks meer doen, de beurt is beeindigd.");
                        this.volgendeSpeler();
                    }
                }
                else
                {
                    this.SpelerAanZet = null;
                    if (!isSimulatie)
                        onSpelerGewonnen(this.Winnaar, new EventArgs());
                }
            }
            else
            {
                aantalDodeZetten = 0;
            }
        }

        public Speler Winnaar
        {
            get
            {
                if (SpelerAanZet != null)
                {
                    return null;
                }
                else
                {
                    return SteentjesPerSpeler.FirstOrDefault(x =>
                                x.Value == SteentjesPerSpeler.Max(y => y.Value))
                            .Key;
                }
            }
        }

        public Dictionary<Speler, int> SteentjesPerSpeler
        {
            get
            {
                Dictionary<Speler, int> steentjesPerSpeler = new Dictionary<Speler, int>();
                foreach (Speler speler in Spelers.Distinct())
                {
                    steentjesPerSpeler.Add(speler, 0);
                }

                for (int x = 0; x < this.VakjesBreedte; x++)
                {
                    for (int y = 0; y < this.VakjesHoogte; y++)
                    {
                        if (BordRepresentatie[x, y] != null)
                        {
                            steentjesPerSpeler[BordRepresentatie[x, y].Eigenaar]++;
                        }
                    }
                }
                return steentjesPerSpeler;
            }
        }

        private void GenereerMogelijkeZetten()
        {
            MogelijkeZetten = new Dictionary<Point, List<Point>>();
            for (int x = 0; x < VakjesBreedte; x++)
            {
                for (int y = 0; y < VakjesHoogte; y++)
                {
                    Point zet = new Point(x, y);
                    List<Point> veroverdBijZet = VeroverdePuntenBijZet(zet, SpelerAanZet);
                    if (veroverdBijZet.Count > 0)
                    {
                        MogelijkeZetten.Add(zet, veroverdBijZet);
                    }
                }
            }
        }

        private List<Point> VeroverdePuntenBijZet(Point zet, Speler speler)
        {
            // Als er al wat op die plek staat
            if (BordRepresentatie[zet.X, zet.Y] != null)
            {
                return new List<Point>();
            }

            List<Point> veroverdePunten = new List<Point>();
            // Loop alle relatieve richtingen
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    List<Point> veroverd = VeroverdePuntenBijRichting(speler, zet, new Point(x, y));
                    veroverdePunten.AddRange(veroverd);
                }
            }
            return veroverdePunten;
        }

        private List<Point> VeroverdePuntenBijRichting(Speler speler, Point punt, Point richting)
        {
            Point echtePunt = new Point(punt.X, punt.Y);
            Speler eigenaar = BordRepresentatie[echtePunt.X, echtePunt.Y]?.Eigenaar;
            // Stack om punten op te gooien voor als het valide is
            //    , is het niet valide dan wordt deze niet teruggegeven
            List<Point> potentieelVeroverdePunten = new List<Point>();

            while (eigenaar != speler)
            {
                // Doe een stap in deze richting
                echtePunt.X += richting.X;
                echtePunt.Y += richting.Y;

                if (isPuntValide(echtePunt))
                {
                    eigenaar = BordRepresentatie[echtePunt.X, echtePunt.Y]?.Eigenaar;
                    if (eigenaar == speler)
                    { // Zet is valide, geef veroverde punten terug
                        return potentieelVeroverdePunten;
                    }
                    else if (eigenaar == null)
                    {
                        // Zet is niet valide
                        return new List<Point>();
                    }
                    else
                    {
                        // Onbekend of zet valide is, voeg punt toe aan potentieel veroverd
                        potentieelVeroverdePunten.Add(echtePunt);
                    }
                }
                else
                {
                    return new List<Point>();
                }
            }
            return new List<Point>();
        }

        private bool isPuntValide(Point punt)
        {
            return !(punt.X < 0 || punt.Y < 0 || punt.X >= VakjesBreedte || punt.Y >= VakjesHoogte);
        }

        public async void DoeZet(Speler speler, Point zet)
        {
            if (speler != SpelerAanZet)
            {
                return;
            }
            if (!MogelijkeZetten.ContainsKey(zet))
            {
                if (!isSimulatie)
                    MessageBox.Show("Dit is geen mogelijke zet!");
            }
            else
            {
                // Zorg dat alle spelers een beslissing hebben genomen (10MS foutmarge)
                await Task.Delay(10);
                if (speler != SpelerAanZet)
                {
                    return;
                }
                this.BordRepresentatie[zet.X, zet.Y] = new Steentje(speler);
                if (MogelijkeZetten.Count > 0)
                {
                    // Verover de zetten
                    foreach (Point veroverd in MogelijkeZetten[zet])
                    {
                        this.BordRepresentatie[veroverd.X, veroverd.Y] = new Steentje(speler);
                    }
                }

                volgendeSpeler();
            }
        }

        private void volgendeSpeler()
        {
            if (this.SpelerAanZet == this.Spelers.Last())
            {
                this.SpelerAanZet = this.Spelers.First();
            }
            else
            {
                this.SpelerAanZet = this.Spelers[this.Spelers.IndexOf(SpelerAanZet) + 1];
            }
        }

        private void Spel_Resize(object sender, EventArgs e)
        {
            // Zorg dat het bord vierkant blijft
            int pixelsPerVakjeBreed = this.Width / this.VakjesBreedte;
            int pixelsPerVakjeHoog = this.Height / this.VakjesHoogte;

            int vakjesGrootte = Math.Min(pixelsPerVakjeBreed, pixelsPerVakjeHoog);

            this.Width = vakjesGrootte * this.VakjesBreedte;
            this.Height = vakjesGrootte * this.VakjesHoogte;
        }

        private void Spel_Paint(object sender, PaintEventArgs e)
        {
            int breedtePerVakje = this.Width / this.VakjesBreedte;
            int hoogtePerVakje = this.Height / this.VakjesHoogte;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen lijnPen = new Pen(Color.Black, Spel.lijnDikte);

            for (int x = 0; x < this.VakjesBreedte; x++)
            {
                for (int y = 0; y < this.VakjesHoogte; y++)
                {
                    Rectangle omlijning = new Rectangle(
                            new Point(breedtePerVakje * x, hoogtePerVakje * y),
                            new Size(breedtePerVakje, hoogtePerVakje)
                            );

                    Pen lijnGradientPen = new Pen(Color.FromArgb(15, 0, 0, 0));
                    lijnGradientPen.Width = Spel.lijnDikte;
                    g.DrawRectangle(
                        lijnGradientPen,
                        omlijning
                        );

                    if (BordRepresentatie[x, y] != null)
                    {
                        Rectangle cirkel = new Rectangle(
                                new Point(breedtePerVakje * x + Spel.lijnDikte, hoogtePerVakje * y + Spel.lijnDikte),
                                new Size(breedtePerVakje - 2 * Spel.lijnDikte, hoogtePerVakje - 2 * Spel.lijnDikte)
                            );

                        BordRepresentatie[x, y].TekenNaarGraphics(g, cirkel);
                    }
                }
            }

            if (this.HintsAanUit)
            {
                foreach (Point hint in MogelijkeZetten.Keys)
                {
                    Rectangle cirkel = new Rectangle(
                                new Point(breedtePerVakje * hint.X + Spel.lijnDikte, hoogtePerVakje * hint.Y + Spel.lijnDikte),
                                new Size(breedtePerVakje - 2 * Spel.lijnDikte, hoogtePerVakje - 2 * Spel.lijnDikte)
                            );
                    Brush b = new SolidBrush(Color.FromArgb(50, Color.Black));
                    g.FillEllipse(b, cirkel);
                }
            }
        }

        public Spel MaakSimulatieVoor(Speler jij, Speler simulatie)
        {
            // Maak een nieuw spel aan zodat een eventuele AI daarop kan simuleren
            Spel kloon = new Spel(this.VakjesBreedte, this.VakjesHoogte, jij, simulatie, true);
            kloon.BordRepresentatie = this.BordRepresentatie.Clone() as Steentje[,];
            kloon.SpelerAanZet = jij;
            return kloon;
        }
    }
}
