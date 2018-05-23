using HotelSimulatie.People;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie.Utility
{
    /// <summary>
    /// A graph with a shortest path method
    /// </summary>
    public class SimplePath
    {
        private List<Node> _allNodes;
        private List<Node> _allNodesCopy;
        /// <summary>
        /// Initialize the lists
        /// </summary>
        public SimplePath()
        {
            _allNodes = new List<Node>();
            _allNodesCopy = new List<Node>();
        }
        /// <summary>
        /// Add the node to the graph
        /// </summary>
        /// <param name="toAdd">node to add to the graph</param>
        public void Add(Node toAdd)
        {
            _allNodesCopy.Add(toAdd);
        }
        /// <summary>
        /// Get the route between 2 points in the hotel
        /// </summary>
        /// <param name="begin">the starting point of the route</param>
        /// <param name="eind">the ending point of the route</param>
        /// <returns>a stack with all the nodes on the route in the correct order</returns>
        public Stack<Node> GetRoute(Vector2 begin, Vector2 eind)
        {
            if (begin == eind)
            {
                return new Stack<Node>();
            }
            //Get the boundaries of the hotel
            float smallestY = _allNodesCopy.OrderBy(n => n.Value.Y).First().Value.Y;
            float smallestX = _allNodesCopy.OrderBy(n => n.Value.X).First().Value.X;
            float greatestX = _allNodesCopy.OrderByDescending(n => n.Value.X).First().Value.X;
            //make a new node on position begin
            Node currentNode = new Node(new Vector2((float)Math.Round(begin.X), (float)Math.Round(begin.Y)));
            //check if the node on begin is in the graph, if it is use that one
            if (_allNodesCopy.Any(n => n.Value == begin))
            {
                currentNode = _allNodesCopy.ConvertAll(node => new Node(node.Value) { Edges = node.Edges }).Find(n => n.Value == begin);
            }
            //if it's not in the graph edges need to be generated to the closest node on the left and right 
            else if (currentNode.Value.X > smallestX && currentNode.Value.X < greatestX)
            {
                //get the right node on the same floor
                Node nextX = _allNodesCopy.Where(n => n.Value.X > currentNode.Value.X && n.Value.Y == currentNode.Value.Y).OrderBy(n => n.Value.X).First();
                //get the left node on the same floor
                Node prevX = _allNodesCopy.Where(n => n.Value.X < currentNode.Value.X && n.Value.Y == currentNode.Value.Y).OrderByDescending(n => n.Value.X).First();
                //calculate distance
                currentNode.Edges.Add(nextX, (int)(nextX.Value.X - currentNode.Value.X));
                currentNode.Edges.Add(prevX, (int)(currentNode.Value.X - prevX.Value.X));
            }
            //eind is always in the graph
            var e = from n in _allNodesCopy where n.Value == eind select n;
            List<Node> eindNode = e.ToList();
            Stack<Node> retVal = Route(currentNode, eindNode.FirstOrDefault());
            return retVal;
        }
        /// <summary>
        /// reset all weights of the edges in the graph
        /// </summary>
        private void Reset()
        {
            //Hardcopy the graph into the graph that's going to be modified by Route
            _allNodes = _allNodesCopy.ConvertAll(node => new Node(node.Value) { Edges = node.Edges });
            foreach (Node node in _allNodes)
            {
                foreach (Node key in node.Edges.Keys)
                {
                    key.Weight = int.MaxValue / 2;
                }
            }
        }
        /// <summary>
        /// Checks for the shortest route to every node until it's sure that the found route is the shortest to the eind node
        /// </summary>
        /// <param name="begin">the starting point of the route</param>
        /// <param name="eind">the ending point of the route</param>
        /// <returns>The shortest path from begin to end</returns>
        public Stack<Node> Route(Node begin, Node eind)
        {
            Reset();
            Stack<Node> nodes = new Stack<Node>();
            if (!_allNodes.Contains(begin))
            {
                _allNodes.Add(begin);
            }
            Node deze = begin;
            deze.Weight = 0;
            while (!Visit(deze, eind))
            {
                if (deze.Edges.Count > 0)
                {
                    deze = _allNodes.Aggregate((l, r) => l.Weight < r.Weight ? l : r);
                }
                else
                {
                    return nodes;
                }
            }
            //get the previous nodes of the shortest route
            while (deze.VorigeNode != null)
            {
                nodes.Push(deze);
                deze = deze.VorigeNode;
            }
            return nodes;
        }
        /// <summary>
        /// Visit all edges of deze and give them a weight
        /// </summary>
        /// <param name="deze">the current node</param>
        /// <param name="eind">the end node</param>
        /// <returns>true when the shortest route to end is found</returns>
        private bool Visit(Node deze, Node eind)
        {
            if (deze == eind)
            {
                return true;
            }
            if (_allNodes.Contains(deze))
            {
                _allNodes.Remove(deze);
            }
            List<Node> prevNodes = new List<Node>();
            Node temp = deze;
            while (temp.VorigeNode != null)
            {
                prevNodes.Add(temp);
                temp = temp.VorigeNode;
            }

            foreach (KeyValuePair<Node, int> Key in deze.Edges)
            {
                int nieuweAfstand = deze.Weight + Key.Value;
                if (nieuweAfstand < Key.Key.Weight && !prevNodes.Contains(Key.Key))
                {
                    Key.Key.Weight = nieuweAfstand;
                    Key.Key.VorigeNode = deze;
                    _allNodes.Add(Key.Key);
                }
            }

            return false;
        }
    }
}
