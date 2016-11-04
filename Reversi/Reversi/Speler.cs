using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public class Speler
    {
        public Color SpelerKleur;

        public Speler(Color spelerKleur)
        {
            this.SpelerKleur = spelerKleur;
        }


        public Point? GekozenPunt;
        public void OnClick(Spel spel, MouseEventArgs e)
        {
            int geselecteerdeBreedte = e.X / (spel.Width / spel.VakjesBreedte);
            int geselecteerdeHoogte = e.Y / (spel.Height / spel.VakjesHoogte);
            GekozenPunt = new Point(geselecteerdeBreedte, geselecteerdeHoogte);
        }
    }
}
