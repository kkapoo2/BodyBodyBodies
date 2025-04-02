using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 2.0f; //제거할 시간 지정

    void Start()
    {
        Destroy(gameObject, deleteTime); //제거 설정
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnDamaged(transform.position); // 플레이어 사망 처리
            Destroy(gameObject); // 화살 제거
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Corpse"))
        {
            Destroy(gameObject); // 벽이나 적과 충돌하면 제거
        }
    }
}
