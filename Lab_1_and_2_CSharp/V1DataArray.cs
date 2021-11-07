using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_and_2_CSharp
{
    class V1DataArray: V1Data
    {
        public int nX { get; set; }
        public double xStep { get; set; }
        public int nY { get; set; }
        public double yStep { get; set; }
        public Complex[,] Grid { get; set; }

        public V1DataArray(string id, DateTime date) : base(id, date)
        {
            nX = 0;
            nY = 0;
            Grid = new Complex[0, 0];
        }

        public V1DataArray(string id, DateTime date, int nx, int ny, double xstep, double ystep, FdblComplex func) : base(id, date)
        {
            nX = nx;
            nY = ny;
            xStep = xstep;
            yStep = ystep;
            double y = 0.0;
            Grid = new Complex[nY, nX];
            for (int i = 0; i < nY; i++)
            {
                double x = 0.0;
                for (int j = 0; j < nX; j++)
                {
                    Grid[i, j] = func(x, y);
                    x += xStep;
                }
                y += yStep;
            }
        }

        public override int Count
        {
            get
            {
                return nX * nY;
            }
        }

        public override double AverageValue
        {
            get
            {
                double sum = 0;
                foreach (var GrEl in Grid)
                {
                    sum += GrEl.Magnitude;
                }
                return sum / Count;
            }
        }

        public override string ToString() => "V1DataArray: " + base.ToString() + $"\tGrid: {nY}x{nX}\tStep_Ox: {xStep}\tStep_Oy: {yStep}";

        public override string ToLongString(string format)
        {
            double y = 0.0;
            string output = "";
            output += ToString();
            for (int i = 0; i < nY; i++)
            {
                double x = 0.0;
                for (int j = 0; j < nX; j++)
                {
                    output += $"\n\t\tElement_[{i},{j}]:\n\t\t\tX: {x}\n\t\t\tY: {y}\n\t\t\tElem_complex: {string.Format(format, Grid[i, j])}\n\t\t\tElem_module: {string.Format(format, Grid[i, j].Magnitude)}";
                    x += xStep;
                }
                y += yStep;
            }
            return output;
        }

        public static implicit operator V1DataList(V1DataArray dA)
        {
            V1DataList dL = new V1DataList(dA.ID, dA.Date);
            double y = 0.0;
            for (int i = 0; i < dA.nY; i++)
            {
                double x = 0.0;
                for (int j = 0; j < dA.nX; j++)
                {
                    dL.Add(new DataItem(x, y, dA.Grid[i, j]));
                    x += dA.xStep;
                }
                y += dA.yStep;
            }
            return dL;
        }
    }
}
