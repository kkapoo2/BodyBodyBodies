using UnityEngine;

public class Switch : MonoBehaviour
{
    public AutoBlock targetBlock;    // ������ ���
    private int objectOnSwich = 0;  // ����ġ�� ��� �ִ� ������Ʈ�� ����

    public GameObject PushedButton; // ���� ��ư ��������Ʈ
    public GameObject Button;       // ���� ��ư ��������Ʈ

    void Start()
    {
        if (PushedButton != null)
        {
            PushedButton.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Corpse"))
        {
            objectOnSwich++;

            // Button Ȱ��ȭ ���� Ȯ�� �� ��Ȱ��ȭ
            if (Button != null && Button.activeSelf)
            {
                Button.SetActive(false);
                Debug.Log("��ư�� ���Ƚ��ϴ�.");
            }

            // PushedButton Ȱ��ȭ ���� Ȯ�� �� Ȱ��ȭ
            if (PushedButton != null && !PushedButton.activeSelf)
            {
                PushedButton.SetActive(true);
                Debug.Log("��ư�� ���Ƚ��ϴ�.");
            }

            if (targetBlock != null)
            {
                targetBlock.isCanMove = true; // ��� �̵� ����
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Corpse"))
        {
            objectOnSwich--;

            // Button Ȱ��ȭ ���� Ȯ�� �� Ȱ��ȭ
            if (Button != null && !Button.activeSelf)
            {
                Button.SetActive(true);
                Debug.Log("��ư�� ������ �ʾҽ��ϴ�.");
            }

            // PushedButton Ȱ��ȭ ���� Ȯ�� �� ��Ȱ��ȭ
            if (PushedButton != null && PushedButton.activeSelf)
            {
                PushedButton.SetActive(false);
                Debug.Log("��ư�� ������ �ʾҽ��ϴ�.");

            }

            if (objectOnSwich <=0 && targetBlock != null)
            {
                targetBlock.isCanMove = false;
            }
            
        }
    }
}
