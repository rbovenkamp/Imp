using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot.Class
{
    class Class1
    {
        public int _a;
        public int a
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value * 2;
            }
        }

        public int b { get; set; }
    }
}
