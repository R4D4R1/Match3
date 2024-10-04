using UnityEngine;

public class GameStateManager
{
    private GameObject gameOverCanvas;
    public bool IsProcessing { get; private set; }
    public bool IsGameOver { get; private set; }

    public void SetGameoverCanvas(GameObject gameOverCanvas)
    {
        this.gameOverCanvas = gameOverCanvas;
    }

    public void StartProcessing()
    {
        IsProcessing = true;
    }

    public void StopProcessing()
    {
        IsProcessing = false;
    }

    public void GameOver()
    {
        IsGameOver = true;
        GameUIController.Instance.EnableGameOverMenu();
        Debug.Log("Game Over");
    }

    public void GameStarted()
    {
        IsGameOver=false;
    }
}
