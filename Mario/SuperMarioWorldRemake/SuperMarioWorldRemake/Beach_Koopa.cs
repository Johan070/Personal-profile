using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{
    class Beach_Koopa: GameObject
    {
        public Beach_Koopa(Texture2D texture, int posX, int posY)
        {
            Position.X = posX;
            Position.Y = posY;
            ObjectTexture = texture;
            Rows = 2;
            Columns = 2;
            totalFrames = Rows * Columns;
            millisecondsPerFrame = 10000;
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            int width = ObjectTexture.Width / Columns;
            int height = ObjectTexture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            //-size+height omdat marios begin draw positie een paar pixels hoger dan de 16 pixels start en rectangles geen floats accepteren waardoor did na afronding anders fout gaat
            Rectangle destinationRectangle = new Rectangle((int)(size * Position.X) - size + height, (int)(size * Position.Y), width, size);

            spriteBatch.Begin();
            spriteBatch.Draw(ObjectTexture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

        public void checkCollisions(GameObject[,] level)
        {

        }
        public override void OnCollision(MovingGameObject mario, GameObject col)
        {

        }

    }
}
