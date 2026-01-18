using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Optional: Name des Events oben einstellbar machen, statt fest im Code
    public string soundEventName = "Play_Pickup"; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // WICHTIG: Wir spielen den Sound auf 'other.gameObject' (dem Player) ab!
            // Denn 'gameObject' (dieses Item) wird gleich gelöscht.
            AkUnitySoundEngine.PostEvent(soundEventName, other.gameObject);

            // Jetzt können wir das Item sicher löschen, der Sound reist mit dem Player weiter.
            Destroy(gameObject);
        }
    }
}