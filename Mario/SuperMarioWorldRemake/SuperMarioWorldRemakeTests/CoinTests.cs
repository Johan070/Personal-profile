using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMarioWorldRemake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake.Tests
{
    [TestClass()]
    public class CoinTests
    {
        ScoreBoard sc = new ScoreBoard();
        Coin coin = new Coin(null, 1, 1);

        [TestInitialize]
        public void TestInit()
        {

            sc.coinCount = 1;

        }
        [TestMethod()]
        public void CoinCheckCollisionTest()
        {
            coin.checkCollisions(null);
        }

        [TestMethod()]
        public void CoinUpdateScoreboardTest()
        {
            coin.UpdateScoreBoard(sc);
            Assert.AreEqual(2, sc.coinCount);
        }
        [TestMethod()]
        public void CoinUpdateScoreboardTestMaxCoins()
        {
            sc.coinCount = 99;
            coin.UpdateScoreBoard(sc);
            Assert.AreEqual(0, sc.coinCount);
        }
    }

}