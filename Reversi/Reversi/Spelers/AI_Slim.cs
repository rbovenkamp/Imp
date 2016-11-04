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
        public const int denkTijdMiliSec = 500;

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
                    // Asynchroon zodat de UI kan updaten
                    await Task.Delay(denkTijdMiliSec);
                    if (spel.SpelerAanZet == this)
                    {
                        Dictionary<Point, int> nettoWinstPerZet = spel.MogelijkeZetten.ToDictionary(x => x.Key, x => x.Value.Count);
                        Dictionary<Point, int> verliesPerZet = new Dictionary<Point, int>();

                        foreach (Point suspect in nettoWinstPerZet.Keys)
                        {
                            // Maak een simulatie spel aan
                            Spel spelKopie = spel.MaakSimulatieVoor(this, new Mens(Color.Black, "Dummy"));

                            // Doe de zet die getest wordt in de simulatie
                            spelKopie.DoeZet(this, suspect);
                            if (spelKopie.MogelijkeZetten.Count == 0)
                            {
                                // Als de tegenstander na deze zet niks kan doen, doe deze zet
                                spel.DoeZet(this, suspect);
                                return;
                            }
                            else
                            {
                                // Bekijk hoe veel de tegenstander max kan veroveren na de testzet
                                verliesPerZet.Add(suspect, spelKopie.MogelijkeZetten.Values.Max(x => x.Count));
                            }
                        }

                        // Zelf gewonnen - max wat de tegenstander kan veroveren
                        Dictionary<Point, int> brutoWinstPerZet = nettoWinstPerZet.ToDictionary(
                                x => x.Key,
                                x => x.Value - verliesPerZet[x.Key]
                            );

                        // Kies de zet waarmee je het meest zal winnen
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
