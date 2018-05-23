using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulatie.Areas;
using HotelSimulatie.People;
using HotelSimulatie.Utility;

namespace HotelSimulatie.Tests
{
    [TestClass()]
    public class MenuFormTests
    {
        Hotel hotel;
        List<Room> hotelRooms;
        SimplePath simplePath;
        List<IPerson> persons;
        Customer person;
        Customer person2;
        Cleaner cleaner;
        Reception reception;
        List<Customer> customers;
        Lobby lobby;
        Stairs stairs;
        List<Cleaner> cleaners;
        EventChecker eventChecker;
        [TestInitialize()]
        public void TestInit()
        {
            hotel = new Hotel();
            hotelRooms = new List<Room>();
            simplePath = new SimplePath();
            person = new Customer();
            person.Route = new Stack<Node>();
            person2 = new Customer();
            person2.Route = new Stack<Node>();
            cleaner = new Cleaner();
            persons = new List<IPerson>();
            customers = new List<Customer>()
            {
                person,
                person2,
            };
            cleaners = new List<Cleaner>
            {
                cleaner
            };
            persons.Add(cleaner);
            persons.Add(person);
            persons.Add(person2);
            reception = new Reception();
            lobby = new Lobby();
            stairs = new Stairs();
            eventChecker = new EventChecker();
        }
        [TestMethod()]
        public void MenuFormTest()
        {
            MenuForm form = new MenuForm(hotel, cleaners, customers, persons, stairs, simplePath);
            Assert.AreEqual(hotel, form.Hotel);
        }

        /// <summary>
        /// Vrij nutteloze tests omdat privates geinitialiseerd worden, maar we weten in ieder gevak dat het geen exceptions throwt
        /// </summary>
        [TestMethod()]
        public void ReInitListboxesTest()
        {
            MenuForm form = new MenuForm(hotel, cleaners, customers, persons, stairs, simplePath);
            form.ReInitListboxes();            
        }
    }
}