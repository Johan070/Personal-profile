using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulatie
{
    /// <summary>
    /// Placeholder area for undifined areas in the JSON
    /// </summary>
    public class UndifinedArea : Area
    {
        /// <summary>
        /// Initialize the properties
        /// </summary>
        public UndifinedArea()
        {
            AreaType = "UndifinedArea";
        }
        /// <summary>
        /// loads the sprites for undifined areas
        /// </summary>
        /// <param name="Content"></param>
       override public void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            Sprites.Add(1, Content.Load<Texture2D>("UndifinedArea"));
            Sprites.Add(11, Content.Load<Texture2D>("UndifinedArea"));
        }
        /// <summary>
        /// returns the areaType as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return AreaType + " ";
        }
    }
}
