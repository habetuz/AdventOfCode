namespace AdventOfCode.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class PriorityQueue<T>
        where T : IComparable<T>
    {
        private List<T> data = new List<T>();

        internal void Enqueue(T item)
        {
            for (int i = 0; i < this.data.Count; i++)
            {
                if (this.data[i].CompareTo(item) < 0)
                {
                    this.data.Insert(i, item);
                    return;
                }
            }

            this.data.Add(item);
        }

        internal T Dequeue()
        {
            var item = this.data[this.data.Count - 1];
            this.data.RemoveAt(this.data.Count - 1);
            return item;

        }

        internal bool IsEmpty()
        {
            return this.data.Count == 0;
        }
    }
}
