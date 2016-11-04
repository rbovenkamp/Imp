using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public interface ISchetsObject
    {
        void Teken(Graphics g);
        bool RaaktCirkel(Point locatie, int radius);
        string Serialiseer();
    }
}
