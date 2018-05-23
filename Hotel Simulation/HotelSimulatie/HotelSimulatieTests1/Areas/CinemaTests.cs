using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulatie.People;
using Microsoft.Xna.Framework;
using System.Reflection;
using HotelEvents;

namespace HotelSimulatie.Tests
{
    [TestClass()]
    public class CinemaTests
    {
        [TestMethod()]
        public void RunMovieTest_Started()
        {
            Cinema cinema = new Cinema() { Position = new Vector2(1, 0) };
            List<Customer> customers = new List<Customer>() { new Customer() { Position = new Vector2(1, 0), Destination = new Vector2(1, 0) }, new Customer() { Position = new Vector2(2, 0), Destination = new Vector2(1, 0) } };

            cinema.Started = true;


            var prop = cinema.GetType().GetField("_passedTimeSinceUpdate", BindingFlags.NonPublic | BindingFlags.Instance);
            prop.SetValue(cinema, cinema.RunTime / HotelEventManager.HTE_Factor);

            cinema.RunMovie(new GameTime(), customers);
            Assert.AreEqual(false, cinema.Started);
        }
        [TestMethod()]
        public void RunMovieTest_NotStarted()
        {
            Cinema cinema = new Cinema() { Position = new Vector2(1, 0) };
            List<Customer> customers = new List<Customer>() { new Customer() { Position = new Vector2(1, 0), Destination = new Vector2(1, 0), WaitingTime = 0 }, new Customer() { Position = new Vector2(2, 0), Destination = new Vector2(1, 0) } };

            cinema.Started = false;


            var prop = cinema.GetType().GetField("_passedTimeSinceUpdate", BindingFlags.NonPublic | BindingFlags.Instance);
            prop.SetValue(cinema, cinema.RunTime / HotelEventManager.HTE_Factor);

            cinema.RunMovie(new GameTime(), customers);
            Assert.AreEqual(customers.Find(x => x.Position == cinema.Position).WaitingTime, int.MaxValue);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Cinema cinema = new Cinema() { AreaType = "Cinema", Position = new Vector2(1, 1), Dimension = new Vector2(99, 89), Started = true };
            Assert.AreEqual("Cinema	ID: 0	Started: yes", cinema.ToString());
        }
    }
}