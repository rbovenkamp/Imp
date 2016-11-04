using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public class GumObject : ISchetsObject
    {
        public int Object;

        public GumObject(int Object)
        {
            this.Object = Object;
        }

        public bool RaaktCirkel(Point locatie, int radius)
        {
            return false;
        }

        public string Serialiseer()
        {
            return Object.ToString();
        }

        public void Teken(Graphics g)
        { }

        public static ISchetsObject VanSerialisatie(string serialisatie)
        {
            int Object = int.Parse(serialisatie);
            return new GumObject(Object);
        }
    }
}
