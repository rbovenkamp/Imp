using SchetsEditor.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public class VolRechthoekObject : TweePuntObject
    {
        private Color kleur;
        public VolRechthoekObject(Point begin, Point einde, Color kleur) : base(begin, einde, kleur)
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
            g.FillRectangle(kwast, TweepuntTool.Punten2Rechthoek(begin, einde));
        }

        public static ISchetsObject VanSerialisatie(string serialisatie)
        {
            List<object> inhoud = LaadPuntenUitSerialisatie(serialisatie);
            return new VolRechthoekObject((Point)inhoud[0], (Point)inhoud[1], (Color)inhoud[2]);
        }

        public override bool RaaktCirkel(Point locatie, int radius)
        {
            Rectangle objectje = TweepuntTool.Punten2Rechthoek(begin, einde);
            return (objectje.Right - radius < locatie.X && locatie.X < objectje.Right + radius && objectje.Bottom - radius < locatie.Y && locatie.Y < objectje.Top + radius);
        }


    }
}
