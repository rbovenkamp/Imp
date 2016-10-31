using System.Drawing;
using System.Windows.Forms;

namespace Reversi.Spelers
{
    public class Mens : Speler
    {
        public Mens(Color spelerKleur, string spelerNaam) : base(spelerKleur, spelerNaam)
        {
        }

        public override void BindSpel(Spel spel)
        {
            spel.MouseClick += Spel_MouseClick;
        }

        private void Spel_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is Spel)
            {
                Spel spel = sender as Spel;

                if (spel.SpelerAanZet == this)
                {
                    int geselecteerdeBreedte = e.X / (spel.Width / spel.VakjesBreedte);
                    int geselecteerdeHoogte = e.Y / (spel.Height / spel.VakjesHoogte);
                    spel.DoeZet(this, new Point(geselecteerdeBreedte, geselecteerdeHoogte));
                }
            }
        }
    }
}
