using HotelSimulatie.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    /// <summary>
    /// The interface with area properties
    /// </summary>
    public interface IArea
    {
        /// <summary>
        /// the identifier of the area
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// The Type of the area
        /// </summary>
        string AreaType { get; set; }
        /// <summary>
        /// The position of the area
        /// </summary>
        Vector2 Position { get; set; }
        /// <summary>
        /// The width and height of the area
        /// </summary>
        Vector2 Dimension { get; set; }
        /// <summary>
        /// Load all the content for the area
        /// </summary>
        /// <param name="Content"></param>
        void LoadContent(ContentManager Content);
        /// <summary>
        /// Draw the area
        /// </summary>
        /// <param name="spriteBatch"></param>
        void Draw(SpriteBatch spriteBatch);
    }
}
