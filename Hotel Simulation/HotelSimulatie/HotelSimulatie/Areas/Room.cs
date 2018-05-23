using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HotelSimulatie
{
    /// <summary>
    /// An area where the guest stays when in the hotel, gets dirty after guest leaves and can be cleaned
    /// </summary>
    public class Room : Area
    {
        /// <summary>
        /// the rating of the room
        /// </summary>
        public string Classification { get; set; }
        /// <summary>
        /// the current state of the room
        /// </summary>
        public RoomState State;
        /// <summary>
        /// The posible states of a room
        /// </summary>
        public enum RoomState
        {
            /// <summary>
            /// The room is free and can be booked
            /// </summary>
            Free,
            /// <summary>
            /// The room is currently booked by a customer
            /// </summary>
            Booked,
            /// <summary>
            /// The room needs to be cleaned
            /// </summary>
            Dirty,
            /// <summary>
            /// A cleaner is currently in progress of cleaning the room
            /// </summary>
            Cleaning,
            /// <summary>
            /// The room needs imediate cleaning and a cleaner is on the way
            /// </summary>
            Emergency,
        }
        /// <summary>
        /// Initialize the room propperties
        /// </summary>
        public Room()
        {
            State = RoomState.Free;
        }
        /// <summary>
        /// loads the room specific sprites
        /// </summary>
        /// <param name="Content"></param>
        override public void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            Sprites.Add(1, Content.Load<Texture2D>("Room"));
            Sprites.Add(11, Content.Load<Texture2D>("RoomS"));
            Sprites.Add(0, Content.Load<Texture2D>("Star"));
        }
        /// <summary>
        /// draws the room with the base draw and draws stars on top of it
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            int classification = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(Classification, "[^\\d]")));
            //Draw the stars
            for (int i = classification; i > 0; i--)
            {
                Rectangle destination = new Rectangle((int)(Size.SCALE * (Position.X) + (3 * Size.SCALE / 5)), (int)((Size.SCALE * (-Position.Y)) + (Size.SCALE / 4) + i * (Size.SCALE / 10)), Size.SCALE / 10, Size.SCALE / 10);
                spriteBatch.Draw(Sprites[0], destinationRectangle: destination, color: color);
            }
        }
        /// <summary>
        /// Return the room as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return AreaType + "\t" + "ID: " + ID + "\tStatus: " + State + "\tClassification: " + Classification;
        }
    }
}
