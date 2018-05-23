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
    /// A fitness area where customers can go
    /// </summary>
    public class Fitness : Area
    {
        /// <summary>
        /// initialize fitness
        /// </summary>
        public Fitness()
        {
            color = Color.GreenYellow;
        }
        /// <summary>
        /// load the fitness specific sprites
        /// </summary>
        /// <param name="Content"></param>
        override public void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            Sprites.Add(1, Content.Load<Texture2D>("Fitness"));
            Sprites.Add(11, Content.Load<Texture2D>("FitnessS"));
        }
        /// <summary>
        /// return the fitness as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()

        {
            return AreaType + "\t" + "ID: " + ID;
        }
    }
}
