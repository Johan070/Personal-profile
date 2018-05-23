using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Remoting.Contexts;

namespace SuperMarioWorldRemake
{
    public abstract class GameObject
    {
        protected int size;
        protected float gravity;
        public Vector2 Position;
        public int currentFrame;
        protected int totalFrames;
        protected int timeSinceLastFrame;
        protected int millisecondsPerFrame;
        /// <summary>
        /// The boundingbox of the gameobject
        /// </summary>
        public virtual Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(size * Position.X), (int)(size * Position.Y) + 1, ObjectTexture.Width / Columns, ObjectTexture.Height / Rows);
            }
        }
        public Texture2D ObjectTexture { get; set; }
        protected int Rows { get; set; }
        protected int Columns { get; set; }
        public GameObject()
        {
            size = 16;
            //7.7 is distance in meters, 193 is distance in pixels, 6.32 is the gravity based on source:http://hypertextbook.com/facts/2007/mariogravity.shtml;
            gravity = 7.7f * 6.32f / 193;
            timeSinceLastFrame = 0;
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            timeSinceLastFrame = 0;
        }
        /// <summary>
        /// Checks if this and obj are colliding
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool Colliding(GameObject obj)
        {
            bool col = false;
            if (BoundingBox.Intersects(obj.BoundingBox))
            {
                col = true;
            }
            return col;
        }
        /// <summary>
        /// Updates the position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void UpdatePosition(float x, float y)
        {
            UpdatePositionX(x);
            UpdatePositionY(y);
        }
        /// <summary>
        /// Updates the position.X
        /// </summary>
        /// <param name="x"></param>
        public void UpdatePositionX(float x)
        {
            Position.X = x;
        }
        /// <summary>
        /// Updates the position.Y
        /// </summary>
        /// <param name="y"></param>
        public void UpdatePositionY(float y)
        {
            Position.Y = y;
        }
        /// <summary>
        /// Updates the frames of an object to select the correct frame in the draw function
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                currentFrame++;
                timeSinceLastFrame = 0;
                if (currentFrame >= totalFrames)
                {
                    currentFrame = 0;
                }
            }
        }
        /// <summary>
        /// The main draw method of gameobject, takes an object texture and selects the specified sprite out of the spritesheet based on the rows and columns. Thesprite gets drawn in a rectangle
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int width = ObjectTexture.Width / Columns;
            int height = ObjectTexture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)(size * Position.X), (int)(size * Position.Y), width, height);
            spriteBatch.Draw(ObjectTexture, destinationRectangle, sourceRectangle, Color.White);
        }

        /// <summary>
        /// This is a method that would be very convenient in the expansion of the project, not yet used
        /// </summary>
        /// <param name="obj1"></param>
        /// <returns></returns>
        protected string GetCollisionLocation(GameObject obj1)
        {
            if (obj1.BoundingBox.Bottom <= this.BoundingBox.Top + size)
            {
                return "Top";
            }
            else if (obj1.BoundingBox.Top > this.BoundingBox.Bottom - size)
            {
                return "Bottom";
            }
            else if (obj1.BoundingBox.Right >= this.BoundingBox.Right)
            {
                return "Right";
            }
            else if (obj1.BoundingBox.Left <= this.BoundingBox.Left)
            {
                return "Left";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// The OnCollision method of a gameobject specifies the actions that should happen on a collision with another object
        /// </summary>
        /// <param name="obj"></param>
        public abstract void OnCollision(MovingGameObject mario, GameObject col);
        /// <summary>
        /// the updateScoreBoard method specifies what should happen with the scoreboard, this method should only get called when certain conditions for updating the score are met
        /// </summary>
        /// <param name="scoreboard"></param>
        public abstract void UpdateScoreBoard(ScoreBoard scoreboard);
        /// <summary>
        /// This is a specific OnCollision action that is mainly used to change the specified gameobject in the level array to eiter a different object or to null.
        /// </summary>
        /// <param name="mario"></param>
        /// <param name="col"></param>
        /// <param name="level"></param>
        public virtual void OnCollision(MovingGameObject mario, GameObject col, GameObject[,] level)
        {
        }
        /// <summary>
        /// Some but not all game objects need to check collision, in future stages there may be special collision interactions between some blocks and platforms
        /// </summary>
        /// <param name="level"></param>
        public abstract void checkCollisions(GameObject[,] level);
    }
}