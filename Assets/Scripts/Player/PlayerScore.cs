using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    int score = 0;
    public TextMeshProUGUI scoreText;

    String currentName = "";
    TextMeshProUGUI nameInput;
    Button newGameButton;

    List<String> names = new List<String>();
    List<int> scores = new List<int>();

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

        if (other.gameObject.CompareTag("Finish"))
        {
            // Save the score to scores.csv
            string filePath = "Assets/Scripts/Player/scores.csv";
            File.WriteAllText(filePath, score.ToString());
            Debug.Log("Score saved to " + filePath);
        }
    }
}
