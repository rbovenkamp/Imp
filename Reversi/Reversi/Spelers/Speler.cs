using System;
using System.Drawing;

namespace Reversi
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public class Speler
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
        public string Naam;
        public Color SpelerKleur;

        private Guid ID;

        public Speler(Color spelerKleur, string spelerNaam)
        {
            this.SpelerKleur = spelerKleur;
            this.Naam = spelerNaam;
            this.ID = Guid.NewGuid();
        }

        public virtual void BindSpel(Spel spel)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Speler speler1, Speler speler2)
        {
            return speler1?.ID == speler2?.ID;
        }

        public static bool operator !=(Speler speler1, Speler speler2)
        {
            return speler1?.ID != speler2?.ID;
        }
    }
}
