namespace HotelSimulatie
{
    /// <summary>
    /// The creatable element by the factory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Element<T> : IElement where T : new()
    {
        /// <summary>
        /// Returns a new object of type T
        /// </summary>
        /// <returns></returns>
        public object New()
        {
            return new T();
        }
    }
}