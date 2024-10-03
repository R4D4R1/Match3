using Cysharp.Threading.Tasks;
using UnityEngine;

public class BallSpawner
{
    private Ball ballPrefab;
    private Transform gridParent;
    private float padding;
    private int gridSizeX;
    private int gridSizeY;
    private BallTypeManager ballTypeManager;
    private GridManager gridManager;
    private ObjectPool<Ball> ballPool;

    public void SetBallSpawner(Ball ballPrefab, Transform gridParent, float padding, int gridSizeX, int gridSizeY, BallTypeManager ballTypeManager, GridManager gridManager)
    {
        this.ballPrefab = ballPrefab;
        this.gridParent = gridParent;
        this.padding = padding;
        this.gridSizeX = gridSizeX;
        this.gridSizeY = gridSizeY;
        this.ballTypeManager = ballTypeManager;
        this.gridManager = gridManager;
        this.ballPool = new ObjectPool<Ball>(ballPrefab, gridParent, gridSizeX * gridSizeY);
    }

    public void InitializeGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                SpawnBallAt(x, y);
            }
        }
    }

    public void SpawnBallAt(int x, int y)
    {
        Vector3 position = new Vector3(x * (1 + padding), y * (1 + padding), 0);
        Ball ball = ballPool.Get();
        ball.transform.SetParent(gridParent);
        ball.transform.localPosition = position;

        do
        {
            ball.Initialize(ballTypeManager.GetRandomBallConfig());
        } while (y == 0 && ball.IsIndestructible);

        gridManager.SetBallAt(x, y, ball);
    }

    public void DestroyBall(Ball ball)
    {
        int x = Mathf.RoundToInt(ball.transform.localPosition.x / (1 + padding));
        int y = Mathf.RoundToInt(ball.transform.localPosition.y / (1 + padding));
        gridManager.ClearBallAt(x, y);
        ballPool.Release(ball);
    }

    public async UniTask HandleNewBalls()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (gridManager.GetBallAt(x, y) == null)
                {
                    for (int aboveY = y + 1; aboveY < gridSizeY; aboveY++)
                    {
                        if (gridManager.GetBallAt(x, aboveY) != null)
                        {
                            Ball ballToMove = gridManager.GetBallAt(x, aboveY);
                            gridManager.ClearBallAt(x, aboveY);

                            if (ballToMove.IsIndestructible && y == 0)
                            {
                                ballPool.Release(ballToMove);
                            }
                            else
                            {
                                gridManager.SetBallAt(x, y, ballToMove);
                                ballToMove.transform.localPosition = new Vector3(x * (1 + padding), y * (1 + padding), 0);
                            }
                            break;
                        }
                    }
                }
            }
        }

        await UniTask.Delay(500);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = gridSizeY - 1; y >= 0; y--)
            {
                if (gridManager.GetBallAt(x, y) == null)
                {
                    SpawnBallAt(x, y);
                }
            }
        }
    }
}
