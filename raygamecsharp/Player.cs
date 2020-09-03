using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using static raygamecsharp.GameObjectList;
using RGPhysics;

namespace raygamecsharp
{
    class Player : Sprite
    {
        public Player(string name, Vector2 pos):base(name, pos) 
        { 
        
        }
        public override void Start()
        {
            //collider = new CircleCollider();
            //collider.IsStatic = true;
            //((CircleCollider)collider).radius = 20;
            transform.scale = new Vector3(40, 40, 0);
            collider = new RectangleCollider();
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            //transform.translation = new Vector3(100, 100, 0);
            HideCursor();
            texture = textures[0];
            color = WHITE;
        }

        public override void Update()
        {
            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                CollisionTestRectangle newTest = new CollisionTestRectangle("Test Falling", new Vector2(transform.translation.X, transform.translation.Y));
                NewObject(newTest);
                newTest.collider.Velocity = new Vector2(0, 0);
                newTest.collider.EnableGravity = true;
                newTest.collider.IsKinematic = true;
                newTest.collider.IsStatic = false;
            }
            if (IsKeyPressed(KeyboardKey.KEY_ONE))
            {
                
                CollisionTestRectangle newTest1 = new CollisionTestRectangle("Platform", new Vector2(transform.translation.X, transform.translation.Y));
                NewObject(newTest1);
                newTest1.collider.Velocity = new Vector2(0, 0);
                newTest1.collider.EnableGravity = false;
                newTest1.collider.IsKinematic = false;
                newTest1.collider.IsStatic = true;
                Console.WriteLine($"{newTest1.transform.translation.X},{newTest1.transform.translation.Y}");
            }
            if (IsKeyPressed(KeyboardKey.KEY_TWO))
            {
                CollisionTestRectangle newTest1 = new CollisionTestRectangle("Platform", new Vector2(transform.translation.X, transform.translation.Y), new Vector2(400, 40));
                NewObject(newTest1);
                newTest1.collider.Velocity = new Vector2(0, 0);
                newTest1.collider.EnableGravity = false;
                newTest1.collider.IsKinematic = false;
                newTest1.collider.IsStatic = true;
                Console.WriteLine($"{newTest1.transform.translation.X},{newTest1.transform.translation.Y}");
            }
            if (IsKeyPressed(KeyboardKey.KEY_THREE))
            {
                CollisionTestRectangle newTest1 = new CollisionTestRectangle("Platform", new Vector2(transform.translation.X, transform.translation.Y), new Vector2(40, 400));
                NewObject(newTest1);
                newTest1.collider.Velocity = new Vector2(0, 0);
                newTest1.collider.EnableGravity = false;
                newTest1.collider.IsKinematic = false;
                newTest1.collider.IsStatic = true;
                Console.WriteLine($"{newTest1.transform.translation.X},{newTest1.transform.translation.Y}");
            }
            if (IsMouseButtonPressed(MouseButton.MOUSE_MIDDLE_BUTTON))
            {
                CollisionTestRectangle newTest = new CollisionTestRectangle("Test Falling", new Vector2(transform.translation.X, transform.translation.Y));
                NewObject(newTest);
                newTest.collider.Velocity = new Vector2(new Random().Next(-600, 600), -(new Random().Next(100, 600)));
                newTest.collider.Velocity = new Vector2(0, -500);
                newTest.collider.IsKinematic = true;
                newTest.collider.IsStatic = false;
            }
            if (IsKeyPressed(KeyboardKey.KEY_Q))
            {
                CollisionTestRectangle newTest = new CollisionTestRectangle("Test Falling", new Vector2(transform.translation.X, transform.translation.Y));
                NewObject(newTest);
                newTest.collider.Velocity = new Vector2(new Random().Next(-600, 600), -(new Random().Next(100, 600)));
                newTest.collider.Velocity = new Vector2(-500, 0);
                newTest.collider.IsKinematic = true;
                newTest.collider.IsStatic = false;
            }
            if (IsKeyPressed(KeyboardKey.KEY_E))
            {
                CollisionTestRectangle newTest = new CollisionTestRectangle("Test Falling", new Vector2(transform.translation.X, transform.translation.Y));
                NewObject(newTest);
                newTest.collider.Velocity = new Vector2(new Random().Next(-600, 600), -(new Random().Next(100, 600)));
                newTest.collider.Velocity = new Vector2(500, 0);
                newTest.collider.IsKinematic = true;
                newTest.collider.IsStatic = false;
            }

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Vector2 mousePos = GetMousePosition();
            transform.translation = new Vector3(mousePos,0);
            

            //transform.translation += new Vector3(0, 2, 0);
        }

        public override void OnCollisionEnter(Collider other)
        {
            if (IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                Destroy(other.gameObject);
            }
        }

        //public override void Draw()
        //{
        //    base.Draw();
        //    DrawRectangle((int)transform.translation.X - (int)(transform.scale.X / 2), (int)transform.translation.Y - (int)(transform.scale.Y / 2), (int)transform.scale.X, (int)transform.scale.Y, RED);
        //}

    }
}
