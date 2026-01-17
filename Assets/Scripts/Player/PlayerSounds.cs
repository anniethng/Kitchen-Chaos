using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    
    {
        Debug.Log("Berührung erkannt mit: " + collision.gameObject.name);
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            AkUnitySoundEngine.PostEvent("Play_Impact", gameObject);
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            AkUnitySoundEngine.SetSwitch("CollisionType", "Hard", gameObject);
            AkUnitySoundEngine.PostEvent("Play_Collision", gameObject);
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            AkUnitySoundEngine.SetSwitch("CollisionType", "Soft", gameObject);
            AkUnitySoundEngine.PostEvent("Play_Collision", gameObject);
        }
    }
}
