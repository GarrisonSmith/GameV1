using Fantasy.Logic.Engine.Graphics.tilemap;
using Fantasy.Logic.Engine.entities;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Hitboxes;
using Fantasy.Logic.Engine.Graphics.Lighting;

namespace Fantasy.Logic.Engine.Graphics.Drawing
{
    /// <summary>
    /// Object class used to manage sprites layering, drawing, lighting, etc.
    /// </summary>
    public class SceneContentManager
    {
        /// <summary>
        /// The TileMap used by the manager.
        /// </summary>
        public TileMap _tileMap;
        /// <summary>
        /// The EntitySet used by the manager.
        /// </summary>
        public EntitySet _entitySet;

        //public static LightsSet _lightsSet; TODO implement

        /// <summary>
        /// Creates a SceneContentManager with the provided parameters.
        /// </summary>
        /// <param name="_tileMap">The TileMap to be used.</param>
        /// <param name="_entitySet">The EntitySet to be used.</param>
        public SceneContentManager(TileMap _tileMap, EntitySet _entitySet)
        {
            this._tileMap = _tileMap;
            this._entitySet = _entitySet;
        }

        /// <summary>
        /// Creates a array containing all the hitboxes on the provided layer.
        /// </summary>
        /// <param name="layer">The layer to used.</param>
        /// <returns>A array containing all the hitboxes on the provided layer.</returns>
        public Hitbox[] GetLayerHitboxes(int layer)
        {
            List<Tile> tiles = _tileMap.GetLayer(layer).map; List<Entity> entities = _entitySet.GetLayer(layer);
            List<Hitbox> foo = new List<Hitbox>();
            foreach (Tile t in tiles)
            {
                if (t.hitboxes != null)
                {
                    foreach (Hitbox h in t.hitboxes)
                    {
                        foo.Add(h);
                    }
                }
            }
            foreach (Entity e in entities)
            {
                foo.Add(e.hitbox);
            }
            return foo.ToArray();
        }

        public Lightbox[] GetLayerLightboxes(int layer)
        {
            List<Tile> tiles = _tileMap.GetLayer(layer).map; List<Entity> entities = _entitySet.GetLayer(layer);
            List<Lightbox> foo = new List<Lightbox>();
            foreach (Tile t in tiles)
            {
                if (t.lightboxes != null)
                {
                    foreach (Lightbox l in t.lightboxes)
                    {
                        foo.Add(l);
                    }
                }
            }

            return foo.ToArray();
        }

        /// <summary>
        /// Draws all layers in the current TileMap and EntitySet. Draws the lighting for each layer in TileMap and EntitySet.
        /// </summary>
        public void DrawLayers()
        {
            int minLayer = Math.Min(_tileMap.GetMinLayer(), _entitySet.GetMinLayer());
            int maxLayer = Math.Max(_tileMap.GetMaxLayer(), _entitySet.GetMaxLayer());

            for (int i = minLayer; i <= maxLayer; i++)
            {
                _tileMap.DrawLayer(i);
                _entitySet.DrawLayer(i);
                //_lightsSet.DrawLayer(i);
            }
        }
        /// <summary>
        /// Draws the provided layer in the current TileMap, EntitySet and LightsSet.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        public void DrawLayer(int layer)
        {
            _tileMap.DrawLayer(layer);
            _entitySet.DrawLayer(layer);
            //_lightsSet.DrawLayer(layer);
        }
        /// <summary>
        /// Draws the provided layers in the current TileMap, EntitySet and LightsSet.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        public void DrawLayers(int[] layers)
        {
            _tileMap.DrawLayers(layers);
            _entitySet.DrawLayers(layers);
            //_lightsSet.DrawLayers(layers);
        }
        /// <summary>
        /// Draws the provided area in the current TileMap, EntitySet and LightsSet.
        /// </summary>
        /// <param name="area">The area to be drawn.</param>
        public void DrawArea(Rectangle area)
        {
            _tileMap.DrawArea(area);
            _entitySet.DrawArea(area);
            //_lightSet.DrawArea(area);
        }
        /// <summary>
        /// Draws the provided layer and area in the current TileMap, EntitySet and LightsSet.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        /// <param name="area">The area to be drawn.</param>
        public void DrawArea(int layer, Rectangle area)
        {
            _tileMap.DrawArea(layer, area);
            _entitySet.DrawArea(layer, area);
            //_lightSet.DrawArea(layer, area);
        }
        /// <summary>
        /// Draws the provided layers and area in the current TileMap, EntitySet and LightsSet.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        /// <param name="area">The area to be drawn.</param>
        public void DrawArea(int[] layers, Rectangle area)
        {
            _tileMap.DrawArea(layers, area);
            _entitySet.DrawArea(layers, area);
            //_lightSet.DrawArea(layer, area);
        }
        /// <summary>
        /// Draws the provided areas in the current TileMap, EntitySet and LightsSet.
        /// </summary>
        /// <param name="areas">The areas to be drawn.</param>
        public void DrawAreas(Rectangle[] areas)
        {
            _tileMap.DrawAreas(areas);
            _entitySet.DrawAreas(areas);
            //_lightSet.DrawArea(areas);
        }
        /// <summary>
        /// Draws the provided layer and areas in the current TileMap, EntitySet and LightsSet.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        /// <param name="areas">The areas to be drawn.</param>
        public void DrawAreas(int layer, Rectangle[] areas)
        {
            _tileMap.DrawAreas(layer, areas);
            _entitySet.DrawAreas(layer, areas);
            //_lightSet.DrawAreas(layer, areas);
        }
        /// <summary>
        /// Draws the provided layers and areas in the current TileMap, EntitySet and LightsSet.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        /// <param name="areas">The areas to be drawn.</param>
        public void DrawAreas(int[] layers, Rectangle[] areas)
        {
            _tileMap.DrawAreas(layers, areas);
            _entitySet.DrawAreas(layers, areas);
            //_lightSet.DrawAreas(layers, areas);
        }
    }
}
