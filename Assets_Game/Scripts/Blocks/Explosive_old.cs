//using UnityEngine;
//using System.Collections;

//namespace Blocks
//{
//    [RequireComponent(typeof(Block))]
//    public class Explosive : MonoBehaviour
//    {
//        Block _block;

//        public LayerMask layer;
//        public float radius = 2.0f;
//        public float power = 500.0f;
//        public float upwardModifier = 1.0f;
//        public GameObject particle;

//        protected LayerMask _layer;

//        void Awake()
//        {
//            _block = GetComponent<Block>();
//        }

//        void OnCollisionStay2D(Collision2D collision)
//        {
//            collision.gameObject.SendMessage("BlockExploded", SendMessageOptions.DontRequireReceiver);
//        }

//        void OnTriggerStay2D(Collider2D other)
//        {
//            other.gameObject.SendMessage("BlockExploded", SendMessageOptions.DontRequireReceiver);
//        }

//        public void Detonate()
//        {
//            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);
//            Rigidbody2D rigidbody2D;

//            foreach (Collider2D hit in colliders)
//            {
//                if (rigidbody2D = hit.GetComponent<Rigidbody2D>())
//                {
//                    rigidbody2D.AddExplosionForce(power, transform.position, radius, upwardModifier);
//                }
//            }
//            Instantiate(particle, transform.position, Quaternion.identity);
//        }

//        public void BlockBurned()
//        {
//            Detonate();
//            _block.Destroy();
//        }
//    }
//}