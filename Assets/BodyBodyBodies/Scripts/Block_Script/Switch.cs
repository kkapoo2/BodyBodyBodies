using UnityEngine;

public class Switch : MonoBehaviour
{
    public AutoBlock targetBlock;   // ������ ���
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
            Debug.Log("����ġ ���� ������Ʈ ����: " + objectOnSwitch);
            spriteRenderer.sprite = pushedSprite;

            if (targetBlock != null)
            {
                targetBlock.SetCanMove(true); // ��� �̵� ����
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Corpse"))
        {
            objectOnSwitch = Mathf.Max(0, objectOnSwitch - 1);  //������ ���� �ʵ��� ��
            Debug.Log("����ġ���� ������, ���� ������Ʈ ����: " + objectOnSwitch);
            if (objectOnSwitch == 0)     //�ö� ������Ʈ�� �ϳ��� ������
            {
                spriteRenderer.sprite = defaultSprite;      //���� ��ư �̹����� ����

                if(targetBlock != null)
                {
                    targetBlock.SetCanMove(false);
                }
            }
        }
    }
}
