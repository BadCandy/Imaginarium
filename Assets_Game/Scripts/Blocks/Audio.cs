using UnityEngine;
using System.Collections;

namespace Blocks
{
    public class Audio : MonoBehaviour
    {
        public AudioClip pickUp;
        public AudioClip putDown;
        public AudioClip destroy;

        // Update is called once per frame
        void Update()
        {

        }

        void BlockPickedUp()
        {
            if(pickUp != null)
                AudioSource.PlayClipAtPoint(pickUp, transform.position);
        }

        void BlockPutDown()
        {
            if(putDown != null)
                AudioSource.PlayClipAtPoint(putDown, transform.position);
        }

        void BlockDestroy()
        {
            if (destroy != null)
                AudioSource.PlayClipAtPoint(destroy, transform.position);
        }
    }
}