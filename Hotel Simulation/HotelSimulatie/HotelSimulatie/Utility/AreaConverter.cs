using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    /// <summary>
    /// The converter to use when deserializing area objects
    /// </summary>
    public class AreaConverter : CustomJsonConverter<IArea>
    {
        /// <summary>
        /// The class that will create Areas when proper json objects are passed in
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        protected override IArea Create(Type objectType, JObject jsonObject)
        {
            // examine the AreaType value
            string typeName = (jsonObject["AreaType"]).ToString();

            // based on the AreaType, instantiate and return a new object
            AreaFactory factory = new AreaFactory();
            return factory.Create(typeName);
        }
    }
}
