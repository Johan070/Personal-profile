
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioWorldRemake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake.Tests
{
    [TestClass()]
    public class TileTests
    {
        Tile tile = new Tile(null, 2, 2);

        [TestMethod()]
        public void UpdateScoreBoardTest()
        {
            tile.UpdateScoreBoard(null);
        }

        [TestMethod()]
        public void checkCollisionsTest()
        {
            tile.checkCollisions(null);
        }
    }
}