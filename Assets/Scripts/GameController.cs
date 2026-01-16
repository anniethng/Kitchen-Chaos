using UnityEngine;
using UnityEngine.SceneManagement; // Das brauchen wir für den Reset

// HIER steht das wichtige MonoBehaviour:
public class GameController : MonoBehaviour 
{
    public static bool isRestarting = false;
    // Variablen für die anderen Skripte
    PlayerController playerController;
    PlayerScore playerScore;
    SpawnPickup spawnPickup;
    GUIController GUIController;

    void Start()
    {
        // Hier suchen wir die anderen Skripte in der Szene
        playerController = FindFirstObjectByType<PlayerController>();
        playerScore = FindFirstObjectByType<PlayerScore>();
        spawnPickup = FindFirstObjectByType<SpawnPickup>();
        GUIController = FindFirstObjectByType<GUIController>();
    }

    public void Reset()
    {
        // Der einfachste Weg für einen Reset: Lade die Szene neu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}