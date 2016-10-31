using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public class PenObject : ISchetsObject
    {
        private Color kleur;
        private const string kleurSerialisatieNaam = "kleur";
        private List<Point> gekleurdePunten;
        private const string puntenSerialisatieNaam = "punten";

        public PenObject(Color kleur, List<Point> gekleurdePunten)
        {
            this.kleur = kleur;
            this.gekleurdePunten = gekleurdePunten;
        }

        public string Serialiseer()
        {
            string geserialiseerd = kleurSerialisatieNaam + ":" + ColorTranslator.ToHtml(kleur) + "\\";
            
            IEnumerable<string> punten = gekleurdePunten.Select(x => x.X + "," + x.Y);
            geserialiseerd += puntenSerialisatieNaam + ":" + String.Join(";", punten);

            return geserialiseerd;
        }

        public static ISchetsObject VanSerialisatie(string serialisatie)
        {
            Color kleur = Color.Black;
            List<Point> punten = new List<Point>();

            string[] instellingen = serialisatie.Split('\\');
            foreach (string instelling in instellingen)
            {
                string[] naamEnWaarde = instelling.Split(':');
                if (naamEnWaarde[0] == kleurSerialisatieNaam)
                {
                    kleur = instellingNaarKleur(naamEnWaarde[1]);
                }
                if (naamEnWaarde[0] == puntenSerialisatieNaam)
                {
                    punten = instellingNaarPunten(naamEnWaarde[1]);
                }
            }
            
            return new PenObject(kleur, punten);
        }

        private static Color instellingNaarKleur(string waarde)
        {
            return ColorTranslator.FromHtml(waarde);
        }

        private static List<Point> instellingNaarPunten(string waarde)
        {
            string[] puntStrings = waarde.Split(';');
            List<Point> punten = new List<Point>();

            foreach (string punt in puntStrings)
            {
                string[] xEnY = punt.Split(',');
                punten.Add(new Point(int.Parse(xEnY[0]), int.Parse(xEnY[1])));
            }

            return punten;
        }

        public void Teken(Graphics g)
        {
            Pen pen = new Pen(kleur);
            Size grootte = new Size(1, 1);
            foreach (Point punt in gekleurdePunten)
            {
                g.DrawRectangle(pen, new Rectangle(punt, grootte));
            }
        }
    }
}
