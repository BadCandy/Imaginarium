using UnityEngine;
using System.Collections;
using Blocks;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlatformerMotor2D))]
public class Player : MonoBehaviour {
    private PlatformerMotor2D _motor;
    private BoxCollider2D _collider2D;
    private GameObject _holdingBlock;
    private bool _isHoldingBlock;
    public Vector2 carryingBlockOffset;
    public LayerMask blockLayer;

    public bool IsHoldingBlock
    {
        get
        {
            return _isHoldingBlock;
        }
    }

	// Use this for initialization
    void Start()
    {
        _isHoldingBlock = false;
        _motor = GetComponent<PlatformerMotor2D>();
        _collider2D = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
	 
	}

    // 동작 성공여부 반환
    public bool Action()
    {
        if (_isHoldingBlock && _holdingBlock != null)
        {
            PutBlockDown();
            return true; // 내려놓기는 항상 성공
        }
        else
        {
            return PickBlockUp();
        }
    }

    public void PutBlockDown()
    {
        Vector2 offset = _collider2D.offset;
        Vector2 size = _collider2D.size;
        offset.x = 0;
        size.x = 0.5f;
        _collider2D.offset = offset;
        _collider2D.size = size;

        _holdingBlock.transform.parent = null;
        Block block = _holdingBlock.GetComponent<Block>();
        block.State = Block.BlockState.Normal;
        _holdingBlock = null;

        _isHoldingBlock = false;
        //Debug.Log("putBlockDown");
    }

    public bool PickBlockUp()
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        bool facingLeft = _motor.facingLeft;
        Physics2D.BoxCastNonAlloc(
            _collider2D.bounds.center,
            _collider2D.bounds.size,
            0f,
            (facingLeft ? Vector2.left : Vector2.right),
            hits,
            0.2f,
            blockLayer);

        if (hits[0].collider != null)
        {
            _holdingBlock = hits[0].collider.gameObject;
            Block block = _holdingBlock.GetComponent<Block>();
            Vector3 position = _holdingBlock.transform.position;

            if (!block.isCarriable || (block.IsOnOther && transform.position.y + 0.1f < position.y) || block.IsOtherOnIt || block.State == Block.BlockState.Holding)
                return false;

            position.x = transform.position.x + (facingLeft ? -0.75f : 0.75f) * (1 + carryingBlockOffset.x);
            position.y = transform.position.y + carryingBlockOffset.y;
            _holdingBlock.transform.position = position;

            _holdingBlock.transform.parent = transform;

            block.State = Block.BlockState.Holding;

            Vector2 offset = _collider2D.offset;
            Vector2 size = _collider2D.size;
            offset.x = (facingLeft ? -0.5f : 0.5f);
            size.x = 1.5f;
            _collider2D.offset = offset;
            _collider2D.size = size;

            _isHoldingBlock = true;
            return true;
        }

        return false;
    }
}
