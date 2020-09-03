using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using raygamecsharp;
using RGPhysics;

namespace raygamecsharp
{
    class GameObjectList
    {
        /// <summary>
        /// Will serve as the Hirerachy 
        /// </summary>
        public static List<GameObject> objects = new List<GameObject>
        {
            
            //new Player("Mouse Magic", new Vector2(0,0)),
            new CollisionTestRectangle("Platform", new Vector2(800,850),new Vector2(1600,40)),
            new CollisionTestRectangle("Platform", new Vector2(1400,650),new Vector2(400,40)),
            new CollisionTestRectangle("Wall", new Vector2(0,450),new Vector2(40,900)),
            new CollisionTestRectangle("Wall", new Vector2(1600,450),new Vector2(40,900)),

            new Pickup("Pickup",new Vector2(1400,810)),
            new Pickup("Pickup",new Vector2(200,810)),
            new Exit("Exit",new Vector2(1400,600)),
            new PlatformPlayer("Player", new Vector2(800,800))
        };

        public static List<Texture2D> textures = new List<Texture2D> 
        {
            LoadTexture("TestImage.png"),
            LoadTexture("Exit.png"),
            LoadTexture("Pickup.png")
        };

        private static List<GameObject> Queue = new List<GameObject>();
        private static List<GameObject> Marked = new List<GameObject>();



        public static void NewObject(GameObject gameObject) 
        {  
            Queue.Add(gameObject);
            gameObject.Start();
        }

        public static void UpdateObjectList() 
        {
            foreach (GameObject g in Queue) 
            {
                objects.Add(g);
            }
            Queue = new List<GameObject>();
            foreach (GameObject g in Marked)
            {
                objects.Remove(g);
            }
            Marked = new List<GameObject>();
        }

        public static void Destroy(GameObject gameObject) 
        {
            Marked.Add(gameObject);
        }

        public static string GetObjectListString() 
        {
            string output = "";

            foreach (GameObject g in objects) 
            {
                output += $"{g.GetType()}:{g.name}\n";
            }

            return output;
        }

    }
}
