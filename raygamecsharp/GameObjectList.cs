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
        /// Will serve as my Hirerachy 
        /// </summary>
        public static List<GameObject> Objects = new List<GameObject>
        {
            new Player("Player", new Vector2(0,0)),
            new CollisionTestRectangle("Test", new Vector2(400,200)),
            new CollisionTestCircle("Test", new Vector2(200,200))
        };

        private static List<GameObject> Queue = new List<GameObject>
        {
        };

        private static List<GameObject> Marked = new List<GameObject>
        {
        };

        public static void NewObject(GameObject gameObject) 
        {
            Queue.Add(gameObject);
            gameObject.Start();
        }

        public static void UpdateObjectList() 
        {
            foreach (GameObject g in Queue) 
            {
                Objects.Add(g);
            }
            Queue = new List<GameObject>();
            foreach (GameObject g in Marked)
            {
                Objects.Remove(g);
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

            foreach (GameObject g in Objects) 
            {
                output += $"{g.GetType()}:{g.name}\n";
            }

            return output;
        }

    }
}
