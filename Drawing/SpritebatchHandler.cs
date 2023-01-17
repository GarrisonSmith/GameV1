﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Engine.Drawing
{
	/// <summary>
	/// An internal utility class for handling a spritebatch object from the Monogame framework.
	/// </summary>
	internal static class SpritebatchHandler
	{
		internal static SpriteBatch spritebatch;

		/// <summary>
		/// The spritebatch object.
		/// </summary>
		internal static SpriteBatch SpriteBatch
		{
			get => spritebatch;
		}

		/// <summary>
		/// Initializes the spritebatch object with a given GraphicsDevice.
		/// </summary>
		/// <param name="foo">The GraphicsDevice to use for initialization.</param>
		internal static void Initialize(GraphicsDevice foo)
		{
			spritebatch = new SpriteBatch(foo);
		}
		/// <summary>
		/// Begins the spritebatch drawing.
		/// </summary>
		internal static void Begin()
		{ 
			spritebatch.Begin();
		}
		/// <summary>
		/// Ends the spritebatch drawing.
		/// </summary>
		internal static void End() 
		{
			spritebatch.End();
		}
		/// <summary>
		/// Draws a texture2D object with a specified destination, source rectangle, and color.
		/// </summary>
		/// <param name="texture2D">The Texture2D object to draw.</param>
		/// <param name="destination">The destination position of the Texture2D object, describes the top left position of the graphic.</param>
		/// <param name="sourceBox">The source rectangle of the Texture2D object.</param>
		/// <param name="color">The color of the Texture2D object.</param>
		internal static void Draw(Texture2D texture2D, Vector2 destination, Rectangle sourceBox, Color color) 
		{
			SpriteBatch.Draw(texture2D, destination, sourceBox, color);
		}
	}
}
