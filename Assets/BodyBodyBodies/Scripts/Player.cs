using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float maxSpeed;
    public float jumpPower;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;


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
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.2f, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.3f)
                    anim.SetBool("isJumping", false);
            }
        }
    }

    //���Ƿ� ���(���ึ�ñ�)
    public void SelfDie()
    {
        //��ü ����
        Instantiate(selfCorpse, transform.position, Quaternion.identity); 

        //��ü�� CorpseManager�� �ڽ����� ����
        GameObject corpseManager = GameObject.Find("Corpse Manager");
        if (corpseManager != null)
        {
            selfCorpse.transform.parent = corpseManager.transform;
        }

        //�÷��̾� ������
        gameManager.PlayerReposition();
    }

    //��ֹ��� ���(����,ȭ��)
    public void ObstacleDie()
    {
        Instantiate(obstacleCorpse, transform.position, Quaternion.identity); //��ü ����
        //��ü�� CorpseManager�� �ڽ����� ����
        GameObject corpseManager = GameObject.Find("Corpse Manager");
        if (corpseManager != null)
        {
            obstacleCorpse.transform.parent = corpseManager.transform;
        }

        //�÷��̾� ������
        gameManager.PlayerReposition();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
        }
        else if (collision.gameObject.CompareTag("Corpse"))
        {
            anim.SetBool("isJumping", false);
        }
    }
}
