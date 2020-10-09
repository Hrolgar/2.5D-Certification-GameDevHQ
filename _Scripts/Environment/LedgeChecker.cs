using UnityEngine;

namespace UllrStudio._Scripts.Environment
{
    public class LedgeChecker : MonoBehaviour
    {
        [SerializeField] private Transform _hangingPosition;
        [SerializeField] private Transform _standingPosition;

        private void Start()
        {
            _standingPosition = transform.GetChild(0);
            _hangingPosition = transform.GetChild(1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("LedgeGrab")) return;
            var player = other.transform.parent.GetComponent<Player.Player>();

            if (player == null) return;
            player.GrabLedge(_hangingPosition.position, this);
        }
        
        public Vector3 GetStandPos()
        {
            return _standingPosition.position;
        }
    }
}
