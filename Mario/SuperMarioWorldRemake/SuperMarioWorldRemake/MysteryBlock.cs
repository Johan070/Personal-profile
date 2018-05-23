using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{
    public class MysteryBlock : GameObject
    {
        public Texture2D MushroomTexture { get; private set; }
        public Texture2D FlowerTexture { get; private set; }
        public Texture2D usedMysteryBlocktexture { get; private set; }
        private bool _enabled;
        public MysteryBlock(Texture2D texture, Texture2D contentMushroom, Texture2D contentFlower, Texture2D usedMyseryBlockTexture, int posX, int posY)
        {
            usedMysteryBlocktexture = usedMyseryBlockTexture;
            _enabled = true;
            MushroomTexture = contentMushroom;
            FlowerTexture = contentFlower;
            Position.X = posX;
            Position.Y = posY;
            ObjectTexture = texture;
            Rows = 1;
            Columns = 4;
            totalFrames = Rows * Columns;
            millisecondsPerFrame = 100;
        }
        /// <summary>
        /// Does nothing because everyting is handled in the second on collision, but future edits may use this to specify actions that should happen on collision and dont change a gameobject in the level array
        /// </summary>
        /// <param name="mario"></param>
        /// <param name="col"></param>
        public override void OnCollision(MovingGameObject mario, GameObject col)
        {
        }
        /// <summary>
        /// Decides what action should happen based on the form of the collision
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="col"></param>
        /// <param name="level"></param>
        public override void OnCollision(MovingGameObject entity, GameObject col, GameObject[,] level)
        {
            if (entity is Mario)
            {
                if (entity.BoundingBox.Bottom <= this.BoundingBox.Top + size)
                {
                    entity.SetOnGround(true);
                    if (entity.Stage == 0)
                    {
                        entity.UpdatePositionY(BoundingBox.Top / size - entity.ObjectTexture.Height / size);
                    }
                    else
                    {
                        entity.UpdatePositionY(BoundingBox.Top / size - entity.ObjectTexture.Height / size - 0.5f);
                    }
                }
                else if (entity.BoundingBox.Top > this.BoundingBox.Bottom - size)
                {
                    entity.UpdatePositionY(BoundingBox.Bottom / size + 1);
                    if (_enabled == true)
                    {
                        PowerUp item = new PowerUp(MushroomTexture, FlowerTexture, (int)Position.X, (int)Position.Y - 1, entity.Stage + 1);

                        level[(int)item.Position.X - 1, (int)item.Position.Y - 1] = item;
                        _enabled = false;

                        ObjectTexture = usedMysteryBlocktexture;
                        totalFrames = 0;
                        Columns = 1;
                    }
                }
                else if (entity.BoundingBox.Left <= this.BoundingBox.Left)
                {
                    entity.UpdatePositionX(BoundingBox.Left / size - entity.ObjectTexture.Width / size);
                }
                else if (entity.BoundingBox.Right >= this.BoundingBox.Right)
                {
                    entity.UpdatePositionX(BoundingBox.Right / size);
                }
            }

            else if (entity is PowerUp)
            {
                if (entity.BoundingBox.Bottom <= this.BoundingBox.Top + size)
                {
                    entity.SetOnGround(true);
                    entity.UpdatePositionY(entity.BoundingBox.Top / 16);
                }
            }
            else if (entity is Koopa)
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

        }
    }
}

