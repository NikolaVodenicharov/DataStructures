namespace Hash_Table
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BalancedOrderedSet<T> : IEnumerable<T> where T : IComparable
    {
        private AVLTree<T> tree = new AVLTree<T>();

        public void Add(T element)
        {
            tree.Insert(element);
        }
        public bool Contains(T element)
        {
            return tree.Contains(element);
        }
        public int Count()
        {
            return tree.Count;
        }
        public void Remove(T element)
        {
            tree.Remove(element);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var elements = new List<T>();
            tree.EachInOrder(elements.Add);

            foreach (var element in elements)
            {
                yield return element;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
