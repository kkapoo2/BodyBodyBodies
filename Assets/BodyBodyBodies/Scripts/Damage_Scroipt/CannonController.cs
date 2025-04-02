using UnityEngine;

public class CannonController : MonoBehaviour
{

    public GameObject objPrefab;        // 화살 Prefab 데이터
    public float delayTime = 3.0f;      // 지연 시간
    public float fireSpeedX = -4.0f;    // 발사 벡터 X
    public float fireSpeedY = 0.0f;     // 발사 벡터 Y
    public float length = 8.0f;

    GameObject player;                  // 플레이어
    GameObject gateObj;                 // 발사구
    bool isFiring = false;               // 발사 가능 여부


    void Start()
    {
        // 대포의 발사 위치(Gate) 찾기
        Transform tr = transform.Find("Gate");
        if (tr != null)
        {
            gateObj = tr.gameObject;
        }

        //플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (player == null || gateObj == null || objPrefab == null) return;

        // 플레이어가 감지 범위 안에 있으면 발사 허용
        bool canFire = CheckLength(player.transform.position);

        if (canFire && !isFiring)
        {
            isFiring = true;
            InvokeRepeating(nameof(FireArrow), 0f, delayTime); // 화살 계속 발사
        }
        else if (!canFire && isFiring)
        {
            isFiring = false;
            CancelInvoke(nameof(FireArrow)); // 범위 밖으로 나가면 멈춤
        }
    }

    void FireArrow()
    {
        // 화살 생성 위치
        Vector3 spawnPos = gateObj.transform.position;
        GameObject arrow = Instantiate(objPrefab, spawnPos, Quaternion.identity);

        // Rigidbody2D가 있는지 확인 후 발사
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(fireSpeedX, fireSpeedY);
        }

        GameObject arrowManager = GameObject.Find("Arrow Manager");
        if (arrowManager != null)
        {
            arrow.transform.SetParent(arrowManager.transform, true);
        }
    }
    
    // 거리 확인
    bool CheckLength(Vector2 targetPos)
    {
        return Vector2.Distance(transform.position, targetPos) <= length;
    }
}
