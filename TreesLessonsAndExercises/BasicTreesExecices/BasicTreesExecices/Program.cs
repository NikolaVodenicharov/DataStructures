namespace BasicTreesExecices
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
            var subtreeSum = int.Parse(Console.ReadLine());

            var subtreesValues = GetSubtreesEqualToGivenSum(subtreeSum);
            var result = AppendPaths(subtreesValues, subtreeSum);
            Console.WriteLine(result);
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

        private static Tree<int> GetDeepestNode()
        {
            var leafs = GetLeafNodes();

            Tree<int> deepestNode = null;
            var depth = 0;

            foreach (var leaf in leafs)
            {
                var currentLeafDepth = GetNodeDepth(leaf);

                if (currentLeafDepth > depth)
                {
                    deepestNode = leaf;
                    depth = currentLeafDepth;
                }
            }

            return deepestNode;
        }
        private static int GetNodeDepth(Tree<int> tree, int depth = 1)
        {
            if (tree.Parent != null)
            {
                return GetNodeDepth(tree.Parent, depth + 1);
            }

            return depth;
        }

        private static string GetLongestPathValues()
        {
            var deepestNode = GetDeepestNode();
            var path = GetTreePathValues(deepestNode);
            var sb = AppendPath(path);

            return sb.ToString();
        }
        private static IList<int> GetTreePathValues(Tree<int> tree)
        {
            var path = new List<int>();

            var current = tree;
            while (current != null)
            {
                path.Add(current.Value);
                current = current.Parent;
            }

            return path;
        }
        private static StringBuilder AppendPath(IList<int> path)
        {
            var sb = new StringBuilder();
            sb.Append("Longest path: ");

            for (int i = path.Count - 1; i >= 0; i--)
            {
                sb.Append($"{path[i]} ");
            }

            return sb;
        }

        private static List<IList<int>> GetPathsEqualToSum(ICollection<Tree<int>> trees, int sum)
        {
            var paths = new List<IList<int>>();

            foreach (var tree in trees)
            {
                var path = GetTreePathValues(tree);

                if (path.Sum() == 27)
                {
                    paths.Add(path);
                }
            }

            return paths;
        }
        private static StringBuilder AppendPaths(IList<IList<int>> collections, int sum)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Subtrees of sum {sum}: ");

            foreach (var collection in collections)
            {
                for (int i = collection.Count - 1; i >= 0; i--)
                {
                    sb.Append($"{collection[i]} ");
                }

                sb.AppendLine();
            }

            return sb;
        }

        private static IList<IList<int>> GetSubtreesEqualToGivenSum(int sum)
        {
            var subtreesValues = new List<IList<int>>();

            var middleNodes = GetMiddleNodes();

            foreach (var node in middleNodes)
            {
                var values = node.BFS();

                if (values.Sum() == sum)
                {
                    subtreesValues.Add(values.ToList());
                }
            }

            return subtreesValues;
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
