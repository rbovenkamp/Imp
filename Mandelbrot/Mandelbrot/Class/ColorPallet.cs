using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot.Class
{
    public class ColorPallet
    {
        public enum ColorScheme { SmoothGreen };

        public ColorScheme Type { get; private set; }
        private static Color[] colorPallet = null;

        public ColorPallet(ColorScheme scheme)
        {
            this.Type = scheme;

            switch (scheme)
            {
                case ColorScheme.SmoothGreen:
                    colorPallet = createSmoothGreenPallet();
                    break;
            }
        }

        public Color GetColor(uint iterations, double c_result)
        {
            switch (this.Type)
            {
                case ColorScheme.SmoothGreen:
                    double mu = iterations + 1 - Math.Log(Math.Log(c_result)) / Math.Log(2);
                    int color1 = Math.Abs((int)mu % colorPallet.Count());
                    return colorPallet[color1];
                default:
                    throw new NotImplementedException();
            }
        }

        private Color[] createSmoothGreenPallet()
        {
            Color[] _colorPallet = new Color[512];
            for (int i = 0; i < 512; i++)
            {
                if (i < 256)
                {
                    _colorPallet[i] = Color.FromArgb(0, i, 0);
                }
                else
                {
                    _colorPallet[i] = Color.FromArgb(0, 0, i - 256);
                }
            }
            return _colorPallet;
        }
    }
}
