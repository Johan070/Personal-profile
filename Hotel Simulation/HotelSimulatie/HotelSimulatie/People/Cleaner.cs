using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;
using Microsoft.Xna.Framework;
using HotelSimulatie.Utility;
using static HotelSimulatie.Room;
using HotelEvents;

namespace HotelSimulatie.People
{
    /// <summary>
    /// A person with the option to clean
    /// </summary>
    public class Cleaner : Person
    {
        /// <summary>
        /// The speed with wich the cleaner cleans
        /// </summary>
        public int CleaningSpeed { get; set; }
        /// <summary>
        /// The assigned room to the cleaner
        /// </summary>
        public Room Room { get; set; }
        /// <summary>
        /// Shows if the cleaner is currently cleaning
        /// </summary>
        public bool Cleaning { get; set; }
        /// <summary>
        /// The time since last update
        /// </summary>
        public float PassedTimeSinceUpdate { get; set; }
        /// <summary>
        /// The default
        /// </summary>
        public Vector2 VasteLocatie { get; set; }
        /// <summary>
        /// True if evacuate event is ongoing
        /// </summary>
        public bool Evacuating { get; set; }
        /// <summary>
        /// Initialize the cleaner properties
        /// </summary>
        public Cleaner()
        {
            CleaningSpeed = 10;
            Room = new Room();
            Cleaning = false;
        }
        /// <summary>
        /// Load the sprite
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content)
        {
            Sprite = Content.Load<Texture2D>("Cleaner");
        }
        /// <summary>
        /// Draw the cleaner on the current position
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Position == VasteLocatie)
            {
                Rectangle destination = new Rectangle((int)(Position.X * Size.SCALE), (int)(-Position.Y * Size.SCALE), Size.SCALE, Size.SCALE);
                spriteBatch.Draw(Sprite, destinationRectangle: destination, color: Color.White);
            }
            base.Draw(spriteBatch);
        }
        /// <summary>
        /// cleaner starts cleaning.
        /// after cleaning is finished the room is free and the cleaner goes back to the optimal position.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="gameTime"></param>
        /// <param name="simplePath"></param>
        private void Clean(Room room, GameTime gameTime, SimplePath simplePath)
        {
            if (Cleaning && Room == null)
            {
                Cleaning = false;
            }
            float tussenTijd = gameTime.ElapsedGameTime.Milliseconds;
            tussenTijd /= 1000;
            passedTimeSinceUpdate += tussenTijd;
            if (passedTimeSinceUpdate >= CleaningSpeed / HotelEventManager.HTE_Factor)
            {
                room.State = Room.RoomState.Free;
                Cleaning = false;
                Destination = VasteLocatie;
                Route = simplePath.GetRoute(Position, Destination);
            }

        }
        /// <summary>
        /// searches hotel to find rooms that are dirty.
        /// after finding one the cleaner walks towards it and starts cleaning.
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="RoomQueue"></param>
        public Queue<Room> SearchRoom(Hotel hotel, Queue<Room> RoomQueue)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room r in hotel.Areas.Where(r => r.AreaType == "Room"))
            {
                rooms.Add(r);
            }
            foreach (Room b in rooms.Where(b => b.State == RoomState.Emergency).OrderBy(y => Math.Abs(y.Position.Y - Position.Y)).ThenBy(x => Math.Abs(x.Position.X - Position.X)))
            {
                RoomQueue.Enqueue(b);
            }
            foreach (Room b in rooms.Where(b => b.State == RoomState.Dirty).OrderBy(y => Math.Abs(y.Position.Y - this.Position.Y)).ThenBy(x => Math.Abs(x.Position.X - this.Position.X)))
            {
                RoomQueue.Enqueue(b);
            }
            return RoomQueue;
        }

        /// <summary>
        /// Gets the next room that needs to be cleaned
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="simplePath"></param>
        /// <param name="RoomQueue"></param>
        public void GetRoom(GameTime gameTime, SimplePath simplePath, Queue<Room> RoomQueue)
        {
            if (!Evacuating)
            {

                if (RoomQueue.Count > 0)
                {
                    if (Room != null && !Cleaning)
                    {
                        Room = RoomQueue.First();
                        RoomQueue.Dequeue();
                        Room.State = RoomState.Cleaning;
                        Cleaning = true;
                        Destination = Room.Position;
                        Route = simplePath.GetRoute(Position, Destination);
                    }
                }
                if (Position == VasteLocatie && Room.State == RoomState.Cleaning)
                {
                    Room.State = RoomState.Dirty;
                }
                if (Room != null && Position == Destination && Cleaning == true)
                {
                    Clean(Room, gameTime, simplePath);
                }

            }
        }
        /// <summary>
        /// Returns the cleaner as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string cleaning = Cleaning ? "Yes" : "No";
            string destination = Destination == new Vector2(0, 0) ? "None" : Destination.ToString();
            string room = Room == null ? "None" : Room.ID.ToString();
            return "AssignedRoomID: " + room + "\tPosition\tX: " + Position.X + "\tY: " + Position.Y + "\tDestination\tX: " + Destination.X + "\tY: " + Destination.Y + "\tCurrentlyCleaning: " + cleaning;
        }
    }
}


