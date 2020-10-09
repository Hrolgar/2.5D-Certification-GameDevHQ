using UnityEngine;

namespace UllrStudio._Scripts.Environment
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform _pointA;
        [SerializeField] private Transform _pointB;
        [SerializeField] private bool _switching = false;
        [SerializeField] private float _moveSpeed = 0;

        private void Awake()
        {
            _pointA = gameObject.transform.parent.Find("PointA");
            _pointB = gameObject.transform.parent.Find("PointB");
            if(_pointA == null || _pointB == null) Debug.Log($"Check {gameObject.transform.parent.name}, one of the transform points are null");
        }

        private void FixedUpdate()
        {
            MovePlatform();
        }

        private void MovePlatform()
        {
            var speed = _moveSpeed * Time.deltaTime;
            
            if (transform.position == _pointA.position)
            {
                _switching = false;
            }
            else if (transform.position == _pointB.position)
            {
                _switching = true;
            }

            if (_switching == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _pointB.position, speed);
            }
            else if (_switching)
            {
                transform.position = Vector3.MoveTowards(transform.position, _pointA.position, speed);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = transform;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = null;
            }
        }
    }
}
