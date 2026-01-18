using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Notwendig für die korrekte Sortierung
using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TextMeshProUGUI highScoreTextEscape;
    public TextMeshProUGUI highScoreTextGameOver;

    public GameObject escapeMenu;
    public GameObject HUD;
    public GameObject gameOverMenu;

    public static string savedUsername = ""; 
    public string username;

    PlayerScore playerScore;
    GameController gameController;

    void Start()
    {
        playerScore = FindFirstObjectByType<PlayerScore>();
        gameController = FindFirstObjectByType<GameController>();

        if (!string.IsNullOrEmpty(savedUsername)) username = savedUsername;

        if (!string.IsNullOrEmpty(username)) {
            escapeMenu.SetActive(false);
            HUD.SetActive(true);
            if(nameInput != null) nameInput.text = username;
        } else {
            escapeMenu.SetActive(true);
            HUD.SetActive(false);
        }
        UpdateScores();
    }

    public void Update()
    {
        if (!string.IsNullOrEmpty(username)) {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                bool isVisible = !escapeMenu.activeSelf;
                escapeMenu.SetActive(isVisible);
                HUD.SetActive(!isVisible);
                UpdateScores();
            }
        }
    }

    public void SaveName() {
        if (!string.IsNullOrEmpty(nameInput.text)) {
            username = nameInput.text;
            savedUsername = username;
            NewGame();
        }
    }

    public void NewGame() {
        escapeMenu.SetActive(false);
        HUD.SetActive(true);
        if(gameController != null) gameController.Reset();
    }

    public void Death() {
        WriteScores(); 
        UpdateScores();
        escapeMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        HUD.SetActive(false);
    }

    public void UpdateScores()
    {
        List<ScoreEntry> allEntries = new List<ScoreEntry>();
        string path = Application.persistentDataPath + "/scores.csv";

        // 1. Lesen & Parsen (String.Split Anforderung)
        if (File.Exists(path)) {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines) {
                string[] parts = line.Split(',');
                if (parts.Length == 2) {
                    // WICHTIG: String in INT umwandeln für numerische Sortierung!
                    if (int.TryParse(parts[1].Trim(), out int s)) {
                        allEntries.Add(new ScoreEntry { name = parts[0], score = s });
                    }
                }
            }
        }

        // 2. Auffüllen auf 5 Einträge (Pflichtpunkt)
        int filler = 100;
        while (allEntries.Count < 5) {
            allEntries.Add(new ScoreEntry { name = "Bot", score = filler });
            filler -= 20;
        }

        // 3. Numerische Sortierung (Absteigend: 100 kommt vor 2)
        allEntries = allEntries.OrderByDescending(x => x.score).ToList();

        // 4. Formatierung für UI (String.Format Anforderung)
        string displayString = "";
        for (int i = 0; i < 5; i++) {
            displayString += string.Format("{0}. {1}: {2}\n", i + 1, allEntries[i].name, allEntries[i].score);
        }

        if(highScoreTextEscape) highScoreTextEscape.text = "Highscores\n\n" + displayString;
        if(highScoreTextGameOver) highScoreTextGameOver.text = "Highscores\n\n" + displayString;
    }

    public void WriteScores()
    {
        string path = Application.persistentDataPath + "/scores.csv";
        int finalScore = (playerScore != null) ? playerScore.score : 0;
        // String.Format für CSV-Eintrag
        string line = string.Format("{0},{1}{2}", username, finalScore, Environment.NewLine);
        File.AppendAllText(path, line);
    }

    public void TryAgain() {
        gameOverMenu.SetActive(false);
        if(gameController != null) gameController.Reset();
    }
}

// Die Hilfsklasse für die Liste
public class ScoreEntry {
    public string name;
    public int score;
}