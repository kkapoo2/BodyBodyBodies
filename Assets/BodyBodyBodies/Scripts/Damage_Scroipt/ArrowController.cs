using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 3.0f; //������ �ð� ����

    void Start()
    {
        Destroy(gameObject, deleteTime); //���� ����
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //���𰡿� �����ϸ� ����
    }
}
