using UnityEngine;

public class ButtonPushed : MonoBehaviour
{
    public GameObject PushedButton; // ���� ��ư ��������Ʈ
    public GameObject Button;       // ���� ��ư ��������Ʈ

    void Start()
    {
        if (PushedButton != null)
        {
            PushedButton.SetActive(false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Item"))
        {
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
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Item"))
        {
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
        }
    }
}
