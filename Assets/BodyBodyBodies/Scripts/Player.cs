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

    List<Item> inventory = new List<Item>(); //������ �κ��丮

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        //����
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

        //���� ��ȯ
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //�ȴ� �ִϸ��̼�
        if (Mathf.Abs(rigid.linearVelocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        //EŰ ������ ������ ���
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem(); //������ ���
        }

        //RŰ ������ �����
        if (Input.GetKeyDown(KeyCode.R))
        {
            inventory.Clear();
            GameManager.Instance.ResetItems();
            gameManager.PlayerReposition();
        }


    }

    void FixedUpdate()
    {
        //�¿� �̵�
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //�ִ� �ӵ�
        if (rigid.linearVelocity.x > maxSpeed) //Right Max Speed
        {
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        }
        else if (rigid.linearVelocity.x < maxSpeed * (-1)) //Left Max Speed
        {
            rigid.linearVelocity = new Vector2(maxSpeed * (-1), rigid.linearVelocity.y);
        }

        //������ ���� ���� ����
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

    //��ֹ� ����
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            OnDamaged(collision.transform.position);
        }
    }

    //���Ƿ� ���(���ึ�ñ�)
    public void SelfDie()
    {
        //��ü ����
        GameObject corpse = Instantiate(selfCorpse, transform.position, Quaternion.identity); 

        //��ü�� CorpseManager�� �ڽ����� ����
        GameObject corpseManager = GameObject.Find("Corpse Manager");
        if (corpseManager != null)
        {
            corpse.transform.SetParent(corpseManager.transform, true);
        }

        //����ȭ ����
        Invoke("OffDamaged", 0.5f);

        //1�� �� �÷��̾� ������
        Invoke("RespawnPlayer", 0.5f);
    }

    //��ֹ��� ���(����,ȭ��)
    public void ObstacleDie()
    {
        GameObject corpse = Instantiate(obstacleCorpse, transform.position, Quaternion.identity); //��ü ����
        
        //��ü�� CorpseManager�� �ڽ����� ����
        GameObject corpseManager = GameObject.Find("Corpse Manager");
        if (corpseManager != null)
        {
            corpse.transform.SetParent(corpseManager.transform, true);
        }

        //����ȭ ����
        Invoke("OffDamaged", 0.5f);

        //1�� �� �÷��̾� ������
        Invoke("RespawnPlayer", 0.5f);
    }

    
    // �÷��̾� ������ ���� �Լ�\
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

        //��ü ����
        ObstacleDie();

        

    }

    void OffDamaged()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    //������ ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            PickUpItem(collision.GetComponent<Item>());
        }
    }

    //�ʻ� �������� �κ��丮�� ����
    public void PickUpItem(Item newItem)
    {
        inventory.Add(newItem);
        GameManager.Instance.UpdateInventoryUI();
    }

    //������ ���
    void UseItem()
    {
        if (inventory.Count > 0)
        {
            Item itemToUse = inventory[0];
            itemToUse.Use();

            inventory.RemoveAt(0);
            Destroy(itemToUse.gameObject);

            Debug.Log($"{itemToUse.itemName} ���! ���� ����: {inventory.Count}");

        }
        else
        {
            Debug.Log("����� �������� �����ϴ�!");
        }
    }


    //�ӵ� 0 ����
    public void VelocityZero()
    {
        rigid.linearVelocity = Vector2.zero;
    }
}
