using HotelEvents;
using HotelSimulatie.People;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulatie.Areas;
using HotelSimulatie.Utility;

namespace HotelSimulatie
{
    /// <summary>
    /// Basically an eventhandler with every event dropped in one method
    /// </summary>
    public class EventChecker
    {
        private bool _evac;
        private int _countPeople;
        /// <summary>
        /// initialize the EventChecker
        /// </summary>
        public EventChecker()
        {
            _countPeople = 0;
            _evac = false;
        }
        /// <summary>
        /// checks if anything has happened in the hotel.
        /// if anything has happend checks what happend and makes sure everyone involved reacts appopriatly.
        /// </summary>
        /// <param name="simplePath"></param>
        /// <param name="hotel">the hotel that is simulated</param>
        /// <param name="persons">every person inside of the hotel</param>
        /// <param name="reception">the reception in the lobby</param>
        /// <param name="customers">every customer inside of the hotel</param>
        /// <param name="listener">the observer of the hotel</param>
        /// <param name="lobby">the lobby inside of the hotel</param>
        /// <param name="elevator">the elevator inside of the hotel</param>
        /// <param name="cleaner">the main cleaner inside of the hotel, this is the only cleaner qualified to clean emergencies</param>
        /// <param name="cleaners">the cleaners in the hotel, they can perform cleaning actions</param>
        /// <param name="gameTime">the runtime of the hotel</param>
        /// <param name="RoomQueue">The dirty rooms in order of cleaning</param>
        public void CheckEvents(SimplePath simplePath, Hotel hotel, List<IPerson> persons, Reception reception, List<Customer> customers, EventListener listener, Lobby lobby, Elevator elevator, Cleaner cleaner, List<Cleaner> cleaners, GameTime gameTime, Queue<Room> RoomQueue)
        {
            foreach (var evt in listener.Events.ToList())
            {
                if (!_evac)
                {
                    if (evt.EventType == HotelEventType.CHECK_IN)
                    {
                        if (evt.Data != null)
                            foreach (var key in evt.Data.Keys)
                            {
                                Customer newCustomer = new Customer() { Preferance = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data[key], "[^\\d]"))), Position = new Vector2(reception.QueuePosition / 4 + 1, 0), ID = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data.Keys.First(), "[^\\d]"))) };
                                persons.Add(newCustomer);
                                customers.Add(newCustomer);
                                reception.Enqueue(newCustomer);
                                elevator.Attach(newCustomer);
                            }
                        listener.Events.Remove(evt);
                    }
                    else if (evt.EventType == HotelEventType.CHECK_OUT)
                    {
                        foreach (var key in evt.Data.Keys)
                        {
                            var obj = from f in customers
                                      where (f.ID == Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data.Values.First(), "[^\\d]"))))
                                      select f;
                            if (obj.Count() > 0)
                            {
                                obj.First().Destination = lobby.Position;
                                if (obj.First().Room != null)
                                {
                                    obj.First().Room.State = Room.RoomState.Dirty;
                                    obj.First().Room = null;
                                    obj.First().Route = simplePath.GetRoute(obj.First().Position, obj.First().Destination);
                                }
                                elevator.Detach(obj.First());
                                persons.Remove(obj.First());
                                customers.Remove(obj.First());
                            }
                            listener.Events.Remove(evt);
                        }
                    }
                    else if (evt.EventType == HotelEventType.GOTO_FITNESS)
                    {
                        foreach (var key in evt.Data.Keys)
                        {
                            var obj = from f in customers
                                      where (f.ID == Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data.Values.First(), "[^\\d]"))))
                                      select f;
                            int TijdsDuur = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data.Values.ElementAt(1), "[^\\d]")));


                            if (obj.Count() > 0)
                            {
                                obj.First().Destination = hotel.Areas.Where(a => a.AreaType == "Fitness").First().Position;
                                obj.First().Route = simplePath.GetRoute(obj.First().Position, obj.First().Destination);
                                obj.First().WaitingTime = TijdsDuur;
                            }
                            listener.Events.Remove(evt);
                        }
                    }
                    else if (evt.EventType == HotelEventType.GOTO_CINEMA)
                    {
                        foreach (var key in evt.Data.Keys)
                        {
                            var obj = from f in customers
                                      where (f.ID == Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data.Values.First(), "[^\\d]"))))
                                      select f;
                            if (obj.Count() > 0)
                            {
                                Cinema leukeCinema = (Cinema)hotel.Areas.Where(a => a.AreaType == "Cinema").First();
                                obj.First().Destination = leukeCinema.Position;
                                obj.First().Route = simplePath.GetRoute(obj.First().Position, obj.First().Destination);
                                leukeCinema.RunTime = int.MaxValue;
                                obj.First().WaitingTime = leukeCinema.RunTime;
                            }
                            listener.Events.Remove(evt);
                        }
                    }
                    else if (evt.EventType == HotelEventType.NEED_FOOD)
                    {
                        foreach (var key in evt.Data.Keys)
                        {
                            var obj = from f in customers
                                      where (f.ID == Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data.Values.First(), "[^\\d]"))))
                                      select f;
                            if (obj.Count() > 0)
                            {
                                Restaurant restaurant = (Restaurant)hotel.Areas.Where(a => a.AreaType == "Restaurant").First();
                                obj.First().Destination = restaurant.Position;
                                restaurant.HuidigeBezetting++;
                                obj.First().Route = simplePath.GetRoute(obj.First().Position, obj.First().Destination);
                                obj.First().WaitingTime = restaurant.EatSpeed;
                            }
                            listener.Events.Remove(evt);
                        }
                    }
                    else if (evt.EventType == HotelEventType.START_CINEMA)
                    {
                        foreach (var key in evt.Data.Keys)
                        {
                            var obj = from f in hotel.Areas
                                      where (f.ID == Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data.Values.First(), "[^\\d]"))))
                                      select f;

                            if (obj.Count() > 0)
                            {
                                Cinema beginnendeCinema = (Cinema)obj.First();
                                beginnendeCinema.Started = true;
                            }
                        }

                        listener.Events.Remove(evt);
                    }
                    else if (evt.EventType == HotelEventType.EVACUATE)
                    {
                        foreach (Person person in persons)
                        {
                            person.Destination = lobby.Position;
                            person.Route = simplePath.GetRoute(person.Position, person.Destination);
                        }

                        List<Room> rooms = new List<Room>();
                        foreach (Room r in hotel.Areas.Where(r => r.AreaType == "Room"))
                        {
                            rooms.Add(r);
                        }
                        foreach (Room room in rooms.Where(r => r.State == Room.RoomState.Cleaning))
                        {
                            room.State = Room.RoomState.Dirty; 
                        }
                        _evac = true;
                        listener.Events.Remove(evt);
                    }
                    else if (evt.EventType == HotelEventType.CLEANING_EMERGENCY)
                    {
                        foreach (var key in evt.Data.Keys)
                        {
                            var obj = from f in hotel.Areas
                                      where (f.ID == Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data.Values.First(), "[^\\d]"))))
                                      select f;
                            int TijdsDuur = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(evt.Data.Values.ElementAt(1), "[^\\d]")));
                            if (obj.First().GetType() == typeof(Room))
                            {
                                Room EmergRoom = (Room)obj.First();
                                EmergRoom.State = Room.RoomState.Emergency;
                            }
                        }

                        listener.Events.Remove(evt);
                    }
                    else
                    {
                        listener.Events.Remove(evt);
                    }
                }
            }
            if (_evac)
            {
                foreach (Person person in persons)
                {
                    if (person.Position == lobby.Position)
                    {
                        _countPeople++;

                        if (person.GetType() == typeof(Customer))
                        {
                            Customer escapeCustomer = (Customer)person;
                            escapeCustomer.WaitingTime = int.MaxValue;
                            escapeCustomer.Route.Clear();
                        }
                        if(person.GetType() == typeof(Cleaner))
                        {
                            Cleaner escapeCleaner = (Cleaner)person;
                            escapeCleaner.Evacuating = true;
                            escapeCleaner.PassedTimeSinceUpdate = 0;
                        }
                    }
                }
                if (_countPeople == persons.Count)
                {
                    foreach (Person runPerson in persons)
                    {
                        if (runPerson.GetType() == typeof(Customer))
                        {
                            Customer runCustomer = (Customer)runPerson;
                            runCustomer.WaitingTime = 0;
                        }
                        else if (runPerson.GetType() == typeof(Cleaner))
                        {
                            Cleaner escapeCleaner = (Cleaner)runPerson;
                            escapeCleaner.Cleaning = false;
                            escapeCleaner.Evacuating = false;
                        }
                    }
                    _evac = false;
                }

            }
            _countPeople = 0;
        }
    }
}


