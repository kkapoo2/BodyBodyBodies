using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 2.0f; //������ �ð� ����

    void Start()
    {
        Destroy(gameObject, deleteTime); //���� ����
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnDamaged(transform.position); // �÷��̾� ��� ó��
            Destroy(gameObject); // ȭ�� ����
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Corpse"))
        {
            Destroy(gameObject); // ���̳� ���� �浹�ϸ� ����
        }
    }
}
