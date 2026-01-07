using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    List<String> scores = new List<String>();
    List<String> names = new List<String>();

    GUIController guiController;
    PlayerController playerController;

    TextMeshProUGUI nameInput;
    Button newGameButton;

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();

        guiController = FindFirstObjectByType<GUIController>();
        playerController = FindFirstObjectByType<PlayerController>();
    }

    public String[] getScoreName()
    {
        // Extract scores from scores.csv and save the name and score to a list
        string filePath = "Assets/Scripts/Player/scores.csv";
        string[] lines = File.ReadAllLines(filePath);
        
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2)
            {
                names.Add(parts[0]);
            }

            return names.ToArray(); 
        }

        return new String[0];
    }

    public String[] getScoreNumber()
    {
        // Extract scores from scores.csv and save the name and score to a list
        string filePath = "Assets/Scripts/Player/scores.csv";
        string[] lines = File.ReadAllLines(filePath);
        
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2)
            {
                scores.Add(parts[1]);
            }

            return scores.ToArray(); 
        }

        return new String[0];
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
            string text = File.ReadAllText(filePath);

            File.WriteAllText(filePath, text + Environment.NewLine + guiController.username + "," + score.ToString());
            Debug.Log("Score saved to " + filePath);

            guiController.UpdateScores();
            playerController.Reset();
            playerController.escapeMenu.SetActive(true);
            playerController.HUD.SetActive(false);
        }
    }
}