using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    public GameObject pickupPrefab;

    public List<GameObject> pickups = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnNewPickups();
    }

    public void SpawnNewPickups()
    {
        // Clear existing pickups
        foreach (GameObject pickup in pickups)
        {
            Destroy(pickup);
        }

        for (int i = 0; i <= 10; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-30f, 30f), 1f, Random.Range(-30f, 30f));
            pickups.Add(Instantiate(pickupPrefab, randomPosition, Quaternion.identity));
        }
    }
}
