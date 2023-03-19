namespace Fantasy.Engine.Physics
{
	/// <summary>
	/// Represents an object that has a location and can calculate the distance to other locations.
	/// </summary>
	public interface ILocatable
    {
		/// <summary>
		/// Gets the BoundingBox2 of the object.
		/// </summary>
		BoundingBox2 BoundingBox2 { get; }
        /// <summary>
        /// Determines if this ILocatable object intersects with another ILocatable object.
        /// </summary>
        /// <param name="foo">The other ILocatable object.</param>
        /// <returns>True if the ILocatable object intersects with the other ILocatable object, False otherwise.</returns>
        bool Intersects(ILocatable foo);
        /// <summary>
        /// Determines if this ILocatable object completely contains another ILocatable object.  
        /// </summary>
        /// <param name="foo">The other ILocatable object.</param>
        /// <returns>True if the ILocatable object contains the other ILocatable object, False otherwise.</returns>
        bool Contains(ILocatable foo);
        /// <summary>
        /// Calculates the distance between the object and another object at the given ILocatable.
        /// </summary>
        /// <param name="foo">The ILocatable of the other object.</param>
        /// <returns>The distance between the object and the other object.</returns>
        float Distance(ILocatable foo);
    }
}
