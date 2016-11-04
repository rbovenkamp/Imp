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

        TekstObject huidigTekstObject;

        public override void MuisDrag(SchetsControl s, Point p) { }

        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);

            if (huidigTekstObject != null)
            {
                huidigTekstObject = null;
            }
            huidigTekstObject = new TekstObject(startpunt, 3, s.PenKleur, "\u23B8");
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
