namespace Fantasy.Engine.Physics
{
	/// <summary>
	/// Represents an object that has a location and can calculate the distance to other locations.
	/// </summary>
	public interface ILocatable
    {
		/// <summary>
		/// Gets the coordinates of the object.
		/// </summary>
		Coordinates Coordinates { get; }
		/// <summary>
		/// Determines whether the object intersects with another object at the given coordinates.
		/// </summary>
		/// <param name="foo">The coordinates of the other object.</param>
		/// <returns>True if the object intersects with the other object, false otherwise.</returns>
		bool Intersects(Coordinates foo);
		/// <summary>
		/// Calculates the distance between the object and another object at the given coordinates.
		/// </summary>
		/// <param name="foo">The coordinates of the other object.</param>
		/// <returns>The distance between the object and the other object.</returns>
		double Distance(Coordinates foo);
    }
}
