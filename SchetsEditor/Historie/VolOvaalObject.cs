using SchetsEditor.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            SizeF stralen = new SizeF(Math.Abs(begin.X - einde.X) / 2.0f, Math.Abs(begin.Y - einde.Y) / 2.0f);
            PointF midden = new PointF(Math.Min(begin.X, einde.X) + stralen.Width, Math.Min(begin.Y, einde.Y) + stralen.Height);
            PointF respafstand = new PointF(locatie.X - midden.X, locatie.Y - midden.Y);
            double hoek;
            if (respafstand.Y == 0)
                hoek = Math.PI / 2;
            else
                hoek = Math.Atan2(respafstand.X, respafstand.Y);

            double afstand = Math.Sqrt(respafstand.X * respafstand.X + respafstand.Y * respafstand.Y);

            // Lijkt op OvaalObject maar moeilijk samen te nemen.

            double sinHeight = stralen.Height * Math.Sin(hoek);
            double cosWidth = stralen.Width * Math.Cos(hoek);
            double straal = stralen.Height*stralen.Width/(Math.Sqrt(sinHeight * sinHeight + cosWidth * cosWidth));
            return (afstand - radius < straal);
        }
    }
}
