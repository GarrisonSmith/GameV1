using Fantasy.Engine.Mapping.Tiling;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Fantasy.Engine.Drawing;
using System;

namespace Fantasy.Engine.Mapping
{
	/// <summary>
	/// Represents a layer of tiles in a game map.
	/// </summary>
	internal class MapLayer : DrawableGameComponent
	{
		private readonly int layer;
		private readonly TileCollection tileLayer;
		private MapLayer next;

		/// <summary>
		/// The layer number.
		/// </summary>
		internal int Layer
		{
			get => layer;
		}
		/// <summary>
		/// The collection of tiles in the layer.
		/// </summary>
		internal TileCollection TileLayer
		{
			get => tileLayer;
		}
		/// <summary>
		/// The next layer in the map. This will be the layer with the next lowest layer value or null if no such layer exists.
		/// </summary>
		internal MapLayer Next
		{
			get => next;
		}

		/// <summary>
		/// Initializes a new instance of the MapLayer class and adds it to the ActiveGameMap.
		/// </summary>
		/// <param name="game">The game object to which the layer will be added.</param>
		/// <param name="layer">The layer number of the new MapLayer.</param>
		/// <exception cref="Exception">Thrown if a layer with the same number already exists in the ActiveGameMap.</exception>
		internal MapLayer(Game game, int layer) : base(game)
		{
			if (ActiveGameMap.MapLayers.ContainsKey(layer))
			{
				throw new Exception("A layer with the value of " + layer + " already exists in the ActiveGameMap.");
			}
			
			this.layer = layer;
			DrawOrder = layer;
			UpdateOrder = layer;
			tileLayer = new TileCollection(this);

			if (ActiveGameMap.HIGHEST_LAYER == null || ActiveGameMap.HIGHEST_LAYER.Layer < layer)
			{
				next = ActiveGameMap.HIGHEST_LAYER;
				ActiveGameMap.HIGHEST_LAYER = this;
				return;
			}

			MapLayer current = ActiveGameMap.HIGHEST_LAYER;
			while (current != null)
			{
				if (current.Next == null)
				{
					current.next = this;
					next = null;
					return;
				}
				if (current.Next.Layer < layer)
				{ 
					next = current.Next;
					current.next = this;
					return;
				}
				current = current.Next;
			}
		}
		/// <summary>
		/// Draws the layer.
		/// </summary>
		/// <param name="gameTime">The current game time.</param>
		public override void Draw(GameTime gameTime)
		{
			tileLayer.Draw(gameTime);
		}
	}
}
