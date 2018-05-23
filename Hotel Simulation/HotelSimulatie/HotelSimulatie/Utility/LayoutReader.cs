
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    /// <summary>
    /// Reads JSON files with Areas
    /// </summary>
    public class LayoutReader
    {
        /// <summary>
        /// Reads a file with destination layoutFile,returns a list with all area objects if it's a valid Json file
        /// </summary>
        /// <param name="layoutFile"></param>
        /// <returns></returns>
        public List<IArea> GetAllObjects(string layoutFile)
        {

            List<IArea> items = new List<IArea>();
            AreaFactory areaFactory = new AreaFactory();
            AreaConverter converter = new AreaConverter();
            var deseralizeSettings = new JsonSerializerSettings();
            string text = File.ReadAllText(layoutFile);
            deseralizeSettings.Converters.Add(converter);
            deseralizeSettings.TypeNameHandling = TypeNameHandling.Auto;

            items = JsonConvert.DeserializeObject<List<IArea>>(text, deseralizeSettings);

            return items;
        }
    }
}
