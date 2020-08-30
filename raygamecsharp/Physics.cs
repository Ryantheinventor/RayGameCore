using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using static raygamecsharp.GameObjectList;
using raygamecsharp;
using RGPhysics;

namespace RGPhysics
{
    class Physics
    {
        //keeps track of all collisions
        private static List<Collision> collisions = new List<Collision>();

        public static float gravity = 9.8f;
        public static void Update() 
        {
            //update kinimatic object positions
            foreach (GameObject g in Objects) {
                if (g.collider != null)
                {
                    Collider c = g.collider;
                    if (c.IsKinematic)
                    {
                        if (c.AutoClean && c.gameObject.transform.translation.Y > 3000)
                        {
                            Destroy(c.gameObject);
                        }
                        Vector2 v = c.Velocity;
                        c.gameObject.transform.translation += new Vector3(v * GetFrameTime(), 0);
                        if (c.EnableGravity)
                        {
                            c.Velocity += new Vector2(0, gravity);
                        }
                    }
                }
            }
            //Run collision checks
            if (Objects.Count > 1)
            {
                for (int i = 0; i < Objects.Count - 1; i++)
                {
                    if (Objects[i].collider != null)
                    {
                        for (int j = i + 1; j < Objects.Count; j++)
                        {
                            if (Objects[j].collider != null)
                            {
                                if (Objects[i].collider.CollidesWith(Objects[j].collider))
                                {
                                    collisions.Add(new Collision(Objects[j].collider, Objects[i].collider, true));
                                }
                            }
                        }
                    }
                    Objects[i].collider.ChecksForExits(collisions);
                }
            }
            if (Objects[Objects.Count - 1].collider != null)
            {
                Objects[Objects.Count - 1].collider.ChecksForExits(collisions);
            }

            //check if kinimatic objects have collided and react acordingly
            foreach (GameObject g in Objects)
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
                                    //TODO implemt reactions on collisions with kinematic objects
                                    
                                    collision.finished = true;
                                } 
                            }
                        }
                    }
                }
            }

            CallCollisions();
        }



        public static void CallCollisions() 
        {
            foreach (Collision c in collisions)
            {
                if (c.entered)
                {
                    c.collider1.gameObject.OnCollisionEnter(c.collider2);
                    c.collider2.gameObject.OnCollisionEnter(c.collider1);
                }
                else 
                {
                    c.collider1.gameObject.OnCollisionExit(c.collider2);
                    c.collider2.gameObject.OnCollisionExit(c.collider1);
                }
            }
        }

    }

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
        }

        public bool Includes(Collider collider) 
        {
            return collider == collider1 || collider == collider2;
        }

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
