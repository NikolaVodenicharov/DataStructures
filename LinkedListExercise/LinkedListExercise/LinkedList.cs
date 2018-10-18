namespace LinkedListExercise
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LinkedList<T> : IEnumerable<T>
    {
        public Node Head { get; private set; }
        public Node Tail { get; private set; }
        public int Count { get; private set; }

        public void AddFirst (T element)
        {
            var first = new Node(element);

            if (this.Count == 0)
            {
                this.Head = this.Tail = first;
            }
            else
            {
                var old = this.Head;
                this.Head = first;
                this.Head.Next = old;
            }

            this.Count++;
        }

        public void AddLast (T element)
        {
            var last = new Node(element);

            if (this.Count == 0)
            {
                this.Head = this.Tail = last;
            }
            else
            {
                var old = this.Tail;
                this.Tail = last;
                this.Tail.Next = last;
            }

            this.Count++;
        }

        public T RemoveFirst()
        {
            if (this.IsEmpty())
            {
                throw new InvalidOperationException("The collection is empty.");
            }

            var element = this.Head.Element;
            this.Head = this.Head.Next;
            this.Count--;

            if (this.IsEmpty())
            {
                this.Tail = null;
            }

            return element;
        }
        public bool IsEmpty()
        {
            return this.Count == 0;
        }

        public T RemoveLast()
        {
            if (this.IsEmpty())
            {
                throw new InvalidOperationException("The collection is empty.");
            }

            var element = this.Tail.Element;


            if (this.Count == 1)
            {
                this.Head = this.Tail = null;
            }
            else if (this.Count == 2)
            {
                this.Head.Next = null;
                this.Tail = this.Head;
            }
            else
            {
                var beforeLastNode = GetBeforeLastNode();
                this.Tail = beforeLastNode;
                this.Tail.Next = null;
            }

            return element;
        }
        private Node GetBeforeLastNode()
        {
            if (this.Count < 2)
            {
                throw new InvalidOperationException("There are less than 2 nodes.");
            }

            var current = this.Head;

            while (true)
            {
                if (current.Next == null)
                {
                    throw new InvalidOperationException("There are is missmatch between Count of nodes and the actually linked nodes.");
                }

                if (current.Next.Next == null)
                {
                    return current;
                }

                current = current.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.Head;

            while (current != null)
            {
                yield return current.Element;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public class Node
        {
            public Node(T element)
            {
                this.Element = element;
            }

            public T Element { get; set; }
            public Node Next { get; set; }
        }
    }
}
