using UnityEngine;

public class ParticlePosition : MonoBehaviour
{
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 0.5f, player.transform.position.z - 0.5f);
        transform.rotation = Quaternion.Euler(-28, 180, 0);
    }
}
