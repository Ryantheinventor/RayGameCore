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
    class Exit : Sprite
    {
        bool hasWon = false;


        public Exit(string name, Vector2 pos) : base(name, pos)
        {

        }
        public override void Start()
        {

            transform.scale = new Vector3(80, 40, 0);
            collider = new RectangleCollider();
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            HideCursor();
            texture = textures[1];
            color = WHITE;
        }
        public override void Update()
        {
        }

        public override void Draw()
        {
            base.Draw();
            if (hasWon)
            {
                DrawText("You Win!!!",560,400,100,GREEN);
            }
            else 
            {
                DrawText("Collect the pickups before exiting", 560, 835, 30, WHITE);
            }
        }

        public override void OnCollisionEnter(Collider other)
        {
            if (other.gameObject.name == "Player") 
            {
                bool canExit = true;
                foreach (GameObject g in objects) 
                {
                    if (g.name == "Pickup") 
                    {
                        canExit = false;
                        break;
                    }
                }
                if (canExit)
                {
                    hasWon = true;
                }
            }
        }
    }
}
