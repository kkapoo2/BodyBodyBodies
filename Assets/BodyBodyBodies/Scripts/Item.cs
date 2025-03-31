using UnityEngine;

public class Item : MonoBehaviour
{
    Rigidbody2D rb;

    public string itemName; // 아이템 이름
    public Sprite itemSprite; // 아이템 이미지
    public Vector3 initialPosition;

    bool useItem = false;

    void Start()
    {
        //아이템 초기 위치 설정
        initialPosition = transform.position; 

        // Rigidbody2D 컴포넌트를 가져오기
        rb = GetComponent<Rigidbody2D>();

        // Rigidbody 2D가 있다면 처음에는 중력 비활성화
        if( rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            useItem = true;
        }

        //useItem이 true일 때 동작
        if (useItem)
        {
            //Rigidbody2D의 물리적 동작 활성화
            if(rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic; //중력 활성화
            }

            useItem = false; //한 번만 실행되도록 플래그 초기화
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if(rb != null)
            {
                rb.bodyType= RigidbodyType2D.Static; //물리적 동작 비활성화
            }
        }
    }

    public void Use()
    {
        GameManager.Instance.player.SelfDie();
    }
}
