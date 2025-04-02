using UnityEngine;

public class Switch : MonoBehaviour
{
    public AutoBlock targetBlock;   // 움직일 블록
    int objectOnSwitch = 0;  // 스위치를 밟고 있는 오브젝트의 개수

    public Sprite pushedSprite;     // 눌린 버튼 스프라이트
    public Sprite defaultSprite;    // 원래 버튼 스프라이트
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Corpse"))
        {
            objectOnSwitch++;
            Debug.Log("스위치 위의 오브젝트 개수: " + objectOnSwitch);
            spriteRenderer.sprite = pushedSprite;

            if (targetBlock != null)
            {
                targetBlock.SetCanMove(true); // 블록 이동 시작
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Corpse"))
        {
            objectOnSwitch = Mathf.Max(0, objectOnSwitch - 1);  //음수가 되지 않도록 함
            Debug.Log("스위치에서 내려옴, 남은 오브젝트 개수: " + objectOnSwitch);
            if (objectOnSwitch == 0)     //올라간 오브젝트가 하나도 없으면
            {
                spriteRenderer.sprite = defaultSprite;      //원래 버튼 이미지로 변경

                if(targetBlock != null)
                {
                    targetBlock.SetCanMove(false);
                }
            }
        }
    }
}
