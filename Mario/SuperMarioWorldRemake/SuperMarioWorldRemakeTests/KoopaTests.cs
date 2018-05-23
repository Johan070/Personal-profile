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
    public class KoopaTests
    {
        Koopa koopa = new Koopa(null,null, null, 1, 1);
        ScoreBoard sc = new ScoreBoard();
        [TestMethod()]
        public void MoveTest()
        {
            koopa.movementDirection = false;
            koopa.Position.X = 1;
            koopa.Stage = 0;
            koopa.speed = 1;
            koopa.Move();
            Assert.AreEqual(2, koopa.Position.X);
        }
        [TestMethod()]
        public void MoveTestLeft()
        {
            koopa.movementDirection = true;
            koopa.Position.X = 1;
            koopa.Stage = 0;
            koopa.speed = 1;
            koopa.Move();
            Assert.AreEqual(0, koopa.Position.X);
        }
        [TestMethod()]
        public void MoveTestStage1()
        {
            koopa.movementDirection = false;
            koopa.Position.X = 1;
            koopa.Stage = 1;
            koopa.shellSpeed = 3;
            koopa.moving = true;
            koopa.Move();
            Assert.AreEqual(4, koopa.Position.X);
        }
        [TestMethod()]
        public void MoveTestLeftStage1()
        {
            koopa.movementDirection = true;
            koopa.Position.X = 8;
            koopa.Stage = 1;
            koopa.shellSpeed = 3;
            koopa.moving = true;
            koopa.Move();
            Assert.AreEqual(5, koopa.Position.X);
        }

   

        [TestMethod()]
        public void UpdateScoreBoardTest()
        {
            sc.score = 34;
            koopa.UpdateScoreBoard(sc);
            Assert.AreEqual(54, sc.score);
        }

    }
}