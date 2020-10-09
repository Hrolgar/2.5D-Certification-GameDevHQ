using System;
using UnityEngine;

namespace UllrStudio._Scripts.Environment
{
    public class ClimbLadder : MonoBehaviour
    {
        [SerializeField] private Transform _endPos = null;
        [SerializeField] private Transform _standPos = null;

        private void Start()
        {
            _endPos = transform.GetChild(0);
            _standPos = transform.GetChild(1);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var player = other.transform.GetComponent<Player.Player>();
            if(player == null) Debug.Log("It's null..");
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.ClimbLadder(_endPos, _standPos);
            }
        }

    }
}