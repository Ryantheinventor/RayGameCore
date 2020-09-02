﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using Raylib_cs;
using raygamecsharp;
using RGPhysics;

namespace RGPhysics
{
    abstract class Collider
    {
        public abstract bool AutoClean { get; set; }
        public abstract bool IsStatic { get; set; }
        public abstract bool IsKinematic { get; set; }
        public abstract Vector2 Velocity { get; set; }
        public abstract float Bounce { get; set; }
        public abstract bool EnableGravity { get; set; }
        protected List<Collider> lastCheck = new List<Collider>();
        protected List<Collider> thisCheck = new List<Collider>();
        public abstract string ColliderType { get; }
        public GameObject gameObject;
        public Vector2 lastPos = new Vector2();

        public abstract void Draw();
        public abstract bool CollidesWith(Collider other);
        public abstract bool CollidesWithCircle(CircleCollider other);
        public abstract bool CollidesWithRec(RectangleCollider other);
        public abstract void ChecksForExits(List<Collision> collisions);
        public abstract Vector2 GetClosestMidpoint(Vector2 target);
    }

    class CircleCollider : Collider
    {
        public override float Bounce { get; set; } = 1f;
        public override bool AutoClean { get; set; } = false;
        public override Vector2 Velocity { get; set; } = new Vector2(0, 0);
        public override bool IsKinematic { get; set; } = false;
        public override bool IsStatic { get; set; } = false;
        public override bool EnableGravity { get; set; } = false;
        public override string ColliderType 
        {
            get => "Circle";
        }
        
        public float radius = 1f;
        
        public override void Draw() 
        {
            
            DrawCircleLines((int)gameObject.transform.translation.X, (int)gameObject.transform.translation.Y, radius, GREEN);
        }

        public override bool CollidesWith(Collider other) 
        {
            return other.CollidesWithCircle(this);
        }

        public override bool CollidesWithCircle(CircleCollider other)
        {
            Vector2 myPos = new Vector2(gameObject.transform.translation.X, gameObject.transform.translation.Y);
            Vector2 otherPos = new Vector2(other.gameObject.transform.translation.X, other.gameObject.transform.translation.Y);
            if (CheckCollisionCircles(myPos, radius, otherPos, other.radius)) 
            {
                thisCheck.Add(other);
                return true;
            }
            return false;
        }

        public override bool CollidesWithRec(RectangleCollider other)
        {
            Vector2 myPos = new Vector2(gameObject.transform.translation.X, gameObject.transform.translation.Y);
            Vector2 otherPos = new Vector2(other.gameObject.transform.translation.X, other.gameObject.transform.translation.Y);
            Rectangle otherRec = new Rectangle(otherPos.X - (other.scale.X / 2), otherPos.Y - (other.scale.Y / 2), other.scale.X, other.scale.Y);
            if (CheckCollisionCircleRec(myPos, radius, otherRec)) 
            {
                thisCheck.Add(other);
                return true;
            }
            return false;
        }
        public override void ChecksForExits(List<Collision> collisions) 
        {
            
            foreach (Collider c in lastCheck) 
            {
                if (!thisCheck.Contains(c)) 
                {
                    collisions.Add(new Collision(this, c, false));
                }
            }
            lastCheck = new List<Collider>(thisCheck);
            thisCheck = new List<Collider>();
        }

        public override Vector2 GetClosestMidpoint(Vector2 target)
        {
            return new Vector2(gameObject.transform.translation.X, gameObject.transform.translation.Y);
        }
    }

    class RectangleCollider : Collider
    {
        public override float Bounce { get; set; } = 1f;
        public override bool AutoClean { get; set; } = false;
        public override bool EnableGravity { get; set; } = false;
        public override Vector2 Velocity { get; set; } = new Vector2(0,0);
        public override bool IsKinematic { get; set; } = false;
        public override bool IsStatic { get; set; } = false;
        public override string ColliderType
        {
            get => "Rectangle";
        }
        public Vector2 scale = new Vector2(1f,1f);

        public override void Draw()
        {
            DrawRectangleLines((int)gameObject.transform.translation.X, (int)gameObject.transform.translation.Y, (int)scale.X, (int)scale.Y,GREEN);
        }

        public override bool CollidesWith(Collider other)
        {
            return other.CollidesWithRec(this);
        }

        public override bool CollidesWithCircle(CircleCollider other)
        {
            Vector2 otherPos = new Vector2(other.gameObject.transform.translation.X, other.gameObject.transform.translation.Y);
            Rectangle myRec = new Rectangle(gameObject.transform.translation.X - (scale.X / 2), gameObject.transform.translation.Y - (scale.Y / 2), scale.X, scale.Y);
            if (CheckCollisionCircleRec(otherPos, other.radius, myRec)) 
            {
                thisCheck.Add(other);
                return true;
            }
            return false;
        }

        public override bool CollidesWithRec(RectangleCollider other)
        {
            Rectangle myRec = new Rectangle(gameObject.transform.translation.X - (scale.X/2), gameObject.transform.translation.Y - (scale.Y / 2), scale.X, scale.Y);
            Rectangle otherRec = new Rectangle(other.gameObject.transform.translation.X - (other.scale.X / 2), other.gameObject.transform.translation.Y - (other.scale.Y / 2), other.scale.X, other.scale.Y);
            if (CheckCollisionRecs(myRec, otherRec)) 
            {
                thisCheck.Add(other);
                return true;
            }
            return false;
        }
        public override void ChecksForExits(List<Collision> collisions)
        {

            foreach (Collider c in lastCheck)
            {
                if (!thisCheck.Contains(c))
                {
                    collisions.Add(new Collision(this, c, false));
                }
            }
            lastCheck = new List<Collider>(thisCheck);
            thisCheck = new List<Collider>();
        }

        public override Vector2 GetClosestMidpoint(Vector2 target)
        {
            Vector2 output = new Vector2(gameObject.transform.translation.X, gameObject.transform.translation.Y);
            if (scale.X > scale.Y)
            {
                float max = ((scale.X - scale.Y) / 2) + gameObject.transform.translation.X;
                float min = (((scale.X - scale.Y) / 2) * -1) + gameObject.transform.translation.X;
                if (target.X <= max && target.X >= min)
                {
                    output.X = target.X;
                }
                else if (target.X > max)
                {
                    output.X = max;
                }
                else if (target.X < min)
                {
                    output.X = min;
                }

            } 
            else if(scale.X < scale.Y)
            {
                float max = ((scale.Y - scale.X) / 2) + gameObject.transform.translation.Y;
                float min = (((scale.Y - scale.X) / 2) * -1) + gameObject.transform.translation.Y;
                if (target.Y <= max && target.Y >= min)
                {
                    output.Y = target.Y;
                }
                else if (target.Y > max)
                {
                    output.Y = max;
                }
                else if (target.Y < min)
                {
                    output.Y = min;
                }
            }


            return output;
        }
    }
}
