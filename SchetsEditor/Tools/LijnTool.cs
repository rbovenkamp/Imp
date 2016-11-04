using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class LijnTool : TweepuntTool
    {
        public override string ToString() { return "lijn"; }

        LijnObject huidigObject;
        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            huidigObject = new LijnObject(s.PenKleur, s.PenDikte, p, p);
            s.Schets.Historie.Push(huidigObject);
        }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            huidigObject.VeranderEinde(p2);
        }

        public override void Compleet(Graphics g, Point p1, Point p2)
        {
            this.Bezig(g, p1, p2);
            huidigObject = null;
        }
    }
}
