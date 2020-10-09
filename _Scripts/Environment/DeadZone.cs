using UnityEngine;
using UnityEngine.SceneManagement;

namespace UllrStudio._Scripts.Environment
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
