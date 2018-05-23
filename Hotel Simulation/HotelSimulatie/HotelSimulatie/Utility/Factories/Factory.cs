using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    /// <summary>
    /// Factory that can create any object in elements
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class Factory<K, T> : IFactory<K, T> where K : IComparable

    {
        /// Elements that can be created
        private Dictionary<K, IElement> _elements = new Dictionary<K, IElement>();

        /// <summary>
        /// Adds new element V under Key key to the creatable factory elements
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="key"></param>
        public void Add<V>(K key) where V : T, new()
        {
            _elements.Add(key, new Element<V>());
        }
        /// <summary>
        /// Create the specified element
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Create(K key)
        {
            if (_elements.ContainsKey(key))
            {
                return (T)_elements[key].New();
            }
            throw new ArgumentException("the key does not exist");
        }
    }
}
