using UnityEngine;

public class Item : MonoBehaviour
{
    Rigidbody2D rb;

    public string itemName; // ������ �̸�
    public Sprite itemSprite; // ������ �̹���
    public Vector3 initialPosition;

    bool useItem = false;

    void Start()
    {
        //������ �ʱ� ��ġ ����
        initialPosition = transform.position; 

        // Rigidbody2D ������Ʈ�� ��������
        rb = GetComponent<Rigidbody2D>();

        // Rigidbody 2D�� �ִٸ� ó������ �߷� ��Ȱ��ȭ
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

        //useItem�� true�� �� ����
        if (useItem)
        {
            //Rigidbody2D�� ������ ���� Ȱ��ȭ
            if(rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic; //�߷� Ȱ��ȭ
            }

            useItem = false; //�� ���� ����ǵ��� �÷��� �ʱ�ȭ
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if(rb != null)
            {
                rb.bodyType= RigidbodyType2D.Static; //������ ���� ��Ȱ��ȭ
            }
        }
    }

    public void Use()
    {
        GameManager.Instance.player.SelfDie();
    }
}
