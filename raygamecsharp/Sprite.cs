using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace raygamecsharp
{
    class Sprite : GameObject
    {
        public Color color = new Color();
        public Texture2D texture = new Texture2D();
        protected bool hasTexture = false;

        public Sprite(string name, Vector2 pos) : base(name,pos)
        { 
        
        }

        public override void Draw() 
        {
            DrawTextureV(texture, new Vector2(transform.translation.X - transform.scale.X / 2, transform.translation.Y - transform.scale.Y / 2), color);
        }

    }
}
