using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using HotelSimulatie.Utility;
using HotelEvents;
using System.Reflection;

namespace HotelSimulatie.People.Tests
{
    [TestClass()]
    public class CustomerTests
    {
        Hotel hotel;
        List<Room> hotelRooms;
        SimplePath simplePath;
        Customer person;
        [TestInitialize()]
        public void TestInit()
        {
            hotel = new Hotel();
            hotelRooms = new List<Room>();
            simplePath = new SimplePath();
            person = new Customer();
            person.Route = new Stack<Node>();
        }
        [TestMethod()]
        public void MoveTestAdjacent()
        {
            person.Position = new Vector2(1, 0);
            person.Route.Push(new Node(new Vector2(2, 0)));

            person.Move();

            Assert.AreEqual(person.Position, new Vector2(1 + HotelEventManager.HTE_Factor / Size.SCALE, 0));
        }
        [TestMethod()]
        public void ReturnToRoomTest()
        {
            Room testRoom = new Room() { AreaType = "Room", ID = 3, State = Room.RoomState.Dirty, Position = new Vector2(2, 0) };
            Restaurant restaurant = new Restaurant() { AreaType = "Restaurant", Position = new Vector2(3, 4) };
            hotelRooms.Add(testRoom);
            hotel.Areas.AddRange(hotelRooms);
            hotel.Areas.Add(restaurant);
            person.Room = testRoom;
            Node restaurantNode = new Node(restaurant.Position);
            Node personNode = new Node(person.Position);
            Node roomNode = new Node(testRoom.Position);
            personNode.Edges.Add(roomNode, 1);
            personNode.Edges.Add(restaurantNode, 1);
            restaurantNode.Edges.Add(personNode, 1);
            roomNode.Edges.Add(personNode, 1);
            simplePath.Add(personNode);
            simplePath.Add(roomNode);
            GameTime gameTime = new GameTime();
            var prop = person.GetType().GetField("_passedTimeSinceUpdate", BindingFlags.NonPublic | BindingFlags.Instance);
            prop.SetValue(person, person.WaitingTime / HotelEventManager.HTE_Factor);
            person.ReturnToRoom(gameTime, simplePath, hotel);

            Assert.AreEqual(testRoom.Position, person.Destination);
        }
        [TestMethod()]
        public void MoveTestLeftAdjacent()
        {
            person.Position = new Vector2(2, 0);
            person.Route.Push(new Node(new Vector2(1, 0)));

            person.Move();

            Assert.AreEqual(person.Position, new Vector2(2 - HotelEventManager.HTE_Factor / Size.SCALE, 0));
        }
        [TestMethod()]
        public void MoveTestUpAdjacent()
        {
            person.Position = new Vector2(0, 1);
            person.Route.Push(new Node(new Vector2(0, 2)));

            person.Move();

            Assert.AreEqual(person.Position, new Vector2(0, 1 + HotelEventManager.HTE_Factor / Size.SCALE));
        }
        [TestMethod()]
        public void MoveTestDownAdjacent()
        {
            person.Position = new Vector2(0, 2);
            person.Route.Push(new Node(new Vector2(0, 1)));

            person.Move();

            Assert.AreEqual(person.Position, new Vector2(0, 2 - HotelEventManager.HTE_Factor / Size.SCALE));
        }

        [TestMethod()]
        public void CheckCinemaTest()
        {
            Cinema cinema = new Cinema() { Position = new Vector2(3, 0) };
            Vector2 wachtVector = new Vector2(2, 0);
            person.Position = new Vector2(1, 0);
            person.Destination = cinema.Position;
            Node a = new Node(person.Position);
            Node b = new Node(wachtVector);
            Node c = new Node(cinema.Position);
            c.Edges.Add(a, 1);
            a.Edges.Add(c, 1);
            b.Edges.Add(a, 1);
            a.Edges.Add(b, 1);
            cinema.Started = true;
            SimplePath simplePath = new SimplePath();
            simplePath.Add(a);
            simplePath.Add(b);
            simplePath.Add(c);
            person.CheckCinema(simplePath, cinema);
            Assert.AreEqual(wachtVector, person.Destination);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Room room = new Room() { ID = 3 };
            Customer testCustomer = new Customer() { ID = 1,Room = room, Position = new Vector2(1, 1), Destination = new Vector2(0, 0), };
            Assert.AreEqual("ID: 1	RoomID: 3	Position	X: 1	Y: 1	Destination	X: 0	Y: 0", testCustomer.ToString());
        }
    }
}