using Fantasy.Engine.Drawing.Animating;
using Fantasy.Engine.Physics;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Fantasy.Engine.Mapping.Tiling
{
	/// <summary>
	/// Represents a animated tile in a MapLayer.
	/// </summary>
	public class AnimatedTile : Tile
	{
		private readonly Dictionary<int, Dictionary<Coordinates, SpritesheetAnimation>> animations;

		/// <summary>
		/// The dictionary of animations for this tile.
		/// </summary>
		public Dictionary<int, Dictionary<Coordinates, SpritesheetAnimation>> Animations
		{
			get => animations;
		}

		/// <summary>
		/// Creates a new AnimatedTile based on the given XML element.
		/// </summary>
		/// <param name="animatedTileElement">The XML element representing the tile.</param>
		/// <returns>true if the tile was created successfully; false if a tile with the same ID already exists.</returns>
		public static new bool CreateTile(XmlElement animatedTileElement)
		{
			if (!UNIQUE_TILES.ContainsKey(animatedTileElement.GetAttribute("id")))
			{
				AnimatedTile tile = new(animatedTileElement);
				UNIQUE_TILES.Add(animatedTileElement.GetAttribute("id"), tile);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Creates a new AnimatedTile based on the given XML element.
		/// </summary>
		/// <param name="animatedTileElement">The XML element representing the tile.</param>
		private AnimatedTile(XmlElement animatedTileElement)
		{ 
		
		}
		/// <summary>
		/// Draws the tile on the specified layer using the current frame of each animation.
		/// </summary>
		/// <param name="gameTime">The current game time.</param>
		/// <param name="layer">The layer on which to draw the tile.</param>
		public new void Draw(GameTime gameTime, int layer)
		{
			DrawCoordinates.TryGetValue(layer, out HashSet<Coordinates> layerDrawCoordinates);
			foreach (Coordinates cord in layerDrawCoordinates)
			{
				Animations[layer][cord].DrawCurrentFrame(cord, Color.White);
			}
		}
	}
}
