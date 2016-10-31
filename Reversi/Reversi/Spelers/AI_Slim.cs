using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reversi.Spelers
{
    public class AI_Slim : Speler
    {
        public const int denkTijdMiliSec = 50;

        public AI_Slim(Color spelerKleur, string spelerNaam) : base(spelerKleur, spelerNaam)
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
                    await Task.Delay(denkTijdMiliSec);
                    if (spel.SpelerAanZet == this)
                    {
                        Dictionary<Point, int> nettoWinstPerZet = spel.MogelijkeZetten.ToDictionary(x => x.Key, x => x.Value.Count);
                        Dictionary<Point, int> verliesPerZet = new Dictionary<Point, int>();

                        foreach (Point suspect in nettoWinstPerZet.Keys)
                        {
                            Spel spelKopie = spel.MaakSimulatieVoor(this, new Mens(Color.Black, "Dummy"));

                            spelKopie.DoeZet(this, suspect);
                            if (spelKopie.MogelijkeZetten.Count == 0)
                            {
                                spel.DoeZet(this, suspect);
                                return;
                            }
                            else
                            {
                                verliesPerZet.Add(suspect, spelKopie.MogelijkeZetten.Values.Max(x => x.Count));
                            }
                        }

                        Dictionary<Point, int> brutoWinstPerZet = nettoWinstPerZet.ToDictionary(
                                x => x.Key,
                                x => x.Value - verliesPerZet[x.Key]
                            );

                        Point gekozenZet = brutoWinstPerZet
                            .FirstOrDefault(x => x.Value == brutoWinstPerZet.Max(y => y.Value))
                            .Key;

                        spel.DoeZet(this, gekozenZet);
                    }
                }
            }
        }
    }
}
