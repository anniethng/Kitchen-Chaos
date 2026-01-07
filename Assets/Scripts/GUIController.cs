using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    Button newGameButton;
    Button closeButton;
    public TMP_InputField nameInput;
    public TextMeshProUGUI highScoreTextEscape;
    public TextMeshProUGUI highScoreTextGameOver;

    public String username;
    PlayerController playerController;
    PlayerScore playerScore;

    List<String> names = new List<String>();
    List<String> scores = new List<String>();
    
    public void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        playerScore = FindFirstObjectByType<PlayerScore>();

        UpdateScores();
    }

    public void SaveName()
    {
        username = nameInput.text;
        nameInput.text = "";

        Debug.Log("Name saved: " + username);
    }

    public void NewGame()
    {
        if (username != "" && username != null)
        {
            playerController.escapeMenu.SetActive(false);
            playerController.HUD.SetActive(true);
            playerController.Reset();
        }
    }

    public void TryAgain()
    {
        playerController.gameOverMenu.SetActive(false);
        playerController.escapeMenu.SetActive(true);
        playerController.Reset();
    }

    public void UpdateScores()
    {
        String t = "";

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

        for (int i = 0; i < 5; i++)
        {
            t += i + 1 + ". " + names[i] + ": " + scores[i] + "\n";
        }
        
        highScoreTextEscape.text = "Highscores \n \n" + t;
        highScoreTextGameOver.text = "Highscores \n \n" + t;
    }
}