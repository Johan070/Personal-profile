using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie.Utility
{
    /// <summary>
    /// A node of a graph based on a location in the hotel
    /// </summary>
    public class Node
    {
        /// <summary>
        /// The neighbour nodes
        /// </summary>
        public Dictionary<Node, int> Edges { get; set; }
        /// <summary>
        /// The weight of the node
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// The previous visited node with the shortest route
        /// </summary>
        public Node VorigeNode { get; set; }
        /// <summary>
        /// The value of the node,  the position in the hotel
        /// </summary>
        public Vector2 Value { get; set; }

        /// <summary>
        /// Initialize the node
        /// </summary>
        /// <param name="value"></param>
        public Node(Vector2 value)
        {
            VorigeNode = null;
            Weight = int.MaxValue/2;
            Value = value;
            Edges = new Dictionary<Node, int>();
        }      
    }
}
