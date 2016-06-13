using UnityEngine;

namespace PC2D
{
    /// <summary>
    /// This is a very very very simple example of how an animation system could query information from the motor to set state.
    /// This can be done to explicitly play states, as is below, or send triggers, float, or bools to the animator. Most likely this
    /// will need to be written to suit your game's needs.
    /// </summary>

    public class PlatformerAnimation2D : MonoBehaviour
    {
        public float jumpRotationSpeed;
        public GameObject scaleWrapper;
        public GameObject visualChild;

        private Player _player;
        private PlatformerMotor2D _motor;
        private Animator _animator;
        private bool _isJumping;
        private bool _currentFacingLeft;

        // Use this for initialization
        void Start()
        {
            _player = GetComponent<Player>();
            _motor = GetComponent<PlatformerMotor2D>();
            _animator = visualChild.GetComponent<Animator>();
            _animator.Play("Idle");

            _motor.onJump += SetCurrentFacingLeft;
        }

        // Update is called once per frame
        void Update()
        {
            if (_motor.motorState == PlatformerMotor2D.MotorState.Jumping ||
                _isJumping &&
                    (_motor.motorState == PlatformerMotor2D.MotorState.Falling ||
                                 _motor.motorState == PlatformerMotor2D.MotorState.FallingFast))
            {
                _isJumping = true;
                if (_player.IsHoldingBlock)
                    _animator.Play("CarryJump");
                else
                    _animator.Play("Jump");

                if (_motor.velocity.x <= -0.1f)
                {
                    _currentFacingLeft = true;
                }
                else if (_motor.velocity.x >= 0.1f)
                {
                    _currentFacingLeft = false;
                }

                Vector3 rotateDir = _currentFacingLeft ? Vector3.forward : Vector3.back;
                visualChild.transform.Rotate(rotateDir, jumpRotationSpeed * Time.deltaTime);
            }
            else
            {
                _isJumping = false;
                visualChild.transform.rotation = Quaternion.identity;

                if (_motor.motorState == PlatformerMotor2D.MotorState.Falling ||
                                 _motor.motorState == PlatformerMotor2D.MotorState.FallingFast)
                {
                    if (_player.IsHoldingBlock)
                        _animator.Play("CarryIdle"); // CarryFall 필요
                    else
                        _animator.Play("Fall");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.WallSliding ||
                         _motor.motorState == PlatformerMotor2D.MotorState.WallSticking)
                {
                    if (_player.IsHoldingBlock)
                        _animator.Play("CarryWalk");
                    else
                        _animator.Play("Cling");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.OnCorner)
                {
                    _animator.Play("On Corner");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.Slipping)
                {
                    //_animator.Play("Slip");
                    if (_player.IsHoldingBlock)
                        _animator.Play("CarryWalk");
                    else
                        _animator.Play("Walk");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.Dashing)
                {
                    _animator.Play("Dash");
                }
                else
                {
                    if (_motor.velocity.sqrMagnitude >= 0.1f * 0.1f)
                    {
                        if(_player.IsHoldingBlock)
                            _animator.Play("CarryWalk");
                        else
                            _animator.Play("Walk");
                    }
                    else
                    {
                        if (_player.IsHoldingBlock)
                            _animator.Play("CarryIdle");
                        else
                            _animator.Play("Idle");
                    }
                }
            }

            
            // Facing
            if (!_player.IsHoldingBlock)
            {
                float valueCheck = _motor.normalizedXMovement;

                if (_motor.motorState == PlatformerMotor2D.MotorState.Slipping ||
                    _motor.motorState == PlatformerMotor2D.MotorState.Dashing ||
                    _motor.motorState == PlatformerMotor2D.MotorState.Jumping)
                {
                    valueCheck = _motor.velocity.x;
                }

                if (Mathf.Abs(valueCheck) >= 0.1f)
                {
                    //Vector3 newScale = visualChild.transform.localScale;
                    Vector3 newScale = scaleWrapper.transform.localScale;
                    newScale.x = Mathf.Abs(newScale.x) * ((valueCheck > 0) ? -1.0f : 1.0f);
                    //visualChild.transform.localScale = newScale;
                    scaleWrapper.transform.localScale = newScale;
                }
            }
        }

        private void SetCurrentFacingLeft()
        {
            _currentFacingLeft = _motor.facingLeft;
        }
    }
}
