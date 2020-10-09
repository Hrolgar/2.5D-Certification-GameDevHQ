using UnityEngine;

namespace UllrStudio._Scripts.Environment
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField] private Transform _pointA = null;
        [SerializeField] private Transform _pointB = null;
        [SerializeField] private float _moveSpeed = 0;

        [SerializeField] private bool _triggered = false;

        private void Awake()
        {
            _pointA = gameObject.transform.parent.Find("PointA");
            _pointB = gameObject.transform.parent.Find("PointB");
            if (_pointA == null || _pointB == null)
                Debug.Log($"Check {gameObject.transform.parent.name}, one of the transform points are null");
        }

        private void FixedUpdate()
        {
            var speed = _moveSpeed * Time.deltaTime;
            if (_triggered)
            {
                transform.position = Vector3.MoveTowards(transform.position, _pointB.position, speed);
            }
            else if (_triggered == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _pointA.position, speed);
            }
        }

        public void CallElevator()
        {
            _triggered = !_triggered;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            other.transform.parent = transform;
        }

        private void OnTriggerExit(Collider other)
        {
            other.transform.parent = null;
        }
    }
}