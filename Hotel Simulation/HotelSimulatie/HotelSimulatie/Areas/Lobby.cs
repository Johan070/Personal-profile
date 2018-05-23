using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulatie.Areas
{
    /// <summary>
    /// The spawn location of the people
    /// </summary>
    public class Lobby : Area
    {
        /// <summary>
        /// Initialize the lobby
        /// </summary>
        public Lobby()
        {
            Position = new Vector2(1, 0);
        }
       
        /// <summary>
        /// load the lobby sprites
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content)
        {
            Sprites.Add(11, Content.Load<Texture2D>("LobbyDesk"));
            Sprites.Add(12, Content.Load<Texture2D>("LobbyBank"));
            Sprites.Add(13, Content.Load<Texture2D>("LobbyBank"));
        }
        /// <summary>
        /// return the lobby as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()

        {
            return AreaType + " ";
        }
    }
}
