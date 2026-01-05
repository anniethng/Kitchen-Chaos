using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    public GameObject pickupPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i <= 10; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-30f, 30f), 1f, Random.Range(-30f, 30f));
            Instantiate(pickupPrefab, randomPosition, Quaternion.identity);
        }
    }

}
