using UnityEngine;

public class Switch : MonoBehaviour
{
    public AutoBlock targetBlock;    // 움직일 블록
    private int objectOnSwich = 0;  // 스위치를 밟고 있는 오브젝트의 개수

    public GameObject PushedButton; // 눌린 버튼 스프라이트
    public GameObject Button;       // 원래 버튼 스프라이트

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

            if (targetBlock != null)
            {
                targetBlock.isCanMove = true; // 블록 이동 시작
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Corpse"))
        {
            objectOnSwich--;

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

            if (objectOnSwich <=0 && targetBlock != null)
            {
                targetBlock.isCanMove = false;
            }
            
        }
    }
}
