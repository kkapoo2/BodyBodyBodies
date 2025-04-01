using UnityEngine;

public class Door : MonoBehaviour
{

    public Sprite openDoorSprite;
    public Sprite closeDoorSprite;
    private SpriteRenderer spriteRenderer;

    private bool isPlayerInDoor = false;
    private float stayTime = 0f;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isPlayerInDoor)
        {
            stayTime += Time.deltaTime;

            if(stayTime >= 2f)
            {
                GameManager.Instance.NextStage();
            }
        }
    }

    //�÷��̾ ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = openDoorSprite;
            isPlayerInDoor = true;
            stayTime = 0f;

        }
    }

    //�÷��̾ ������ ���� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = closeDoorSprite;
            isPlayerInDoor = false;
        }
    }
    
}
