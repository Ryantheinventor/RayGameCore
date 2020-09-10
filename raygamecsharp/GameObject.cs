using System.Numerics;
using Raylib_cs;
using RGCore.RGPhysics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace RGCore
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

        /// <summary>
        /// Called when the object is created
        /// </summary>
        public virtual void Start()
        {
            if (collider != null) 
            {
                collider.gameObject = this;
            }
        }

        /// <summary>
        /// Called before phsics are calculated
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Called after phsics are calculated
        /// </summary>
        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Draw()
        {

        }

        /// <summary>
        /// Called after Physics are calculated and when a collision is still occurring 
        /// </summary>
        /// <param name="other">The object that is being collided with</param>
        public virtual void OnCollisionStay(Collider other)
        {

        }

        /// <summary>
        /// Called after Physics are calculated and when a collision just ended 
        /// </summary>
        /// <param name="other">The object that has stoped colliding</param>
        public virtual void OnCollisionExit(Collider other)
        {

        }


    }
}
