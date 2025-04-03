using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;  // 시작 화면 (StartDisplay)
    public GameObject gameStages;  // 모든 스테이지를 포함하는 GameStages

    public void StartGame()
    {
        startPanel.SetActive(false); // 시작 화면 비활성화
        gameStages.SetActive(true);  // 스테이지 전체 활성화
        GameManager.Instance.GoToStage(0); // 1-1 스테이지로 이동
    }
}