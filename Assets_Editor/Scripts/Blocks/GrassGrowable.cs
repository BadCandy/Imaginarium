using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(Block))]
    public class GrassGrowable : MonoBehaviour
    {
        public Sprite grassed;

        Block _block;
        int collisionCount;

        void Awake()
        {
            _block = GetComponent<Block>();
        }

        //void OnParticleCollision(GameObject other)
        //{
        //    collisionCount++;

        //    if (collisionCount > 100)
        //    {
        //        _block.gameObject.GetComponent<SpriteRenderer>().sprite = grassed;
        //        Destroy(this);
        //    }
        //}

        public void BlockWatered()
        {
            collisionCount++;

            if (collisionCount > 100)
            {
                _block.gameObject.GetComponent<SpriteRenderer>().sprite = grassed;
                Destroy(this);
            }
        }
    }
}