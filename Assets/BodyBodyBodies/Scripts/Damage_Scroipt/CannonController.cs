using UnityEngine;

public class CannonController : MonoBehaviour
{

    public GameObject objPrefab;        // ȭ�� Prefab ������
    public float delayTime = 3.0f;      // ���� �ð�
    public float fireSpeedX = -4.0f;    // �߻� ���� X
    public float fireSpeedY = 0.0f;     // �߻� ���� Y
    public float length = 8.0f;

    GameObject player;                  // �÷��̾�
    GameObject gateObj;                 // �߻籸
    bool isFiring = false;               // �߻� ���� ����


    void Start()
    {
        // ������ �߻� ��ġ(Gate) ã��
        Transform tr = transform.Find("Gate");
        if (tr != null)
        {
            gateObj = tr.gameObject;
        }

        //�÷��̾� ã��
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (player == null || gateObj == null || objPrefab == null) return;

        // �÷��̾ ���� ���� �ȿ� ������ �߻� ���
        bool canFire = CheckLength(player.transform.position);

        if (canFire && !isFiring)
        {
            isFiring = true;
            InvokeRepeating(nameof(FireArrow), 0f, delayTime); // ȭ�� ��� �߻�
        }
        else if (!canFire && isFiring)
        {
            isFiring = false;
            CancelInvoke(nameof(FireArrow)); // ���� ������ ������ ����
        }
    }

    void FireArrow()
    {
        // ȭ�� ���� ��ġ
        Vector3 spawnPos = gateObj.transform.position;
        GameObject arrow = Instantiate(objPrefab, spawnPos, Quaternion.identity);

        // Rigidbody2D�� �ִ��� Ȯ�� �� �߻�
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
    
    // �Ÿ� Ȯ��
    bool CheckLength(Vector2 targetPos)
    {
        return Vector2.Distance(transform.position, targetPos) <= length;
    }
}
