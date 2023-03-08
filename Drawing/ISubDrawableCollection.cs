using Fantasy.Engine.Mapping;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Fantasy.Engine.Drawing
{
	/// <summary>
	/// Defines a contract for a collection of subdrawable components that can be used as child elements of another drawable component. 
	/// The collection contains elements of type U that implement the ISubDrawable interface and are identified by keys of type T.
	/// </summary>
	/// <typeparam name="T">The type of keys used to identify the elements in the collection.</typeparam>
	/// <typeparam name="U">The type of elements in the collection, which implement the ISubDrawable interface.</typeparam>
	public interface ISubDrawableCollection<T, U> where U : ISubDrawable
	{
		/// <summary>
		/// Describes if the entire collection is visible or not.
		/// </summary>
		bool IsVisible { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether to use a combined texture for all elements in the collection.
		/// </summary>
		public bool UseCombinedTexture { get; set; }
		/// <summary>
		/// Gets the combined texture used for all elements in the collection.
		/// </summary>
		public Texture2D CombinedTexture { get; }
		/// <summary>
		/// Gets the map layer associated with the collection.
		/// </summary>
		public MapLayer Map { get; }
		/// <summary>
		/// Gets the dictionary of elements in the collection, identified by keys of type T.
		/// </summary>
		public Dictionary<T, U> Tiles { get; }
		/// <summary>
		/// Draws the component using the specified game time.
		/// </summary>
		/// <param name="gameTime">The elapsed game time since the last update.</param>
		void Draw(GameTime gameTime);
	}
}
