using HotelSimulatie.People;
using HotelSimulatie.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    /// <summary>
    /// The reception of the hotelm handles movement of people to their rooms after checkin
    /// </summary>
    public class Reception
    {
        /// <summary>
        /// the length of the queue
        /// </summary>
        public float QueuePosition;
        private Queue<Customer> _queue;
        /// <summary>
        /// Initialize the reception
        /// </summary>
        public Reception()
        {

            QueuePosition = 0;
            _queue = new Queue<Customer>();

        }
        /// <summary>
        /// searches for an empty room inside of the hotel based on the amount of stars the customer desires.
        /// if there are no rooms left with the desired amount of stars, the customer gets a free upgrade
        /// </summary>
        /// <param name="classification"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        public Room GetFreeRoom(int classification, Hotel hotel)
        {            
            Room room = hotel.Rooms[classification].Where(r => r.State == Room.RoomState.Free).OrderBy(x => x.Position.X).ThenBy(y =>y.Position.Y).First();
            if(room == null)
            {
                classification++;
                if(classification > 5)
                {
                    classification = 5;
                }
                GetFreeRoom(classification, hotel);
            }
            return room;
        }
        /// <summary>
        /// finds an empty room for every customer and sends them to their room
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="simplePath"></param>
        public void HelpQueue(Hotel hotel, SimplePath simplePath)
        {
            if (_queue.Count > 0)
            {
                Customer helpMe = _queue.Dequeue();
                    helpMe.Room = GetFreeRoom(helpMe.Preferance, hotel);
                    helpMe.Room.State = Room.RoomState.Booked;
                    helpMe.Destination = helpMe.Room.Position;
                    helpMe.Route = simplePath.GetRoute(helpMe.Position, helpMe.Destination);
                
                QueuePosition--;
            }
        }
        /// <summary>
        /// puts a customer in the queue to be helped.
        /// </summary>
        /// <param name="customer"></param>
        public void Enqueue(Customer customer)
        {
            _queue.Enqueue(customer);
            QueuePosition++;
        }
    }
}
