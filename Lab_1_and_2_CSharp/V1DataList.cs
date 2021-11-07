using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_and_2_CSharp
{
    class V1DataList: V1Data
    {
        public List<DataItem> Measurments_data { get; set; }

        public V1DataList(string id, DateTime date) : base(id, date)
        {
            Measurments_data = new List<DataItem>();
        }

        public bool Add(DataItem newItem)
        {
            foreach (var DI in Measurments_data)
            {
                if (newItem.X.Equals(DI.X) && newItem.Y.Equals(DI.Y))
                {
                    return false;
                }
            }
            Measurments_data.Add(newItem);
            return true;
        }

        public int AddDefaults(int nItems, FdblComplex func)
        {
            int nAdd = 0;
            Random rand = new Random();
            for (int i = 0; i < nItems; i++)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();
                DataItem dataitem = new DataItem(x, y, func(x, y));
                if (Add(dataitem))
                {
                    nAdd += 1;
                }
            }
            return nAdd;
        }

        public override int Count
        {
            get
            {
                return Measurments_data.Count;
            }
        }

        public override double AverageValue
        {
            get
            {
                double sum = 0;
                foreach (var DI in Measurments_data)
                {
                    sum += DI.Field.Magnitude;
                }
                return sum / Count;
            }
        }

        public override string ToString() => "V1DataList: " + base.ToString() + $",\tnItems: {Count}";

        public override string ToLongString(string format)
        {
            int i = 1;
            string output = "";
            output += ToString();
            foreach (var DI in Measurments_data)
            {
                output += $"\n\t\tElement_{i}:\n\t\t\tX: {string.Format(format, DI.X)}\n\t\t\tY: {string.Format(format, DI.Y)}\n\t\t\tField_complex: {string.Format(format, DI.Field)}\n\t\t\tField_module: {string.Format(format, DI.Field.Magnitude)}";
                i += 1;
            }
            return output;
        }
    }
}
