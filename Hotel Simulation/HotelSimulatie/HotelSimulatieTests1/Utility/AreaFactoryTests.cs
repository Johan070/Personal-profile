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
    public class AreaFactoryTests
    {
        [TestMethod()]
        public void Create_ExistingKey()
        {
            //arrange
            AreaFactory areaFactory = new AreaFactory();
            Room room = new Room();
            Type expected = room.GetType();
            
            areaFactory.internalFactory.Add<Room>("key");
            IArea actual;
            //act
            actual = areaFactory.Create("key");
            //assert
            Assert.IsInstanceOfType(actual, expected);
        }
        [TestMethod()]
        public void Create_NonexistingKey()
        {
            //arrange
            AreaFactory areaFactory = new AreaFactory();
            UndifinedArea undifinedArea = new UndifinedArea();
            Type expected = undifinedArea.GetType();
            IArea actual;
            //act
            actual = areaFactory.Create("RandomKeyNotInFact");
            //assert
            Assert.IsInstanceOfType(actual, expected);
        }
    }
}