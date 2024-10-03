using TMPro;
using UnityEngine;

public class ScoreEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text dateText;

    public void SetScoreEntry(ScoreManager.ScoreEntry scoreEntry)
    {
        rankText.text = scoreEntry.Rank.ToString();
        scoreText.text = scoreEntry.Score.ToString();
        dateText.text = scoreEntry.Date;
    }
}
