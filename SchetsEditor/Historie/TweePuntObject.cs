using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public abstract class TweePuntObject : ISchetsObject
    {
        private Point begin;
        private Point einde;
        public TweePuntObject(Point begin, Point einde)
        {
            this.begin = begin;
            this.einde = einde;
        }

        public abstract string Naam();
        public abstract void Teken(Graphics g);

        public string Serialiseer()
        {
            return begin.X + "," + begin.Y + ";" + einde.X + "," + einde.Y;
        }

        public void LaadPuntenUitSerialisatie(string serialisatie)
        {
            string[] punten = serialisatie.Split(';');
            string[] begin = punten[0].Split(',');
            string[] einde = punten[1].Split(',');

            this.begin = stringsNaarPoint(begin);
            this.einde = stringsNaarPoint(einde);
        }

        public abstract ISchetsObject VanSerialisatie(string serialisatie);

        private Point stringsNaarPoint(string[] strings)
        {
            int x = int.Parse(strings[0]);
            int y = int.Parse(strings[1]);
            return new Point(x, y);
        }

        public bool RaaktCirkel(Point locatie, int radius)
        {
            throw new NotImplementedException();
        }
    }
}
