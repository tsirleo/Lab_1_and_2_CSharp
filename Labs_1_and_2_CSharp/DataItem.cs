using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_and_2_CSharp
{
    [Serializable]
    struct DataItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Complex Field { get; set; }

        public DataItem(double x, double y, Complex field)
        {
            X = x;
            Y = y;
            Field = field;
        }

        public string ToLongString(string format) =>
            $"DataItem: X: {string.Format(format, X)}, Y: {string.Format(format, Y)}, Field:  {string.Format(format, Field)}";

        public override string ToString() => $"X: {X}, Y: {Y}, Field: {Field}";
    }

    delegate Complex FdblComplex(double x, double y);
}
