using UnityEngine;

public class GridManager
{
    public Ball[,] Grid { get; private set; }

    public void SetGridSize(int gridSizeX, int gridSizeY)
    {
        Grid = new Ball[gridSizeX, gridSizeY];
    }

    public void SetBallAt(int x, int y, Ball ball)
    {
        Grid[x, y] = ball;
    }

    public Ball GetBallAt(int x, int y)
    {
        return Grid[x, y];
    }

    public void ClearBallAt(int x, int y)
    {
        Grid[x, y] = null;
    }

    public bool IsPositionWithinBounds(int x, int y)
    {
        return x >= 0 && x < Grid.GetLength(0) && y >= 0 && y < Grid.GetLength(1);
    }
}
