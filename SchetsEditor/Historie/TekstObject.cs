using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SchetsEditor.Historie
{
    class TekstObject : ISchetsObject
    {
        public Point locatie;
        int grootte;
        Color kleur;
        public string tekst;
        public TekstObject(Point locatie, int grootte, Color kleur, string tekst)
        {
            this.locatie = locatie;
            this.grootte = grootte;
            this.kleur = kleur;
            this.tekst = tekst;
        }

        public string Serialiseer()
        {
            return locatie.X + "|" + locatie.Y + "|" + grootte + "|" + ColorTranslator.ToHtml(kleur) + "|" + tekst;
        }

        public static ISchetsObject VanSerialisatie(string serialisatie)
        {
            string[] splits = serialisatie.Split('|');
            List<object> inhoud = new List<object> { new Point(int.Parse(splits[0]), int.Parse(splits[1])),
                int.Parse(splits[2]),
                ColorTranslator.FromHtml(splits[3]),
                splits[4] };
            return new TekstObject((Point)inhoud[0], (int)inhoud[1], (Color)inhoud[2], (string)inhoud[3]);
        }
        
        public void Letter(char c)
        {
            if (c >= 32)
            {
                if (tekst == "\u23B8")
                    tekst = c.ToString();
                else
                    tekst += c.ToString();
            }
            else if (c == (char)8 && tekst != "")
                tekst = tekst.Remove(tekst.Length - 1);
        }

        public void Teken(Graphics g)
        {
            Brush kwast = new SolidBrush(kleur);
            Font font = new Font("Tahoma", 10*grootte);
            g.DrawString(tekst, font, kwast, locatie, StringFormat.GenericTypographic);
        }

        public bool RaaktCirkel(Point locatiegum, int radius)
        {
            // Maak een oppervlakte die ongeveer de grootte van de tekst voorsteld
            Size oppervlakte = TextRenderer.MeasureText(tekst, new Font("Tahoma", 10 * grootte));

            // Check of er binnen die oppervlakte 'gegumd' wordt.
            return VolRechthoekObject.RaaktCirkel(locatiegum, radius,
                locatie
                , new Point(locatie.X+oppervlakte.Width, locatie.Y+oppervlakte.Height));
        }

    }
}
