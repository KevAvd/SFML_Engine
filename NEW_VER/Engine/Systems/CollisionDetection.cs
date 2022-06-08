using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.GameObjects;
using SFML_Engine.Mathematics;

namespace SFML_Engine.Systems
{
    static class CollisionDetection
    {
        /// <summary>
        /// Detect collision between AABB and a ray
        /// </summary>
        /// <param name="box1"> AABB to check </param>
        /// <param name="ray"> Ray to check </param>
        /// <param name="pNear"> Near collision point </param>
        /// <param name="pFar"> Fat collision point </param>
        /// <param name="normal"> Surface normal of the near collision point </param>
        /// <returns> True if collision </returns>
        public static bool AABB_RAY(AABB box1, Ray ray, out Vector2f pNear, out Vector2f pFar, out Vector2f normal)
        {
            float swap;                               //Used for swaping two values
            Vector2f[] aabbCoords = box1.GetPoints(); //Contains AABB's coords
            Vector2f[] rayCoords = ray.GetPoints();   //Contains ray's coords
            Vector2f d = rayCoords[1] - rayCoords[0]; //Ray distance
            Vector2f tNear = new Vector2f((aabbCoords[0].X - rayCoords[0].X) / d.X, (aabbCoords[0].Y - rayCoords[0].Y) / d.Y);   //time to near collision
            Vector2f tFar = new Vector2f((aabbCoords[2].X - rayCoords[0].X) / d.X, (aabbCoords[2].Y - rayCoords[0].Y) / d.Y);    //time to far collision

            //Init out parametres
            pNear = new Vector2f(0, 0);
            pFar = new Vector2f(0, 0);
            normal = new Vector2f(0, 0);

            //Sort values
            if (tNear.X > tFar.X)
            {
                swap = tNear.X;
                tNear.X = tFar.X;
                tFar.X = swap;
            }
            if (tNear.Y > tFar.Y) 
            {
                swap = tNear.Y;
                tNear.Y = tFar.Y;
                tFar.Y = swap;
            }

            //Check if there is a collision
            if (tNear.X > tFar.Y || tNear.Y > tFar.X)  { return false; }
            if (Math.Min(tFar.X, tFar.Y) < 0)  { return false; }
            float tHitNear = Math.Max(tNear.X, tNear.Y);
            float tHitFar = Math.Min(tFar.X, tFar.Y);
            if (tHitNear > 1) { return false; }

            //Get collision point
            pNear = rayCoords[0] + tHitNear * d;
            pFar = rayCoords[0] + tHitFar * d;

            //Find surface normal of the AABB at collision point
            if(tNear.X > tNear.Y)
            {
                if(d.X < 0)
                {
                    normal = new Vector2f(1, 0);
                }
                else
                {
                    normal = new Vector2f(-1, 0);
                }
            }
            else if (tNear.X < tNear.Y)
            {
                if(d.Y < 0)
                {
                    normal = new Vector2f(0, 1);
                }
                else
                {
                    normal = new Vector2f(0, -1);
                }
            }

            return true;
        } 

        /// <summary>
        /// Detect collision between two AABBs
        /// </summary>
        /// <param name="box1"> AABB to check </param>
        /// <param name="box2"> AABB to check </param>
        /// <returns> True if collision </returns>
        public static bool AABB_AABB(AABB box1, AABB box2)
        {
            //Get AABBs positions
            Vector2f[] p1 = box1.GetPoints();
            Vector2f[] p2 = box2.GetPoints();

            //Check for collision
            if (p1[0].X > p2[2].X)
            {
                return false;
            }
            if (p1[2].X < p2[0].X)
            {
                return false;
            }
            if (p1[0].Y > p2[2].Y)
            {
                return false;
            }
            if (p1[2].Y < p2[0].Y)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Detect collision between two circles
        /// </summary>
        /// <param name="circle1"> Circle to check </param>
        /// <param name="circle2"> Circle to check </param>
        /// <returns> True if collision </returns>
        public static bool CIRCLE_CIRCLE(Circle circle1, Circle circle2)
        {
            //Get circles positions
            Vector2f pos1 = circle1.GetPoints()[0];
            Vector2f pos2 = circle2.GetPoints()[0];

            //Calculate sum of both radius
            float radius_sum = circle1.Radius + circle2.Radius;

            //Calculate distance between both circles
            float d = GameMath.GetVectorLength(pos2 - pos1);

            //Check for collision
            if(d <= radius_sum)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Detect collision between Circle and AABB
        /// </summary>
        /// <param name="circle"> Circle to check </param>
        /// <param name="box"> AABB to check </param>
        /// <returns> True if collision </returns>
        public static bool CIRCLE_AABB(Circle circle, AABB box)
        {
            Vector2f contactPoint;
            Vector2f circlePosition = circle.GetPoints()[0];
            
            float half_w = box.Width / 2.0f;
            float half_h = box.Height / 2.0f;
            Vector2f AABBPosition = box.GetPoints()[0] + new Vector2f(half_w, half_h);

            float dx = circlePosition.X - AABBPosition.X; 
            float dy = circlePosition.Y - AABBPosition.Y;

            if(Math.Abs(dx) > half_w)
            {
                if(dx < 0)
                {
                    dx = half_w * -1;
                }
                else
                {
                    dx = half_w;
                }
            }

            if(Math.Abs(dy) > half_h)
            {
                if(dy < 0)
                {
                    dy = half_h * -1;
                }
                else
                {
                    dy = half_h;
                }
            }

            contactPoint = AABBPosition + new Vector2f(dx, dy);

            if(GameMath.GetVectorLength(circlePosition - contactPoint) > circle.Radius)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Detect collision between two physic object
        /// </summary>
        /// <param name="obj1"> Physic object to check </param>
        /// <param name="obj2"> Physic object to check </param>
        /// <param name="type"> Type of collision </param>
        /// <returns> True if collision </returns>
        public static bool PHYSOBJ_PHYSOBJ(PhysicObject obj1, PhysicObject obj2, out Collision.CollisionType type)
        {
            if(obj1.GetType() == typeof(AABB))
            {
                if(obj2.GetType() == typeof(AABB))
                {
                    type = Collision.CollisionType.AABB_AABB;
                    return AABB_AABB(obj1 as AABB, obj2 as AABB);
                }

                else if (obj2.GetType() == typeof(Ray))
                {
                    type = Collision.CollisionType.AABB_RAY;
                    return AABB_RAY(obj1 as AABB, obj2 as Ray, out Vector2f pNear, out Vector2f pFar, out Vector2f normal);
                }

                else if (obj2.GetType() == typeof(Circle))
                {
                    type = Collision.CollisionType.AABB_CIRCLE;
                    return CIRCLE_AABB(obj2 as Circle, obj1 as AABB);
                }
            }

            else if(obj1.GetType() == typeof(Ray))
            {
                if(obj2.GetType() == typeof(AABB))
                {
                    type = Collision.CollisionType.AABB_RAY;
                    return AABB_RAY(obj2 as AABB, obj1 as Ray, out Vector2f pNear, out Vector2f pFar, out Vector2f normal);
                }
            }

            else if(obj1.GetType() == typeof(Circle))
            {
                if(obj2.GetType() == typeof(Circle))
                {
                    type = Collision.CollisionType.CIRCLE_CIRCLE;
                    return CIRCLE_CIRCLE(obj1 as Circle, obj2 as Circle);
                }

                else if(obj2.GetType() == typeof(AABB))
                {
                    type = Collision.CollisionType.AABB_CIRCLE;
                    return CIRCLE_AABB(obj1 as Circle, obj2 as AABB);
                }
            }

            type = Collision.CollisionType.AABB_AABB;
            return false;
        }

        /// <summary>
        /// Detect all collision in a game state
        /// </summary>
        /// <param name="state"></param>
        public static void DetectAllCollision(GameState state)
        {
            foreach(GameObject obj1 in state.Objects)
            {
                foreach (GameObject obj2 in state.Objects)
                {
                    if(obj1 == obj2) { continue; }

                    if(PHYSOBJ_PHYSOBJ(obj1.PhysicObject, obj2.PhysicObject, out Collision.CollisionType type))
                    {
                        state.AddCollision(new Collision(obj1, obj2, type));
                    }
                }
            }
        }
    }
}
