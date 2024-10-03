using UnityEditor;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private Bootstrapper entryPoint = Bootstrapper.Instance;

    public void LoadStartScene()
    {
        entryPoint.loadLevelController.LoadSceneAsync("StartScene");
    }

    public void LoadMainGameScene()
    {
        entryPoint.loadLevelController.LoadSceneAsync("MainScene");
    }

    public void LoadHighScoreScene()
    {
        entryPoint.loadLevelController.LoadSceneAsync("ScoreScene");
    }

    public void LoadAboutGameScene()
    {
        entryPoint.loadLevelController.LoadSceneAsync("AboutGameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
