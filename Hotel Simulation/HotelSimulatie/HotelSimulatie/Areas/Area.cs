using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HotelSimulatie.Utility;

namespace HotelSimulatie
{
    /// <summary>
    /// An area in the hotel
    /// </summary>
    public abstract class Area : IArea
    {
        /// <summary>
        /// The color of the area, default is white
        /// </summary>
        protected Color color;
        /// <summary>
        /// The identifier of the area
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// the type of the area
        /// </summary>
        public string AreaType { get; set; }
        /// <summary>
        /// The position of the area
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The width and height of the area
        /// </summary>
        public Vector2 Dimension { get; set; }
        /// <summary>
        /// A dictionary with all the sprites of the area
        /// </summary>
        public Dictionary<int, Texture2D> Sprites { get; set; }

        /// <summary>
        /// Initialize the area
        /// </summary>
        public Area()
        {
            color = Color.White;
            Sprites = new Dictionary<int, Texture2D>();
        }
        /// <summary>
        /// Draw the area and select sprites for each positon based on the dimensions of the area
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int dx = (int)Dimension.X;
            int dy = (int)Dimension.Y;

            for (int i = 0; i < dx; i++)
            {
                for (int j = 0; j < dy; j++)
                {
                    Rectangle destination = new Rectangle((int)(Size.SCALE * (Position.X + i)), (int)(Size.SCALE * (-Position.Y - j)), Size.SCALE, Size.SCALE);
                    //Get the ID of the sprite on the selected position
                    int spriteID = GetSpriteId(i, j, dx, dy);

                    if (Sprites[spriteID] != null)
                    {
                        spriteBatch.Draw(Sprites[spriteID], destinationRectangle: destination, color: color);
                    }
                }
            }
        }
        /// <summary>
        /// Selects the correct spriteID from the sprite dictionary
        /// Bottom left is 1, bottom right is 3(Numpad like ordering in the dictionary)
        /// </summary>
        /// <param name="x">the current x coordinat of the sprite</param>
        /// <param name="y">the current y coordinat of the sprite</param>
        /// <param name="dx">Max x dimension</param>
        /// <param name="dy">Max y dimension</param>
        /// <returns></returns>
        public int GetSpriteId(int x, int y, int dx, int dy)
        {
            int sprite = 0;

            //adds the number for resp the bottom, middle and top sprite
            if (y == 0)
            {
                sprite += 0;
            }
            else if (y > 0 && y < dy - 1)
            {
                sprite += 3;
            }
            else if (y == dy - 1)
            {
                sprite += 6;
            }

            //adds the number for respectively the left, middle and right sprite
            if (x == 0)
            {
                sprite += 1;
            }
            else if (x > 0 && x < dx - 1)
            {
                sprite += 2;
            }
            else if (x == dx - 1)
            {
                sprite += 3;
            }

            //selects alternate sprite template if the height > 1
            if (dy == 1)
            {
                sprite += 10;
            }
            return sprite;
        }
        /// <summary>
        /// loads all default area sprites
        /// </summary>
        /// <param name="Content"></param>
        public virtual void LoadContent(ContentManager Content)
        {
            Sprites.Add(2, Content.Load<Texture2D>("BottomMiddle"));
            Sprites.Add(3, Content.Load<Texture2D>("BottomRight"));
            Sprites.Add(4, Content.Load<Texture2D>("MiddleLeft"));
            Sprites.Add(5, Content.Load<Texture2D>("MiddleMiddle"));
            Sprites.Add(6, Content.Load<Texture2D>("MiddleRight"));
            Sprites.Add(7, Content.Load<Texture2D>("TopLeft"));
            Sprites.Add(8, Content.Load<Texture2D>("TopMiddle"));
            Sprites.Add(9, Content.Load<Texture2D>("TopRight"));
            Sprites.Add(12, Content.Load<Texture2D>("MiddleS"));
            Sprites.Add(13, Content.Load<Texture2D>("RightS"));
        }
        /// <summary>
        /// Return the area as a string
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();
    }
}
