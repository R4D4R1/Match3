using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelController : MonoBehaviour
{

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Slider _progressBar;

    public async void LoadSceneAsync(string sceneName)
    {
        var entryPoint = Bootstrapper.Instance;
        _progressBar.value = 0;

        await UniTask.Delay((int)(entryPoint.gameController.GameStartDelay * 1000));

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

       _loaderCanvas.SetActive(true);

        do
        {
            await UniTask.Delay(100);

            _progressBar.value = Mathf.Clamp01(scene.progress / 0.9f);

        } while (scene.progress < 0.9f);

        await UniTask.Delay((int)(entryPoint.gameController.GameAfterLoadDelay * 1000));

        scene.allowSceneActivation = true;

        await UniTask.Yield();
        _loaderCanvas.SetActive(false);
    }
}
