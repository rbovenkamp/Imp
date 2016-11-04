using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class TekstTool : StartpuntTool
    {
        public override string ToString() { return "tekst"; }

        private const string lijntje = "\u23B8";

        TekstObject huidigTekstObject;

        public override void MuisDrag(SchetsControl s, Point p) { }

        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);

            // Ter verduidelijking wordt in eerste instantie een lijntje getekend om duidelijk te maken waar je gaat typen na te klikken.
            if (huidigTekstObject != null)
            {
                if (huidigTekstObject.tekst == lijntje)
                {
                    huidigTekstObject.tekst = string.Empty;
                }
                huidigTekstObject = null;
            }
            huidigTekstObject = new TekstObject(startpunt, s.PenDikte, s.PenKleur, lijntje);
            s.Schets.Historie.Push(huidigTekstObject);
            s.Invalidate();
        }

        public override void Letter(SchetsControl s, char c)
        {
            huidigTekstObject.Letter(c);
            s.Invalidate();
        }
    }
}
