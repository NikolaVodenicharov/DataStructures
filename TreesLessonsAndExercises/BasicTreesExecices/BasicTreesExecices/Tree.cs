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
    }
}
