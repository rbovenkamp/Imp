using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public class RechthoekObject : PenObject
    {
        Point begin;

        public RechthoekObject(Color kleur, int dikte, Point begin, Point einde) 
            : base (kleur, dikte, maakRechthoek(begin, einde))
        {
            this.begin = begin;
        }

        public void VeranderEinde(Point einde)
        {
            this.Punten = maakRechthoek(this.begin, einde);
        }

        private static Stack<Point> maakRechthoek(Point begin, Point einde)
        {
            return new Stack<Point>(new Point[] { begin, new Point(einde.X, begin.Y), einde, new Point(begin.X, einde.Y), begin });
        }
    }
}
