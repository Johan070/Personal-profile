using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    /// <summary>
    /// An abstract factory that can create any object of type T if it has a key K
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IFactory<K, T> where K : IComparable
    {
        /// <summary>
        /// Get an object associated with key K of type T
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Object of type T</returns>
        T Create(K key);
    }
}
