using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class GumTool : ISchetsTool
    {
        public void Letter(SchetsControl s, char c)
        {
        }

        public void MuisDrag(SchetsControl s, Point p)
        {
        }

        public void MuisLos(SchetsControl s, Point p)
        {
            for (int n = 0; n < s.Schets.Historie.Count; n++)
            {
                ISchetsObject so = s.Schets.Historie[n];
                if (so.RaaktCirkel(p, 10))
                {
                    int nummerVanOnderop = s.Schets.Historie.Count - n;
                    s.Schets.Historie.Push(new GumObject(nummerVanOnderop));
                    s.Invalidate();
                    return;
                }
            }
        }

        public void MuisVast(SchetsControl s, Point p)
        {
        }

        public override string ToString() { return "gum"; }
    }
}
