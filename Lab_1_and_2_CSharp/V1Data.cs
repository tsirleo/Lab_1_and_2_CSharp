using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_and_2_CSharp
{
    [Serializable]
    abstract class V1Data: IEnumerable<DataItem>
    {
        public string ID { get; protected set; }
        public DateTime Date { get; protected set; }

        public V1Data(string id, DateTime date)
        {
            ID = id;
            Date = date;
        }

        public abstract int Count { get; }
        public abstract double AverageValue { get; }
        public abstract string ToLongString(string format);
        public override string ToString() => $"\tID: {ID},\tDate: {Date}";

        public abstract IEnumerator<DataItem> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
