namespace HotelSimulatie
{
    /// <summary>
    /// The creatable element by the factory
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// The returned object
        /// </summary>
        /// <returns></returns>
        object New();
    }
}