using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMarioWorldRemake
{
    public class Tile : GameObject
    {
        /// <summary>
        /// the boundingbox of tile
        /// </summary>
        override public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(size * Position.X), (int)(size * Position.Y), ObjectTexture.Width, ObjectTexture.Height);
            }
        }
        public Tile(Texture2D texture, int posX, int posY)
        {
            Position.X = posX;
            Position.Y = posY;
            ObjectTexture = texture;
        }
        /// <summary>
        /// the specific actions tile performs on collision
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
                    
                    if (entity.Stage == 0)
                    {
                        //entity.BoundingBox.Top/16 werkt niet omdat de mario sprite niet uit 16*16 vakjes bestaat
                        entity.UpdatePositionY(BoundingBox.Top / size - entity.ObjectTexture.Height / size);
                    }
                    else
                    {
                        entity.UpdatePositionY(BoundingBox.Top / size - entity.ObjectTexture.Height / size-0.5f);
                    }
                }
                else if (entity.BoundingBox.Right <= this.BoundingBox.Left + size)
                {
                    if(entity.Stage == 0)
                    {
                        entity.UpdatePositionX(BoundingBox.Left / size - entity.ObjectTexture.Width / size);
                    }
                    else
                    {
                        entity.UpdatePositionX(BoundingBox.Left / size - (entity.ObjectTexture.Width-16) / size);
                    }
                    
                }
                else if (entity.BoundingBox.Left >= this.BoundingBox.Right - size)
                {
                    entity.UpdatePositionX(BoundingBox.Right / size);
                }
                else if (entity.BoundingBox.Top <= this.BoundingBox.Bottom - size)
                {
                    entity.UpdatePositionY(BoundingBox.Bottom / size);
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
        public override void UpdateScoreBoard(ScoreBoard sc)
        {
        }
        public override void checkCollisions(GameObject[,] level)
        {
           //tiles dont do anything on collision with anything other than mario yet
        }
    }
}