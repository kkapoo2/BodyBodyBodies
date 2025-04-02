using UnityEngine;

public class Switch : MonoBehaviour
{
    public AutoBlock targetAutoBlock;   // 움직일 블록
    public MovingBlockH targetMovingBlock;

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
            spriteRenderer.sprite = pushedSprite;

            if (targetAutoBlock != null)
            {
                targetAutoBlock.SetCanMove(true); // 블록 이동 시작
            }

            if (targetMovingBlock != null)
            {
                targetMovingBlock.SetCanMove(true); // 블록 이동 시작
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Corpse"))
        {
            objectOnSwitch = Mathf.Max(0, objectOnSwitch - 1);  //음수가 되지 않도록 함

            if (objectOnSwitch == 0)     //올라간 오브젝트가 하나도 없으면
            {
                spriteRenderer.sprite = defaultSprite;      //원래 버튼 이미지로 변경

                if(targetAutoBlock != null)
                {
                    targetAutoBlock.SetCanMove(false);
                }

                if(targetMovingBlock != null)
                {
                    targetMovingBlock.SetCanMove(false);
                }
            }
        }
    }
}
