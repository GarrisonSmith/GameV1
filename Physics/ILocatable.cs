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
		/// Determines whether the object intersects with another object at the given BoundingBox2.
		/// </summary>
		/// <param name="foo">The BoundingBox2 of the other object.</param>
		/// <returns>True if the object intersects with the other object, false otherwise.</returns>
		bool Intersects(BoundingBox2 foo);
		/// <summary>
		/// Calculates the distance between the object and another object at the given BoundingBox2.
		/// </summary>
		/// <param name="foo">The BoundingBox2 of the other object.</param>
		/// <returns>The distance between the object and the other object.</returns>
		double Distance(BoundingBox2 foo);
    }
}
