using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SuperMarioWorldRemake
{
    public class Finish : GameObject
    {
        /// <summary>
        /// Boolean that shows if the finish has been touched by mario
        /// </summary>
        public bool touched;
        public Finish(Texture2D texture, int posX, int posY)
        {
            Position.X = posX;
            Position.Y = posY;
            ObjectTexture = texture;
        }
        /// <summary>
        /// Finish currently has no reason to check collisions, in future it may
        /// </summary>
        /// <param name="level"></param>
        public override void checkCollisions(GameObject[,] level)
        {
            
        }
        /// <summary>
        /// If the entity colliding with the finish is mario, set touched to true
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="col"></param>
        public override void OnCollision(MovingGameObject entity, GameObject col)
        {
            if (entity is Mario)
            {
                touched = true;
            }
        }
        /// <summary>
        /// Currently there are no points bound to finishing
        /// </summary>
        /// <param name="sc"></param>
        public override void UpdateScoreBoard(ScoreBoard sc)
        {
        }
    }
}
