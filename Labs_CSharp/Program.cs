using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Lab_1_and_2_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //testFirstLab();
            //testIEnumerable();
            //testFileRdWr();
            //testLINQ();
            //testNewFunc();
            //testMKLInterpolation();
        }

        static void testFirstLab()
        {
            FdblComplex func;
            func = Operations.MakeComplex;

            V1DataArray dA = new V1DataArray("information0", DateTime.Now, 3, 3, 0.066, 0.15, func);
            Console.WriteLine(dA.ToLongString("{0:f4}"));
            Console.WriteLine('\n');

            V1DataList dL = dA;
            Console.WriteLine(dL.ToLongString("{0:f5}"));
            Console.WriteLine('\n');

            Console.WriteLine("V1DataArray:\n\tCount: {0}\n\tAverageValue: {1}\n", dA.Count, dA.AverageValue);
            Console.WriteLine("V1DataList:\n\tCount: {0}\n\tAverageValue: {1}\n", dL.Count, dL.AverageValue);
            Console.WriteLine('\n');

            V1MainCollection mnCol = new V1MainCollection();
            mnCol.Add(new V1DataArray("information1", DateTime.Now, 2, 2, 0.3, 0.5, func));
            mnCol.Add(new V1DataArray("information2", DateTime.Now, 1, 3, 0.6, 0.15, func));
            mnCol.Add(new V1DataList("information3", DateTime.Now));
            mnCol.Add(dL);
            Console.WriteLine(mnCol.ToLongString("{0:f5}"));
            Console.WriteLine('\n');

            Console.WriteLine("V1MainCollection:");
            for (int i = 0; i < mnCol.Count; i++)
            {
                Console.WriteLine(
                    $"\tItem_{i + 1}:\n\t\tCount: {mnCol[i].Count}\n\t\tAverageValue: {mnCol[i].AverageValue}\n");
            }
        }

        static void testIEnumerable()
        {
            FdblComplex func;
            func = Operations.MakeComplex;
            V1DataArray dA = new V1DataArray("information0", DateTime.Now, 3, 3, 0.066, 0.15, func);
            Console.WriteLine(dA.ToLongString("{0:f3}"));
            Console.WriteLine("***********************************************************");
            foreach (DataItem item in dA)
            {
                Console.WriteLine(item.ToLongString("{0:f3}"));
            }

            Console.WriteLine($"\nV1DataArray is IEnumerable<DataItem> {dA.GetType()}");

            Console.WriteLine("***********************************************************");
            V1DataList dL = dA;
            Console.WriteLine(dL.ToLongString("{0:f3}"));
            Console.WriteLine("***********************************************************");
            foreach (DataItem item in dL)
            {
                Console.WriteLine(item.ToLongString("{0:f3}"));
            }

            Console.WriteLine($"\nV1DataList is IEnumerable<DataItem> {dL.GetType()}");
        }

        static void testLINQ()
        {
            FdblComplex func;
            func = Operations.MakeComplex;

            V1DataArray dA1 = new V1DataArray("information1", DateTime.Now, 2, 4, 0.066, 0.15, func);
            V1DataArray dA2 = new V1DataArray("information2", DateTime.Now, 2, 1, 0.11, 0.08, func);
            V1DataArray dA3 = new V1DataArray("information3", DateTime.Now, 4, 2, 0.234, 1.5, func);

            V1DataList dL1 = dA1;
            V1DataList dL2 = dA2;
            V1DataList dL3 = dA3;

            V1MainCollection mnCol = new V1MainCollection();
            mnCol.Add(dL1);
            mnCol.Add(new V1DataArray("information4", DateTime.Now, 2, 2, 0.3, 0.5, func));
            mnCol.Add(new V1DataList("empty list", DateTime.Now));
            mnCol.Add(new V1DataArray("empty array", DateTime.Now));
            mnCol.Add(new V1DataArray("information5", DateTime.Now, 1, 3, 0.6, 0.15, func));
            mnCol.Add(dL2);
            mnCol.Add(dL3);

            Console.WriteLine(mnCol.ToLongString("{0:f3}"));
            Console.WriteLine("******************************************************************************************");

            Console.WriteLine("LINQ_1: minimum value of measurement time\n");
            Console.WriteLine(mnCol.minTimeValue);
            Console.WriteLine("******************************************************************************************");

            Console.WriteLine("LINQ_2: elements of the collection of type V1DataList in descending order of the average value of the field module\n");
            if (mnCol.DecAverVal != null)
            {
                foreach (var list in mnCol.DecAverVal)
                {
                    Console.WriteLine(list);
                }
            }
            else
            {
                Console.WriteLine("V1MainCollection is empty");
            }
            Console.WriteLine("******************************************************************************************");

            Console.WriteLine("LINQ_3: collection elements with the maximum number of field measurement results\n");
            if (mnCol.maxMeasRes != null)
            {
                foreach (var list in mnCol.maxMeasRes)
                {
                    Console.WriteLine(list);
                }
            }
            else
            {
                Console.WriteLine("V1MainCollection is empty");
            }
            Console.WriteLine("******************************************************************************************");
        }

        static void testFileRdWr()
        {
            FdblComplex func;
            func = Operations.MakeComplex;
            V1DataArray dATest = new V1DataArray("test", DateTime.Now, 2, 1, 0.11, 0.08, func);
            V1DataList dLTest = dATest;

            V1DataArray dA = new V1DataArray("information3", DateTime.Now, 3, 1, 2.234, 1.5, func);
            dA.SaveAsText("Save_text_result.txt");
            V1DataArray.LoadAsText("Save_text_result.txt", ref dATest);

            V1DataList dL= dA;
            dL.SaveBinary("Save_binary_result");
            V1DataList.LoadBinary("Save_binary_result", ref dLTest);

            Console.WriteLine("Source array:");
            Console.WriteLine(dA.ToLongString("{0:f4}"));
            Console.WriteLine('\n');
            Console.WriteLine("Array after loading from file:");
            Console.WriteLine(dATest.ToLongString("{0:f4}"));
            Console.WriteLine('\n');
            Console.WriteLine("**********************************************************");
            Console.WriteLine("Source list:");
            Console.WriteLine(dL.ToLongString("{0:f4}"));
            Console.WriteLine('\n');
            Console.WriteLine("List after loading from file:");
            Console.WriteLine(dLTest.ToLongString("{0:f4}"));
            Console.WriteLine('\n');
        }

        static void testNewFunc()
        {
            FdblComplex func;
            double min=0, max=0;
            func = Operations.MakeComplex;
            V1DataArray dATest = new V1DataArray("test", DateTime.Now, 4, 2, 0.11, 0.08, func);
            Console.WriteLine(dATest.ToLongString("{0:f4}"));
            Console.WriteLine("\n**********************************************************\n");
            Console.WriteLine("field at jx = 2, jy = 1");
            Console.WriteLine(dATest.FieldAt(2, 1));
            Console.WriteLine('\n');

            Console.WriteLine("max and min real  jy = 1");
            dATest.Max_Field_Re(1, ref min, ref max);
            Console.WriteLine("minimum = " + min + "    " + "maximum = " + max);
            Console.WriteLine('\n');
            Console.WriteLine("max and min imaginary   jy = 1");
            dATest.Max_Field_Im(1, ref min, ref max);
            Console.WriteLine("minimum = " + min + "    " + "maximum = " + max);
            Console.WriteLine('\n');
        }

        static void testMKLInterpolation()
        {
            FdblComplex func;
            double min = 0, max = 0;
            func = Operations.MakeComplex;
            V1DataArray dA = new V1DataArray("test", DateTime.Now, 3, 2, 0.11, 0.08, func);
            V1DataArray scaledDA = dA.ToSmallerGrid(6);
            Console.WriteLine(dA.ToLongString("{0:f4}"));
            Console.WriteLine("\n**********************************************************\n");
            Console.WriteLine("DataArray with scaled grid: ");
            Console.WriteLine(scaledDA.ToLongString("{0:f4}"));
            Console.WriteLine("\n**********************************************************\n");
            Console.WriteLine("Original array: max and min real  jy = 1");
            dA.Max_Field_Re(1, ref min, ref max);
            Console.WriteLine("minimum = " + min + "    " + "maximum = " + max);
            Console.WriteLine("Original array: max and min imaginary   jy = 1");
            dA.Max_Field_Im(1, ref min, ref max);
            Console.WriteLine("minimum = " + min + "    " + "maximum = " + max);
            Console.WriteLine("\n**********************************************************\n");
            Console.WriteLine("Scaled array: max and min real  jy = 1");
            scaledDA.Max_Field_Re(1, ref min, ref max);
            Console.WriteLine("minimum = " + min + "    " + "maximum = " + max);
            Console.WriteLine("Scaled array: max and min imaginary   jy = 1");
            scaledDA.Max_Field_Im(1, ref min, ref max);
            Console.WriteLine("minimum = " + min + "    " + "maximum = " + max);
            Console.WriteLine("\n");
        }
    }
}
