using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HotelSimulatie.Utility;

namespace HotelSimulatie.Areas
{
    /// <summary>
    /// An area in wich a guest can use to travel up and down
    /// </summary>
    public class Stairs : TransportationArea
    {
        /// <summary>
        /// Initialize the stairs properties
        /// </summary>
        public Stairs()
        {
            AreaType = "Stairs";
            Weight = 5;
        }
        /// <summary>
        /// load the stairs sprites
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content)
        {
            Sprites.Add(1, Content.Load<Texture2D>("StairsBottom"));
            Sprites.Add(4, Content.Load<Texture2D>("Stairs"));
            Sprites.Add(7, Content.Load<Texture2D>("StairsTop"));
        }
        /// <summary>
        /// Returns the stairs as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return AreaType + "\tSpeed: " + (6-Weight);
        }
    }
}
