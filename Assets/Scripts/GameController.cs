using UnityEngine;

public class GameController : MonoBehaviour
{
    // External classes
    PlayerController playerController;
    PlayerScore playerScore;
    SpawnPickup spawnPickup;
    GUIController GUIController;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        playerScore = FindFirstObjectByType<PlayerScore>();
        spawnPickup = FindFirstObjectByType<SpawnPickup>();
        GUIController = FindFirstObjectByType<GUIController>();
    }

    public void Reset()
    {
        playerScore.score = 0;
        playerScore.scoreText.text = "Score: " + "0";
        playerController.transform.position = new Vector3(0, 1, 33);
        playerController.speed = 5;

        GUIController.username = "";

        GUIController.HUD.SetActive(false);
        GUIController.escapeMenu.SetActive(true);

        spawnPickup.SpawnNewPickups();
    }
}
