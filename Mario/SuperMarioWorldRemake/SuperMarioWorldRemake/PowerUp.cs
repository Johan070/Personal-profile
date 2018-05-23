using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{
    public class PowerUp : MovingGameObject
    {

        public Texture2D MushroomTexture { get; private set; }
        public Texture2D FlowerTexture { get; private set; }
        private int powerUpStage;
        public PowerUp(Texture2D stage1Texture, Texture2D stage2Texture, int posX, int posY, int stage)
        {
            movementDirection = false;
            Position.X = posX;
            Position.Y = posY;
            speed = 0.05f;
            fallSpeed = 0f;
            maxFallSpeed = 0.5f;
            MushroomTexture = stage1Texture;
            FlowerTexture = stage2Texture;
            powerUpStage = stage;
            /*FireFlowerNotYetImplemented
             * if (powerUpStage >= 2)
            {
                ObjectTexture = FlowerTexture;
            }
            else
            {*/
            if (powerUpStage >= 2)
            {
                powerUpStage = 1;
            }
            ObjectTexture = MushroomTexture;
            //}
        }
        /// <summary>
        /// Decides based on the stage if the powerup should move and how it should move
        /// </summary>
        public override void Move()
        {
            if (powerUpStage == 1)
            {
                if (movementDirection == true)
                {
                    Position.X += speed;
                }
                else
                {
                    Position.X -= speed;
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
        }
        /// <summary>
        /// Changes the state of mario if mario picks up a powerup
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="col"></param>
        public override void OnCollision(MovingGameObject entity, GameObject col)
        {
            if (entity is Mario)
            {
                entity.Stage = this.powerUpStage;
                entity.Position.Y -= 1;
                entity.MakeMeNull();
            }
        }
        /// <summary>
        /// Currently powerups have no influence on the score, this may change in future expansions
        /// </summary>
        /// <param name="sc"></param>
        public override void UpdateScoreBoard(ScoreBoard sc)
        {
        }
    }
}
