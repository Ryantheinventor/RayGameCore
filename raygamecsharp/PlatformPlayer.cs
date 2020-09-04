using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using RGCore.RGPhysics;

namespace RGCore
{
    class PlatformPlayer : GameObject
    {

        public float maxSpeed = 500f;
        public float secondsToMaxSpeed = 0.5f;
        public float jumpPower = 700f;
        private bool jump = false;
        public PlatformPlayer(string name, Vector2 pos) : base(name, pos)
        {

        }
        public override void Start()
        {
            transform.scale = new Vector3(40, 40, 0);
            collider = new RectangleCollider();
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            collider.IsKinematic = true;
            collider.EnableGravity = true;
            collider.Bounce = 0f;
            base.Start();
            //transform.translation = new Vector3(100, 100, 0);
            HideCursor();
        }
        
        public override void Update()
        {
            bool moved = false;
            if (IsKeyDown(KeyboardKey.KEY_D) && collider.Velocity.X < maxSpeed)
            {
                collider.Velocity.X += maxSpeed * GetFrameTime() / secondsToMaxSpeed;
                moved = true;
            }
            if (IsKeyDown(KeyboardKey.KEY_A) && collider.Velocity.X > -maxSpeed)
            {
                collider.Velocity.X -= maxSpeed * GetFrameTime() / secondsToMaxSpeed;
                moved = true;
            }
            if (jump) 
            {
                jump = false;
            }
            if (!moved) 
            {
                if (collider.Velocity.X < 0)
                    collider.Velocity.X += maxSpeed * GetFrameTime() / secondsToMaxSpeed;
                if (collider.Velocity.X > 0)
                    collider.Velocity.X -= maxSpeed * GetFrameTime() / secondsToMaxSpeed;
                if (collider.Velocity.X < 20 && collider.Velocity.X > -20)
                    collider.Velocity.X = 0;
            }
        }

        public override void Draw()
        {
            base.Draw();
            DrawRectangle((int)transform.translation.X - (int)(transform.scale.X / 2), (int)transform.translation.Y - (int)(transform.scale.Y / 2), (int)transform.scale.X, (int)transform.scale.Y, BLUE);
        }
        public override void OnCollisionEnter(Collider other)
        {
            if (other.IsStatic && other.gameObject.name == "Platform") 
            {
                if (IsKeyDown(KeyboardKey.KEY_SPACE) || IsKeyDown(KeyboardKey.KEY_W))
                {
                    
                    collider.Velocity.Y = -jumpPower;
                    //jump = true;
                }
            }

        }

    }
}
