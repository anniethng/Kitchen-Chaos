using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            score += 1;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
