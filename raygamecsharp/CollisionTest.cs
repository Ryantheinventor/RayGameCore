using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using static raygamecsharp.GameObjectList;

namespace raygamecsharp
{
    class CollisionTestRectangle : GameObject
    {
        float timePassed = 0;
        Color color = RED;
        public CollisionTestRectangle(string name, Vector2 pos) : base(name, pos)
        { 
        
        }

        public override void Start()
        {
            transform.scale = new Vector3(40, 40, 0);
            collider = new RectangleCollider();
            collider.IsKinematic = true;
            collider.EnableGravity = true;
            collider.Velocity = new Vector2(100, -500);
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
        }

        public override void Update()
        {
            timePassed += GetFrameTime();
            if (timePassed > 1)
            {
                timePassed = 0;
                CollisionTestRectangle newTest = new CollisionTestRectangle("Test", new Vector2(transform.translation.X, transform.translation.Y));
                NewObject(newTest);
                newTest.collider.Velocity = new Vector2(new Random().Next(-600, 600), -(new Random().Next(100, 600)));
            }
        }

        public override void Draw()
        {
            base.Draw();
            DrawRectangle((int)transform.translation.X - (int)(transform.scale.X / 2), (int)transform.translation.Y - (int)(transform.scale.Y / 2), (int)transform.scale.X, (int)transform.scale.Y, color);

        }

        public override void OnCollisionEnter(Collider other)
        {
            color = GREEN;
        }

        public override void OnCollisionExit(Collider other)
        {
            color = RED;
            
        }
    }
    class CollisionTestCircle : GameObject
    {
        Color color = RED;
        public CollisionTestCircle(string name, Vector2 pos) : base(name, pos)
        {

        }

        public override void Start()
        {
            transform.scale = new Vector3(40, 40, 0);
            collider = new CircleCollider();
            ((CircleCollider)collider).radius = 20;
            base.Start();

        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            base.Draw();
            
            DrawCircle((int)transform.translation.X, (int)transform.translation.Y, 20, color);
        }

        public override void OnCollisionEnter(Collider other)
        {
            color = GREEN;
            
        }

        public override void OnCollisionExit(Collider other)
        {
            color = RED;
        }
    }
}
