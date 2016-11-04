using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class VolOvaalTool : RechthoekTool
    {
        public override string ToString() { return "ei"; }
        
        VolOvaalObject huidigVolOvaalObject;
        SchetsControl temphulp;
        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            temphulp = s;
            huidigVolOvaalObject = new VolOvaalObject(p, p, s.PenKleur);
            s.Schets.Historie.Push(huidigVolOvaalObject);
        }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            huidigVolOvaalObject.einde = p2;
        }

        public override void Compleet(Graphics g, Point p1, Point p2)
        {
            Bezig(g, p1, p2);
            huidigVolOvaalObject = null;
        }
    }
}
