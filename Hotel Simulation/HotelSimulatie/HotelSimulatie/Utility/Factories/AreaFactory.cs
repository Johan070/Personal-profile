using HotelSimulatie.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    /// <summary>
    /// A factory to create areas
    /// </summary>
    public class AreaFactory
    {
        /// <summary>
        /// The factory used to create the areas
        /// </summary>
        public Factory<string, IArea> internalFactory = new Factory<string, IArea>();

        /// <summary>
        /// Initialize the creatable areas
        /// </summary>
        public AreaFactory()
        {
            internalFactory.Add<Room>("Room");
            internalFactory.Add<Cinema>("Cinema");
            internalFactory.Add<Restaurant>("Restaurant");
            internalFactory.Add<Fitness>("Fitness");
            internalFactory.Add<UndifinedArea>("UndifinedArea");
            internalFactory.Add<Elevator>("Elevator");
            internalFactory.Add<Stairs>("Stairs");
            internalFactory.Add<Lobby>("Lobby");
            //add areas here
        }
        /// <summary>
        /// Let factory create the area if the key is in the dictionary
        /// </summary>
        /// <param name="areaType"></param>
        /// <returns></returns>
        public IArea Create(string areaType)
        {
            try
            {
                return internalFactory.Create(areaType);
            }
            //returns UndifinedArea whenever an object in the layout is not specified in a class
            catch (ArgumentException e)
            {
                return internalFactory.Create("UndifinedArea");
            }

        }
    }
}
