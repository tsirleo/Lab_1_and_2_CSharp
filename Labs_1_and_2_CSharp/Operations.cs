using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_and_2_CSharp
{
    static class Operations
    {
        public static Complex MakeComplex(double x, double y)
        {
            return new Complex((x * y) / (x + y + 0.0001), (y - x));
        }
    }
}
