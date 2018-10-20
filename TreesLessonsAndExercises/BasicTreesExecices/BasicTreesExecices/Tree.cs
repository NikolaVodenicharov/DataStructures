namespace BasicTreesExecices
{
    using System;
    using System.Collections.Generic;

    public class Tree<T>
    {
        public Tree (T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.Children.Add(child);
                child.Parent = this;
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; set; }
        public IList<Tree<T>> Children { get; private set; }

        public void Print(int indent = 0)
        {
            Console.Write(new string(' ', indent * 2));
            Console.WriteLine(this.Value);
            foreach (var child in this.Children)
            {
                child.Print(indent + 1);
            }
        }

        //public IEnumerable<T> OrderDFS()
        //{
        //    var elements = new List<T>();

        //    this.DFS(this, elements);

        //    return elements;
        //}

        //private void DFS(Tree<T> tree, List<T> elements)
        //{
        //    foreach (var children in tree.Children)
        //    {
        //        this.DFS(children, elements);
        //    }

        //    elements.Add(tree.Value);
        //}

        public IEnumerable<T> BFS()
        {
            var elements = new List<T>();

            var trees = new Queue<Tree<T>>();
            trees.Enqueue(this);

            while (trees.Count > 0)
            {
                var tree = trees.Dequeue();
                elements.Add(tree.Value);

                foreach (var child in tree.Children)
                {
                    trees.Enqueue(child);
                }
            }

            return elements;
        }
    }
}
