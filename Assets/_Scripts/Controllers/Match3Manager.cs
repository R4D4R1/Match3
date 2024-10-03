using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class Match3Manager : MonoBehaviour
{
    public static Match3Manager Instance { get; private set; }

    [SerializeField] private int gridSizeX = 8;
    [SerializeField] private int gridSizeY = 8;
    [SerializeField] private Transform gridParent;
    [SerializeField] private float padding = 0.1f;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private BallTypeManager ballTypeManager;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject hintArrow;

    public int Score { get; private set; }

    private GridManager gridManager;
    private BallSpawner ballSpawner;
    private GameStateManager gameStateManager;
    private MatchFinder matchFinder;
    private ScoreManager scoreManager;
    private HintManager hintManager;

    public event Action BallClicked;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        var bootstrapper = Bootstrapper.Instance;
        gridManager = bootstrapper.gridManager;
        gridManager.SetGridSize(gridSizeX, gridSizeY);

        ballSpawner = bootstrapper.ballSpawner;
        ballSpawner.SetBallSpawner(ballPrefab, gridParent, padding, gridSizeX, gridSizeY, ballTypeManager, gridManager);

        scoreManager = bootstrapper.scoreManager;

        gameStateManager = bootstrapper.gameStateManager;
        gameStateManager.SetGameoverCanvas(gameOverCanvas);

        matchFinder = bootstrapper.matchFinder;
        matchFinder.SetGridForMatchfindingBalls(gridManager, padding);

        hintManager = bootstrapper.hintManager;
        hintManager.SetHintManager(gridManager, matchFinder, hintArrow);

        InitializeGrid();
    }

    private void InitializeGrid()
    {
        do
        {
            ballSpawner.InitializeGrid();
        } while (!matchFinder.HasPossibleMoves());

        hintManager.ShowFirstHint(gridSizeX, gridSizeY);
    }

    public async void OnBallClicked(Ball clickedBall)
    {
        if (gameStateManager.IsProcessing || gameStateManager.IsGameOver) return;
        if (clickedBall == hintManager.GetHintedBall())
        {
            hintManager.HideHint();
        }

        var matchedBalls = matchFinder.GetMatchedBalls(clickedBall);
        if (matchedBalls.Count >= 3)
        {
            hintManager.HideHint();

            gameStateManager.StartProcessing();

            foreach (var ball in matchedBalls)
            {
                if (!ball.IsIndestructible)
                {
                    ballSpawner.DestroyBall(ball);
                }
            }

            scoreManager.UpdateScore(matchedBalls.Count);
            BallClicked?.Invoke();

            await UniTask.Delay(500);
            await ballSpawner.HandleNewBalls();

            if (!matchFinder.HasPossibleMoves())
            {
                gameStateManager.GameOver();
                scoreManager.AddScore();
            }

            gameStateManager.StopProcessing();
        }
    }
}
