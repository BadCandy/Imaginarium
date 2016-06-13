using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(Block))]
    public class Cube : MonoBehaviour
    {
        Block _block;
        GameManager _gameManager;

        void Awake()
        {
            _block = GetComponent<Block>();
            _gameManager = GameManager.Instance;
            _gameManager.AddCube(this);
        }

        void OnBlockCollisionEnter(GameObject other)
        {
            other.SendMessage("BlockCube", _block, SendMessageOptions.DontRequireReceiver);
        }
    }
}