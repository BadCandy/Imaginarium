using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(Block))]
    public class Burning : MonoBehaviour
    {
        Block _block;

        public GameObject particle;

        void Awake()
        {
            _block = GetComponent<Block>();
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            collision.gameObject.SendMessage("BlockBurned", SendMessageOptions.DontRequireReceiver);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            other.SendMessage("BlockBurned", _block, SendMessageOptions.DontRequireReceiver);
        }

        public void BlockWatered()
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            _block.Destroy();
        }

        public void BlockExploded()
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            _block.Destroy();
        }
    }
}