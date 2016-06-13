using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(Block))]    
    public class Burning : MonoBehaviour
    {
        Block _block;
        
        void Awake()
        {
            _block = GetComponent<Block>();
        }

        void OnBlockCollisionEnter(GameObject other)
        {
            other.SendMessage("BlockBurned", _block, SendMessageOptions.DontRequireReceiver);
        }
    }
}