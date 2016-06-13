using UnityEngine;
using System.Collections;

namespace Blocks
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Block : MonoBehaviour
    {
        public enum BlockState
        {
            Normal,
            Holding
        }
        public bool isFixed;
        public bool isCarriable = true;
        public bool isDestructible = true;
        public bool broadcastCollision;
        public string holdLayerString = "Default";
        public GameObject destroyParticle;
        public bool overrideParticleMaterial;

        BoxCollider2D _collider2D;
        Rigidbody2D _rigidbody2D;
        Player _player;

        int _holdLayer;
        BlockState _state;
        int _defaultLayer;
        Transform _parent;

        public bool IsDestroying
        {
            get;
            private set;
        }

        public bool IsOnOther
        {
            get
            {
                RaycastHit2D[] hits = new RaycastHit2D[1];
                return !isFixed &&
                0 < Physics2D.BoxCastNonAlloc(
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
                if (0 < Physics2D.BoxCastNonAlloc(
                _collider2D.bounds.center + Vector3.up * 1.03f,
                new Vector3(_collider2D.bounds.size.x * 0.75f, /*_collider2D.bounds.size.y*/0.07f, _collider2D.bounds.size.z),
                0f,
                Vector2.up,
                hits,
                0.03f,
                1 << gameObject.layer))
                {
                    return !hits[0].collider.GetComponent<Block>().isFixed;
                }
                else
                    return false;
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
                    transform.parent = _parent;
                    gameObject.layer = _defaultLayer;
                    _collider2D.isTrigger = false;
                    //_collider2D.size = Vector2.one;
                    _collider2D.size = new Vector2(0.985f, 0.99f);
                    _rigidbody2D.isKinematic = isFixed;
                    _player = null;
                    SendMessage("BlockPutDown", SendMessageOptions.DontRequireReceiver);
                    _state = value;
                }
                else if (value == BlockState.Holding)
                {
                    gameObject.layer = _holdLayer;
                    _collider2D.isTrigger = true;
                    _collider2D.size = _collider2D.size * 1.05f;
                    _rigidbody2D.isKinematic = true;
                    _player = GetComponentInParent<Player>();
                    SendMessage("BlockPickedUp", SendMessageOptions.DontRequireReceiver);
                    _state = value;
                }
            }
        }

        void Start()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _holdLayer = LayerMask.NameToLayer(holdLayerString);
            _defaultLayer = gameObject.layer;
            _rigidbody2D.isKinematic = isFixed;
            _parent = transform.parent;
        }

        public IEnumerator DestroyInSeconds(float t, bool ignoreDestructible = false)
        {
            if (!ignoreDestructible && !isDestructible || IsDestroying)
                yield break;
           
            IsDestroying = true;
            isCarriable = false;
            broadcastCollision = false;

            if (_state == BlockState.Holding)
                _player.PutBlockDown();

            yield return new WaitForSeconds(t);

            if (destroyParticle != null)
            {
                GameObject particle = Instantiate(destroyParticle);
                particle.transform.position = transform.position;
                if (overrideParticleMaterial)
                    particle.GetComponent<ParticleSystemRenderer>().material = GetComponent<Renderer>().material;
            }

            SendMessage("BlockDestroy", SendMessageOptions.DontRequireReceiver);

            GameObject.Destroy(gameObject);
        }

        public void Destroy(bool ignoreDestructible = false)
        {
            if (!ignoreDestructible && !isDestructible || IsDestroying)
                return;

            IsDestroying = true;
            isCarriable = false;
            broadcastCollision = false;

            if (_state == BlockState.Holding)
                _player.PutBlockDown();

            if (destroyParticle != null)
            {
                GameObject particle = Instantiate(destroyParticle);
                particle.transform.position = transform.position;
                if (overrideParticleMaterial)
                    particle.GetComponent<ParticleSystemRenderer>().material = GetComponent<Renderer>().material;
            }

            SendMessage("BlockDestroy", SendMessageOptions.DontRequireReceiver);

            GameObject.Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            broadcastCollisionEnter(collision.gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            broadcastCollisionEnter(other.gameObject);
        }

        void broadcastCollisionEnter(GameObject other)
        {
            if (!broadcastCollision)
                return;
            
            SendMessage("OnBlockCollisionEnter", other.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}