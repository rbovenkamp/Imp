using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class VerplaatsTool : ISchetsTool
    {
        public override string ToString() { return "move"; }

        ISchetsObject geselecteerdObject;
        Point huidigPunt;

        public void Letter(SchetsControl s, char c)
        {
        }

        public void MuisDrag(SchetsControl s, Point p)
        {
            if (geselecteerdObject is PenObject)
            {
                PenObject po = geselecteerdObject as PenObject;
                int xDiff = huidigPunt.X - p.X;
                int yDiff = huidigPunt.Y - p.Y;

                Stack<Point> newPunten = new Stack<Point>();
                foreach (Point oldP in po.Punten)
                {
                    newPunten.Push(new Point(oldP.X - xDiff, oldP.Y - yDiff));
                }
                po.Punten = newPunten;

                huidigPunt = p;
                s.Invalidate();
            }
        }

        public void MuisLos(SchetsControl s, Point p)
        {
            geselecteerdObject = null;
        }

        public void MuisVast(SchetsControl s, Point p)
        {
            for (int n = 0; n < s.Schets.Historie.Count; n++)
            {
                ISchetsObject so = s.Schets.Historie[n];
                if (so.RaaktCirkel(p, 10))
                {
                    geselecteerdObject = so;
                    huidigPunt = p;
                    return;
                }
            }
        }
    }
}
