using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SuperMarioWorldRemake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake.Tests
{
    [TestClass()]
    public class ScoreBoardTests
    {

        ScoreBoard sc;
        int prevTime;
        int prevCoins;
        int prevScore;
        int prevLives;
        [TestInitialize]
        public void TestInit()
        {

            sc = new ScoreBoard();
            sc.time = 800;
            sc.score = 0;
            sc.lives = 3;
            sc.coinCount = 0;
            prevTime = sc.time;
            prevScore = sc.score;
            prevCoins = sc.coinCount;
            prevLives = sc.lives;
            sc.ending = false;

        }
       
        [TestMethod()]
        public void UpdateTimeTest()
        {
            sc.UpdateTime(300);
            Assert.AreEqual(prevTime - 300, sc.time);
        }

        [TestMethod()]
        public void UpdateCoinCountTest()
        {
            sc.UpdateCoinCount();
            Assert.AreEqual(prevCoins + 1, sc.coinCount);
        }

        [TestMethod()]
        public void UpdateCoinCountTest2()
        {
            sc.coinCount = 99;
            sc.UpdateCoinCount();
            Assert.AreEqual(0, sc.coinCount);
        }

        [TestMethod()]
        public void UpdateScoreTest()
        {
            sc.UpdateScore(40);
            Assert.AreEqual(prevScore + 40, sc.score);
        }

        [TestMethod()]
        public void UpdateLivesTest()
        {
            sc.UpdateLives(40);
            Assert.AreEqual(prevLives + 40, sc.lives);
        }

        [TestMethod()]
        public void checkLivesFalseTest()
        {
           Assert.IsFalse(sc.checkStatus());
        }

        [TestMethod()]
        public void checkLivesTrueTest()
        {
            sc.lives = 0;
            Assert.IsTrue(sc.checkStatus());
        }
        
    }
}