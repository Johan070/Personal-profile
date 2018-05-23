using HotelSimulatie.Areas;
using HotelSimulatie.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    /// <summary>
    /// The hotel class wich stores the areas
    /// </summary>
    public class Hotel
    {
        /// <summary>
        /// A list with all areas in the hotel
        /// </summary>
        public List<IArea> Areas { get; set; }
        /// <summary>
        /// Dictionary with the rooms with rating as key
        /// </summary>
        public Dictionary<int, List<Room>> Rooms { get; set; }
        /// <summary>
        /// Initialize the hotel
        /// </summary>
        public Hotel()
        {
            Areas = new List<IArea>();
            Rooms = new Dictionary<int, List<Room>>();
        }
        /// <summary>
        /// Load the dictionary
        /// </summary>
        public void Load()
        {
            foreach (Room room in Areas.Where(type => type.AreaType == "Room"))
            {
                int key = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(room.Classification, "[^\\d]")));
                if (Rooms.ContainsKey(key))
                {
                    Rooms[key].Add(room);
                }
                else
                {
                    Rooms.Add(key, new List<Room>()
                    {
                        room,
                    });
                }


            }
        }
        /// <summary>
        /// adds edges to every node and adds every node to the graph in simplePath
        /// </summary>
        /// <param name="simplePath"></param>
        public void AddToGraph(SimplePath simplePath)
        {
            List<Area> noEdges = new List<Area>();
            List<Node> transportationAreas = new List<Node>();
            int maxX = 0;
            foreach (Area area in Areas)
            {
                if (area.AreaType == "Elevator" || area.AreaType == "Stairs")
                {
                    transportationAreas.AddRange((area as TransportationArea).GetInternalNodes());
                    if (area.GetType() == typeof(Stairs))
                    {
                        maxX = (int)area.Position.X;
                    }
                }
                else
                {
                    noEdges.Add(area);
                }
            }
            Node prevNode = null;
            List<Node> toAdd = new List<Node>();

            foreach (Area area in noEdges.OrderBy(n => n.Position.Y).ThenBy(n => n.Position.X))
            {
                Node positionNode = new Node(new Vector2(area.Position.X, area.Position.Y));
                if (prevNode != null && positionNode.Value.Y != prevNode.Value.Y)
                {
                    prevNode = null;
                }
                if (prevNode != null)
                {
                    positionNode.Edges.Add(prevNode, 1);
                }
                else
                {
                    positionNode.Edges.Add(transportationAreas.Find(ta => ta.Value == new Vector2(0, positionNode.Value.Y)), (int)positionNode.Value.X);
                }
                toAdd.Add(positionNode);
                prevNode = positionNode;
            }
            prevNode = null;
            foreach (Node node in toAdd.OrderBy(n => n.Value.Y).ThenByDescending(n => n.Value.X))
            {
                if (prevNode != null && node.Value.Y != prevNode.Value.Y)
                {
                    prevNode = null;
                }
                if (prevNode != null)
                {
                    node.Edges.Add(prevNode, 1);
                }
                else
                {
                    node.Edges.Add(transportationAreas.Find(ta => ta.Value == new Vector2(maxX, node.Value.Y)), maxX - (int)node.Value.X);
                }
                prevNode = node;
            }
            foreach (Node node in toAdd)
            {
                if (node.Edges.Any(e => e.Key.Value.X == 0))
                {
                    transportationAreas.Where(n => n.Value == node.Edges.Where(k => k.Key.Value.X == 0).First().Key.Value).First().Edges.Add(node, 1);
                }
                if (node.Edges.Any(e => e.Key.Value.X == maxX))
                {
                    transportationAreas.Where(n => n.Value == node.Edges.Where(k => k.Key.Value.X == maxX).First().Key.Value).First().Edges.Add(node, 1);
                }
                simplePath.Add(node);
            }
            foreach (Node node in transportationAreas)
            {
                simplePath.Add(node);
            }
        }
    }
}
