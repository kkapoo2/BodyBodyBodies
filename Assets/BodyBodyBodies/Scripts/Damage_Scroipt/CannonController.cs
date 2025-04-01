using UnityEngine;

public class CannonController : MonoBehaviour
{

    public GameObject objPrefab;        // �߻���Ű�� Prefab ������
    public float delayTime = 3.0f;      // ���� �ð�
    public float fireSpeedX = -4.0f;    // �߻� ���� X
    public float fireSpeedY = 0.0f;     // �߻� ���� Y
    public float length = 8.0f;

    GameObject player;                  // �÷��̾�
    GameObject gateObj;                 // �߻籸
    float passedTimes = 0;              // ��� �ð�
    void Start()
    {
        // �߻籸 ������Ʈ ���
        Transform tr = transform.Find("Gate");
        gateObj = tr.gameObject;

        //�÷��̾�
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // �߻� �ð� ����
        passedTimes += Time.deltaTime;
        // �Ÿ� Ȯ��
        if (CheckLength(player.transform.position))
        {
            if(passedTimes > delayTime)
            {
                // �߻�!!
                passedTimes = 0;
                Vector3 pos = new Vector3(gateObj.transform.position.x + 10, 
                                          gateObj.transform.position.y, 
                                          transform.position.z);
                // Prefab ���� GameObject �����
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                // �߻� ����
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(fireSpeedX, fireSpeedY);
                rbody.AddForce(v, ForceMode2D.Impulse);

            }
        }
    }

    // �Ÿ� Ȯ��
    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }
}
