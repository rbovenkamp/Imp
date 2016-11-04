using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class VolRechthoekTool : TweepuntTool
    {
        public override string ToString() { return "vlak"; }

        VolRechthoekObject huidigVolRechthoekObject;
        SchetsControl temphulp;
        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            temphulp = s;
            huidigVolRechthoekObject = new VolRechthoekObject(p, p, s.PenKleur);
            s.Schets.Historie.Push(huidigVolRechthoekObject);
        }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            huidigVolRechthoekObject.einde = p2;
        }

        public override void Compleet(Graphics g, Point p1, Point p2)
        {
            Bezig(g, p1, p2);
            huidigVolRechthoekObject = null;
        }
    }
}
