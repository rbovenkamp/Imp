using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class GumTool : PenTool
    {
        public override string ToString() { return "gum"; }

        //public override void Bezig(Graphics g, Point p1, Point p2)
        //{
        //    g.DrawLine(MaakPen(Brushes.White, 7), p1, p2);
        //}
    }
}
