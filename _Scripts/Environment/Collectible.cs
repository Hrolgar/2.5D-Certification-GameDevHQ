using UnityEngine;

namespace UllrStudio._Scripts.Environment
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField]private float _rotationSpeed = 20f;

        private void Update()
        {
            // transform.Rotate(new Vector3(0,1,0) * (_rotationSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var player = GetComponent<Player.Player>();
        
                player.PickUpCollectible();
            
            Destroy(gameObject);
        }
    }
}
