using UnityEngine;

public class Switch : MonoBehaviour
{
    public AutoBlock targetAutoBlock;   // ������ ���
    public MovingBlockH targetMovingBlock;

    int objectOnSwitch = 0;  // ����ġ�� ��� �ִ� ������Ʈ�� ����

    public Sprite pushedSprite;     // ���� ��ư ��������Ʈ
    public Sprite defaultSprite;    // ���� ��ư ��������Ʈ
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
                targetAutoBlock.SetCanMove(true); // ��� �̵� ����
            }

            if (targetMovingBlock != null)
            {
                targetMovingBlock.SetCanMove(true); // ��� �̵� ����
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Corpse"))
        {
            objectOnSwitch = Mathf.Max(0, objectOnSwitch - 1);  //������ ���� �ʵ��� ��

            if (objectOnSwitch == 0)     //�ö� ������Ʈ�� �ϳ��� ������
            {
                spriteRenderer.sprite = defaultSprite;      //���� ��ư �̹����� ����

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
