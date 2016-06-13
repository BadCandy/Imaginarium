using UnityEngine;
using System.Collections;

namespace Blocks
{
    public class Spring : MonoBehaviour
    {
        public float jumpHeight = 3f;
        /*
        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("SPRING - " + collision.gameObject.tag);
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlatformerMotor2D>().Jump(jumpHeight);
            }
        }
        */
    }
}