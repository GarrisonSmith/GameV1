using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.hitboxes;
using Fantasy.Logic.Engine.physics;
using Fantasy.Logic.Controls;
using System.Collections.Generic;

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
        /// This entities hitbox for collision on a TileMap or other entities. Also stores the characters location.
        /// </summary>
        public Entitybox hitbox;
        /// <summary>
        /// The spritesheet this entity uses when drawing or when animated.
        /// </summary>
        public Texture2D spriteSheet;
        /// <summary>
        /// Describes the MoveSpeed of the entity.
        /// </summary>
        public MoveSpeed speed;
        /// <summary>
        /// Describe the current movement state of the Entity.
        /// </summary>
        public EntityMovementState movement;
        /// <summary>
        /// Determines if the current movement is forced.
        /// </summary>
        public bool forcedMovement;

        /// <summary>
        /// Generic inheriated constructor.
        /// </summary>
        public Entity() { }
        /// <summary>
        /// Creates a Entity with the provided parameters.
        /// </summary>
        /// <param name="id">String that will be used to identify this entity.</param>
        /// <param name="type">The type this enity will be.</param>
        /// <param name="spriteSheet">The spritesheet this enity will use when drawing or when animated.</param>
        /// <param name="layer">The layer this entity will occupy.</param>
        /// <param name="hitbox">This entities hitbox for collision on a TileMap or other entities.</param>
        /// <param name="speed">Describes the MoveSpeed of the entity.</param>
        public Entity(string id, string type, Texture2D spriteSheet, int layer, Entitybox hitbox, MoveSpeed speed)
        {
            this.id = id;
            this.type = type;
            this.spriteSheet = spriteSheet;
            this.layer = layer;
            this.hitbox = hitbox;
            this.speed = speed;
            movement = EntityMovementState.idle;
            forcedMovement = false;
        }

        /// <summary>
        /// Updates this entity.
        /// </summary>
        public void UpdateEntity()
        {
            int movementAmount = speed.MovementAmount();
            switch (movement)
            {
                case EntityMovementState.idle:
                    //speed.RefreshLastMovementTime();
                    break;
                case EntityMovementState.movingUp:
                    MoveEntity(Orientation.up, movementAmount);
                    break;
                case EntityMovementState.movingDown:
                    MoveEntity(Orientation.down, movementAmount);
                    break;
                case EntityMovementState.movingRight:
                    MoveEntity(Orientation.right, movementAmount);
                    break;
                case EntityMovementState.movingRightUp:
                    MoveEntity(Orientation.right, (int)(movementAmount * (1 / Math.Sqrt(2))));
                    MoveEntity(Orientation.up, (int)(movementAmount * (1 / Math.Sqrt(2))));
                    break;
                case EntityMovementState.movingRightDown:
                    MoveEntity(Orientation.right, (int)(movementAmount * (1 / Math.Sqrt(2))));
                    MoveEntity(Orientation.down, (int)(movementAmount * (1 / Math.Sqrt(2))));
                    break;
                case EntityMovementState.movingLeft:
                    MoveEntity(Orientation.left, movementAmount);
                    break;
                case EntityMovementState.movingLeftUp:
                    MoveEntity(Orientation.left, (int)(movementAmount * (1 / Math.Sqrt(2))));
                    MoveEntity(Orientation.up, (int)(movementAmount * (1 / Math.Sqrt(2))));
                    break;
                case EntityMovementState.movingLeftDown:
                    MoveEntity(Orientation.left, (int)(movementAmount * (1 / Math.Sqrt(2))));
                    MoveEntity(Orientation.down, (int)(movementAmount * (1 / Math.Sqrt(2))));
                    break;
            }
        }
        /// <summary>
        /// Camera will do the provided action.
        /// </summary>
        /// <param name="actives">All active actionControls for the camera to do.</param>
        public void DoActions(List<ActionControl> actives)
        {
            bool up = false;
            bool down = false;
            bool right = false;
            bool left = false;

            if (actives.Exists(x => x.action == Actions.up))
            {
                up = true;
            }
            if (actives.Exists(x => x.action == Actions.down))
            {
                down = true;
            }
            if (actives.Exists(x => x.action == Actions.right))
            {
                right = true;
            }
            if (actives.Exists(x => x.action == Actions.left))
            {
                left = true;
            }

            if (up && !down)
            {
                if (right && !left)
                {
                   SetMovement(EntityMovementState.movingRightUp, false);
                }
                else if (left && !right)
                {
                    SetMovement(EntityMovementState.movingLeftUp, false);
                }
                else
                {
                    SetMovement(EntityMovementState.movingUp, false);
                    //System.Diagnostics.Debug.WriteLine("hello");
                }
            }
            else if (down && !up)
            {
                if (right && !left)
                {
                    SetMovement(EntityMovementState.movingRightDown, false);
                }
                else if (left && !right)
                {
                    SetMovement(EntityMovementState.movingLeftDown, false);
                }
                else
                {
                    SetMovement(EntityMovementState.movingDown, false);
                }
            }
            else if (right && !left)
            {
                SetMovement(EntityMovementState.movingRight, false);
            }
            else if (left && !right)
            {
                SetMovement(EntityMovementState.movingLeft, false);
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine("oh no");
                SetMovement(EntityMovementState.idle, false);
            }
        }
        /// <summary>
        /// Sets this entities movement state to the provided direction.
        /// </summary>
        /// <param name="direction">The direction this entity will move in per update.</param>
        /// <param name="forced">Determines if the provided movement state is forced.</param>
        public void SetMovement(EntityMovementState direction, bool forced)
        {
            if (movement != direction)
            {
                speed.RefreshLastMovementTime();
                movement = direction;
            }
            forcedMovement = forced;
        }
        /// <summary>
        /// Move the entity in the provided direction by the provided amount.
        /// </summary>
        /// <param name="direction">The direction for the entity to move.</param>
        /// <param name="amount">The amount for the entity to be moved by.</param>
        public void MoveEntity(Orientation direction, int amount)
        {
            Rectangle newCharacterArea;
            do
            {
                newCharacterArea = hitbox.characterArea;
                switch (direction)
                {
                    case Orientation.up:
                        newCharacterArea.Y += amount;
                        break;
                    case Orientation.right:
                        newCharacterArea.X += amount;
                        break;
                    case Orientation.left:
                        newCharacterArea.X -= amount;
                        break;
                    case Orientation.down:
                        newCharacterArea.Y -= amount;
                        break;
                }
                amount--;
            } while (!hitbox.AttemptMovement(layer, newCharacterArea) && amount != 0 && !forcedMovement);
        }
        /// <summary>
        /// Sets the characters position to be the provide point.
        /// </summary>
        /// <param name="posistion">The position for the entity to be moved to.</param>
        public void SetCharacterPosition(Point posistion)
        {
            hitbox.characterArea.X = posistion.X;
            hitbox.characterArea.Y = posistion.Y;
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