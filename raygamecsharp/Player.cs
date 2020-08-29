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
    class Player : GameObject
    {
        public Player(string name, Vector2 pos):base(name, pos) 
        { 
        
        }
        public override void Start()
        {
            collider = new CircleCollider();
            ((CircleCollider)collider).radius = 20;
            base.Start();
            //transform.translation = new Vector3(100, 100, 0);
            HideCursor();
        }

        public override void Update()
        {
            base.Update();
            Vector2 mousePos = GetMousePosition();
            transform.translation = new Vector3(mousePos,0);
            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) 
            {
                CollisionTestRectangle newTest = new CollisionTestRectangle("Test",new Vector2(transform.translation.X, transform.translation.Y));
                NewObject(newTest);
                newTest.collider.Velocity = new Vector2(new Random().Next(-600, 600),-(new Random().Next(100,600)));
            }

            //transform.translation += new Vector3(0, 2, 0);
        }

        public override void Draw()
        {
            base.Draw();
            DrawCircle((int)transform.translation.X, (int)transform.translation.Y, 20, RED);
        }

    }
}
