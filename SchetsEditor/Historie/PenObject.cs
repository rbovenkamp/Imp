﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public class PenObject : ISchetsObject
    {
        private Color kleur;
        private const string kleurSerialisatieNaam = "kleur";
        private int dikte;
        private const string dikteSerialisatieNaam = "dikte";
        public Stack<Point> Punten;
        private const string puntenSerialisatieNaam = "punten";

        public PenObject(Color kleur, int dikte, Stack<Point> gekleurdePunten)
        {
            this.kleur = kleur;
            this.dikte = dikte;
            this.Punten = gekleurdePunten;
        }

        public string Serialiseer()
        {
            string geserialiseerd = kleurSerialisatieNaam + ":" + ColorTranslator.ToHtml(kleur) + "\\";
            geserialiseerd += dikteSerialisatieNaam + ":" + dikte + "\\";

            IEnumerable<string> punten = Punten.Select(x => x.X + "," + x.Y);
            geserialiseerd += puntenSerialisatieNaam + ":" + String.Join(";", punten);

            return geserialiseerd;
        }

        public static ISchetsObject VanSerialisatie(string serialisatie)
        {
            Color kleur = Color.Black;
            int dikte = 0;
            Stack<Point> punten = new Stack<Point>();

            string[] instellingen = serialisatie.Split('\\');
            foreach (string instelling in instellingen)
            {
                string[] naamEnWaarde = instelling.Split(':');
                switch (naamEnWaarde[0])
                {
                    case kleurSerialisatieNaam:
                        kleur = instellingNaarKleur(naamEnWaarde[1]);
                        break;
                    case dikteSerialisatieNaam:
                        dikte = instellingNaarDikte(naamEnWaarde[1]);
                        break;
                    case puntenSerialisatieNaam:
                        punten = instellingNaarPunten(naamEnWaarde[1]);
                        break;
                }
            }
            
            return new PenObject(kleur, dikte, punten);
        }

        private static Color instellingNaarKleur(string waarde)
        {
            return ColorTranslator.FromHtml(waarde);
        }

        private static int instellingNaarDikte(string waarde)
        {
            return Int32.Parse(waarde);
        }

        private static Stack<Point> instellingNaarPunten(string waarde)
        {
            string[] puntStrings = waarde.Split(';');
            Stack<Point> punten = new Stack<Point>();

            foreach (string punt in puntStrings)
            {
                string[] xEnY = punt.Split(',');
                punten.Push(new Point(int.Parse(xEnY[0]), int.Parse(xEnY[1])));
            }

            return punten;
        }

        public void Teken(Graphics g)
        {
            Pen pen = new Pen(kleur, dikte);

            Point vorigePunt = Punten.First();
            foreach (Point punt in Punten)
            {
                g.DrawLine(pen, vorigePunt, punt);
                vorigePunt = punt;
            }
        }
    }
}
