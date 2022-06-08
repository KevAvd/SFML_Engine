using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine.GameObjects;

namespace SFML_Engine.Systems
{
    class Collision
    {
        GameObject _obj1;
        GameObject _obj2;
        CollisionType _collisionType;

        /// <summary>
        /// Get collided game object
        /// </summary>
        internal GameObject Obj1 { get => _obj1; }

        /// <summary>
        /// Get collided game object
        /// </summary>
        internal GameObject Obj2 { get => _obj2; }

        /// <summary>
        /// Get collision type
        /// </summary>
        internal CollisionType Type { get => _collisionType; }

        public enum CollisionType
        {
            AABB_AABB,
            AABB_RAY,
            AABB_CIRCLE,
            CIRCLE_CIRCLE
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obj1"> Collided object </param>
        /// <param name="obj2"> Collided object </param>
        /// <param name="type"> Type of collision </param>
        public Collision(GameObject obj1, GameObject obj2, CollisionType type)
        {
            _obj1 = obj1;
            _obj2 = obj2;
            _collisionType = type;
        }
    }
}
