using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reversi.Spelers
{
    public class AI_Random : Speler
    {
        public const int denkTijdMiliSec = 500;

        public AI_Random(Color spelerKleur, string spelerNaam) : base(spelerKleur, spelerNaam)
        {
        }

        public override void BindSpel(Spel spel)
        {
            spel.onNieuweBeurt += Spel_onNieuweBeurt;
        }

        private async void Spel_onNieuweBeurt(object sender, EventArgs e)
        {
            if (sender is Spel)
            {
                Spel spel = sender as Spel;

                if (spel.SpelerAanZet == this)
                {
                    // Asynchroon zodat de UI kan updaten
                    await Task.Delay(denkTijdMiliSec);

                    int aantalMogelijkeZetten = spel.MogelijkeZetten.Count;
                    if (aantalMogelijkeZetten != 0)
                    {
                        Point zet = spel.MogelijkeZetten.Keys
                        .ToList()
                        [new Random().Next(0, aantalMogelijkeZetten - 1)];

                        spel.DoeZet(this, zet);
                    }
                }
            }
        }
    }
}
