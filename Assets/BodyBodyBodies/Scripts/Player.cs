using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float maxSpeed;
    public float jumpPower;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public Animator anim;


    public GameObject selfCorpse;
    public GameObject obstacleCorpse;

    List<Item> inventory = new List<Item>(); //아이템 인벤토리

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        //점프
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);

        }
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.normalized.x * 0.5f, rigid.linearVelocity.y);
        }

        //방향 전환
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //걷는 애니메이션
        if (Mathf.Abs(rigid.linearVelocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        //E키 누르면 아이템 사용
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem(); //아이템 사용
        }

        //R키 누르면 재시작
        if (Input.GetKeyDown(KeyCode.R))
        {
            inventory.Clear();
            GameManager.Instance.ResetItems();
            gameManager.PlayerReposition();
        }


    }

    void FixedUpdate()
    {
        //좌우 이동
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //최대 속도
        if (rigid.linearVelocity.x > maxSpeed) //Right Max Speed
        {
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        }
        else if (rigid.linearVelocity.x < maxSpeed * (-1)) //Left Max Speed
        {
            rigid.linearVelocity = new Vector2(maxSpeed * (-1), rigid.linearVelocity.y);
        }

        //착지시 점프 상태 해제
        if (rigid.linearVelocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.2f, LayerMask.GetMask("Ground"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    anim.SetBool("isJumping", false);
            }
        }
    }

    //장애물 접촉
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            OnDamaged(collision.transform.position);
        }
    }

    //자의로 사망(물약마시기)
    public void SelfDie()
    {
        //시체 생성
        GameObject corpse = Instantiate(selfCorpse, transform.position, Quaternion.identity); 

        //시체를 CorpseManager의 자식으로 설정
        GameObject corpseManager = GameObject.Find("Corpse Manager");
        if (corpseManager != null)
        {
            corpse.transform.SetParent(corpseManager.transform, true);
        }

        //투명화 해제
        Invoke("OffDamaged", 0.5f);

        //1초 뒤 플레이어 리스폰
        Invoke("RespawnPlayer", 0.5f);
    }

    //장애물로 사망(가시,화살)
    public void ObstacleDie()
    {
        GameObject corpse = Instantiate(obstacleCorpse, transform.position, Quaternion.identity); //시체 생성
        
        //시체를 CorpseManager의 자식으로 설정
        GameObject corpseManager = GameObject.Find("Corpse Manager");
        if (corpseManager != null)
        {
            corpse.transform.SetParent(corpseManager.transform, true);
        }

        //투명화 해제
        Invoke("OffDamaged", 0.5f);

        //1초 뒤 플레이어 리스폰
        Invoke("RespawnPlayer", 0.5f);
    }

    
    // 플레이어 리스폰 실행 함수\
    void RespawnPlayer()
    {
        gameManager.PlayerReposition();
    }

    void OnDamaged(Vector2 targetPos)
    {
        // Change Layer (Imoratal Active)
        gameObject.layer = 11;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        //Animation
        anim.SetTrigger("doDamaged");

        //시체 생성
        ObstacleDie();

        

    }

    void OffDamaged()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    //아이템 감지
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            PickUpItem(collision.GetComponent<Item>());
        }
    }

    //맵상 아이템을 인벤토리에 저장
    public void PickUpItem(Item newItem)
    {
        inventory.Add(newItem);
        GameManager.Instance.UpdateInventoryUI();
    }

    //아이템 사용
    void UseItem()
    {
        if (inventory.Count > 0)
        {
            Item itemToUse = inventory[0];
            itemToUse.Use();

            inventory.RemoveAt(0);
            Destroy(itemToUse.gameObject);

            Debug.Log($"{itemToUse.itemName} 사용! 남은 개수: {inventory.Count}");

        }
        else
        {
            Debug.Log("사용할 아이템이 없습니다!");
        }
    }


    //속도 0 설정
    public void VelocityZero()
    {
        rigid.linearVelocity = Vector2.zero;
    }
}
