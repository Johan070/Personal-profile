using HotelSimulatie.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie.Areas
{
    /// <summary>
    /// An area for moving up and down
    /// </summary>
    public abstract class TransportationArea : Area
    {
        /// <summary>
        /// The weight of the edges
        /// </summary>
        public int Weight { get; set; } = 1;
        /// <summary>
        /// generates nodes within the transportationareas
        /// </summary>
        /// <returns></returns>
        public virtual List<Node> GetInternalNodes()
        {
            Node prevNode = null;
            List<Node> internalNodes = new List<Node>();
            //Generate edges up
            for (int i = 0, dim = (int)Dimension.Y; i < dim; i++)
            {
                Node positionNode = new Node(new Vector2(Position.X, Position.Y + i));
                if (prevNode != null)
                {
                    positionNode.Edges.Add(prevNode, this.Weight);
                }
                internalNodes.Add(positionNode);
                prevNode = positionNode;
            }
            prevNode = null;
            //Generate edges down
            foreach (Node node in internalNodes.OrderByDescending(n => n.Value.Y))
            {
                if (prevNode != null)
                {
                    node.Edges.Add(prevNode,this.Weight);
                }
                prevNode = node;
            }
            return internalNodes;

        }
    }
}
