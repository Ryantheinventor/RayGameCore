using System;
using System.Collections.Generic;
using System.Numerics;
using static Raylib_cs.Raylib;
using static RGCore.GameObjectList;


namespace RGCore.RGPhysics
{
    class Physics
    {
        //keeps track of all collisions
        private static List<Collision> collisions = new List<Collision>();

        public static float gravity = 19.6f;
        public static void Update() 
        {
            //update kinimatic object positions
            foreach (GameObject g in objects) {
                if (g.collider != null)
                {
                    
                    Collider c = g.collider;
                    c.lastPos = new Vector2(c.gameObject.transform.translation.X,c.gameObject.transform.translation.Y);
                    if (c.IsKinematic)
                    {
                        if (c.AutoClean && c.gameObject.transform.translation.Y > 3000) //destroy "AutoClean" objects that fall out of the world.
                        {
                            Destroy(c.gameObject);
                        }
                        Vector2 v = c.Velocity;
                        c.gameObject.transform.translation += new Vector3(v * GetFrameTime(), 0); //translate object based off it's velocity
                        if (c.EnableGravity)//add effect of gravity to velocity
                        {
                            c.Velocity += new Vector2(0, gravity);
                        }
                    }
                }
            }
            //Run collision checks
            if (objects.Count > 1)
            {
                for (int i = 0; i < objects.Count - 1; i++)
                {
                    if (objects[i].collider != null)
                    {
                        for (int j = i + 1; j < objects.Count; j++)
                        {
                            if (objects[j].collider != null)
                            {
                                if (objects[i].collider.CollidesWith(objects[j].collider))
                                {
                                    collisions.Add(new Collision(objects[j].collider, objects[i].collider, true));
                                }
                            }
                        }
                        objects[i].collider.ChecksForExits(collisions);
                    }
                    
                }
            }
            if (objects[objects.Count - 1].collider != null)
            {
                objects[objects.Count - 1].collider.ChecksForExits(collisions);
            }

            //check if kinimatic objects have collided and react acordingly
            foreach (GameObject g in objects)
            {
                if (g.collider != null)
                {
                    Collider c = g.collider;
                    if (c.IsKinematic)
                    {
                        foreach (Collision collision in collisions) 
                        {
                            if (!collision.finished) {
                                if (collision.Includes(c))
                                {
                                    Vector2 c2Pos = collision.collider2.GetClosestMidpoint(new Vector2(collision.collider1.gameObject.transform.translation.X,collision.collider1.gameObject.transform.translation.Y));
                                    string direction = "UP";
                                    if (collision.collider1.ColliderType == "Rectangle")
                                    {
                                        //find where the edges of the collider1 are
                                        float edgeRight = collision.collider1.lastPos.X + (((RectangleCollider)collision.collider1).scale.X / 2);
                                        float edgeLeft = collision.collider1.lastPos.X - (((RectangleCollider)collision.collider1).scale.X / 2);
                                        float edgeDown = collision.collider1.lastPos.Y + (((RectangleCollider)collision.collider1).scale.Y / 2);
                                        float edgeUp = collision.collider1.lastPos.Y - (((RectangleCollider)collision.collider1).scale.Y / 2);

                                        //find where collider2 is in relation to the edges of collider1
                                        float edgeRightDist = (edgeRight - c2Pos.X) * -1;
                                        float edgeLeftDist = edgeLeft - c2Pos.X;
                                        float edgeDownDist = (edgeDown - c2Pos.Y) * -1;
                                        float edgeUpDist = edgeUp - c2Pos.Y;

                                        //find the shortest distance from an edge to the center of collider1
                                        float inToMid = (((RectangleCollider)collision.collider1).scale.Y / 2) * -1;
                                        if ((((RectangleCollider)collision.collider1).scale.X / 2) >= inToMid)
                                        {
                                            inToMid = (((RectangleCollider)collision.collider1).scale.X / 2) * -1;
                                        }


                                        //find where collider2 is in relation to collider1
                                        if (edgeUpDist >= inToMid && edgeLeftDist <= edgeUpDist && edgeRightDist <= edgeUpDist)
                                        {
                                            direction = "Up";
                                        }
                                        else if (edgeDownDist >= inToMid && edgeLeftDist <= edgeDownDist && edgeRightDist <= edgeDownDist)
                                        {
                                            direction = "Down";
                                        }
                                        else if (edgeLeftDist > inToMid && edgeUpDist <= edgeLeftDist && edgeDownDist <= edgeLeftDist)
                                        {
                                            direction = "Left";
                                        }
                                        else if (edgeRightDist >= inToMid && edgeUpDist <= edgeRightDist && edgeDownDist <= edgeRightDist)
                                        {
                                            direction = "Right";
                                        }
                                    }
                                    else 
                                    {
                                        Console.WriteLine($"Unsuported physics collider:{collision.collider1.ColliderType}");
                                    }
                                    
                                    //bounce uses the highest bounce value involved 
                                    float bounce = collision.collider1.Bounce;
                                    if (bounce < collision.collider2.Bounce) 
                                    {
                                        bounce = collision.collider2.Bounce;
                                    }


                                    if (collision.collider1.IsKinematic && !collision.collider2.IsKinematic)//move collider1 out of collider2
                                    {
                                        switch (direction)
                                        {
                                            case "Down":
                                                if (collision.collider1.ColliderType == "Rectangle" && collision.collider2.ColliderType == "Rectangle")
                                                {
                                                    collision.collider1.gameObject.transform.translation.Y = collision.collider2.gameObject.transform.translation.Y - ((((RectangleCollider)collision.collider2).scale.Y / 2) + (((RectangleCollider)collision.collider1).scale.Y / 2));
                                                    if(collision.collider1.Velocity.Y >= 0)
                                                        collision.collider1.Velocity = new Vector2(collision.collider1.Velocity.X, -collision.collider1.Velocity.Y*bounce);
                                                }
                                                break;
                                            case "Up":
                                                if (collision.collider1.ColliderType == "Rectangle" && collision.collider2.ColliderType == "Rectangle")
                                                {
                                                    collision.collider1.gameObject.transform.translation.Y = collision.collider2.gameObject.transform.translation.Y + ((((RectangleCollider)collision.collider2).scale.Y / 2) + (((RectangleCollider)collision.collider1).scale.Y / 2));
                                                    if (collision.collider1.Velocity.Y <= 0)
                                                        collision.collider1.Velocity = new Vector2(collision.collider1.Velocity.X, -collision.collider1.Velocity.Y * bounce);
                                                }
                                                break;
                                            case "Left":
                                                if (collision.collider1.ColliderType == "Rectangle" && collision.collider2.ColliderType == "Rectangle")
                                                {
                                                    collision.collider1.gameObject.transform.translation.X = collision.collider2.gameObject.transform.translation.X + ((((RectangleCollider)collision.collider2).scale.X / 2) + (((RectangleCollider)collision.collider1).scale.X / 2));
                                                    if (collision.collider1.Velocity.X <= 0)
                                                        collision.collider1.Velocity = new Vector2(-collision.collider1.Velocity.X * bounce, collision.collider1.Velocity.Y);
                                                }
                                                break;
                                            case "Right":
                                                if (collision.collider1.ColliderType == "Rectangle" && collision.collider2.ColliderType == "Rectangle")
                                                {
                                                    collision.collider1.gameObject.transform.translation.X = collision.collider2.gameObject.transform.translation.X - ((((RectangleCollider)collision.collider2).scale.X / 2) + (((RectangleCollider)collision.collider1).scale.X / 2));
                                                    if (collision.collider1.Velocity.X >= 0)
                                                        collision.collider1.Velocity = new Vector2(-collision.collider1.Velocity.X * bounce, collision.collider1.Velocity.Y);
                                                }
                                                break;

                                        }
                                    }
                                    else if (!collision.collider1.IsKinematic && collision.collider2.IsKinematic)//move collider2 out of collider1
                                    {
                                        switch (direction)
                                        {
                                            case "Up":
                                                if (collision.collider2.ColliderType == "Rectangle" && collision.collider1.ColliderType == "Rectangle")
                                                {
                                                    collision.collider2.gameObject.transform.translation.Y = collision.collider1.gameObject.transform.translation.Y - ((((RectangleCollider)collision.collider1).scale.Y / 2) + (((RectangleCollider)collision.collider2).scale.Y / 2));
                                                    if (collision.collider2.Velocity.Y >= 0)
                                                        collision.collider2.Velocity = new Vector2(collision.collider2.Velocity.X, -collision.collider2.Velocity.Y * bounce);
                                                }
                                                break;
                                            case "Down":
                                                if (collision.collider2.ColliderType == "Rectangle" && collision.collider1.ColliderType == "Rectangle")
                                                {
                                                    collision.collider2.gameObject.transform.translation.Y = collision.collider1.gameObject.transform.translation.Y + ((((RectangleCollider)collision.collider1).scale.Y / 2) + (((RectangleCollider)collision.collider2).scale.Y / 2));
                                                    if (collision.collider2.Velocity.Y <= 0)
                                                        collision.collider2.Velocity = new Vector2(collision.collider2.Velocity.X, -collision.collider2.Velocity.Y * bounce);
                                                }
                                                break;
                                            case "Left":
                                                if (collision.collider2.ColliderType == "Rectangle" && collision.collider1.ColliderType == "Rectangle")
                                                {
                                                    collision.collider2.gameObject.transform.translation.X = collision.collider1.gameObject.transform.translation.X - ((((RectangleCollider)collision.collider1).scale.X / 2) + (((RectangleCollider)collision.collider2).scale.X / 2));
                                                    if (collision.collider1.Velocity.X <= 0)
                                                        collision.collider2.Velocity = new Vector2(-collision.collider2.Velocity.X * bounce, collision.collider2.Velocity.Y);
                                                }
                                                break;
                                            case "Right":
                                                if (collision.collider2.ColliderType == "Rectangle" && collision.collider1.ColliderType == "Rectangle")
                                                {
                                                    collision.collider2.gameObject.transform.translation.X = collision.collider1.gameObject.transform.translation.X + ((((RectangleCollider)collision.collider1).scale.X / 2) + (((RectangleCollider)collision.collider2).scale.X / 2));
                                                    if (collision.collider1.Velocity.X >= 0)
                                                        collision.collider2.Velocity = new Vector2(-collision.collider2.Velocity.X * bounce, collision.collider2.Velocity.Y);
                                                }
                                                break;
                                        }
                                        
                                    }
                                    else if (collision.collider1.IsKinematic && collision.collider2.IsKinematic)//move collider1 out of collider2 then mix velocities 
                                    {
                                        
                                        switch (direction)
                                        {
                                            case "Up":
                                                if (collision.collider2.ColliderType == "Rectangle" && collision.collider1.ColliderType == "Rectangle")
                                                {
                                                    collision.collider2.gameObject.transform.translation.Y = collision.collider1.gameObject.transform.translation.Y - ((((RectangleCollider)collision.collider1).scale.Y / 2) + (((RectangleCollider)collision.collider2).scale.Y / 2));
                                                    float newV = (collision.collider1.Velocity.Y + collision.collider2.Velocity.Y) / 2;
                                                    collision.collider1.Velocity = new Vector2(collision.collider1.Velocity.X, newV);
                                                    collision.collider2.Velocity = new Vector2(collision.collider2.Velocity.X, newV);
                                                }
                                                break;
                                            case "Down":
                                                if (collision.collider2.ColliderType == "Rectangle" && collision.collider1.ColliderType == "Rectangle")
                                                {
                                                    collision.collider2.gameObject.transform.translation.Y = collision.collider1.gameObject.transform.translation.Y + ((((RectangleCollider)collision.collider1).scale.Y / 2) + (((RectangleCollider)collision.collider2).scale.Y / 2));
                                                    float newV = (collision.collider1.Velocity.Y + collision.collider2.Velocity.Y) / 2;
                                                    collision.collider1.Velocity = new Vector2(collision.collider1.Velocity.X, newV);
                                                    collision.collider2.Velocity = new Vector2(collision.collider2.Velocity.X, newV);
                                                }
                                                break;
                                            case "Left":
                                                if (collision.collider2.ColliderType == "Rectangle" && collision.collider1.ColliderType == "Rectangle")
                                                {
                                                    collision.collider2.gameObject.transform.translation.X = collision.collider1.gameObject.transform.translation.X - ((((RectangleCollider)collision.collider1).scale.X / 2) + (((RectangleCollider)collision.collider2).scale.X / 2));
                                                    float newV = (collision.collider1.Velocity.X + collision.collider2.Velocity.X) / 2;
                                                    collision.collider1.Velocity = new Vector2(newV, collision.collider1.Velocity.Y);
                                                    collision.collider2.Velocity = new Vector2(newV, collision.collider2.Velocity.Y);
                                                }
                                                break;
                                            case "Right":
                                                if (collision.collider2.ColliderType == "Rectangle" && collision.collider1.ColliderType == "Rectangle")
                                                {
                                                    collision.collider2.gameObject.transform.translation.X = collision.collider1.gameObject.transform.translation.X + ((((RectangleCollider)collision.collider1).scale.X / 2) + (((RectangleCollider)collision.collider2).scale.X / 2));
                                                    float newV = (collision.collider1.Velocity.X + collision.collider2.Velocity.X) / 2;
                                                    collision.collider1.Velocity = new Vector2(newV, collision.collider1.Velocity.Y);
                                                    collision.collider2.Velocity = new Vector2(newV, collision.collider2.Velocity.Y);
                                                }
                                                break;
                                        }
                                    }

                                    //mark the collision as complete so it is not attempted again
                                    collision.finished = true;
                                } 
                            }
                        }
                    }
                }
            }

           
            CallCollisions();
            collisions = new List<Collision>();
        }


        /// <summary>
        /// Call GameObject collison functions
        /// </summary>
        public static void CallCollisions() 
        {
            foreach (Collision c in collisions) 
            { 
                if (c.entered)
                {
                    c.collider1.gameObject.OnCollisionStay(c.collider2);
                    c.collider2.gameObject.OnCollisionStay(c.collider1);
                }
                else 
                {
                    c.collider1.gameObject.OnCollisionExit(c.collider2);
                    c.collider2.gameObject.OnCollisionExit(c.collider1);
                }
            }
        }

    }

    /// <summary>
    /// Includes all collision related data
    /// </summary>
    class Collision 
    {
        public bool finished = false;
        public bool entered = true;
        public Collider collider1;
        public Collider collider2;
        public Collision(Collider collider1, Collider collider2, bool entered) 
        {
            this.entered = entered;
            this.collider1 = collider1;
            this.collider2 = collider2;
            if ((!collider1.IsStatic && !collider1.IsKinematic) || (!collider2.IsStatic && !collider2.IsKinematic)) 
            {
                finished = true;
            }
        }

        /// <returns>True if collider is part of this collision</returns>
        public bool Includes(Collider collider) 
        {
            return collider == collider1 || collider == collider2;
        }

        /// <returns>The collider not passed in</returns>
        public Collider GetOther(Collider collider)
        {
            if (collider == collider1)
            {
                return collider2;
            }
            else 
            {
                return collider1;
            }
        }

    }

}
