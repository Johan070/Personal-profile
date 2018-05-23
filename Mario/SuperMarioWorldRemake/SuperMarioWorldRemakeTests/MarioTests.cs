using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SuperMarioWorldRemake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake.Tests
{
    [TestClass()]
    public class MarioTests
    {
        Mario mario = new Mario(null, null, null, null, null, null, null, null, null, null, null, null, null, null, 1, 1);

        [TestMethod()]
        public void DieTest()
        {
            mario.sc.lives = 3;
            mario.Die();
            Assert.AreEqual(2, mario.sc.lives);
        }

        [TestMethod()]
        public void MoveTest()
        {
            //Empty tests are to make sure methods don't contain exceptions
            mario.Move();
        }
        [TestMethod()]
        public void GetPositionTest()
        {
            mario.Position = new Vector2(1, 1);
            Assert.AreEqual(new Vector2(16, 16), mario.GetPosition());
        }

        [TestMethod()]
        public void OnCollisionTest()
        {
            mario.OnCollision(mario, mario);
        }
        [TestMethod()]
        public void UpdateScoreBoardTest()
        {
            mario.UpdateScoreBoard(null);
        }
        [TestMethod()]
        public void JumpTest()
        {
            float prev = mario.Position.Y;
            mario.jumpSpeed = -10;
            mario.maxJumpSpeed = -100;
            mario.Jump();
            mario.Position.Y += mario.jumpSpeed;
            Assert.IsTrue(prev > mario.Position.Y);
        }
        [TestMethod()]
        public void FallTest()
        {
            float prev = mario.Position.Y;
            mario.fallSpeed = 1;
            mario.maxFallSpeed = 2;
            mario.Fall();
            mario.Position.Y += mario.fallSpeed;
            Assert.IsTrue(prev < mario.Position.Y);
        }
        [TestMethod()]
        public void FallTestStartFallSpeed0()
        {
            float prev = mario.Position.Y;
            mario.fallSpeed = 0;
            mario.maxFallSpeed = 1;
            mario.Fall();
            mario.Position.Y += mario.fallSpeed;
            Assert.IsTrue(prev < mario.Position.Y);
        }
        [TestMethod()]
        public void FallTestSpeedGreaterMaxSpeed()
        {
            mario.Position.Y = 27;
            mario.sc.lives = 8;
            mario.Fall();
            mario.Position.Y += mario.fallSpeed;
            Assert.AreEqual(7, mario.sc.lives);
        }
        [TestMethod()]
        public void MoveLeftTest()
        {
            float prev = mario.Position.X;
            mario.speed = -0.1f;
            mario.MoveLeft();
            mario.Position.X += mario.speed;
            Assert.IsTrue(prev > mario.Position.X);
        }
        [TestMethod()]
        public void MoveRightTest()
        {
            float prev = mario.Position.X;
            mario.speed = 0.1f;
            mario.MoveRight();
            mario.Position.X += mario.speed;
            Assert.IsTrue(prev < mario.Position.X);
        }
    }
}