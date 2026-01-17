using UnityEngine;

public class Pickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            // Destroy the pickup object
            Destroy(gameObject);
        }
    }
}
