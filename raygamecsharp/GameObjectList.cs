using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace RGCore
{
    class GameObjectList
    {
        /// <summary>
        /// Will serve as the Hirerachy 
        /// </summary>
        public static List<GameObject> objects = new List<GameObject>
        {
            new CollisionTestRectangle("Platform", new Vector2(800,850),new Vector2(1600,40)),
            new CollisionTestRectangle("Platform", new Vector2(1400,650),new Vector2(400,40)),
            new CollisionTestRectangle("Platform", new Vector2(200,700),new Vector2(400,40)),
            new CollisionTestRectangle("Platform", new Vector2(800,550),new Vector2(400,40)),
            new CollisionTestRectangle("Platform", new Vector2(200,450),new Vector2(400,40)),
            new CollisionTestRectangle("Platform", new Vector2(200,230),new Vector2(400,40)),
            new CollisionTestRectangle("Platform", new Vector2(800,110),new Vector2(400,40)),
            new CollisionTestRectangle("Platform", new Vector2(800,300),new Vector2(400,40)),
            new CollisionTestRectangle("Platform", new Vector2(1400,300),new Vector2(400,40)),
            new CollisionTestRectangle("Wall", new Vector2(600,150),new Vector2(40,340)),
            new CollisionTestRectangle("Wall", new Vector2(0,450),new Vector2(40,900)),
            new CollisionTestRectangle("Wall", new Vector2(1600,450),new Vector2(40,900)),
            new CollisionTestRectangle("Wall", new Vector2(800,0),new Vector2(1600,40)),
            new CollisionTestRectangle("Wall", new Vector2(800,880),new Vector2(1600,40)),
            new Sprite("Map",new Vector2(800,450),LoadTexture(@"Textures\Map.png"),1,1),

            //new Player("Mouse Magic", new Vector2(0,0)),

            new Pickup("Pickup",new Vector2(1550,610)),
            new Pickup("Pickup",new Vector2(800,510)),
            new Pickup("Pickup",new Vector2(640,260)),
            new Pickup("Pickup",new Vector2(50,190)),
            new Pickup("Pickup",new Vector2(50,660)),
            new Pickup("Pickup",new Vector2(1400,810)),
            new Pickup("Pickup",new Vector2(200,810)),
            new JumpPad("JumpPad",new Vector2(1100,825), 1500f,10f),
            new Exit("Exit",new Vector2(650,60)),
            new PlatformPlayer("Player", new Vector2(800,800))
        };

        /// <summary>
        /// All preloaded textures
        /// </summary>
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// All preloaded sounds
        /// </summary>
        public static Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();

        /// <summary>
        /// GameObjects waiting to be added to main objects list
        /// </summary>
        private static List<GameObject> Queue = new List<GameObject>();

        /// <summary>
        /// GameObjects waiting to be removed from the main objects list
        /// </summary>
        private static List<GameObject> Marked = new List<GameObject>();

        /// <summary>
        /// Loads all textures to be used into the texture list.
        /// </summary>
        public static void LoadTextures() 
        {
            textures.Add("TestImage", LoadTexture(@"Textures\TestImage.png"));
            textures.Add("Exit", LoadTexture(@"Textures\Exit.png"));
            textures.Add("ExitDoor1", LoadTexture(@"Textures\ExitDoor1.png"));
            textures.Add("ExitDoor2", LoadTexture(@"Textures\ExitDoor2.png"));
            textures.Add("PickupSheet", LoadTexture(@"Textures\PickupSheet.png"));
            textures.Add("JumpPadCharged", LoadTexture(@"Textures\JumpPadCharged.png"));
            textures.Add("JumpPadDischarged", LoadTexture(@"Textures\JumpPadDischarged.png"));
            textures.Add("JumpPadJumping", LoadTexture(@"Textures\JumpPadJumping.png"));
        }
        /// <summary>
        /// Loads all sounds to be used into the sound list.
        /// </summary>
        public static void LoadSounds()
        {
            sounds.Add("CoinSound", LoadSound(@"Sounds\CoinSound.wav"));
            sounds.Add("untitled", LoadSound(@"Sounds\untitled.wav"));
        }
        /// <summary>
        /// Add a GameObject to object list.
        /// </summary>
        public static void NewObject(GameObject gameObject) 
        {  
            Queue.Add(gameObject);
            gameObject.Start();
        }

        /// <summary>
        /// Will check the queue and marked lists for objects that need to be modified in the objects array.
        /// </summary>
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

        /// <summary>
        /// Mark a GameObject for removal
        /// </summary>
        public static void Destroy(GameObject gameObject) 
        {
            Marked.Add(gameObject);
        }

        /// <summary>
        /// Get the types and names of all active GameObjects in a string format
        /// </summary>
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
