using System.Collections.Generic;
using UnityEngine;

public class MatchFinder
{
    private GridManager gridManager;
    private float padding;

    public void SetGridForMatchfindingBalls(GridManager gridManager, float padding)
    {
        this.gridManager = gridManager;
        this.padding = padding;
    }

    public List<Ball> GetMatchedBalls(Ball ball)
    {
        List<Ball> matchedBalls = new List<Ball> { ball };

        CheckDirection(ball, Vector2.left, matchedBalls);
        CheckDirection(ball, Vector2.right, matchedBalls);
        CheckDirection(ball, Vector2.up, matchedBalls);
        CheckDirection(ball, Vector2.down, matchedBalls);

        if (matchedBalls.Count < 3)
        {
            matchedBalls.Clear();
        }

        return matchedBalls;
    }

    private void CheckDirection(Ball ball, Vector2 direction, List<Ball> matchedBalls)
    {
        int x = Mathf.RoundToInt(ball.transform.localPosition.x / (1 + padding));
        int y = Mathf.RoundToInt(ball.transform.localPosition.y / (1 + padding));

        while (true)
        {
            x += (int)direction.x;
            y += (int)direction.y;

            if (!gridManager.IsPositionWithinBounds(x, y))
                break;

            Ball nextBall = gridManager.GetBallAt(x, y);

            if (nextBall == null || nextBall.IsIndestructible)
            {
                break;
            }

            if (nextBall.Type != ball.Type || nextBall.Color != ball.Color)
            {
                break;
            }

            matchedBalls.Add(nextBall);
        }
    }

    public bool HasPossibleMoves()
    {
        for (int x = 0; x < gridManager.Grid.GetLength(0); x++)
        {
            for (int y = 0; y < gridManager.Grid.GetLength(1); y++)
            {
                Ball ball = gridManager.GetBallAt(x, y);
                if (ball != null && GetMatchedBalls(ball).Count >= 3)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
