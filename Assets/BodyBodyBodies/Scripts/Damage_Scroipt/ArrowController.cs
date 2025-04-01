using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 3.0f; //제거할 시간 지정

    void Start()
    {
        Destroy(gameObject, deleteTime); //제거 설정
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //무언가에 접촉하면 제거
    }
}
