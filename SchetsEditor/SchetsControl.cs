using SchetsEditor.Historie;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SchetsEditor
{   public class SchetsControl : UserControl
    {
        public Color PenKleur { get; private set; } = Color.Black;
        public int PenDikte { get; private set; } = 3;
        public Schets Schets { get; private set; }
        public SchetsControl()
        {   
            this.Schets = new Schets();
            initialiseerControls();
        }
        public SchetsControl(Bitmap bmp)
        {
            this.Schets = new Schets(bmp);
            initialiseerControls();
        }
        
        public void LaadHistorie(string historie)
        {
            this.Schets.LaadHistorieUitString(historie);
            this.Invalidate();
        }

        private void initialiseerControls()
        {
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.DoubleBuffered = true;
            this.Paint += this.teken;
            this.Resize += this.veranderAfmeting;
            this.veranderAfmeting(null, null);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
        private void teken(object o, PaintEventArgs pea)
        {
            pea.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pea.Graphics.FillRectangle(Brushes.White, 0, 0, ClientSize.Width, ClientSize.Height);
            Schets.Teken(pea.Graphics);
        }
        private void veranderAfmeting(object o, EventArgs ea)
        {
            Schets.VeranderAfmeting(this.ClientSize);
            this.Invalidate();
        }
        public Graphics MaakBitmapGraphics()
        {
        //    Graphics g = schets.BitmapGraphics;
        //    g.SmoothingMode = SmoothingMode.AntiAlias;
            return null;
        }
        public void Schoon(object o, EventArgs ea)
        {
            Schets.Schoon();
            this.Invalidate();
        }
        public void Roteer(object o, EventArgs ea)
        {
            Schets.VeranderAfmeting(new Size(this.ClientSize.Height, this.ClientSize.Width));
            Schets.Roteer();
            this.Invalidate();
        }
        public void VeranderKleur(object obj, EventArgs ea)
        {
            ColorDialog d = new ColorDialog();
            d.Color = (obj as Button).BackColor;
            if (d.ShowDialog() == DialogResult.OK)
            {
                this.PenKleur = d.Color;
                (obj as Button).BackColor = d.Color;
            }
        }

        public void VeranderPenDikte(int dikte)
        {
            this.PenDikte = dikte;
        }
    }
}
