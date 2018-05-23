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
    public class MysteryBlockTests
    {
        MysteryBlock block = new MysteryBlock(null, null,null,null,1, 1);
        Mario mario = new Mario(null, null, null, null, null, null, null, null, null, null, null, null, null, null, 1, 1);
        ScoreBoard sc = new ScoreBoard();
        [TestMethod()]
        public void OnCollisionTest()
        {
            block.OnCollision(mario, block);
        }

        [TestMethod()]
        public void UpdateScoreboardTest()
        {
            //No errors is passed test
            block.UpdateScoreBoard(sc);
        }
        [TestMethod()]
        public void CheckCollisionsTest()
        {
            //No errors is passed test
            block.checkCollisions(null);
        }
    }
}