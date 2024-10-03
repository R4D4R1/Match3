using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private GameObject middleUICanvas;
    [SerializeField] private GameObject GOLabel;
    [SerializeField] private GameObject PauseLabel;
    [SerializeField] private Button PauseButton;


    public void EnableGameOverMenu()
    {
        middleUICanvas.SetActive(true);
        GOLabel.SetActive(true);
        PauseLabel.SetActive(false);
        PauseButton.interactable = false;
    }

    public void TogglePauseMenu()
    {
        
        if (!middleUICanvas.activeSelf)
            middleUICanvas.SetActive(true);
        else
            middleUICanvas.SetActive(false);
    }
}
