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

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            g.DrawLine(MaakPen(this.kwast, 3), p1, p2);
        }

        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            this.sc = s;
        }

        SchetsControl sc;
        public override void Compleet(Graphics g, Point p1, Point p2)
        {
            sc.Historie.Push(new PenObject(MaakPen(this.kwast, 3).Color, 3, new List<Point>() { p1, p2 }));
            this.Bezig(g, p1, p2);
        }
    }
}
