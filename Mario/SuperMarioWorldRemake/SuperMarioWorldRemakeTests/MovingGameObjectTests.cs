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
    public class MovingGameObjectTests
    {
        Koopa koopa = new Koopa(null, null, null, 1, 1);
      
        [TestMethod()]
        public void SetOnGroundTest()
        {
            koopa.onGround = false;
            koopa.SetOnGround(true);
            Assert.IsTrue(koopa.onGround);
        }
        [TestMethod()]
        public void DieTest()
        {
            //no exceptions is good
            koopa.Die();
        }

        [TestMethod()]
        public void MakeMeNullTest()
        {
            //no exceptions
            koopa.MakeMeNull();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            koopa.speed = 1;
            koopa.movementDirection = true;
            koopa.Update(new Microsoft.Xna.Framework.GameTime());
            Assert.AreEqual(0,koopa.Position.X);
        }
        
    }
}