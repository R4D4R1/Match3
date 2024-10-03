using System;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;


    private void Start()
    {
        Match3Manager.Instance.BallClicked += BalledTouched;
        Init();

    }

    private void OnDestroy()
    {
        Match3Manager.Instance.BallClicked -= BalledTouched;
    }

    private void Init()
    {
        scoreText.text = $"Score {ScoreManager.Score} ";
    }

    private void BalledTouched()
    {
        scoreText.text = $"Score {ScoreManager.Score} ";
    }
}
