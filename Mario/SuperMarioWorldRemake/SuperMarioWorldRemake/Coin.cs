using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{
   public class Coin:GameObject
    {
        public Coin(Texture2D texture, int posX, int posY)
        {
            Position.X = posX;
            Position.Y = posY;
            ObjectTexture = texture;
            Rows = 1;
            Columns = 4;
            totalFrames = Rows * Columns;
            millisecondsPerFrame = 100;
        }
        /// <summary>
        /// On collision between a moving game object and a game object it sets a variable in mario to null
        /// </summary>
        /// <param name="mario"></param>
        /// <param name="col"></param>
        public override void OnCollision(MovingGameObject entity,GameObject col)
        {
            if(entity is Mario)
            entity.MakeMeNull();
        }
        /// <summary>
        /// Updates the scoreboard coincoint with a value specified in the updatecoincount method
        /// </summary>
        /// <param name="scoreboard"></param>
        public override void UpdateScoreBoard(ScoreBoard scoreboard)
        {
            scoreboard.UpdateCoinCount();
        }
        public override void checkCollisions(GameObject[,] level)
        {
           //coin doesnt have to do anything on collision with anything other than mario yet
        }
    }
}