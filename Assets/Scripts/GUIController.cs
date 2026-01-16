using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
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

    // --- NEU: Statische Variable, die den Reset überlebt ---
    public static string savedUsername = ""; 
    // ------------------------------------------------------

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
        gameController = FindFirstObjectByType<GameController>(); // Wichtig!

        // --- NEU: Logik für den automatischen Start ---
        // 1. Haben wir noch einen Namen im Gedächtnis (vom letzten Spiel)?
        if (!string.IsNullOrEmpty(savedUsername))
        {
            username = savedUsername; // Name wiederherstellen
        }

        // 2. Entscheidung: Wenn Name da ist -> Sofort spielen!
        if (!string.IsNullOrEmpty(username))
        {
            escapeMenu.SetActive(false);
            HUD.SetActive(true);
            if(nameInput != null) nameInput.text = username; // Fürs Menü
        }
        else 
        {
            // Kein Name -> Startbildschirm zeigen
            escapeMenu.SetActive(true);
            HUD.SetActive(false);
        }
        // ---------------------------------------------

        UpdateScores();
    }

    public void Update()
    {
        // Nur Escape erlauben, wenn wir auch wirklich spielen (Name existiert)
        if (!string.IsNullOrEmpty(username))
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(escapeMenu.activeSelf)
                {
                    // Menü schließen
                    escapeMenu.SetActive(false);
                    HUD.SetActive(true);
                    
                    UpdateScores();
                    if (!updated) { WriteScores(); updated = true; }
                } 
                else
                {
                    // Menü öffnen
                    escapeMenu.SetActive(true);
                    HUD.SetActive(false);
                    
                    UpdateScores();
                    if (!updated) { WriteScores(); updated = true; }
                }
            }
        }
    }

    public void SaveName()
    {
        username = nameInput.text;
        
        // --- NEU: Name im statischen Gedächtnis sichern ---
        savedUsername = username;
        // --------------------------------------------------

        nameInput.text = "";
        updated = true;
        Debug.Log("Name saved: " + username);
        
        // Direkt das Spiel starten nach Eingabe
        NewGame(); 
    }

    public void NewGame()
    {
        if (!string.IsNullOrEmpty(username))
        {
            escapeMenu.SetActive(false);
            HUD.SetActive(true);
            if(gameController != null) gameController.Reset(); // Reset nur beim Tod, hier eigentlich nur UI umschalten
        }
    }
    
    // ... Rest (Death, TryAgain, UpdateScores, WriteScores) bleibt gleich ...
    public void TryAgain()
    {
        gameOverMenu.SetActive(false);
        // Da wir den Namen noch wissen, können wir direkt resetten ohne Menü
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
        if(playerScore == null) return; // Sicherheits-Check

        names.Clear();
        scores.Clear();
        names = playerScore.getScoreName();
        scores = playerScore.getScoreNumber();

        // (Hier dein Sortier-Code von vorhin einfügen oder lassen)
        // ...
        
        // Nur als Platzhalter, damit kein Fehler kommt, falls du den Code unten gekürzt hast:
        String t = "";
        if(names != null && scores != null) {
             for (int i = 0; i < Mathf.Min(names.Count, 5); i++) {
                t += (i + 1) + ". " + names[i] + ": " + scores[i] + "\n";
            }
        }
        if(highScoreTextEscape) highScoreTextEscape.text = "Highscores \n \n" + t;
        if(highScoreTextGameOver) highScoreTextGameOver.text = "Highscores \n \n" + t;
    }

    public void WriteScores()
    {
        // Dein WriteScores Code...
        string filePath = "Assets/Scripts/Player/scores.csv";
        if(File.Exists(filePath)) {
            string text = File.ReadAllText(filePath);
            File.WriteAllText(filePath, text + Environment.NewLine + username + "," + playerScore.score.ToString());
        }
    }
}