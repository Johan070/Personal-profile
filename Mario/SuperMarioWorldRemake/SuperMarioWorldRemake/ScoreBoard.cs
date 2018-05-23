using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{
   public class ScoreBoard
    {
        
        public int time;
        public int coinCount;
        public int lives;
        public int score;
        public bool ending;
        public ScoreBoard()
        {
            score = 0;
            lives = 3;
            coinCount = 0;
            time = 600000;
            ending = false;
        }
        /// <summary>
        /// the update time method of scoreboard based on gametime
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateTime(int gameTime)
        {
           
            time -= gameTime;
            checkStatus();
            
        }
        /// <summary>
        /// This method updates the coincount of the scoreboard
        /// </summary>
        public void UpdateCoinCount()
        {
            coinCount += 1;
            if(coinCount == 100)
            {
                coinCount = 0;
                lives++;
            }
        }
        /// <summary>
        /// THis method updates the score by the specified amount
        /// </summary>
        /// <param name="amount"></param>
        public void UpdateScore(int amount)
        {
            score += amount;
        }
        /// <summary>
        /// this method updates the lives by the specified amount
        /// </summary>
        /// <param name="count"></param>
        public void UpdateLives(int count)
        {
            lives += count;
            checkStatus();
            
        }
        /// <summary>
        /// returns true if there are no lives available on the scoreboard, used for ending the game
        /// </summary>
        /// <returns></returns>
        public bool checkStatus()
        {
            if (lives <= 0)
            {
                ending = true;
            }
            if (time <= 0)
            {
                ending = true;
            }
            return ending;
        }

  
        /// <summary>
        /// Draws the score in the selected font on location 140,40
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="font"></param>
        public void Draw(SpriteBatch spriteBatch,SpriteFont font)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font,"Score: " + score.ToString() + " coins: "+coinCount.ToString() + " time: " + ((time/300).ToString() + " lives: " + lives.ToString()), new Vector2(140, 40), Color.DarkSlateBlue);

            spriteBatch.End();
        }
    }
}
