using UnityEngine;

public class HintManager
{
    private GridManager gridManager;
    private MatchFinder matchFinder;
    private GameObject hintArrow;
    private Ball hintedBall;

    public void SetHintManager(GridManager gridManager, MatchFinder matchFinder, GameObject hintArrow)
    {
        this.gridManager = gridManager;
        this.matchFinder = matchFinder;
        this.hintArrow = hintArrow;
    }

    public void ShowFirstHint(int gridSizeX, int gridSizeY)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (gridManager.GetBallAt(x, y) != null)
                {
                    var matchedBalls = matchFinder.GetMatchedBalls(gridManager.GetBallAt(x, y));
                    if (matchedBalls.Count >= 3)
                    {
                        hintedBall = matchedBalls[0];
                        ShowHint(hintedBall);
                        return;
                    }
                }
            }
        }
    }

    public void HideHint()
    {
        hintArrow.SetActive(false);
        hintedBall = null;
    }

    public void ShowHint(Ball ball)
    {
        hintArrow.SetActive(true);
        hintArrow.transform.position = ball.transform.position;
    }

    public Ball GetHintedBall()
    {
        return hintedBall;
    }
}
