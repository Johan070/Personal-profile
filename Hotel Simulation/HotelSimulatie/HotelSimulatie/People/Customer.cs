using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HotelEvents;
using HotelSimulatie.Utility;
using Microsoft.Xna.Framework;

namespace HotelSimulatie.People
{
    /// <summary>
    /// A person who can book a room
    /// </summary>
    public class Customer : Person
    {
        /// <summary>
        /// The rating of the preffered room
        /// </summary>
        public int Preferance { get; set; }
        /// <summary>
        /// The assigned room
        /// </summary>
        public Room Room { get; set; }
        private float _passedTimeSinceUpdate;
        /// <summary>
        /// The time the customer needs to wait
        /// </summary>
        public int WaitingTime { get; set; }

        /// <summary>
        /// Initialize the customer
        /// </summary>
        public Customer()
        {
            _passedTimeSinceUpdate = 0;
            WaitingTime = int.MaxValue;
        }
        /// <summary>
        /// Load the sprite of customer
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content)
        {
            Sprite = Content.Load<Texture2D>("Klant");
        }
        /// <summary>
        /// after customer is done waiting he returns to his room.
        /// if he wants to enter the restaurant and it's full he also returns to his room.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="simplePath"></param>
        /// <param name="hotel"></param>
        public void ReturnToRoom(GameTime gameTime, SimplePath simplePath, Hotel hotel)
        {
            if (Room != null && Position != Room.Position && Position == Destination)
            {
                Restaurant restaurant = (Restaurant)hotel.Areas.Where(a => a.AreaType == "Restaurant").FirstOrDefault();
                float tussenTijd = gameTime.ElapsedGameTime.Milliseconds;
                tussenTijd /= 1000;
                _passedTimeSinceUpdate += tussenTijd;
                if (_passedTimeSinceUpdate >= WaitingTime / HotelEventManager.HTE_Factor)
                {
                    Destination = Room.Position;
                    Route = simplePath.GetRoute(Position, Destination);
                    if (Position == restaurant.Position)
                    {
                        restaurant.HuidigeBezetting--;
                    }
                }
                if (Position == restaurant.Position && restaurant.Capacity < restaurant.HuidigeBezetting)
                {
                    Destination = Room.Position;
                    Route = simplePath.GetRoute(Position, Destination);
                    restaurant.HuidigeBezetting--;
                }
            }
        }
        /// <summary>
        /// customer checks if the cinema he wants to enter has started. 
        /// if the cinema is already running a movie before he has entered he returns to his room.
        /// </summary>
        /// <param name="simplePath"></param>
        /// <param name="cinema"></param>
        public void CheckCinema(SimplePath simplePath, Cinema cinema)
        {
            if (cinema != null)
            {
                if (Destination == cinema.Position && Position != cinema.Position && cinema.Started)
                {
                    Destination = new Vector2(cinema.Position.X - 1f, cinema.Position.Y);
                    Route = simplePath.GetRoute(Position, Destination);
                }
                if (Destination.X == cinema.Position.X - 1f && Position != cinema.Position && cinema.Started)
                {
                    WaitingTime = int.MaxValue;
                }
            }
        }
        /// <summary>
        /// Return the important fields of customer as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ID: " + ID + "\tRoomID: " + Room.ID + "\tPosition\tX: " + Position.X + "\tY: " + Position.Y + "\tDestination\tX: " + Destination.X + "\tY: " + Destination.Y;
        }
    }
}
