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
            Point stralen = new Point(Math.Abs(begin.X - einde.X) / 2, Math.Abs(begin.Y - einde.Y) / 2);
            Point midden = new Point(Math.Min(begin.X, einde.X) - stralen.X, Math.Min(begin.Y, einde.Y) - stralen.Y);
            Point respafstand = new Point(locatie.X - midden.X, locatie.Y - midden.Y);
            double hoek = Math.Atan(respafstand.Y / respafstand.X);

            return 
                (
                    (Math.Sqrt(respafstand.X * respafstand.X + respafstand.Y * respafstand.Y) * 2 < stralen.X * Math.Cos(hoek) + stralen.Y + Math.Sin(hoek) + (double)dikte/2 + radius) && 
                    (Math.Sqrt(respafstand.X * respafstand.X + respafstand.Y * respafstand.Y) * 2 > stralen.X * Math.Cos(hoek) + stralen.Y + Math.Sin(hoek) - (double)dikte/2 - radius)
                );

        }
        
    }

}