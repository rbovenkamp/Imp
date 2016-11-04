using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reversi.Spelers
{
    public class AI_MeestVeroverd : Speler
    {
        public const int denkTijdMiliSec = 500;

        public AI_MeestVeroverd(Color spelerKleur, string spelerNaam) : base(spelerKleur, spelerNaam)
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
                    if (spel.SpelerAanZet == this)
                    {
                        // Zoek het hoogst aantal veroverde punten uit mogelijke zetten
                        Point zet = spel.MogelijkeZetten
                            .FirstOrDefault(x =>
                                x.Value.Count ==
                                    spel.MogelijkeZetten.Max(y => y.Value.Count)
                            ).Key;

                        spel.DoeZet(this, zet);
                    }
                }
            }
        }
    }
}
