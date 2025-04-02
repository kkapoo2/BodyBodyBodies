using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameManager gameManager;
    public float defaultSize = 7.0f;
    public float zoomedOutSize = 8.0f;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (gameManager.stageIndex == gameManager.Stages.Length - 3 || 
            gameManager.stageIndex == gameManager.Stages.Length - 2)
        {
            cam.orthographicSize = zoomedOutSize; // ������ ������������ �ܾƿ�
        }
        else
        {
            cam.orthographicSize = defaultSize; // �⺻ ī�޶� ũ��
        }
    }

}
