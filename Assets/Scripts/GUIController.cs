using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    Button newGameButton;
    Button closeButton;
    public TMP_InputField nameInput;
    public TextMeshProUGUI[] highScoreTexts = new TextMeshProUGUI[2];

    public String username;
    PlayerController playerController;
    PlayerScore playerScore;
    
    public void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        playerScore = FindFirstObjectByType<PlayerScore>();
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
            playerController.Reset();
        }
    }

    public void UpdateScores()
    {
        for (int i = 0; i < 2; i++)
        {
            highScoreTexts[i].text = "Scores: \n";

            String[] names = playerScore.getScoreName();
            String[] scores = playerScore.getScoreNumber();

            for (int j = 0; j < names.Length && j < scores.Length; j++)
            {
                highScoreTexts[i].text += names[j] + ": " + scores[j] + "\n";
            }
        }
    }
}