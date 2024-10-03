using System.Collections.Generic;
using UnityEngine;

public class HighscoreUIManager : MonoBehaviour
{
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject scoreEntryPrefab;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = Bootstrapper.Instance.scoreManager;
        DisplayScores(scoreManager.GetScores());
    }

    private void DisplayScores(List<ScoreManager.ScoreEntry> scores)
    {
        foreach (var score in scores)
        {
            GameObject scoreEntryObject = Instantiate(scoreEntryPrefab, contentPanel);
            ScoreEntryUI scoreEntryUI = scoreEntryObject.GetComponent<ScoreEntryUI>();
            scoreEntryUI.SetScoreEntry(score);
        }
    }
}
