using System;

namespace Lab_1_and_2_CSharp
{
    class Program
    {
        static void FirstLab()
        {
            FdblComplex func;
            func = Operations.MakeComplex;

            V1DataArray dA = new V1DataArray("information0", DateTime.Now, 14, 9, 0.066, 0.15, func);
            Console.WriteLine(dA.ToLongString("{0:f4}"));
            Console.WriteLine('\n');

            V1DataList dL = dA;
            Console.WriteLine(dL.ToLongString("{0:f5}"));
            Console.WriteLine('\n');

            Console.WriteLine("V1DataArray:\n\tCount: {0}\n\tAverageValue: {1}\n", dA.Count, dA.AverageValue);
            Console.WriteLine("V1DataList:\n\tCount: {0}\n\tAverageValue: {1}\n", dL.Count, dL.AverageValue);
            Console.WriteLine('\n');

            V1MainCollection mnCol = new V1MainCollection();
            mnCol.Add(new V1DataArray("information1", DateTime.Now, 4, 2, 0.3, 0.5, func));
            mnCol.Add(new V1DataArray("information2", DateTime.Now, 3, 5, 0.6, 0.15, func));
            mnCol.Add(new V1DataList("information3", DateTime.Now));
            mnCol.Add(dL);
            Console.WriteLine(mnCol.ToLongString("{0:f5}"));
            Console.WriteLine('\n');

            Console.WriteLine("V1MainCollection:");
            for (int i = 0; i < mnCol.Count; i++)
            {
                Console.WriteLine($"\tItem_{i + 1}:\n\t\tCount: {mnCol[i].Count}\n\t\tAverageValue: {mnCol[i].AverageValue}\n");
            }
        }
        static void Main(string[] args)
        {
            FirstLab();
        }
    }
}
