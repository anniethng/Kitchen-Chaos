using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TextMeshProUGUI highScoreTextEscape;
    public TextMeshProUGUI highScoreTextGameOver;

    public GameObject escapeMenu;
    public GameObject HUD;
    public GameObject gameOverMenu;

    public String username;
    bool updated = false;

    // External classes
    PlayerController playerController;
    PlayerScore playerScore;
    GameController gameController;

    List<String> names = new List<String>();
    List<String> scores = new List<String>();
    
    public void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        playerScore = FindFirstObjectByType<PlayerScore>();

        UpdateScores();
    }

    public void Update()
    {
        if (username != "" && username != null)
        {
            if(Input.GetKeyDown(KeyCode.Escape) && escapeMenu.gameObject.activeSelf)
            {
                escapeMenu.gameObject.SetActive(false);
                HUD.SetActive(true);

                UpdateScores();

                if (!updated)
                {
                    WriteScores();
                    updated = true;
                }
            } else if (Input.GetKeyDown(KeyCode.Escape) && !escapeMenu.gameObject.activeSelf)
            {
                escapeMenu.gameObject.SetActive(true);
                HUD.SetActive(false);

                UpdateScores();

                if (!updated)
                {
                    WriteScores();
                    updated = true;
                }
            }
        }
    }

    public void SaveName()
    {
        username = nameInput.text;
        nameInput.text = "";

        updated = true;

        Debug.Log("Name saved: " + username);
    }

    public void NewGame()
    {
        if (username != "" && username != null)
        {
            escapeMenu.SetActive(false);
            HUD.SetActive(true);
        }
    }

    public void TryAgain()
    {
        gameOverMenu.SetActive(false);
        escapeMenu.SetActive(true);
        gameController.Reset();
    }

    public void Death()
    {
        escapeMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        HUD.SetActive(false);
    }

    public void UpdateScores()
    {
        names.Clear();
        scores.Clear();

        names = playerScore.getScoreName();
        scores = playerScore.getScoreNumber();

        Debug.Log(names.ToString());

        //Sort scores and names based on scores descending
        for (int i = 0; i < scores.Count - 1; i++)
        {
            for (int j = i + 1; j < scores.Count; j++)
            {
                if (Int32.Parse(scores[i]) < Int32.Parse(scores[j]))
                {
                    //Swap scores
                    String tempScore = scores[i];
                    scores[i] = scores[j];
                    scores[j] = tempScore;

                    //Swap names
                    String tempName = names[i];
                    names[i] = names[j];
                    names[j] = tempName;
                }
            }
        }

        String t = "";

        for (int i = 0; i < names.Count; i++)
        {
            t += (i + 1) + ". " + names[i] + ": " + scores[i] + "\n";
        }

        highScoreTextEscape.text = "Highscores \n \n" + t;
        highScoreTextGameOver.text = "Highscores \n \n" + t;
    }

    public void WriteScores()
    {
        // Save the score to scores.csv
        string filePath = "Assets/Scripts/Player/scores.csv";
        string text = File.ReadAllText(filePath);

        File.WriteAllText(filePath, text + Environment.NewLine + username + "," + playerScore.score.ToString());

        escapeMenu.SetActive(true);
        HUD.SetActive(false);
    }
}