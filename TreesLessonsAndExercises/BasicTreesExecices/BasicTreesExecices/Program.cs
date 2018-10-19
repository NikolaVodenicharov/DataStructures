﻿namespace BasicTreesExecices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Program
    {
        private static IDictionary<int, Tree<int>> nodesByValue = new Dictionary<int, Tree<int>>();

        public static void Main()
        {
            ReadTree();
            var deepestNodeValue = GetDeepestNodeValue();
            Console.WriteLine($"Deepest node: {deepestNodeValue}");
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
            if (!nodesByValue.ContainsKey(value))
            {
                nodesByValue[value] = new Tree<int>(value);
            }

            return nodesByValue[value];
        }

        private static Tree<int> GetRootNode()
        {
            return nodesByValue.Values.FirstOrDefault(x => x.Parent == null);
        }

        private static ICollection<Tree<int>> GetLeafNodes()
        {
            return nodesByValue.Values.Where(x => x.Children.Count == 0).Select(x => x).ToList();
        }

        private static ICollection<Tree<int>> GetMiddleNodes()
        {
            return nodesByValue
                .Values
                .Where(n => 
                    n.Parent != null &&
                    n.Children.Count > 0)
                .Select(n => n)
                .ToList();
        }

        private static int GetDeepestNodeValue()
        {
            var leafs = GetLeafNodes();
            var deepestNodeValue = 0;
            var depth = 0;

            foreach (var leaf in leafs)
            {
                var currentLeafDepth = GetNodeDepth(leaf);

                if (currentLeafDepth > depth)
                {
                    deepestNodeValue = leaf.Value;
                    depth = currentLeafDepth;
                }
            }

            return deepestNodeValue;
        }
        private static int GetNodeDepth(Tree<int> tree, int depth = 1)
        {
            if (tree.Parent != null)
            {
                return GetNodeDepth(tree.Parent, depth + 1);
            }

            return depth;
        }

        private static void PrintValues(string text, IEnumerable<int> values)
        {
            var sb = new StringBuilder();
            sb.Append(text);

            foreach (var value in values)
            {
                sb.Append($"{value} ");
            }

            Console.WriteLine(sb);
        }
    }
}
