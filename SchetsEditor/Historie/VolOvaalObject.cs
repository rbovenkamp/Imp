using SchetsEditor.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public class VolOvaalObject : TweePuntObject
    {
        private Color kleur;
        public VolOvaalObject(Point begin, Point einde, Color kleur) : base(begin, einde, kleur)
        {
            this.begin = begin;
            this.einde = einde;
            this.kleur = kleur;
        }

        public override string Naam()
        {
            throw new NotImplementedException();
        }

        public override void Teken(Graphics g)
        {
            Brush kwast = new SolidBrush(kleur);
            g.FillEllipse(kwast, TweepuntTool.Punten2Rechthoek(begin, einde));
        }

        public static ISchetsObject VanSerialisatie(string serialisatie)
        {
            List<object> inhoud = LaadPuntenUitSerialisatie(serialisatie);
            return new VolOvaalObject((Point)inhoud[0], (Point)inhoud[1], (Color)inhoud[2]);
        }

        public override bool RaaktCirkel(Point locatie, int radius)
        {
            Size stralen = new Size(Math.Abs(begin.X - einde.X) / 2, Math.Abs(begin.Y - einde.Y) / 2);
            Point midden = new Point(Math.Min(begin.X, einde.X) + stralen.Width, Math.Min(begin.Y, einde.Y) + stralen.Height);
            Point respafstand = new Point(locatie.X - midden.X, locatie.Y - midden.Y);
            double hoek = Math.Atan(respafstand.Y/respafstand.X);

            return (Math.Sqrt(respafstand.X * respafstand.X + respafstand.Y * respafstand.Y) * 2 < stralen.Width * Math.Cos(hoek) + stralen.Width + Math.Sin(hoek) + radius);
        }
    }
}
