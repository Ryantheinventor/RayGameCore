/*******************************************************************************************
*
*   raylib [core] example - Basic window
*
*   Welcome to raylib!
*
*   To test examples, just press F6 and execute raylib_compile_execute script
*   Note that compiled executable is placed in the same folder as .c file
*
*   You can find all basic examples on C:\raylib\raylib\examples folder or
*   raylib official webpage: www.raylib.com
*
*   Enjoy using raylib. :)
*
*   This example has been created using raylib 1.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2013-2016 Ramon Santamaria (@raysan5)
*
********************************************************************************************/


using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static raygamecsharp.GameObjectList;
using System.Numerics;

namespace raygamecsharp
{
    public class core_basic_window
    {
        static Vector2 cameraPos = new Vector2(0,0);
        
        public static void Start()
        {
            foreach (GameObject g in Objects) 
            {
                g.Start();
            }
        }

        public static void Update() 
        {
            //Run all collision checks
            if (Objects.Count > 1) {
                for (int i = 0; i < Objects.Count - 1; i++)
                {
                    if (Objects[i].collider != null)
                    {
                        for (int j = i + 1; j < Objects.Count; j++)
                        {
                            if (Objects[j].collider != null)
                            {
                                if (Objects[i].collider.CollidesWith(Objects[j].collider))
                                {
                                    Objects[i].OnCollisionEnter(Objects[j].collider);
                                    Objects[j].OnCollisionEnter(Objects[i].collider);
                                }
                            }
                        }
                    }
                    Objects[i].collider.ChecksForExits();
                }
            }
            if (Objects[Objects.Count - 1].collider != null) {
                Objects[Objects.Count - 1].collider.ChecksForExits();
            }

            //Run all update functions
            foreach (GameObject g in Objects)
            {
                g.Update();
            }

            //Physics update
            foreach (GameObject g in Objects) 
            {
                if (g.collider != null) 
                {
                    if (g.collider.IsKinematic) 
                    {
                        Physics.UpdateObject(g.collider);
                    }
                }
            }

            //Load queue in main object list
            UpdateObjectList();

        }

        public static void Draw() 
        {
            ClearBackground(RAYWHITE);
            foreach (GameObject g in Objects)
            {
                g.Draw();
            }
            DrawText(GetFPS().ToString(), 10, 10, 20, GREEN);
            //DrawText("Congrats! You created your first window!", 190, 200, 20, MAROON);
        }

        public static int Main()
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            const int screenWidth = 1600;
            const int screenHeight = 900;

            InitWindow(screenWidth, screenHeight, "Raylib Game C#");

            SetTargetFPS(60);
            //--------------------------------------------------------------------------------------
            
            Start();
            // Main game loop
            while (!WindowShouldClose())    // Detect window close button or ESC key
            {
                
                // Update 
                //----------------------------------------------------------------------------------
                Update();
                //----------------------------------------------------------------------------------
                // Draw
                //----------------------------------------------------------------------------------
                BeginDrawing();
                Draw();
                EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            CloseWindow();        // Close window and OpenGL context
            //--------------------------------------------------------------------------------------

            return 0;
        }
    }
}