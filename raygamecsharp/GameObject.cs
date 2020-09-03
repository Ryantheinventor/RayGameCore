using System.Numerics;
using Raylib_cs;
using RGCore.RGPhysics;

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
