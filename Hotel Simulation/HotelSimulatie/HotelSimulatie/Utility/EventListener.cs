using HotelEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie.Utility
{
    /// <summary>
    /// Listener for the dll events
    /// </summary>
    public class EventListener : HotelEventListener
    {
        /// <summary>
        /// The events received from the dll
        /// </summary>
        public List<HotelEvent> Events { get; set; }
        /// <summary>
        /// Initialize the listener
        /// </summary>
        public EventListener()
        {
            Events = new List<HotelEvent>();
        }
        /// <summary>
        /// Notify the listener if a new event happens
        /// </summary>
        /// <param name="evt"></param>
        public void Notify(HotelEvent evt)
        {
            Events.Add(evt);
        }
    }
}
