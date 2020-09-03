using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace RGCore
{
    class Sprite : GameObject
    {
        public Color color = WHITE;
        public Texture2D texture = new Texture2D();
        protected bool hasTexture = false;
        public int sheetSize = 1;
        public int frame = 0;
        public float frameTime = 0.5f;
        private float curFrameTime = 0f;
        private List<Rectangle> frames = new List<Rectangle>();
        

        public Sprite(string name, Vector2 pos) : base(name, pos)
        {

        }
        public Sprite(string name, Vector2 pos, Texture2D texture, int sheetSize, float frameTime) : base(name, pos)
        {
            SetSprite(texture,sheetSize,frameTime);
            transform.scale = new Vector3(texture.width, texture.height,0);
            color = WHITE;
        }
        public override void Start()
        {
            SetSprite(texture, sheetSize, frameTime);
            base.Start();
        }

        public void SetSprite(Texture2D texture, int sheetSize, float frameTime) 
        {
            frames = new List<Rectangle>();
            this.sheetSize = sheetSize;
            this.frameTime = frameTime;
            this.texture = texture;
            curFrameTime = 0f;
            frame = 0;

            Vector2 frameSize = new Vector2(texture.width / sheetSize, texture.height);
            for (int i = 0; i < sheetSize; i++)
            {
                frames.Add(new Rectangle(frameSize.X * i, 0f, frameSize.X, frameSize.Y));
            }
        }

        public override void Draw() 
        {
            if (curFrameTime > frameTime)
            {
                curFrameTime = 0;
                if (frame == sheetSize - 1)
                {
                    frame = 0;
                }
                else
                {
                    frame++;
                }
            }
            curFrameTime += GetFrameTime();

            Rectangle rec = new Rectangle(transform.translation.X - transform.scale.X / 2, transform.translation.Y - transform.scale.Y / 2, transform.scale.X, transform.scale.Y);
            DrawTexturePro(texture, frames[frame], rec, new Vector2(0,0),0,color);
            //DrawTextureV(texture, new Vector2(transform.translation.X - transform.scale.X / 2, transform.translation.Y - transform.scale.Y / 2), color);
        }

    }
}
