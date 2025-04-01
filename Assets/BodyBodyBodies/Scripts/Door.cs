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

    //플레이어가 문에 도달
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = openDoorSprite;
            isPlayerInDoor = true;
            stayTime = 0f;

        }
    }

    //플레이어가 문에서 빠져 나감
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = closeDoorSprite;
            isPlayerInDoor = false;
        }
    }
    
}
