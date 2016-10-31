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

        public Form1()
        {
            InitializeComponent();
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

            // Maak een nieuw menu en laat hem zien.
            huidigScherm = new Menu();
            huidigScherm.Dock = DockStyle.Fill;
            this.Controls.Add(huidigScherm);

            // Verander menubalk eigenschappen om verwarring te voorkomen.
            setToolStripEnabled(false);

            // Voeg de werking van de start knop toe.
            (huidigScherm as Menu).button3.Click += Button3_Click; ;
        }

        private void MaakSpel(int breedte, int hoogte, Speler speler1, Speler speler2)
        {
            huidigScherm.Dispose();

            huidigScherm = new Spel(breedte, hoogte, speler1, speler2);
            huidigScherm.Dock = DockStyle.Fill;
            this.Controls.Add(huidigScherm);

            setToolStripEnabled(true);

            (huidigScherm as Spel).onNieuweBeurt += Form1_onNieuweBeurt;
        }

        private void Form1_onNieuweBeurt(object sender, EventArgs e)
        {
            if (sender is Spel)
            {
                Spel spel = sender as Spel;
                // Spel.winnaar == null dan nog niemand gewonnen
                // Spel.SteentjesPerSpeler om huidige status op te vragen
            }
        }

        private void setToolStripEnabled(bool enabled)
        {
            resetSpelToolStripMenuItem.Enabled = enabled;
            spelToolStripMenuItem.Enabled = enabled;
            hintToolStripMenuItem.Enabled = enabled;
        }

        // Lijkt op de opdracht beginspel, maar hierin staan een aantal opties al vast.
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
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Menu menu = huidigScherm as Menu;
            Type test = Type.GetType(menu.TypeP1);
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
