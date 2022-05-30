using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.hitboxes;

namespace Fantasy.Logic.Engine.entities
{
    /// <summary>
    /// Describes a basic entity, inherited by more complex entity types.
    /// </summary>
    class Entity
    {
        /// <summary>
        /// String used to identify this entity.
        /// </summary>
        public string id;
        /// <summary>
        /// The type of this enity. TODO possibly should be switched to a enum eventually.
        /// </summary>
        public string type;
        /// <summary>
        /// The layer this entity currently occupies.
        /// </summary>
        public int layer;
        /// <summary>
        /// This entities hitbox for collision on a TileMap or other entities.
        /// </summary>
        public Entitybox hitbox;
        /// <summary>
        /// The spritesheet this entity uses when drawing or when animated.
        /// </summary>
        public Texture2D spriteSheet;

        /// <summary>
        /// Generic inheriated constructor.
        /// </summary>
        public Entity() { }

        /// <summary>
        /// Creates a Entity with the provided parameters.
        /// </summary>
        /// <param name="id">String that will be used to identify this entity.</param>
        /// <param name="type">The type this enity will be.</param>
        /// <param name="spriteSheetName">The name of the spritesheet this enity will use when drawing or when aniamted.</param>
        /// <param name="layer">The layer this entity will occupy.</param>
        /// <param name="hitbox">This entities hitbox for collision on a TileMap or other entities.</param>
        public Entity(string id, string type, string spriteSheetName, int layer, Entitybox hitbox)
        {
            this.id = id;
            this.type = type;
            this.layer = layer;
            this.hitbox = hitbox;

            spriteSheet = Global._content.Load<Texture2D>(spriteSheetName);
        }

        /// <summary>
        /// Draws the collision area of the hitbox of this entity, 
        /// </summary>
        public void DrawHitbox()
        {
            hitbox.DrawHitbox();
        }
    }
}