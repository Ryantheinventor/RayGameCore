using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using static RGCore.GameObjectList;
using RGCore.RGPhysics;

namespace RGCore
{
    class Exit : Sprite
    {
        bool hasWon = false;
        bool canExit = false;

        public Exit(string name, Vector2 pos) : base(name, pos)
        {

        }
        public override void Start()
        {

            transform.scale = new Vector3(40, 60, 0);
            collider = new RectangleCollider();
            ((RectangleCollider)collider).scale = new Vector2(transform.scale.X, transform.scale.Y);
            texture = textures["ExitDoor1"];
            color = WHITE;
            base.Start();
        }

        public override void Update()
        {
            canExit = true;
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
                texture = textures["ExitDoor2"];
            }
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
                DrawRectangle(550, 840, 545, 50, Fade(BLACK, 0.5f));
                DrawText("Collect the pickups before exiting", 560, 850, 30, GREEN);
            }
        }

        public override void OnCollisionEnter(Collider other)
        {
            if (other.gameObject.name == "Player") 
            {
                if (canExit)
                { 
                    hasWon = true;
                    foreach (GameObject g in objects) 
                    {
                        if (g.name == "Player") 
                        {
                            Destroy(g);
                        }
                    }
                }
            }
        }
    }
}
