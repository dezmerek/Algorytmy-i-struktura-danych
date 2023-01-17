using Internal;
using System;
using System.Collections.Generic;

namespace projekt
{
    internal class Programming
    {
        static void Main(string[] args)
        {
            try
            {
                IGraph<int, double> graph = new NListGraph();
                graph.AddDirectedEdge(1, 2, 2);
                graph.AddDirectedEdge(1, 3, 0.5);
                graph.AddDirectedEdge(2, 3, 2.5);
                graph.AddDirectedEdge(2, 4, 3);
                graph.AddDirectedEdge(3, 5, 2);
                graph.AddUndirectedEdge(4, 5, 0.5);
                graph.AddDirectedEdge(5, 6, 2);
                if (graph.IsPath(1, 4))
                {
                    Console.WriteLine("Test 1 passed");
                }
                else
                {
                    Console.WriteLine("Test 1 failed");
                }

                if (graph.CanReturn(4) && graph.CanReturn(1))
                {
                    Console.WriteLine("Test 2 passed");
                }
                else
                {
                    Console.WriteLine("Test 2 failed");
                }

                List<Edge<int, double>> list = graph.GetShortestPath(2, 5);
                if (list.Count == 2 && list[0].Node == 4 && list[0].Weight == 3 && list[1].Node == 5 && list[1].Weight == 0.5)
                {
                    Console.WriteLine("Test 3 passed");
                }
                else
                {
                    Console.WriteLine("Test 3 failed");
                }
                List<int> n = graph.Neighbours(2);
                if (n.Count == 2 && n.Contains(3) && n.Contains(4))
                {
                    Console.WriteLine("Test 4 passed");
                }
                else
                {
                    Console.WriteLine("Test 4 failed");
                }
            }
            catch
            {
                Console.WriteLine("Some metods are not implemented");
            }
        }
    }
    public class Edge<T, W> : IComparable<Edge<T, W>> where W : IComparable<W>

    {
        public T Node { get; set; }
        public W Weight { get; set; }

        public int CompareTo(Edge<T, W> other)
        {
            return Weight.CompareTo(other.Weight);
        }
    }

    public interface IGraph<T, W> where W : IComparable<W>
    {
        public void AddDirectedEdge(T source, T target, W weight);
        public void AddUndirectedEdge(T source, T target, W weight);
        public bool IsPath(T start, T end);
        public bool CanReturn(T node);
        public List<Edge<T, W>> GetShortestPath(T start, T end);
        public void Traversal(T start, Action<T> action);
        public List<T> Neighbours(T node);
    }

    public class NListGraph : IGraph<int, double>
    {
        private Dictionary<int, List<Edge<int, double>>> _adjList;

        public NListGraph()
        {
            _adjList = new Dictionary<int, List<Edge<int, double>>>();
        }

        public void AddDirectedEdge(int source, int target, double weight)
        {
            if (!_adjList.ContainsKey(source))
                _adjList[source] = new List<Edge<int, double>>();

            _adjList[source].Add(new Edge<int, double> { Node = target, Weight = weight });
        }

        public void AddUndirectedEdge(int source, int target, double weight)
        {
            AddDirectedEdge(source, target, weight);
            AddDirectedEdge(target, source, weight);
        }

        public bool CanReturn(int node)
        {
            foreach (var item in _adjList)
            {
                if (item.Value.Any(x => x.Node == node))
                    return true;
            }
            return false;
        }

        public List<Edge<int, double>> GetShortestPath(int start, int end)
        {
            var distances = new Dictionary<int, double>();
            var previous = new Dictionary<int, int>();
            var unvisited = new HashSet<int>();

            foreach (var vertex in _adjList.Keys)
            {
                distances[vertex] = double.PositiveInfinity;
                previous[vertex] = -1;
                unvisited.Add(vertex);
            }

            distances[start] = 0;

            while (unvisited.Count > 0)
            {
                var current = GetClosest(distances, unvisited);
                unvisited.Remove(current);

                if (current == end)
                    break;

                foreach (var neighbours in _adjList[current])
                {
                    var distance = distances[current] + neighbours.Weight;
                    if (distance < distances[neighbours.Node])
                    {
                        distances[neighbours.Node] = distance;
                        previous[neighbours.Node] = current;
                    }
                }
            }

            if (previous[end] == -1)
                return new List<Edge<int, double>>();

            var path = new List<Edge<int, double>>();
            var currentNode = end;
            while (currentNode != start)
            {
                var previousNode = previous[currentNode];
                var edge = _adjList[previousNode].First(x => x.Node == currentNode);
                path.Add(edge);
                currentNode = previousNode;
            }

            path.Reverse();
            return path;
        }

        public bool IsPath(int start, int end)
        {
            var visited = new HashSet<int>();
            DFS(start, visited);

            return visited.Contains(end);
        }

        public List<int> Neighbours(int node)
        {
            List<int> neighbours = new List<int>();
            if (_adjList.ContainsKey(node))
            {
                neighbours = _adjList[node].Select(x => x.Node).ToList();
            }
            return neighbours;
        }


        public void Traversal(int start, Action<int> action)
        {
            var visited = new HashSet<int>();
            DFS(start, visited, action);
        }

        private void DFS(int node, HashSet<int> visited)
        {
            if (visited.Contains(node))
                return;

            visited.Add(node);

            if (_adjList.ContainsKey(node))
            {
                foreach (var neighbours in _adjList[node])
                    DFS(neighbours.Node, visited);
            }
        }

        private void DFS(int node, HashSet<int> visited, Action<int> action)
        {
            if (visited.Contains(node))
                return;

            action(node);
            visited.Add(node);

            if (_adjList.ContainsKey(node))
            {
                foreach (var neighbours in _adjList[node])
                    DFS(neighbours.Node, visited, action);
            }
        }

        private int GetClosest(Dictionary<int, double> distances, HashSet<int> unvisited)
        {
            var closest = -1;
            var closestDistance = double.PositiveInfinity;

            foreach (var vertex in unvisited)
            {
                if (distances[vertex] < closestDistance)
                {
                    closest = vertex;
                    closestDistance = distances[vertex];
                }
            }
            return closest;
        }
    }
}