using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulatie.Utility;
using Microsoft.Xna.Framework;

namespace HotelSimulatie.People.Tests
{
    [TestClass()]
    public class CleanerTests
    {
        Hotel hotel;
        List<Room> hotelRooms;
        Cleaner cleaner;
        SimplePath simplePath;
        [TestInitialize()]
        public void TestInit()
        {
            hotel = new Hotel();
            hotelRooms = new List<Room>();
            cleaner = new Cleaner();
            simplePath = new SimplePath();
        }
        [TestMethod()]
        public void SearchRoomTest_State()
        {
            Room testRoom = new Room() { AreaType = "Room", ID = 3, State = Room.RoomState.Dirty, Position = new Vector2(2, 0) };
            hotelRooms.Add(testRoom);
            Queue<Room> rooms = new Queue<Room>();
            hotel.Areas.AddRange(hotelRooms);
            cleaner.Position = new Vector2(1, 0);
            Node cleanerNode = new Node(cleaner.Position);
            Node roomNode = new Node(testRoom.Position);
            cleanerNode.Edges.Add(roomNode, 1);
            roomNode.Edges.Add(cleanerNode, 1);
            simplePath.Add(cleanerNode);
            simplePath.Add(roomNode);

            cleaner.SearchRoom(hotel, rooms);

            Assert.AreEqual(testRoom, rooms.First());
        }
        [TestMethod()]
        public void SearchRoomTest_Destination()
        {
            Room testRoom = new Room() { AreaType = "Room", ID = 3, State = Room.RoomState.Emergency, Position = new Vector2(2, 0) };
            hotelRooms.Add(testRoom);
            hotel.Areas.AddRange(hotelRooms);
            cleaner.Position = new Vector2(1, 0);
            Queue<Room> rooms = new Queue<Room>();
            Node cleanerNode = new Node(cleaner.Position);
            Node roomNode = new Node(testRoom.Position);
            cleanerNode.Edges.Add(roomNode, 1);
            roomNode.Edges.Add(cleanerNode, 1);
            simplePath.Add(cleanerNode);
            simplePath.Add(roomNode);

            cleaner.SearchRoom(hotel, rooms);

            Assert.AreEqual(testRoom, rooms.First());
        }

        [TestMethod()]
        public void GetRoomTest()
        {
            Room testRoom = new Room() { AreaType = "Room", ID = 3, State = Room.RoomState.Dirty, Position = new Vector2(2, 0) };
            hotelRooms.Add(testRoom);
            hotel.Areas.AddRange(hotelRooms);
            cleaner.Position = new Vector2(1, 0);
            Queue<Room> rooms = new Queue<Room>();
            Node cleanerNode = new Node(cleaner.Position);
            Node roomNode = new Node(testRoom.Position);
            cleanerNode.Edges.Add(roomNode, 1);
            roomNode.Edges.Add(cleanerNode, 1);
            simplePath.Add(cleanerNode);
            simplePath.Add(roomNode);
            cleaner.SearchRoom(hotel, rooms);
            cleaner.Evacuating = false;
            cleaner.Cleaning = false;

            cleaner.GetRoom(new GameTime(), simplePath, rooms);

            Assert.AreEqual(testRoom.State, Room.RoomState.Cleaning);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Room room = new Room() { ID = 3 };
            Cleaner testCleaner = new Cleaner() { Room = room,Position=new Vector2(1,1),Destination = new Vector2(0,0),Cleaning=true, };
            Assert.AreEqual("AssignedRoomID: 0	Position	X: 0	Y: 0	Destination	X: 0	Y: 0	CurrentlyCleaning: No", cleaner.ToString());
        }
    }
}