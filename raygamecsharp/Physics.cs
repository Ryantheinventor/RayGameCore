using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using static raygamecsharp.GameObjectList;

namespace raygamecsharp
{
    class Physics
    {
        public static float gravity = 9.8f;
        public static void UpdateObject(Collider c) 
        {
            if (c.AutoClean && c.gameObject.transform.translation.Y>4000) 
            {
                Destroy(c.gameObject);
            }
            Vector2 v = c.Velocity;
            c.gameObject.transform.translation += new Vector3(v * GetFrameTime(),0);
            if (c.EnableGravity) 
            {
                c.Velocity += new Vector2(0,gravity);
            }
        }
    }
}
