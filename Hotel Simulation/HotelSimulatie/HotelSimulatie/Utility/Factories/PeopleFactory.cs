using HotelSimulatie.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie.Utility
{
    /// <summary>
    /// A factory to create persons
    /// </summary>
    public class PeopleFactory
    {
        /// <summary>
        /// The internal factory that does the work
        /// </summary>
        public Factory<string, IPerson> internalFactory = new Factory<string, IPerson>();

        /// <summary>
        /// add people to the internal factory
        /// </summary>
        public PeopleFactory()
        {
            internalFactory.Add<Cleaner>("Cleaner");
            internalFactory.Add<Customer>("Customer");
            internalFactory.Add<Customer>("UndifinedPerson");
            //add people here
        }

        /// <summary>
        /// Create a person
        /// </summary>
        /// <param name="personType"></param>
        /// <returns></returns>
        public IPerson Create(string personType)
        {
            try
            {
                return internalFactory.Create(personType);
            }
            //returns nothing whenever an object in the layout is not specified in a class
            catch (ArgumentException e)
            {
                return internalFactory.Create("UndifinedPerson");
            }

        }
    }
}
