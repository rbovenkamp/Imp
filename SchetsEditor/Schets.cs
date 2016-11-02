using SchetsEditor.Historie;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SchetsEditor
{
    public class Schets
    {
        public bool Bewerkt = false;
        public SchetsHistorie Historie;
        private Size sz;

        public Bitmap Bitmap
        {
            get
            {
                Bitmap bmp = new Bitmap(sz.Width, sz.Height);
                Graphics g = Graphics.FromImage(bmp);
                this.Teken(g);
                return bmp;
            }
        }

        public Schets()
        {
            this.Historie = new SchetsHistorie();
            Initialiseer();
        }
        public Schets(Bitmap bmp)
        {
            this.Historie = new SchetsHistorie();
            this.Historie.Push(new PlaatjeObject(bmp));
            Initialiseer();
        }

        public void Initialiseer()
        {
            this.Historie.onObjectToegevoegd += Historie_onObjectToegevoegd;
        }

        private void Historie_onObjectToegevoegd(object sender, EventArgs e)
        {
            this.Bewerkt = true;
        }

        public void LaadHistorieUitString(string schetsBestand)
        {
            this.Historie = new SchetsHistorie(schetsBestand);
        }

        public void VeranderAfmeting(Size sz)
        {
            this.sz = sz;
            if (this.Historie.Count == 0)
            {
                this.Historie.Push(new PlaatjeObject(new Bitmap(sz.Width, sz.Height)));
            }
        }
        public void Teken(Graphics gr)
        {
            List<int> nietTekenen = new List<int>();
            foreach (ISchetsObject so in this.Historie)
            {
                if (so is GumObject)
                {
                    nietTekenen.Add((so as GumObject).Object);
                }
            }
            for (int n = Historie.Count - 1; n >= 0 ; n--)
            {
                if (!nietTekenen.Contains(n))
                    Historie[n].Teken(gr);
            }
        }
        public void Schoon()
        {
            Historie = new SchetsHistorie();
        }
        public void Roteer()
        {
            Bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }
    }
}
