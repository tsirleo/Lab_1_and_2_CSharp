using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace Lab_1_and_2_CSharp
{
    class V1DataArray: V1Data
    {
        public int nX { get; private set; }
        public double xStep { get; private set; }
        public int nY { get; private set; }
        public double yStep { get; private set; }
        public Complex[,] Grid { get; private set; }

        public V1DataArray(string id, DateTime date) : base(id, date)
        {
            nX = 0;
            nY = 0;
            xStep = 0;
            yStep = 0;
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

        public Complex? FieldAt(int jx, int jy)
        {
            if (jx < nX && jy < nY)
            {
                return Grid[jy, jx];
            }
 
            return null;
        }

        public bool Max_Field_Re(int jy, ref double min, ref double max)
        {
            if (jy < nY)
            {
                min = Grid[jy, 0].Real;
                max = Grid[jy, 0].Real;
                for (int i = 1; i < nX; i++)
                {
                    double elem = Grid[jy, i].Real;
                    if (elem > max)
                    {
                        max = elem;
                    }
                    else if (elem < min)
                    {
                        min = elem;
                    }
                }

                return true;
            }

            return false;
        }

        public bool Max_Field_Im(int jy, ref double min, ref double max)
        {
            if (jy < nY)
            {
                min = Grid[jy, 0].Imaginary;
                max = Grid[jy, 0].Imaginary;
                for (int i = 1; i < nX; i++)
                {
                    double elem = Grid[jy, i].Imaginary;
                    if (elem > max)
                    {
                        max = elem;
                    }
                    else if (elem < min)
                    {
                        min = elem;
                    }
                }

                return true;
            }

            return false;
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

        public override IEnumerator<DataItem> GetEnumerator()
        {
            double y = 0.0;
            for (int i = 0; i < nY; i++)
            {
                double x = 0.0;
                for (int j = 0; j < nX; j++)
                {
                    yield return new DataItem(x, y, Grid[i, j]);
                    x += xStep;
                }
                y += yStep;
            }
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

        public bool SaveAsText(string filename)
        {
            FileStream fStrm = null;
            StreamWriter sWrt = null;
            try
            {
                fStrm = File.Create(filename);
                sWrt = new StreamWriter(fStrm);
                sWrt.WriteLine(ID);
                sWrt.WriteLine(Date.ToString());
                sWrt.WriteLine(nX.ToString());
                sWrt.WriteLine(nY.ToString());
                sWrt.WriteLine(xStep.ToString());
                sWrt.WriteLine(yStep.ToString());
                for (int i = 0; i < nY; i++)
                {
                    for (int j = 0; j < nX; j++)
                    {
                        sWrt.WriteLine(Grid[i, j].Real.ToString());
                        sWrt.WriteLine(Grid[i, j].Imaginary.ToString());
                    }
                }
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine($"Error saving text file: {x}");
                return false;
            }
            finally
            {
                if (sWrt != null) { sWrt.Dispose(); }
                if (fStrm != null) { fStrm.Close(); }
            }
        }

        public static bool LoadAsText(string filename, ref V1DataArray v1)
        {
            FileStream fStrm = null;
            StreamReader sRdr = null;
            try
            {
                fStrm = File.OpenRead(filename);
                sRdr = new StreamReader(fStrm);
                v1.ID = sRdr.ReadLine();
                v1.Date = DateTime.Parse(sRdr.ReadLine());
                v1.nX = Convert.ToInt32(sRdr.ReadLine());
                v1.nY = Convert.ToInt32(sRdr.ReadLine());
                v1.xStep = Convert.ToDouble(sRdr.ReadLine());
                v1.yStep = Convert.ToDouble(sRdr.ReadLine());
                v1.Grid = new Complex [v1.nY, v1.nX];
                for (int i = 0; i < v1.nY; i++)
                {
                    for (int j = 0; j < v1.nX; j++)
                    {
                        var real = Convert.ToDouble(sRdr.ReadLine());
                        var imaginary = Convert.ToDouble(sRdr.ReadLine());
                        v1.Grid[i,j] = new Complex(real, imaginary);
                    }
                }
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine($"Error loading text file: {x}");
                return false;
            }
            finally
            {
                if (sRdr != null) { sRdr.Dispose(); }
                if (fStrm != null) { fStrm.Close(); }
            }
        }
    }
}
