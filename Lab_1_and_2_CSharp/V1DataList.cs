using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_1_and_2_CSharp
{
    [Serializable]
    class V1DataList : V1Data
    {
        public List<DataItem> Measurments_data { get; private set; }

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

        public override string ToString() => "V1DataList: " + base.ToString() + $",\tnItems: {Count},\tAverageVal: {AverageValue}";

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

        public override IEnumerator<DataItem> GetEnumerator()
        {
            foreach (var dI in Measurments_data)
            {
                yield return dI;
            }
        }

        public bool SaveBinary(string filename)
        {
            FileStream fStrm = null;
            try
            {
                fStrm = File.Create(filename);
                BinaryFormatter binForm = new BinaryFormatter();
                binForm.Serialize(fStrm, this);
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine($"Error saving binary file: {x}");
                return false;
            }
            finally
            {
                if (fStrm !=null) { fStrm.Close(); }
            }
        }

        public static bool LoadBinary(string filename, ref V1DataList v1)
        {
            FileStream fStrm = null;
            try
            {
                fStrm = File.OpenRead(filename);
                BinaryFormatter binForm = new BinaryFormatter();
                v1 = binForm.Deserialize(fStrm) as V1DataList;
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine($"Error loading binary file: {x}");
                return false;
            }
            finally
            {
                if (fStrm != null) { fStrm.Close(); }
            }
        }
    }
}
