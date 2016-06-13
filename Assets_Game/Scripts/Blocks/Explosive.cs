using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Blocks
{
    [RequireComponent(typeof(Block))]
    public class Explosive : MonoBehaviour
    {
        Block _block;

        public LayerMask layer;
        public float range = 3.0f;
        public float detonateDelayTime = 0.5f;

        protected LayerMask _layer;

        void Awake()
        {
            _block = GetComponent<Block>();
        }

        public void Detonate()
        {
            StartCoroutine(detonate());
        }

        IEnumerator detonate()
        {
            yield return _block.DestroyInSeconds(detonateDelayTime, true);

            var position = transform.position;
            Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(position.x - range / 2, position.y - range / 2), new Vector2(position.x + range / 2, position.y + range / 2), layer);
            LinkedList<Action> detonateActions = new LinkedList<Action>();

            foreach (Collider2D hit in colliders)
            {
                Block block;
                Explosive explosive;

                if ((block = hit.GetComponent<Block>()) != null && !block.IsDestroying)
                {
                    if ((explosive = hit.GetComponent<Explosive>()) != null)
                    {
                        detonateActions.AddFirst(explosive.Detonate);
                    }
                    else
                    {
                        block.Destroy();
                    }
                }
            }

            foreach (Action action in detonateActions)
            {
                action();
            }
        }

        public void BlockBurned(Block otherBlock)
        {
            if (otherBlock.IsDestroying || _block.IsDestroying)
                return;

            otherBlock.Destroy();
            Detonate();
        }
    }
}