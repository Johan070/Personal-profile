using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using HotelSimulatie.Utility;
using System.Reflection;
using HotelSimulatie.Areas;

namespace HotelSimulatie.Tests
{
    [TestClass()]
    public class HotelTests
    {
        Hotel hotel;
        SimplePath simplePath;
        [TestInitialize()]
        public void TestInit()
        {
            hotel = new Hotel();
            simplePath = new SimplePath();
        }
        [TestMethod()]
        public void LoadTest()
        {
            hotel.Areas.Add(new Room()
            {
                AreaType = "Room",
                Classification = "1 Vis"
            });
            hotel.Areas.Add(new Room()
            {
                AreaType = "Room",
                Classification = "1 Vis"
            });
            hotel.Areas.Add(new Room()
            {
                AreaType = "Room",
                Classification = "1 Vis"
            });
            hotel.Areas.Add(new Room()
            {
                AreaType = "Room",
                Classification = "2 Vis"
            });
            hotel.Load();
            List<Room> expected = hotel.Areas.OfType<Room>().Where(q => q.Classification == "1 Vis").ToList();
            Assert.AreEqual(expected.Count, hotel.Rooms[1].Count);
        }

        [TestMethod()]
        public void AddToGraphTest()
        {
            hotel.Areas.Add(new Room()
            {
                AreaType = "Room",
                Classification = "1 Vis",
                Position = new Vector2(1, 1),
            });
            hotel.Areas.Add(new Room()
            {
                ID = 5,
                AreaType = "Room",
                Classification = "1 Vis",
                Position = new Vector2(1, 2),
            });
            hotel.Areas.Add(new Room()
            {
                AreaType = "Room",
                Classification = "1 Vis",
                Position = new Vector2(1, 3),
            });
            hotel.Areas.Add(new Room()
            {
                AreaType = "Room",
                Classification = "2 Vis",
                Position = new Vector2(1, 4),
            });
            hotel.Areas.Add(new Stairs()
            {
                AreaType = "Stairs",
                Position = new Vector2(2, 1),
                Dimension = new Vector2(1, 4),
            });
            hotel.Areas.Add(new Elevator()
            {
                AreaType = "Elevator",
                Position = new Vector2(0, 1),
                Dimension = new Vector2(1, 4),
            });
            List<Node> nodes = new List<Node>();
            var prop = simplePath.GetType().GetField("_allNodesCopy", BindingFlags.NonPublic | BindingFlags.Instance);
            prop.SetValue(simplePath, nodes);
            hotel.AddToGraph(simplePath);

            Assert.AreEqual(2,nodes.Find(pos => pos.Value == new Vector2(1, 2)).Edges.Count);
          
        }
    }
}