using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    public static Bootstrapper Instance;

    public GridManager gridManager {  get; private set; }
    public BallSpawner ballSpawner {  get; private set; }
    public ScoreManager scoreManager {  get; private set; }
    public GameStateManager gameStateManager {  get; private set; }
    public MatchFinder matchFinder {  get; private set; }

    public LoadLevelController loadLevelController { get; private set; }
    public GameController gameController { get; private set; }

    public HintManager hintManager { get; private set; }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        gridManager = new GridManager();
        ballSpawner = new BallSpawner();
        scoreManager = new ScoreManager();
        gameStateManager = new GameStateManager();
        matchFinder = new MatchFinder();
        hintManager = new HintManager();

        loadLevelController = GetComponentInChildren<LoadLevelController>();
        gameController = GetComponentInChildren<GameController>();

        loadLevelController.LoadSceneAsync("StartScene");
    }
}
