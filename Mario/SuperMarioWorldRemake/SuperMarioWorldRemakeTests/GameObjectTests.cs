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
    public class GameObjectTests
    {
        Tile tile = new Tile(null, 1, 1);

        [TestMethod()]
        public void UpdatePositionTest()
        {

            tile.UpdatePosition(2, 2);
            Assert.AreEqual(2, tile.Position.X);
            Assert.AreEqual(2, tile.Position.Y);
        }

        [TestMethod()]
        public void UpdatePositionXTest()
        {
            tile.UpdatePositionX(2);
            Assert.AreEqual(2, tile.Position.X);
        }

        [TestMethod()]
        public void UpdatePositionYTest()
        {
            tile.UpdatePositionY(2);
            Assert.AreEqual(2, tile.Position.Y);
        }

    }
}