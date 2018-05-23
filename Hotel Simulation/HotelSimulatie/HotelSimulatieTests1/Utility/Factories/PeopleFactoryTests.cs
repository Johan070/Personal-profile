using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulatie.People;

namespace HotelSimulatie.Utility.Tests
{
    [TestClass()]
    public class PeopleFactoryTests
    {
        [TestMethod()]
        public void Create_ExistingKey()
        {
            //arrange
            PeopleFactory peopleFactory = new PeopleFactory();
            Person person = new Customer();
            Type expected = person.GetType();

            peopleFactory.internalFactory.Add<Customer>("key");
            IPerson actual;
            //act
            actual = peopleFactory.Create("key");
            //assert
            Assert.IsInstanceOfType(actual, expected);
        }
        [TestMethod()]
        public void Create_NonexistingKey()
        {
            //arrange
            PeopleFactory peopleFactory = new PeopleFactory();
            Person undifinedPerson = new Customer();
            Type expected = undifinedPerson.GetType();
            IPerson actual;
            //act
            actual = peopleFactory.Create("RandomKeyNotInFact");
            //assert
            Assert.IsInstanceOfType(actual, expected);
        }
    }
}