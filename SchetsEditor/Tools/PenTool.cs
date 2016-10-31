using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace SchetsEditor.Tools
{
    public class PenTool : StartpuntTool
    {
        public override string ToString() { return "pen"; }

        private List<Point> puntBuffer = new List<Point>();

        private Point huidigePunt;

        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            huidigePunt = p;
            this.TekenPunt(s, p);
        }
        public override void MuisDrag(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            base.MuisLos(s, p);
            this.TekenPunt(s, p);
            s.Refresh();
        }
        public override void MuisLos(SchetsControl s, Point p)
        {
            base.MuisLos(s, p);
            this.TekenPunt(s, p);
            puntBuffer.Distinct().ToList();
            s.Historie.Push(new PenObject(s.PenKleur, 3, puntBuffer));
            puntBuffer = new List<Point>();
            s.Invalidate();
        }

        private void TekenPunt(SchetsControl s, Point p)
        {
            puntBuffer.Add(p);
            Graphics g = s.MaakBitmapGraphics();
            s.Invalidate();
            g.DrawLine(new Pen(s.PenKleur, 3), huidigePunt, p);

            huidigePunt = p;
        }

        public override void Letter(SchetsControl s, char c)
        {
        }
    }
}
