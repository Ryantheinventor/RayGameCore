using static Raylib_cs.Raylib;
using System.Numerics;
using static RGCore.GameObjectList;
using RGCore.RGPhysics;

namespace RGCore
{
    class JumpPad : Sprite
    {
        public float chargeTime = 1f;
        private float timeCharging = 0f;
        public float jumpPower = 1000f;
        public JumpPad(string name, Vector2 pos) : base(name, pos)
        {

        }

        public JumpPad(string name, Vector2 pos, float jumpPower, float chargeTime) : base(name, pos)
        {
            this.jumpPower = jumpPower;
            this.chargeTime = chargeTime;
        }

        public override void Start()
        {
            transform.scale = new Vector3(40, 10, 0);
            collider = new RectangleCollider();
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            base.Start();
            SetSprite(textures["JumpPadDischarged"], 1, 1f);
        }

        public override void Update()
        {
            if (timeCharging < chargeTime) 
            {
                timeCharging += GetFrameTime();
            }

            if (timeCharging >= chargeTime)
            {
                SetSprite(textures["JumpPadCharged"], 1, 1f);
            }
            else if (sheetSize > 1 && frame == sheetSize - 1)
            {
                SetSprite(textures["JumpPadDischarged"], 1, 1f);
            }

        }

        public override void OnCollisionStay(Collider other)
        {
            if (other.gameObject.name == "Player" && timeCharging >= chargeTime) 
            {
                SetSprite(textures["JumpPadJumping"], 5, 0.1f);
                other.Velocity = new Vector2(other.Velocity.X, -jumpPower);
                timeCharging = 0f;
            }
        }

    }
}
