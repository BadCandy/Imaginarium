using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(Block))]
    [RequireComponent(typeof(AudioSource))]
    public class Watery : MonoBehaviour
    {
        public AudioClip fireReact;
        Block _block;

        void Awake()
        {
            _block = GetComponent<Block>();
        }

        public void BlockBurned(Block otherBlock)
        {
            if (otherBlock.IsDestroying)
                return;

            if (fireReact != null)
                AudioSource.PlayClipAtPoint(fireReact, transform.position);
            
            otherBlock.Destroy();
            _block.Destroy();
        }
    }
}