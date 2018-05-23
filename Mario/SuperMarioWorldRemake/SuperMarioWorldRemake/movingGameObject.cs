using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{

    public abstract class MovingGameObject : GameObject
    {
        //made public for testing purposes
        public float speed;
        //made public for testing purposes
        public float fallSpeed;
        public float maxFallSpeed;
        protected float maxSpeed;
        //onground is public for testing purposes
        public bool onGround;
        public bool movementDirection;
        public bool forcedJump;
        public bool lostLife;
        public bool moving;
        public int Stage { get;  set; }
        public abstract void Move();
        protected GameObject makeMeNull;
        public MovingGameObject()
        {
            onGround = false;
            movementDirection = true;
        }
        /// <summary>
        /// Changes the onGround state of the game object,the on ground state influences the collision behavior of the movinggameobject with the ground, and in mario's case enables jumping
        /// </summary>
        /// <param name="set"></param>
        public void SetOnGround(bool set)
        {
            onGround = set;
        }
        /// <summary>
        /// All movinggameobjects can have a die function, but dont have to. The die function specifies what happens to a moving game object on death
        /// </summary>
        public virtual void Die()
        {
        }
        /// <summary>
        /// can be used to make game the location of the array that refferences the gameobject refference null, this makes the gameobject dissapear from the level
        /// </summary>
        public void MakeMeNull()
        {
            makeMeNull = null;
        }
        /// <summary>
        /// Checks collision between the current movinggameobject and all objects in the level array
        /// </summary>
        /// <param name="level"></param>
        public override void checkCollisions(GameObject[,] level)
        {
            //higer numbers to make sure large bounding boxes get checked
            //max boundary can be +0 if moving objects work correctly
            int minBoundaryX = (int)Math.Floor(Position.X) - 10;
            int minBoundaryY = (int)Math.Floor(Position.Y) - 10;
            int maxBoundaryX = (int)Math.Floor(Position.X) + 100;
            int maxBoundaryY = (int)Math.Floor(Position.Y) + 100;
            bool noCollisions = true;
            for (int i = minBoundaryX; i <= maxBoundaryX; i++)
            {
                for (int j = minBoundaryY; j <= maxBoundaryY; j++)
                {
                    if (i >= 0 && i <= level.GetLength(0) - 1 && j >= 0 && j <= level.GetLength(1) - 1)
                        if (level[i, j] != null)
                        {
                            bool col = level[i, j].Colliding(this);
                            if (col == true && level[i, j] != this)
                            {
                                makeMeNull = level[i, j];
                                level[i, j].OnCollision(this, level[i, j]);
                                level[i,j].OnCollision(this, level[i, j], level);
                                level[i, j] = makeMeNull;
                                noCollisions = false;
                            }
                        }
                }
            }
            if (noCollisions)
            {
                onGround = false;
            }
        }
        /// <summary>
        /// Updates the frametimes based on the gametime and uses the move method for moving game object
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Move();
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                currentFrame++;
                timeSinceLastFrame = 0;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }
        }
        /// <summary>
        /// Updates the fallspeed of movinggameobjects
        /// </summary>
        //made public for testing purposes
        public virtual void Fall()
        {
            if (fallSpeed < maxFallSpeed)
            {
                //maths
                fallSpeed += 0.01f + fallSpeed * 0.04f;
            }
            Position.Y += fallSpeed;
        }
    }
}
