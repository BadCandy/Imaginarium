using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(Block))]
    public class Watery : MonoBehaviour
    {
        Block _block;

        public GameObject particle;

        void Awake()
        {
            _block = GetComponent<Block>();
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            collision.gameObject.SendMessage("BlockWatered", SendMessageOptions.DontRequireReceiver);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            other.SendMessage("BlockWatered", SendMessageOptions.DontRequireReceiver);
        }

        public void BlockBurned()
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            _block.Destroy();
        }
    }
}