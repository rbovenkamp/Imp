using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public class LijnObject : PenObject
    {
        public LijnObject(Color kleur, int dikte, Point begin, Point einde) 
            : base (kleur, dikte, new Stack<Point>(new Point[] { begin, einde }))
        {

        }

        public void VeranderEinde(Point einde)
        {
            this.Punten.Pop();
            this.Punten.Push(einde);
        }
    }
}
