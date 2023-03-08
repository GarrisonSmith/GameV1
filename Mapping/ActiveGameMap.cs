using Fantasy.Engine.Mapping.Tiling;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Xml;

namespace Fantasy.Engine.Mapping
{
	/// <summary>
	/// Represents a game map consisting of multiple layers of tiles.
	/// </summary>
	public static class ActiveGameMap
    {
		private static string tileMapName;
		private static MapLayer highest_layer;
		private static Dictionary<int, MapLayer> mapLayers;
		private static readonly Game game;

		/// <summary>
		/// The name of the tile map.
		/// </summary>
		public static string TileMapName
		{ 
			get => tileMapName;
		}
		/// <summary>
		/// The highest layer in the map.
		/// </summary>
		public static MapLayer HIGHEST_LAYER
		{
			get => highest_layer;
			set => highest_layer = value;
		}
		/// <summary>
		/// The collection of layers in the map.
		/// </summary>
		public static Dictionary<int, MapLayer> MapLayers
		{
			get => mapLayers;
		}
		/// <summary>
		/// The game object associated with the map.
		/// </summary>
		public static Game Game
		{
			get => game;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="game"></param>
		/// <param name="mapName"></param>
		public static void LoadMap(Game game, string mapName)
        {
            XmlDocument mapDoc = new XmlDocument();
            mapDoc.Load(@"Content\tilemaps\" + mapName + ".xml");
            XmlElement mapElement = (XmlElement)mapDoc.SelectSingleNode("Engine.Logic.Mapping.GameMap");

			tileMapName = mapElement.GetAttribute("name");
			mapLayers = new Dictionary<int, MapLayer>();
			foreach (XmlElement tileElement in mapElement.GetElementsByTagName("Engine.Logic.Mapping.Tiling.Tile"))
			{
				Tile.CreateTile(tileElement);
			}

			foreach (XmlElement layerElement in mapElement.SelectSingleNode("Layers"))
			{
				int layer = int.Parse(layerElement.GetAttribute("id"));
				mapLayers.Add(layer, new MapLayer(game, layer));
			}

			Tile.UpdateTileDrawLocations();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="game"></param>
		/// <param name="mapElement"></param>
		public static void LoadMap(Game game, XmlElement mapElement)
        {
            tileMapName = mapElement.GetAttribute("name");
            mapLayers = new Dictionary<int, MapLayer>();
            foreach (XmlElement tileElement in mapElement.GetElementsByTagName("Engine.Logic.Mapping.Tiling.Tile"))
            {
                Tile.CreateTile(tileElement);
            }

            foreach (XmlElement layerElement in mapElement.SelectSingleNode("Layers"))
            {
                int layer = int.Parse(layerElement.GetAttribute("id"));
                mapLayers.Add(layer, new MapLayer(game, layer));
            }

			Tile.UpdateTileDrawLocations();
		}
		/// <summary>
		/// Adds the layers of the map to a specified game component collection.
		/// </summary>
		/// <param name="foo">The game component collection to add the layers to.</param>
		public static void GetGameComponents(GameComponentCollection foo)
        {
            foreach (MapLayer layer in mapLayers.Values)
            {
                foo.Add(layer);
            }
        }
    }
}