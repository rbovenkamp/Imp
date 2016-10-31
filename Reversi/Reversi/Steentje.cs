using System.Drawing;
using System.Drawing.Drawing2D;

namespace Reversi
{
    public class Steentje
    {
        public Speler Eigenaar;

        public Steentje(Speler eigenaar)
        {
            this.Eigenaar = eigenaar;
        }

        public void TekenNaarGraphics(Graphics g, Rectangle cirkel)
        {
            Brush kwast = new SolidBrush(
                                this.Eigenaar.SpelerKleur
                            );

            g.FillEllipse(kwast, cirkel);

            GraphicsPath cirkelpad = new GraphicsPath();
            cirkelpad.AddEllipse(cirkel);

            PathGradientBrush gradientKwast = new PathGradientBrush(
                    cirkelpad
                );
            gradientKwast.CenterPoint = new PointF(cirkel.Size.Width * 0.4f + cirkel.Location.X, cirkel.Size.Height * 0.4f + cirkel.Location.Y);
            gradientKwast.CenterColor = Color.FromArgb(150, 255, 255, 255);
            gradientKwast.SurroundColors = new Color[] { Color.FromArgb(100, 0, 0, 0) };

            Pen gradientPen = new Pen(gradientKwast);
            gradientPen.Width = 8;
            Rectangle binnenCirkel = cirkel;
            binnenCirkel.Inflate(-8, -8);
            g.DrawEllipse(gradientPen, binnenCirkel);

            g.FillEllipse(gradientKwast, cirkel);
        }
    }
}
