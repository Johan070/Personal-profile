using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie.Tests
{
    [TestClass()]
    public class LayoutReaderTests
    {
        [TestMethod()]
        public void GetAllObjectsTest()
        {
            LayoutReader reader = new LayoutReader();
            var data = reader.GetAllObjects(@"TestResources\Hotel.layout");
            string expected = "Cinema Restaurant Restaurant Room Room Room Room Room Room Room Room Room Room Room Room Room Room Room Room Room Room Room Room Room Room Room Fitness ";
            string actual = "";
            foreach(var area in data)
            {
                actual += area.AreaType + " ";
            }
            Assert.AreEqual(expected, actual);
        }
    }
}