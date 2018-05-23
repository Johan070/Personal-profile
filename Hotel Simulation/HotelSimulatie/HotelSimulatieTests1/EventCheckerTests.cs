using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulatie.Utility;
using HotelSimulatie.People;
using HotelSimulatie.Areas;
using HotelEvents;
using Microsoft.Xna.Framework;

namespace HotelSimulatie.Tests
{
    [TestClass()]
    public class EventCheckerTests
    {
        Hotel hotel;
        List<Area> hotelRooms;
        SimplePath simplePath;
        List<IPerson> persons;
        Customer person;
        Customer person2;
        Cleaner cleaner;
        Reception reception;
        List<Customer> customers;
        EventListener listener;
        Lobby lobby;
        Room room;
        Elevator elevator;
        List<Cleaner> cleaners;
        EventChecker eventChecker;
        GameTime gameTime;
        Queue<Room> RoomQueue;
        [TestInitialize()]
        public void TestInit()
        {
            RoomQueue = null;
            gameTime = null;
            room = new Room();
            hotel = new Hotel();
            hotelRooms = new List<Area>();
            simplePath = new SimplePath();
            person = new Customer();
            person.ID = 1;
            person.Room = room;
            person.Position = new Vector2(6, 7);
            person.Route = new Stack<Node>();
            person2 = new Customer();
            person.Position = new Vector2(6, 9);
            person2.Route = new Stack<Node>();
            cleaner = new Cleaner() { Position = new Vector2(34, 2), Room = room };
            persons = new List<IPerson>();
            customers = new List<Customer>()
            {
                person,
                person2,
            };
            cleaners = new List<Cleaner>
            {
                cleaner
            };
            persons.Add(cleaner);
            persons.Add(person);
            persons.Add(person2);
            reception = new Reception();
            lobby = new Lobby() { Position = new Vector2(2, 2) };
            elevator = new Elevator();
            listener = new EventListener();
            eventChecker = new EventChecker();
        }
        [TestMethod()]
        public void CheckEventsTest_Checkin()
        {
            HotelEvent evt = new HotelEvent() { EventType = HotelEventType.CHECK_IN, Data = new Dictionary<string, string> { { "Gast1", "Checkin4Stars" } } };
            listener.Events.Add(evt);

            eventChecker.CheckEvents(simplePath, hotel, persons, reception, customers, listener, lobby, elevator, cleaner, cleaners, gameTime, RoomQueue);

            Assert.AreEqual(3, customers.Count);
        }
        [TestMethod()]
        public void CheckEventsTest_CheckOut()
        {
            HotelEvent evt = new HotelEvent() { EventType = HotelEventType.CHECK_OUT, Data = new Dictionary<string, string> { { "Gast", "1" } } };
            listener.Events.Add(evt);
            hotel.Areas.Add(lobby);
            Node n1 = new Node(lobby.Position);
            Node n2 = new Node(customers.Find(id => id.ID == 1).Position);
            n1.Edges.Add(n2, 1);
            n2.Edges.Add(n1, 1);
            simplePath.Add(n1);
            simplePath.Add(n2);

            eventChecker.CheckEvents(simplePath, hotel, persons, reception, customers, listener, lobby, elevator, cleaner, cleaners, gameTime, RoomQueue);

            Assert.AreEqual(1, customers.Count);
        }
        [TestMethod()]
        public void CheckEventsTest_Fitness()
        {
            HotelEvent evt = new HotelEvent() { EventType = HotelEventType.GOTO_FITNESS, Data = new Dictionary<string, string> { { "Gast", "1" }, { "HTE", "8" } } };
            listener.Events.Add(evt);
            hotel.Areas.Add(new Fitness { Position = new Vector2(1, 0), AreaType = "Fitness" });
            Node n1 = new Node(hotel.Areas.Find(type => type.AreaType == "Fitness").Position);
            Node n2 = new Node(customers.Find(id => id.ID == 1).Position);
            n1.Edges.Add(n2, 1);
            n2.Edges.Add(n1, 1);
            simplePath.Add(n1);
            simplePath.Add(n2);

            eventChecker.CheckEvents(simplePath, hotel, persons, reception, customers, listener, lobby, elevator, cleaner, cleaners, gameTime, RoomQueue);

            Assert.AreEqual(customers.Find(id => id.ID == 1).Destination, hotel.Areas.Find(type => type.AreaType == "Fitness").Position);
        }
        [TestMethod()]
        public void CheckEventsTest_Cinema()
        {
            HotelEvent evt = new HotelEvent() { EventType = HotelEventType.GOTO_CINEMA, Data = new Dictionary<string, string> { { "Gast", "1" } } };
            listener.Events.Add(evt);
            hotel.Areas.Add(new Cinema { Position = new Vector2(1, 0), AreaType = "Cinema" });
            Node n1 = new Node(hotel.Areas.Find(type => type.AreaType == "Cinema").Position);
            Node n2 = new Node(customers.Find(id => id.ID == 1).Position);
            n1.Edges.Add(n2, 1);
            n2.Edges.Add(n1, 1);
            simplePath.Add(n1);
            simplePath.Add(n2);

            eventChecker.CheckEvents(simplePath, hotel, persons, reception, customers, listener, lobby, elevator, cleaner, cleaners, gameTime, RoomQueue);

            Assert.AreEqual(customers.Find(id => id.ID == 1).Destination, hotel.Areas.Find(type => type.AreaType == "Cinema").Position);
        }
        [TestMethod()]
        public void CheckEventsTest_Need_Food()
        {
            HotelEvent evt = new HotelEvent() { EventType = HotelEventType.NEED_FOOD, Data = new Dictionary<string, string> { { "Gast", "1" } } };
            listener.Events.Add(evt);
            hotel.Areas.Add(new Restaurant { Position = new Vector2(1, 0), AreaType = "Restaurant" });
            Node n1 = new Node(hotel.Areas.Find(type => type.AreaType == "Restaurant").Position);
            Node n2 = new Node(customers.Find(id => id.ID == 1).Position);
            n1.Edges.Add(n2, 1);
            n2.Edges.Add(n1, 1);
            simplePath.Add(n1);
            simplePath.Add(n2);

            eventChecker.CheckEvents(simplePath, hotel, persons, reception, customers, listener, lobby, elevator, cleaner, cleaners, gameTime, RoomQueue);

            Assert.AreEqual(1, ((Restaurant)hotel.Areas.Find(type => type.AreaType == "Restaurant")).HuidigeBezetting);
        }
        [TestMethod()]
        public void CheckEventsTest_StartCinema()
        {
            HotelEvent evt = new HotelEvent() { EventType = HotelEventType.START_CINEMA, Data = new Dictionary<string, string> { { "ID", "9" } } };
            listener.Events.Add(evt);
            hotel.Areas.Add(new Cinema { Position = new Vector2(1, 0), AreaType = "Cinema", ID = 9 });
            Node n1 = new Node(hotel.Areas.Find(type => type.AreaType == "Cinema").Position);
            Node n2 = new Node(customers.Find(id => id.ID == 1).Position);
            n1.Edges.Add(n2, 1);
            n2.Edges.Add(n1, 1);
            simplePath.Add(n1);
            simplePath.Add(n2);

            eventChecker.CheckEvents(simplePath, hotel, persons, reception, customers, listener, lobby, elevator, cleaner, cleaners, gameTime, RoomQueue);

            Assert.AreEqual(true, ((Cinema)hotel.Areas.Find(type => type.AreaType == "Cinema")).Started);
        }
        [TestMethod()]
        public void CheckEventsTest_Evac()
        {
            HotelEvent evt = new HotelEvent() { EventType = HotelEventType.EVACUATE, Data = new Dictionary<string, string> { { "ID", "9" } } };
            listener.Events.Add(evt);
            hotel.Areas.Add(lobby);
            Node n1 = new Node(lobby.Position);
            Node n2 = new Node(customers.Find(id => id.ID == 1).Position);
            Node n3 = new Node(person2.Position);
            n1.Edges.Add(n2, 1);
            n2.Edges.Add(n1, 1);
            n3.Edges.Add(n1, 1);
            simplePath.Add(n1);
            simplePath.Add(n2);

            eventChecker.CheckEvents(simplePath, hotel, persons, reception, customers, listener, lobby, elevator, cleaner, cleaners, gameTime, RoomQueue);

            Assert.AreEqual(person2.Destination, lobby.Position);
        }
        [TestMethod()]
        public void CheckEventsTest_CLEANING_EMERGENCY()
        {
            HotelEvent evt = new HotelEvent() { EventType = HotelEventType.CLEANING_EMERGENCY, Data = new Dictionary<string, string> { { "kamer", "9" }, { "HTE", "9" } } };
            listener.Events.Add(evt);
            Room room2 = new Room()
            {
                ID = 9,
                AreaType = "Room",
                Position = new Vector2(1, 1),
            };
            room.State = Room.RoomState.Dirty;
            hotel.Areas.Add(room2);
            Node n1 = new Node(room2.Position);
            Node n2 = new Node(cleaner.Position);

            n1.Edges.Add(n2, 1);
            n2.Edges.Add(n1, 1);

            simplePath.Add(n1);
            simplePath.Add(n2);

            eventChecker.CheckEvents(simplePath, hotel, persons, reception, customers, listener, lobby, elevator, cleaner, cleaners, gameTime, RoomQueue);

            Assert.AreEqual(Room.RoomState.Emergency, room2.State);
        }
    }
}