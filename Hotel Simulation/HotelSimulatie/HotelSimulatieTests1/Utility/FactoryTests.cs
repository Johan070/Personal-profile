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
    public class FactoryTests
    {
        Factory<string, TestObject> fact;
        internal class TestObject
        {
            public string Name = "testObject";
        }
        [TestInitialize()]
        public void TestInit()
        {
            fact = new Factory<string, TestObject>();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "the key does not exist")]
        public void CreateTest_NonExistantKey()
        {
            fact.Create("1");
        }
        [TestMethod()]
        public void CreateTest_ExistantKey()
        {
            fact.Add<TestObject>("1");
            var a = fact.Create("1");
            Assert.AreEqual("testObject", a.Name);
        }

    }
}
