using System.Collections;
using UllrStudio._Scripts.Player;
using UnityEngine;

namespace UllrStudio._Scripts.Environment
{
    public class ElevatorPanel : MonoBehaviour
    {
        [SerializeField] private Elevator _elevator = null;
        [SerializeField] private Transform _alarmLight = null;
        // [SerializeField] private bool _elevatorCalled = false;

        private void Start()
        {
            _elevator = transform.GetComponentInParent<Elevator>();
            if(!_elevator) Debug.Log($"The {transform.parent.transform.parent}'s Elevators Panel has not assigned not assigned Elevator.");
            _alarmLight = transform.Find("Scifi_Light").transform.Find("SpinningLight");
            if(!_elevator) Debug.Log($"The {transform.parent.transform.parent}'s Alarm light has not assigned not assigned Elevator.");
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (!Input.GetKeyDown(KeyCode.E)) return;
            var playerAnimations = other.GetComponentInChildren<PlayerAnimations>();
            playerAnimations.ActivateElevator();
            StartCoroutine(LetAnimationPlay());
        }

        private IEnumerator LetAnimationPlay()
        {
            yield return new WaitForSeconds(6f);
            StartCoroutine(SpinningLightRoutine());
            _elevator.CallElevator();
        }
        
        private IEnumerator SpinningLightRoutine()
        {
            _alarmLight.gameObject.SetActive(true);
            yield return new WaitForSeconds(6f);
            _alarmLight.gameObject.SetActive(false);
        }
    }
}
