using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ScoreManager
{
    private const string ScoreFileName = "scores.json";
    private List<ScoreEntry> scores;

    public static int Score { get; private set; }

    public ScoreManager()
    {
        Score = 0;
        LoadScores();
    }

    public void UpdateScore(int matchedCount)
    {
        Score += 10 + (matchedCount - 3) * 5;
    }

    public void AddScore()
    {
        ScoreEntry newScore = new ScoreEntry
        {
            Score = Score,
            Date = DateTime.Now.ToString("MM/dd/yyyy")
        };

        scores.Add(newScore);
        scores = scores.OrderByDescending(s => s.Score).ToList();

        for (int i = 0; i < scores.Count; i++)
        {
            scores[i].Rank = i + 1;
        }

        SaveScores();
    }

    public List<ScoreEntry> GetScores()
    {
        return scores;
    }

    private void LoadScores()
    {
        if (File.Exists(GetFilePath()))
        {
            string json = File.ReadAllText(GetFilePath());
            ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);
            scores = scoreList != null ? scoreList.scores : new List<ScoreEntry>();
        }
        else
        {
            scores = new List<ScoreEntry>();
        }
    }

    private void SaveScores()
    {
        ScoreList scoreList = new ScoreList { scores = scores };
        string json = JsonUtility.ToJson(scoreList, true);

        string directoryPath = Path.GetDirectoryName(GetFilePath());
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(GetFilePath(), json);
    }

    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, ScoreFileName);
    }

    [Serializable]
    private class ScoreList
    {
        public List<ScoreEntry> scores;
    }

    [Serializable]
    public class ScoreEntry
    {
        public int Rank;
        public int Score;
        public string Date;
    }
}
