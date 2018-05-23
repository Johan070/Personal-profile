using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{
    public class BanzaiBill : MovingGameObject
    {
        public BanzaiBill(Texture2D texture, int posX, int posY)
        {
            Position.X = posX;
            Position.Y = posY;
            ObjectTexture = texture;
            Rows = 1;
            Columns = 1;
            totalFrames = Rows * Columns;
            speed = 0.10f;
        }
        /// <summary>
        /// The move method of banzaibill
        /// </summary>
        public override void Move()
        {
            MoveLeft();
        }
        /// <summary>
        /// Decreases the position by the speed
        /// </summary>
        public void MoveLeft()
        {//if no collision on left
            int a = (int)Position.X;
            Position.X -= speed;
        }
        /// <summary>
        /// This metod defines what should happen on collision with banzaibill
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="col"></param>
        public override void OnCollision(MovingGameObject entity, GameObject col)
        {
            if (entity is Mario)
            {
                if (entity.BoundingBox.Bottom <= this.BoundingBox.Top + size)
                {
                    entity.SetOnGround(true);
                    entity.forcedJump = true;
                    entity.MakeMeNull();
                }
                else if (entity.BoundingBox.Left <= this.BoundingBox.Left|| entity.BoundingBox.Right >= this.BoundingBox.Right|| entity.BoundingBox.Top > this.BoundingBox.Bottom - size)
                {
                    if (entity.Stage == 0)
                    {
                        entity.Die();
                        entity.lostLife = true;
                        entity.UpdatePosition(1, 20);
                    }
                    else
                    {
                        entity.Stage--;
                    }
                }
            }
        }
        /// <summary>
        /// The scoreboard gets +20 points
        /// </summary>
        /// <param name="scoreboard"></param>
        public override void UpdateScoreBoard(ScoreBoard scoreboard)
        {
            scoreboard.UpdateScore(20);
        }
        /// <summary>
        /// Calls the move function
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Move();
        }
        /// <summary>
        /// Currently unused
        /// </summary>
        /// <param name="level"></param>
        public override void checkCollisions(GameObject[,] level)
        {
            //bill doesnt have to do anything on collision with an instance other than mario yet
        }
    }
}
