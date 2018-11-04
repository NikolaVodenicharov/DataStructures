namespace Hash_Table
{
    public class Node<T>
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
        public int Height { get; set; } = 1;
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
    }
}
