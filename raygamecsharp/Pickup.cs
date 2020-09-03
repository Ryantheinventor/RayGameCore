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
    class Pickup : Sprite
    {
        public Pickup(string name, Vector2 pos) : base(name, pos)
        {

        }
        public override void Start()
        {

            transform.scale = new Vector3(30, 30, 0);
            collider = new RectangleCollider();
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            HideCursor();
            texture = textures[2];
            color = WHITE;
        }

        public override void OnCollisionEnter(Collider other)
        {
            if (other.gameObject.name == "Player")
            {
                Destroy(this);
            }
        }
    }
}
