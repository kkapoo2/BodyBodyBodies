using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button startButton; // UI ¹öÆ°
    public GameManager gameManager;

    void Start()
    {
        startButton.onClick.AddListener(() => gameManager.GoToStage(1));
    }
}
