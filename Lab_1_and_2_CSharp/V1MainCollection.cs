using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Lab_1_and_2_CSharp
{
    class V1MainCollection
    {
        private List<V1Data> dLst;

        public V1MainCollection()
        {
            dLst = new List<V1Data>();
        }

        public DateTime? minTimeValue
        {
            get
            {
                if (dLst.Count() == 0)
                {
                    return null;
                }

                return dLst.Min(vD => vD.Date);
            }
        }

        public IEnumerable<V1Data> DecAverVal
        {
            get
            {
                if (dLst.Count() == 0)
                {
                    return null;
                }

                return dLst.Where(vD => vD is V1DataList).OrderByDescending(vD => vD.AverageValue);
            }
        }

        public IEnumerable<V1Data> maxMeasRes
        {
            get
            {
                if (dLst.Count() == 0)
                {
                    return null;
                }

                int maximum = dLst.Max(vD => vD.Count);
                return dLst.Where(vD => vD.Count == maximum);
            }
        }

        public int Count
        {
            get
            {
                return dLst.Count;
            }
        }

        public V1Data this[int index]
        {
            get
            {
                return dLst[index];
            }
        }

        public bool Contains(string id)
        {
            foreach (var vD in dLst)
            {
                if (vD.ID == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Add(V1Data v1Data)
        {
            foreach (var vD in dLst)
            {
                if (vD.ID == v1Data.ID)
                {
                    return false;
                }
            }
            dLst.Add(v1Data);
            return true;
        }

        public override string ToString()
        {
            string output = "V1MainCollection:\n";
            int i = 1;
            foreach (var elem in dLst)
            {
                output += $"\tItem_{i}: " + elem.ToString() + "\n";
                i += 1;
            }
            return output;
        }

        public string ToLongString(string format)
        {
            string output = "V1MainCollection:\n";
            int i = 1;
            foreach (var elem in dLst)
            {
                output += $"\tItem_{i}: " + elem.ToLongString(format) + "\n";
                i += 1;
            }
            return output;
        }
    }
}
