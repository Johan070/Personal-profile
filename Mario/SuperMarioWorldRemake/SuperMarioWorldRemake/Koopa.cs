using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{
    public class Koopa : MovingGameObject
    {
        public ScoreBoard sc2;
        private Texture2D LeftTexture;
        private Texture2D RightTexture;
        private Texture2D ShellTexture;
        //public for testing purposes
        public float shellSpeed;
        private bool gotPoints;
        /// <summary>
        /// The bounding boxes of coopa in both states
        /// </summary>
        public override Rectangle BoundingBox
        {
            get
            {
                if(Stage == 0)
                {
                    return new Rectangle((int)(size * Position.X), (int)(size * Position.Y), ObjectTexture.Width / Columns, ObjectTexture.Height + 16 / Rows);
                }
                else
                {
                    return new Rectangle((int)(size * Position.X), (int)(size * Position.Y), ObjectTexture.Width / Columns, ObjectTexture.Height/ Rows);
                }

            }
        }

        public Koopa(Texture2D texture, Texture2D textureLeft, Texture2D shellTexture, int posX, int posY)
        {
            gotPoints = false;
            Stage = 0;
            sc2 = new ScoreBoard();
            Position.X = posX;
            Position.Y = posY;
            ObjectTexture = texture;
            RightTexture = texture;
            LeftTexture = textureLeft;
            ShellTexture = shellTexture;
            Rows = 1;
            Columns = 2;
            totalFrames = Rows * Columns;
            millisecondsPerFrame = 100;
            speed = 0.05f;
            shellSpeed = 0.33f;
            fallSpeed = 0f;
            maxFallSpeed = 0.5f;
            moving = false;
        }
        /// <summary>
        /// Decides on the moving direction and speed of koopa, also sets textures based on the koopa state
        /// </summary>
        public override void Move()
        {
            if (Stage == 0)
            {
                if (movementDirection)
                {
                    MoveLeft();
                    ObjectTexture = LeftTexture;
                }
                else
                {
                    MoveRight();
                    ObjectTexture = RightTexture;
                }
            }
            else
            {
                ObjectTexture = ShellTexture;
                if (moving)
                {
                    if (movementDirection)
                    {
                        MoveLeft();
                    }
                    else
                    {
                        MoveRight();
                    }
                }
                else
                {
                    currentFrame = 0;
                }
            }
            if (this.onGround == false)
            {
                Fall();
            }
            else
            {
                fallSpeed = 0;
            }
        }
      /// <summary>
      /// Updates mario's score if it is not updated yet
      /// </summary>
      /// <param name="sc"></param>
        public override void UpdateScoreBoard(ScoreBoard sc)
        {
            if (gotPoints == false)
            {
                sc.UpdateScore(20);
                gotPoints = true;
            }
        }
        /// <summary>
        /// The actions on collision between the entity and the colliding object col are excecuted here
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
                    if (this.Stage == 0)
                    {
                        RightTexture = ShellTexture;
                        LeftTexture = ShellTexture;
                        Stage = 1;
                        Position.Y++;
                        Columns = 4;
                    }
                    // KoopaShell shell = new KoopaShell(col.ObjectTexture, (int)Position.X, (int)Position.Y - 1);
                    //level[(int)shell.Position.X, (int)shell.Position.Y] = shell;
                    else
                    {
                        moving = !moving;
                        //entity.DoIllegalStuff(null);
                    }
                }
                else if (entity.BoundingBox.Left <= this.BoundingBox.Left)
                {
                    if (Stage == 0 || moving)
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
                    else
                    {
                        movementDirection = false;
                        moving = true;
                    }
                }
                else if (entity.BoundingBox.Right >= this.BoundingBox.Right)
                {
                    if (Stage == 0 || moving)
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
                    else
                    {
                        movementDirection = true;
                        moving = true;
                    }
                }
            }
            else if (entity is Koopa)
            {
                if (entity.Stage == 1 && entity.moving)
                {
                    if (entity.BoundingBox.Bottom <= this.BoundingBox.Top + size)
                    {
                        entity.SetOnGround(true);
                        entity.UpdatePositionY(entity.BoundingBox.Top / 16);
                    }
                    else if (entity.BoundingBox.Right <= this.BoundingBox.Left + size)
                    {
                        entity.MakeMeNull();
                    }
                    else if (entity.BoundingBox.Left >= this.BoundingBox.Right - size)
                    {
                        entity.MakeMeNull();
                    }
                }
                else
                {
                    if (entity.BoundingBox.Bottom <= this.BoundingBox.Top + size)
                    {
                        entity.SetOnGround(true);
                        entity.UpdatePositionY(entity.BoundingBox.Top / 16);
                    }

                    else if (entity.BoundingBox.Right <= this.BoundingBox.Left + size)
                    {
                        entity.movementDirection = true;
                    }
                    else if (entity.BoundingBox.Left >= this.BoundingBox.Right - size)
                    {
                        entity.movementDirection = false;
                    }
                }
            }
        }
        /// <summary>
        /// The movement if the direction is right
        /// </summary>
        private void MoveRight()
        {//if no collision on right
            if (Stage == 0)
            {
                Position.X += speed;
            }
            else
            {
                Position.X += shellSpeed;
            }
        }

        /// <summary>
        /// the movement if the direction is left
        /// </summary>
        private void MoveLeft()
        {//if no collision on left
            if (Stage == 0)
            {
                Position.X -= speed;
            }
            else
            {
                Position.X -= shellSpeed;
            }
        }
    }
}
