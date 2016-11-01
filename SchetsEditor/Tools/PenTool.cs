using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class PenTool : TweepuntTool
    {
        public override string ToString() { return "pen"; }

        PenObject huidigPenObject;
        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            Stack<Point> st = new Stack<Point>();
            st.Push(p);
            huidigPenObject = new PenObject(s.PenKleur, 3, st);
            s.Schets.Historie.Push(huidigPenObject);
        }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            huidigPenObject.Punten.Push(p2);
        }

        public override void Compleet(Graphics g, Point p1, Point p2)
        {
            this.Bezig(g, p1, p2);
            huidigPenObject = null;
        }
    }
}
