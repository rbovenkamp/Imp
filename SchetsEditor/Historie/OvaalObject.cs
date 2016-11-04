using SchetsEditor.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    class OvaalObject : TweePuntObject
    {
        private Color kleur;
        private int dikte;
        public OvaalObject(Point begin, Point einde, int dikte, Color kleur) : base(begin, einde, kleur)
        {
            this.begin = begin;
            this.einde = einde;
            this.kleur = kleur;
            this.dikte = dikte;
        }

        public override string Naam()
        {
            throw new NotImplementedException();
        }

        public override void Teken(Graphics g)
        {
            Pen eenpen = new Pen(kleur, dikte);
            g.DrawEllipse(eenpen, TweepuntTool.Punten2Rechthoek(begin, einde));
        }

        public override string Serialiseer()
        {
            return begin.X + "," + begin.Y + ";" + einde.X + "," + einde.Y + "/" + ColorTranslator.ToHtml(kleur) + ":" + dikte;
        }

        public static ISchetsObject VanSerialisatie(string serialisatie)
        {
            List<object> inhoud = LaadMeerPuntenUitSerialisatie(serialisatie);
            return new OvaalObject((Point)inhoud[0], (Point)inhoud[1], (int)inhoud[3], (Color)inhoud[2]);
        }

        public static List<object> LaadMeerPuntenUitSerialisatie(string serialisatie)
        {
            string[] splits = serialisatie.Split(':');
            string rest = splits[0];
            List<object> resultaat = LaadPuntenUitSerialisatie(rest);
            resultaat.Add(int.Parse(splits[1]));
            return resultaat;
        }

        public override bool RaaktCirkel(Point locatie, int radius)
        {
            SizeF stralen = new SizeF(Math.Abs(begin.X - einde.X) / 2.0f, Math.Abs(begin.Y - einde.Y) / 2.0f);
            PointF midden = new PointF(Math.Min(begin.X, einde.X) + stralen.Width, Math.Min(begin.Y, einde.Y) + stralen.Height);
            PointF respafstand = new PointF(locatie.X - midden.X, locatie.Y - midden.Y);
            double hoek;

            // Bereken de hoek van de gegumde plek relatief tot het centrum van de ovaal.
            if (respafstand.Y == 0)
                hoek = Math.PI / 2;
            else
                hoek = Math.Atan2(respafstand.X, respafstand.Y);

            double afstand = Math.Sqrt(respafstand.X * respafstand.X + respafstand.Y * respafstand.Y);
            double sinHeight = stralen.Height * Math.Sin(hoek);
            double cosWidth = stralen.Width * Math.Cos(hoek);

            // Check of voor deze hoek de afstand tot het centrum van de ovaal is waar het uitgegumt moet worden.
            double straal = stralen.Height * stralen.Width / (Math.Sqrt(sinHeight * sinHeight + cosWidth * cosWidth));
            return (afstand - dikte / 2 - radius < straal && afstand + dikte/2 + radius > straal);
        }
    }
}