using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HotelEvents;
using HotelSimulatie.People;
using HotelSimulatie.Utility;

namespace HotelSimulatie
{
    /// <summary>
    /// The cinema area, customers can watch films here
    /// </summary>
    public class Cinema : Area
    {
        private float _passedTimeSinceUpdate;
        /// <summary>
        /// The time a film needs to run
        /// </summary>
        public int RunTime { get; set; }

        /// <summary>
        /// True if cinema is started
        /// </summary>
        public bool Started { get; set; }
        /// <summary>
        /// initialize cinema
        /// </summary>
        public Cinema()
        {
            RunTime = int.MaxValue;
            Started = false;
            color = Color.Turquoise;
        }
        /// <summary>
        /// load the specific sprites for cinema
        /// </summary>
        /// <param name="Content"></param>
        override public void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            Sprites.Add(1, Content.Load<Texture2D>("Cinema"));
            Sprites.Add(11, Content.Load<Texture2D>("CinemaS"));
        }
        /// <summary>
        /// check every cinema if the movie has started
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="hotel"></param>
        /// <param name="huidigeCinema"></param>
        /// <param name="simplePath"></param>
        /// <param name="customers"></param>
        public void CheckCinema(GameTime gameTime, Hotel hotel, Cinema huidigeCinema, SimplePath simplePath, List<Customer> customers)
        {
            var Cinemas = from f in hotel.Areas
                          where (f.ID == hotel.Areas.Where(a => a.AreaType == "Cinema").First().ID)
                          select f;

            if (Cinemas.Count() > 0)
            {
                huidigeCinema = (Cinema)Cinemas.First();

                foreach (Cinema cinema in Cinemas)
                {
                    cinema.RunMovie(gameTime, customers);
                }
            }
        }
        /// <summary>
        /// check if the movie has started.
        /// if the movie is running, when it ends send every customer back to their room.
        /// if the movie isn't running, makes sure every customer stays inside of it until it begins (unless there is a evacuation).
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="customers"></param>
        public void RunMovie(GameTime gameTime, List<Customer> customers)
        {
            if (Started == true)
            {
                float tussenTijd = gameTime.ElapsedGameTime.Milliseconds;
                tussenTijd /= 1000;
                _passedTimeSinceUpdate += tussenTijd;
                if (_passedTimeSinceUpdate >= RunTime / HotelEventManager.HTE_Factor)
                {
                    Started = false;
                    var bezoekers = from b in customers
                                    where (b.Position == Position)
                                    select b;
                    foreach (Customer customer in bezoekers)
                    {
                        customer.WaitingTime = 0;
                    }
                }
            }
            else
            {
                var bezoekers = from b in customers
                                where (b.Position == Position && b.Destination == Position)
                                select b;
                foreach (Customer customer in bezoekers)
                {
                    customer.WaitingTime = int.MaxValue;
                }
            }
        }
        /// <summary>
        /// retun the important cinema fields as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()

        {
            string started = Started ? "yes" : "no";
            return AreaType + "\t" + "ID: " + ID + "\tStarted: " + started;
        }
    }
}
