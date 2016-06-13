using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Player))]
    public class Block : MonoBehaviour
    {
        public enum BlockState
        {
            Normal,
            Holding
        }
        public bool isFixed;
        public string holdLayerString = "Default";

        BoxCollider2D _collider2D;
        Rigidbody2D _rigidbody2D;
        Player _player;

        int _holdLayer;
        BlockState _state;
        int _defaultLayer;

        public bool IsOnOther
        {
            get
            {
                RaycastHit2D[] hits = new RaycastHit2D[1];
                return 0 < Physics2D.BoxCastNonAlloc(
                _collider2D.bounds.center + Vector3.down * 1.03f,
                new Vector3(_collider2D.bounds.size.x * 0.75f, /*_collider2D.bounds.size.y*/0.07f, _collider2D.bounds.size.z),
                0f,
                Vector2.up,
                hits,
                0.03f,
                1 << gameObject.layer);
            }
        }

        public bool IsOtherOnIt
        {
            get
            {
                RaycastHit2D[] hits = new RaycastHit2D[1];
                return 0 < Physics2D.BoxCastNonAlloc(
                _collider2D.bounds.center + Vector3.up * 1.03f,
                new Vector3(_collider2D.bounds.size.x * 0.75f, /*_collider2D.bounds.size.y*/0.07f, _collider2D.bounds.size.z),
                0f,
                Vector2.up,
                hits,
                0.03f,
                1 << gameObject.layer);
            }
        }

        public BlockState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (value == BlockState.Normal)
                {
                    gameObject.layer = _defaultLayer;
                    //_collider2D.enabled = true;
                    _collider2D.isTrigger = false;
                    _collider2D.size = Vector2.one;
                    _rigidbody2D.isKinematic = isFixed;
                    _player = null;
                    SendMessage("BlockPutDown", SendMessageOptions.DontRequireReceiver);
                    _state = value;
                }
                else if (value == BlockState.Holding)
                {
                    gameObject.layer = _holdLayer;
                    //_collider2D.enabled = false;
                    _collider2D.isTrigger = true;
                    _collider2D.size = _collider2D.size * 1.05f;
                    _rigidbody2D.isKinematic = true;
                    _player = GetComponentInParent<Player>();
                    SendMessage("BlockPickedUp", SendMessageOptions.DontRequireReceiver);
                    _state = value;
                }
            }
        }

        void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _holdLayer = LayerMask.NameToLayer(holdLayerString);
            _defaultLayer = gameObject.layer;
            _rigidbody2D.isKinematic = isFixed;
        }

        public void Destroy()
        {
            if (_state == BlockState.Holding)
                _player.PutBlockDown();
            Destroy(gameObject);
        }
    }
}