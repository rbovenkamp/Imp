using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class OvaalTool : TweepuntTool
    {
        public override string ToString() { return "ovaal"; }

        OvaalObject huidigOvaalObject;
        SchetsControl temphulp;
        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            temphulp = s;
            huidigOvaalObject = new OvaalObject(p, p, 3, s.PenKleur);
            s.Schets.Historie.Push(huidigOvaalObject);
        }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            huidigOvaalObject.einde = p2;
        }

        public override void Compleet(Graphics g, Point p1, Point p2)
        {
            Bezig(g, p1, p2);
            huidigOvaalObject = null;
        }
    }
}
