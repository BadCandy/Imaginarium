using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(Block))]
    public class Objective : MonoBehaviour
    {
        public Transform innerCube;
        GameManager _gameManager;
        int _objectiveCount;

        void Awake()
        {
            _gameManager = GameManager.Instance;
            _gameManager.AddObjective(this);
            innerCube.localScale = new Vector3(1, 0, 1);
        }

        void Start()
        {
            if (_gameManager.CubeCount == 0)
            {
                innerCube.localScale = Vector3.one;
            }
        }

        void BlockCube(Block otherBlock)
        {
            if (otherBlock.IsDestroying)
                return;

            _gameManager.CollectCube(otherBlock.GetComponent<Cube>());
            otherBlock.Destroy(true);
        }
    }
}