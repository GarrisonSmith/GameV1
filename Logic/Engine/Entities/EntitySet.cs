using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.entities
{
    /// <summary>
    /// Object classes that groups entities together for easier management. 
    /// </summary>
    public class EntitySet
    {
        /// <summary>
        /// The entity the player is currently controlling.
        /// </summary>
        public Entity player;
        /// <summary>
        /// List containing all entities in the EntitySet.
        /// </summary>
        public List<Entity> entitySet;

        /// <summary>
        /// Creates a empty EntitySet.
        /// </summary>
        public EntitySet()
        {
            entitySet = new List<Entity>();
        }
        /// <summary>
        /// Creates a EntitySet using the provided entity list as a starting list.
        /// </summary>
        /// <param name="foo">The starting list for this EntitySet.</param>
        public EntitySet(List<Entity> foo)
        {
            if (foo == null)
            {
                entitySet = new List<Entity>();
            }
            entitySet = foo;
        }

        /// <summary>
        /// Updates all entities inside of this EntitySet.
        /// </summary>
        public void UpdateEntitySet()
        {
            foreach (Entity e in entitySet)
            {
                e.UpdateEntity();
            }
        }
        /// <summary>
        /// Sets the sets player entity to the entity with the provided id if it is also in the EntitySet.
        /// </summary>
        /// <param name="id">The id to be looked for.</param>
        /// <returns>True if the entity was found in the EntitySet and made the player, False if not.</returns>
        public bool MakeEntityPlayer(string id)
        {
            foreach (Entity e in entitySet)
            {
                if (e.id == id)
                {
                    player = e;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Adds the provided entity to the EntitySet.
        /// </summary>
        /// <param name="entity">The entity to be added to the EntitySet.</param>
        /// <param name="makePlayer">Determines if the added entity is made the player.</param>
        public void AddEntity(Entity entity, bool makePlayer = false)
        {
            if (makePlayer)
            {
                player = entity;
            }

            entitySet.Add(entity);
        }
        /// <summary>
        /// Removes the provided entity from the EntitySet.
        /// </summary>
        /// <param name="entity">The entity to be removed from the EntitySet.</param>
        /// <returns>True if the entity was removed from the EntitySet, False if not or if the entity was not found in the EntitySet.</returns>
        public bool RemoveEntity(Entity entity)
        {
            return entitySet.Remove(entity);
        }
        /// <summary>
        /// Attempts to get a entity from the EntitySet with the proived id.
        /// </summary>
        /// <param name="id">The id to be looked for.</param>
        /// <returns>A Entity with the provided id if found, null if no such entity is in the EntitySet.</returns>
        public Entity GetEntity(string id)
        {
            foreach (Entity e in entitySet)
            {
                if (e.id == id)
                {
                    return e;
                }
            }
            return null;
        }
        /// <summary>
        /// Gets the value of the highest layer in this EntitySet.
        /// </summary>
        /// <returns>The value of the highest layer.</returns>
        public int GetMaxLayer()
        {
            int heighest = int.MinValue;
            foreach (Entity e in entitySet)
            {
                if (e.layer > heighest)
                {
                    heighest = e.layer;
                }
            }
            return heighest;
        }
        /// <summary>
        /// Gets the value of the lowest layer in this EntitySet.
        /// </summary>
        /// <returns>The value of the lowest layer.</returns>
        public int GetMinLayer()
        {
            int lowest = int.MaxValue;
            foreach (Entity e in entitySet)
            {
                if (e.layer < lowest)
                {
                    lowest = e.layer;
                }
            }
            return lowest;
        }
        /// <summary>
        /// Gets the number of entities present in the EntitySet.
        /// </summary>
        /// <returns>The number of entities in the EntitySet that are not null.</returns>
        public int GetNumberOfLayer()
        {
            int count = 0;
            foreach (Entity e in entitySet)
            {
                if (e != null)
                {
                    count++;
                }
            }
            return count;
        }
        /// <summary>
        /// Creates and returns a list containing the entities on the provided layer.
        /// </summary>
        /// <param name="layer">The layer to be used.</param>
        /// <returns>A list containing the entities on the provided layer.</returns>
        public List<Entity> GetLayer(int layer)
        {
            List<Entity> foo = new List<Entity>();
            foreach (Entity e in entitySet)
            {
                if (e.layer == layer)
                {
                    foo.Add(e);
                }
            }
            if (foo.Count == 0)
            {
                return null;
            }
            else
            {
                return foo;
            }
        }
        /// <summary>
        /// Creates and returns a list containing the entities on the provided layers.
        /// </summary>
        /// <param name="layers">The layers to be used.</param>
        /// <returns>A list containing the entities on the provided layers.</returns>
        public List<Entity> GetLayer(int[] layers)
        {
            List<Entity> foo = new List<Entity>();
            foreach (int l in layers)
            {
                foreach (Entity e in entitySet)
                {
                    if (e.layer == l)
                    {
                        foo.Add(e);
                    }
                }
            }
            if (foo.Count == 0)
            {
                return null;
            }
            else
            {
                return foo;
            }
        }
        
        /// <summary>
        /// Draws all entities on all layers in this EntitySet.
        /// </summary>
        public void DrawLayers()
        {
            foreach (Entity e in entitySet)
            {
                e.Draw();
            }
        }
        /// <summary>
        /// Draws all entities on the provided layer.
        /// </summary>
        /// <param name="layer">The layer to be used.</param>
        public void DrawLayer(int layer)
        {
            foreach (Entity e in entitySet)
            {
                if (e.layer == layer)
                {
                    e.Draw();
                }
            }
        }
        /// <summary>
        /// Draws all entities on the provided layers.
        /// </summary>
        /// <param name="layers">The layers to be used.</param>
        public void DrawLayers(int[] layers)
        {
            foreach (int l in layers)
            {
                foreach (Entity e in entitySet)
                {
                    if (e.layer == l)
                    {
                        e.Draw();
                    }
                }
            }
        }
        /// <summary>
        /// Draws all entities in the provided draw area.
        /// </summary>
        /// <param name="drawArea">Rectangle describing the area to be drawn.</param>
        public void DrawArea(Rectangle drawArea)
        {
            foreach (Entity e in entitySet)
            { 
                if(Util.PointInsideRectangle(e.GetEntityVisualCenter(), drawArea))
                {
                    e.Draw();
                }
            }
        }
        /// <summary>
        /// Draws all entities in the provided draw area and on the provided layer.
        /// </summary>
        /// <param name="layer">The layers to be used.</param>
        /// <param name="drawArea">Rectangle describing the area to be drawn.</param>
        public void DrawArea(int layer, Rectangle drawArea)
        {
            foreach (Entity e in entitySet)
            {
                if (Util.PointInsideRectangle(e.GetEntityVisualCenter(), drawArea) && e.layer == layer)
                {
                    e.Draw();
                }
            }
        }
        /// <summary>
        /// Draws all entities in the provided draw area and on the provided layers.
        /// </summary>
        /// <param name="layers">The layers to be used.</param>
        /// <param name="drawArea">Rectangle describing the area to be drawn.</param>
        public void DrawArea(int[] layers, Rectangle drawArea)
        {
            foreach (int l in layers)
            {
                foreach (Entity e in entitySet)
                {
                    if (Util.PointInsideRectangle(e.GetEntityVisualCenter(), drawArea) && e.layer == l)
                    {
                        e.Draw();
                    }
                }
            }
        }
        /// <summary>
        /// Draws all entities in the provided draw areas.
        /// </summary>
        /// <param name="drawAreas">Rectangle describing the area to be drawn.</param>
        public void DrawAreas(Rectangle[] drawAreas)
        {
            foreach (Rectangle drawArea in drawAreas)
            {
                DrawArea(drawArea);
            }
        }
        /// <summary>
        /// Draws all entities in the provided draw areas and on the provied layer.
        /// </summary>
        /// <param name="layer">The layers to be used.</param>
        /// <param name="drawAreas">Rectangle describing the area to be drawn.</param>
        public void DrawAreas(int layer, Rectangle[] drawAreas)
        {
            foreach (Rectangle drawArea in drawAreas)
            {
                DrawArea(layer, drawArea);
            }
        }
        /// <summary>
        /// Draws all entities in the provided draw areas and on the provied layers.
        /// </summary>
        /// <param name="layers">The layers to be used.</param>
        /// <param name="drawAreas">Rectangle describing the area to be drawn.</param>
        public void DrawAreas(int[] layers, Rectangle[] drawAreas)
        {
            foreach (int l in layers)
            {
                foreach (Rectangle drawArea in drawAreas)
                {
                    DrawArea(l, drawArea);
                }
            }
        }
    }
}
