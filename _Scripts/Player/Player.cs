using System.Collections;
using UllrStudio._Scripts.Environment;
using UnityEngine;

namespace UllrStudio._Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private CharacterController _controller;
        private PlayerAnimations _playerAnim;
        private LedgeChecker _activeLedge;

        private Vector3 _direction, _velocity;
        private float _yVelocity;

        [SerializeField] private float _moveSpeed = 0;
        [SerializeField] private float _jumpHeight = 0;
        [SerializeField] private float _gravity = 0;
        [SerializeField] private int _collectible = 0;

        private bool _jumping = false;
        private bool _onLedge = false;
        private bool _rolling = false;
        private bool _flipAllowed = true;
        private bool _canMove = true;

        private void Start()
        {
            Application.targetFrameRate = 60;
            _controller = GetComponent<CharacterController>();
            if (!_controller) Debug.Log("Character Controller is not found on the Player Script");
            _playerAnim = GetComponentInChildren<PlayerAnimations>();
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            if (_controller.isGrounded)
            {
                var horizontal = Input.GetAxisRaw("Horizontal");
                _direction = new Vector3(0, 0, horizontal);
                _velocity = _direction * _moveSpeed;
                _playerAnim.Running(Mathf.Abs(horizontal));


                if (_jumping)
                {
                    _jumping = false;
                    _playerAnim.Jumping(_jumping);
                }

                if (_rolling)
                {
                    _rolling = false;
                    _playerAnim.Rolling(_rolling);
                }

                // Flip the character
                if (horizontal != 0 && _flipAllowed)
                {
                    var facing = transform.localEulerAngles;
                    facing.y = _direction.z > 0 ? 0 : 180;
                    transform.localEulerAngles = facing;
                }

                // Jumping
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _yVelocity = _jumpHeight;
                    _jumping = true;
                    _playerAnim.Jumping(_jumping);
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    _rolling = true;

                    _playerAnim.Rolling(_rolling);
                }
            }
            else
            {
                _yVelocity -= _gravity;
            }

            if (_onLedge)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _playerAnim.ClimbUpTrigger();
                }
            }

            _velocity.y = _yVelocity;
            if(_canMove)
                _controller.Move(_velocity * Time.deltaTime);
        }
        
        public void GrabLedge(Vector3 handPos, LedgeChecker currentLedge)
        {
            _canMove = false;
            _controller.enabled = false;
            _onLedge = true;
            _playerAnim.LedgeHanging(true);
            _playerAnim.Jumping(_jumping);
            _playerAnim.Running(0f);
            transform.position = handPos;
            _activeLedge = currentLedge;
        }

        public void ClimbUpComplete()
        {
            transform.position = _activeLedge.GetStandPos();
            _playerAnim.LedgeHanging(false);
            _controller.enabled = true;
            _canMove = true;
        }

        public void ClimbLadder(Transform pos, Transform standPos)
        {
            StartCoroutine(Climb(pos, standPos));
        }

        private IEnumerator Climb(Transform pos, Transform stand)
        {
            var distance = Vector3.Distance(transform.position, pos.position);

            while (distance > 7f)
            {
                _controller.enabled = false;
                _canMove = false;
                _playerAnim.ClimbLadder(true);
                transform.position = Vector3.MoveTowards(transform.position, pos.position, 2 * Time.deltaTime);
                distance = Vector3.Distance(transform.position, pos.position);
                yield return null;
            }
            _playerAnim.ClimbLadder(false);
            Ladder();
            // yield return new WaitForSeconds(2f);
            _controller.enabled = true;
            _canMove = true;
            transform.position = stand.position;
        }

        public void Ladder()
        {
            _playerAnim.ClimbUpTrigger();

        }
        
        public void CanMove()
        {
            _controller.enabled = true;
            _flipAllowed = true;
            _canMove = true;
        }

        public void CannotMove()
        {
            _controller.enabled = false;
            _flipAllowed = false;
            _canMove = false;
        }

        public void PickUpCollectible()
        {
            _collectible++;
            Debug.Log("Soo, am I here?");
        }
    }
}