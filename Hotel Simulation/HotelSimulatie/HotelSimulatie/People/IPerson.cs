using HotelSimulatie.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie.People
{
    /// <summary>
    /// The interface with the properties from person
    /// </summary>
    public interface IPerson
    {
        /// <summary>
        /// The movespeed of the person
        /// </summary>
        float WalkSpeed { get; set; }
        /// <summary>
        /// The next location the person wants to go to
        /// </summary>
        Vector2 Destination { get; set; }
        /// <summary>
        /// The current position of the person
        /// </summary>
        Vector2 Position { get; set; }
        /// <summary>
        /// The shortest route from the current position to the destination
        /// </summary>
        Stack<Node> Route { get; set; }
        /// <summary>
        /// The sprite of the person
        /// </summary>
        Texture2D Sprite { get; set; }
        /// <summary>
        /// Load the sprites
        /// </summary>
        /// <param name="Content"></param>
        void LoadContent(ContentManager Content);
        /// <summary>
        /// Draw the person on the current position
        /// </summary>
        /// <param name="spriteBatch"></param>
        void Draw(SpriteBatch spriteBatch);
        /// <summary>
        /// Move the person in the direction to the next node
        /// </summary>
        void Move();
        /// <summary>
        /// Update the person
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);
        /// <summary>
        /// On HTE Factor change round the current position to a valid position
        /// </summary>
        void RoundPosition();
    }
}
