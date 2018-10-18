namespace BasicTreesExecices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static IDictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        public static void Main()
        {
            ReadTree();
            var rootNode = GetRootNode();
            Console.WriteLine(rootNode.Value);
        }

        private static void ReadTree()
        {
            int nodeCount = int.Parse(Console.ReadLine());

            for (int i = 1; i < nodeCount; i++)
            {
                var args = Console.ReadLine().Split().Select(int.Parse).ToArray();
                AddEdge(args[0], args[1]);
            }
        }
        private static void AddEdge(int parent, int child)
        {
            var parentNode = GetNodeByValue(parent);
            var childNode = GetNodeByValue(child);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }
        private static Tree<int> GetNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        private static Tree<int> GetRootNode()
        {
            return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
        }
    }
}
