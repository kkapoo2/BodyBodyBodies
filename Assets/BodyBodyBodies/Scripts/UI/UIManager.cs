using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;  // ���� ȭ�� (StartDisplay)
    public GameObject gameStages;  // ��� ���������� �����ϴ� GameStages

    public void StartGame()
    {
        startPanel.SetActive(false); // ���� ȭ�� ��Ȱ��ȭ
        gameStages.SetActive(true);  // �������� ��ü Ȱ��ȭ
        GameManager.Instance.GoToStage(0); // 1-1 ���������� �̵�
    }
}