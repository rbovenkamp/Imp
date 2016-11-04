using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public abstract class TweePuntObject : ISchetsObject
    {
        public Point begin;
        public Point einde;
        private Color kleur;
        public TweePuntObject(Point begin, Point einde, Color kleur)
        {
            this.begin = begin;
            this.einde = einde;
            this.kleur = kleur;
        }

        public abstract string Naam();
        public abstract void Teken(Graphics g);

        public virtual string Serialiseer()
        {
            return begin.X + "," + begin.Y + ";" + einde.X + "," + einde.Y + "/" + ColorTranslator.ToHtml(kleur);
        }

        public static List<Object> LaadPuntenUitSerialisatie(string serialisatie)
        {
            string[] eigenschappen = serialisatie.Split('/');
            string[] punten = eigenschappen[0].Split(';');
            string[] begin = punten[0].Split(',');
            string[] einde = punten[1].Split(',');

            Point rbegin = stringsNaarPoint(begin);
            Point reinde = stringsNaarPoint(einde);
            Color rkleur = ColorTranslator.FromHtml(eigenschappen[1]);
            return new List<object> { rbegin, reinde, rkleur };
        }
        

        private static Point stringsNaarPoint(string[] strings)
        {
            int x = int.Parse(strings[0]);
            int y = int.Parse(strings[1]);
            return new Point(x, y);
        }

        public abstract bool RaaktCirkel(Point locatie, int radius);
    }
}
