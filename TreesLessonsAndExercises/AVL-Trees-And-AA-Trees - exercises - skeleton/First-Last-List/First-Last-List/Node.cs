namespace First_Last_List
{
    using System;

    public class Node<T> where T : IComparable<T>
    {
        public Node (int key, T value)
        {
            this.Key = key;
            this.Value = value;
            this.Height = 1;
        }

        public int Key { get; set; }
        public T Value { get; set; }
        public int Height { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public int CompareTo(Node<T> other)
        {
            if (other == null)
            {
                return 1;
            }

            var result = this.Value.CompareTo(other.Value);

            if (result == 0)
            {
                result = this.Key.CompareTo(other.Key) * -1;
            }

            return result;
        }
    }

}
