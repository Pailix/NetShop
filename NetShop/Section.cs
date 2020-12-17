using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetShop
{
    class Section<T> : IEnumerable<T>
        where T:Product
    {
        private List<T> sectionCollection;
        public int SectionID { get; private set; }
        public string SectionName { get; private set; }
        public Section(int id, string sectionName)
        {
            SectionID = id;
            SectionName = sectionName;
            sectionCollection = new List<T>();
        }
        public int Count
        {
            get
            {
                return sectionCollection.Count();
            }
        }
        public T this[int index]
        {
            get
            {
                return sectionCollection[index];
            }
        }
        public void AddProduct (T obj)
        {
            sectionCollection.Add(obj);
        }
        public bool CheckOnEmpty()
        {
            if (sectionCollection.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var obj in sectionCollection)
                yield return obj;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
