using System;

namespace Mandelbrot
{
    public class MandelbrotState
    {
        public int MaxIterations { get; private set; }
        public double Scale { get; private set; }
        public PointD Center { get; private set; }

        public MandelbrotState(int MaxIterations, double Scale, PointD Center)
        {
            this.MaxIterations = MaxIterations;
            this.Scale = Scale;
            this.Center = Center;
        }
    }
}