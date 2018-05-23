using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMarioWorldRemake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake.Tests
{
    [TestClass()]
    public class BanzaiBillTests
    {
        BanzaiBill bill = new BanzaiBill(null, 1, 1);
        ScoreBoard sc = new ScoreBoard();
        //Mario mario = new Mario(null, null, null, null, null, null, null, null, null, null, null, null, null, null, 1, 1);
        [TestInitialize]
        public void TestInit()
        {
            bill.speed = 1f;
            sc.score = 20;

        }
        [TestMethod()]
        public void MoveLeftTest()
        {
            bill.MoveLeft();
            Assert.AreEqual(0, bill.Position.X);
        }

        [TestMethod()]
        public void OnCollisionTest()
        {
            //2 blocks covered
            bill.OnCollision(bill, bill);

        }

        [TestMethod()]
        public void UpdateScoreBoardTest()
        {
            bill.UpdateScoreBoard(sc);
            Assert.AreEqual(40, sc.score);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            bill.Update(null);
            Assert.AreEqual(0, bill.Position.X);
        }

        [TestMethod()]
        public void checkCollisionsTest()
        {
            bill.checkCollisions(null);
        }
    }
}