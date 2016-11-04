using Reversi.Spelers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public partial class Form1 : Form
    {
        public Control huidigScherm;
        public Speler speler1, speler2;
        public int[,] spelverloop;

        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "BGTexture.png");
            this.BackgroundImageLayout = ImageLayout.Tile;
            panel1.BackColor = Color.Transparent;
            MaakMenu();
        }

        private void terugNaarMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaakMenu();
        }

        public void MaakMenu()
        {
            // Laad het spel en verwijder het scherm als er al iets staat (ivm de 1e keer maken).
            if (huidigScherm != null)
                huidigScherm.Dispose();

            huidigScherm = new Menu();
            huidigScherm.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(huidigScherm);

            setToolStripEnabled(false);
            // u2611 is unicode voor het een gevinkt vierkantje (u2610 is ongevinkt)
            hintToolStripMenuItem.Text = "Hints \u2611";

            (huidigScherm as Menu).button3.Click += Button3_Click;
            huidigScherm.BackColor = Color.Transparent;
        }

        private void MaakSpel(int breedte, int hoogte, Speler speler1, Speler speler2)
        {
            huidigScherm.Dispose();

            huidigScherm = new Spel(breedte, hoogte, speler1, speler2);
            huidigScherm.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(huidigScherm);

            setToolStripEnabled(true);
            spelOverzichtVernieuwen((Spel)huidigScherm);
            spelverloop = new int[breedte * hoogte, 2];

            (huidigScherm as Spel).onNieuweBeurt += Form1_onNieuweBeurt;
            (huidigScherm as Spel).onSpelerGewonnen += Form1_onSpelerGewonnen;
            huidigScherm.BackColor = Color.Transparent;
        }

        private void Form1_onSpelerGewonnen(object sender, EventArgs e)
        {
            if (sender is Speler)
            {
                Speler speler = sender as Speler;
                MessageBox.Show(speler.Naam.ToString() + " heeft gewonnen!");
            }
        }

        private void Form1_onNieuweBeurt(object sender, EventArgs e)
        {
            if (sender is Spel)
            {
                Spel spel = sender as Spel;
                spelOverzichtVernieuwen(spel);
            }
        }

        private void spelOverzichtVernieuwen(Spel spel)
        {

            foreach (KeyValuePair<Speler, int> speler in spel.SteentjesPerSpeler)
            {
                ToolStripMenuItem stand = new ToolStripMenuItem();
                stand.Tag = speler.Key.Naam;
                stand.Text = speler.Key.Naam + " : " + speler.Value.ToString();
                stand.Size = new Size(195, 26);
                if (speler.Key != spel.SpelerAanZet)
                    stand.Enabled = false;

                menuStrip1.Items.Add(stand);
            }

            // Verwijder de weergave van het overzicht van de vorige beurt.
            while (menuStrip1.Items.Count > spel.Spelers.Count + 2)
                menuStrip1.Items.RemoveAt(2);

        }


        private void setToolStripEnabled(bool enabled)
        {
            resetSpelToolStripMenuItem.Enabled = enabled;
            hintToolStripMenuItem.Enabled = enabled;
        }


        private void snelNieuwSpelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speler1 = new Mens(Color.Blue, "Speler 1");
            speler2 = new Mens(Color.Red, "Speler 2");
            MaakSpel(6, 6, speler1, speler2);
        }

        private void hintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (huidigScherm is Spel)
            {
                Spel spel = huidigScherm as Spel;
                spel.HintsAanUit = !spel.HintsAanUit;
                spel.Invalidate();
                if (spel.HintsAanUit == true)
                    hintToolStripMenuItem.Text = "Hints \u2611";
                else
                    hintToolStripMenuItem.Text = "Hints \u2610";
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Menu menu = huidigScherm as Menu;
            Type test = Type.GetType(menu.TypeP1);
            // Verkrijg het type van de string die gekozen is en maak hem dynamisch aan
            speler1 = (Speler)Activator.CreateInstance(Type.GetType(menu.TypeP1), menu.KleurP1, menu.NameP1);
            speler2 = (Speler)Activator.CreateInstance(Type.GetType(menu.TypeP2), menu.KleurP2, menu.NameP2);

            MaakSpel(menu.bordBreedte, menu.bordHoogte, speler1, speler2);
        }

        public void resetSpel(object sender, EventArgs e)
        {
            Spel oudspel = (Spel)huidigScherm;
            MaakSpel(oudspel.VakjesBreedte, oudspel.VakjesHoogte, oudspel.Spelers[0], oudspel.Spelers[1]);
        }
    }
}
