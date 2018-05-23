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
    public class FinishTests
    {
        Mario mario = new Mario(null, null, null, null, null, null, null, null, null, null, null, null, null, null, 1, 1);
        Finish finish = new Finish(null, 1, 1);
        ScoreBoard sc = new ScoreBoard();
        [TestMethod()]
        public void OnCollisionTest()
        {
            finish.OnCollision(mario, finish);
            Assert.IsTrue(finish.touched);
        }
        [TestMethod()]
        public void UpdateScoreBoardTest()
        {
            finish.UpdateScoreBoard(sc);
        }
        [TestMethod()]
        public void checkCollisionsTest()
        {
            finish.checkCollisions(null);
        }
    }
}