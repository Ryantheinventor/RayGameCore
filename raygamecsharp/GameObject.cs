using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static raygamecsharp.GameObjectList;
using raygamecsharp;
using RGPhysics;

namespace raygamecsharp
{
    class GameObject
    {
        
        public string name = "";
        public Transform transform = new Transform();
        public Collider collider = null;

        public GameObject(string name,Vector2 pos) 
        {
            this.name = name;
            transform.translation = new Vector3(pos,0);
        }

        public virtual void Start()
        {
            if (collider != null) 
            {
                collider.gameObject = this;
            }
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            if (collider != null) 
            { 
                
            }
        }

        /// <summary>
        /// Collisions are checked before Update is called
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollisionEnter(Collider other)
        {

        }

        public virtual void OnCollisionExit(Collider other)
        {

        }


    }
}
