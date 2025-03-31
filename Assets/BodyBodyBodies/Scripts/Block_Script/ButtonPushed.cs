using UnityEngine;

public class ButtonPushed : MonoBehaviour
{
    public GameObject PushedButton; // 눌린 버튼 스프라이트
    public GameObject Button;       // 원래 버튼 스프라이트

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
            // Button 활성화 상태 확인 후 비활성화
            if (Button != null && Button.activeSelf)
            {
                Button.SetActive(false);
                Debug.Log("버튼이 눌렸습니다.");
            }

            // PushedButton 활성화 상태 확인 후 활성화
            if (PushedButton != null && !PushedButton.activeSelf)
            {
                PushedButton.SetActive(true);
                Debug.Log("버튼이 눌렸습니다.");
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Item"))
        {
            // Button 활성화 상태 확인 후 활성화
            if (Button != null && !Button.activeSelf)
            {
                Button.SetActive(true);
                Debug.Log("버튼이 눌리지 않았습니다.");
            }

            // PushedButton 활성화 상태 확인 후 비활성화
            if (PushedButton != null && PushedButton.activeSelf)
            {
                PushedButton.SetActive(false);
                Debug.Log("버튼이 눌리지 않았습니다.");

            }
        }
    }
}
