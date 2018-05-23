using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMarioWorldRemake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioWorldRemake.Tests
{
    [TestClass()]
    public class PowerUpTests
    {
        PowerUp powUp = new PowerUp(null, null, 1, 1, 1);

        
        [TestMethod()]
        public void MoveTest()
        {
            powUp.movementDirection = true;
            powUp.speed = 1;
            powUp.Position.X = 1;
            powUp.Move();
            Assert.AreEqual(2, powUp.Position.X);
        }
        [TestMethod()]
        public void MoveTestDirectionFalse()
        {
            powUp.movementDirection = false;
            powUp.speed = 1;
            powUp.Position.X = 1;
            powUp.Move();
            Assert.AreEqual(0, powUp.Position.X);
        }
        [TestMethod()]
        public void MoveTestOnGroundFalse()
        {
            powUp.movementDirection = false;
            powUp.speed = 1;
            powUp.Position.Y = 1;
            powUp.onGround = false;
            powUp.Move();
            Assert.IsTrue(powUp.Position.Y > 1);
        }
        [TestMethod()]
        public void MoveTestOnGroundTrue()
        {
            powUp.movementDirection = false;
            powUp.speed = 1;
            powUp.Position.Y = 1;
            powUp.onGround = true;
            powUp.Move();
            Assert.IsTrue(powUp.Position.Y == 1);
        }
        [TestMethod()]
        public void UpdateScoreBoard()
        {
            powUp.UpdateScoreBoard(null);
        }
    }
}