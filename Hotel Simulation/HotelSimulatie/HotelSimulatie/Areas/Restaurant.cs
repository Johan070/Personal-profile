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
    /// An area that gets used by customers for eating
    /// </summary>
    public class Restaurant : Area
    {
        /// <summary>
        /// The amount of people allowed in the area
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// The current people in the area
        /// </summary>
        public int HuidigeBezetting { get; set; }
        /// <summary>
        /// The speed with wich people eat
        /// </summary>
        public int EatSpeed { get; set; }
        /// <summary>
        /// Initialize the properties
        /// </summary>
        public Restaurant()
        {
            EatSpeed = 10;
            Capacity = 5;
            HuidigeBezetting = 0;
            color = Color.MonoGameOrange;
        }
        /// <summary>
        /// load the restaurant specific sprites
        /// </summary>
        /// <param name="Content"></param>
        override public void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            Sprites.Add(1, Content.Load<Texture2D>("Restaurant"));
            Sprites.Add(11, Content.Load<Texture2D>("RestaurantS"));
        }
        /// <summary>
        /// return the restaurant as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()

        {

            return AreaType + "\t" + "ID: " + ID+ "\tCapacity: " + Capacity+ "\tCurrentCustomers: " + HuidigeBezetting;
        }
    }
}
