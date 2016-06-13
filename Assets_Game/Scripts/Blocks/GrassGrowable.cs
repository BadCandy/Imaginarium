using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(Block))]
    public class GrassGrowable : MonoBehaviour
    {
        public Material grassed;
        int collisionCount;

        void OnParticleCollision(GameObject other)
        {
            collisionCount++;

            if (collisionCount > 300)
            {
                gameObject.GetComponent<MeshRenderer>().material = grassed;
                Destroy(this);
            }
        }

        //public void BlockWatered()
        //{
        //    collisionCount++;

        //    if (collisionCount > 100)
        //    {
        //        gameObject.GetComponent<MeshRenderer>().material = grassed;
        //        Destroy(this);
        //    }
        //}
    }
}