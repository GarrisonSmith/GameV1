using Microsoft.Xna.Framework;

namespace Fantasy.Engine.Drawing
{
	/// <summary>
	/// Defines a contract for a drawable game component that can be used as a child element of another drawable component.
	/// </summary>
	public interface ISubDrawable
	{
		/// <summary>
		/// Gets or sets a value indicating whether the component should be drawn.
		/// </summary>
		/// <value><c>true</c> if the component should be drawn; otherwise, <c>false</c>.</value>
		bool IsVisible { get; set; }
		/// <summary>
		/// Draws the component using the specified game time.
		/// </summary>
		/// <param name="gameTime">The elapsed game time since the last update.</param>
		void Draw(GameTime gameTime);
	}
}
