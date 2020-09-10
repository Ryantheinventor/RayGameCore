using System;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using static RGCore.GameObjectList;
using RGCore.RGPhysics;

namespace RGCore
{
    class Pickup : Sprite
    {
        float startingY = 0f;
        bool goingUp = true;
        public Pickup(string name, Vector2 pos) : base(name, pos)
        {

        }
        public override void Start()
        {

            transform.scale = new Vector3(30, 30, 0);
            collider = new RectangleCollider();
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            texture = textures["PickupSheet"];
            sheetSize = 36;
            frameTime = 0.05f;
            color = WHITE;
            base.Start();
            HideCursor();
            startingY = transform.translation.Y;
            transform.translation.Y = startingY - new Random().Next(1, 10);
            frame = new Random().Next(11, 36);
        }

        public override void Update()
        {
            if (goingUp)
            {
                transform.translation.Y -= 10f*GetFrameTime();
                if (transform.translation.Y <= startingY - 10) 
                {
                    goingUp = false;
                }
            }
            else 
            {
                transform.translation.Y += 10f * GetFrameTime();
                if (transform.translation.Y >= startingY)
                {
                    goingUp = true;
                }
            }
        }

        public override void OnCollisionStay(Collider other)
        {
            if (other.gameObject.name == "Player")
            { 
                PlaySound(sounds["CoinSound"]);
                Destroy(this);
            }
        }
    }
}
