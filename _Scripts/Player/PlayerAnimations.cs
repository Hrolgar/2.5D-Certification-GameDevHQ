using UnityEngine;

namespace UllrStudio._Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");
        private static readonly int Jumping1 = Animator.StringToHash("jumping");
        private static readonly int Rolling1 = Animator.StringToHash("rolling");
        private static readonly int GrabLedge = Animator.StringToHash("grabLedge");
        private static readonly int ClimbUp = Animator.StringToHash("climbUp");
        private static readonly int Ladder = Animator.StringToHash("climbLadder");
        private static readonly int Elevator = Animator.StringToHash("activateElevator");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Running(float speed)
        {
            _animator.SetFloat(MoveSpeed, speed);

        }
        
        public void Jumping(bool jumping)
        {
            _animator.SetBool(Jumping1, jumping);

        }

        public void Rolling(bool rolling)
        {
            _animator.SetBool(Rolling1, rolling);
        }
        // Climb ledge
        public void LedgeHanging(bool hanging)
        {
            _animator.SetBool(GrabLedge,hanging);
        }

        public void ClimbUpTrigger()
        {
            _animator.SetTrigger(ClimbUp);
        }
        
        // Climb Ladder
        public void ClimbLadder(bool climb)
        {
            _animator.SetBool(Ladder, climb);
        }

        public void ActivateElevator()
        {
            _animator.SetTrigger(Elevator);
        }
    }
}
