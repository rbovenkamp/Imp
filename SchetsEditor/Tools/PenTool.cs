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

        public static Pen MaakPen(Brush b, int dikte)
        {
            Pen pen = new Pen(b, dikte);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            return pen;
        }

        public override void MuisVast(SchetsControl s, Point p)
        {
            this.TekenPunt(s, p);
            base.MuisVast(s, p);
        }
        public override void MuisDrag(SchetsControl s, Point p)
        {
            this.TekenPunt(s, p);
            base.MuisVast(s, p);
            base.MuisLos(s, p);
            s.Refresh();
        }
        public override void MuisLos(SchetsControl s, Point p)
        {
            this.TekenPunt(s, p);
            s.Historie.Push(new PenObject(s.PenKleur, puntBuffer));
            puntBuffer = new List<Point>();
            base.MuisLos(s, p);
            s.Invalidate();
        }

        private void TekenPunt(SchetsControl s, Point p)
        {
            puntBuffer.Add(p);
            s.MaakBitmapGraphics().DrawLine(new Pen(s.PenKleur, 3), p, p);
        }

        public override void Letter(SchetsControl s, char c)
        {
        }
    }
}
