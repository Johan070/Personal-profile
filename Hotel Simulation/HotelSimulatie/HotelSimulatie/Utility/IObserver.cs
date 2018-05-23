using HotelSimulatie.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie.Utility
{
    /// <summary>
    /// Interface of wich Observers inherit
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Update the observer on the elevator state
        /// </summary>
        /// <param name="elevator"></param>
        void Update(Elevator elevator);
    }
}
