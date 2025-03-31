using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public float smoothTime = 0.3F;
    private Vector2 velocity = Vector2.zero;
    private bool moveToTarget = false;
    private bool backToTarget = false;

    private ButtonPushed buttonPushed;
    public GameObject Button;


    void Start()
    {
        if (Button != null)
        {
            buttonPushed = Button.GetComponent<ButtonPushed>();
        }
    }
    void Update()
    {

        if (buttonPushed != null)
        {
            // ��ư�� Ȱ��ȭ�Ǿ����� Ȯ��
            if (!buttonPushed.Button.activeSelf)
            {
                Debug.Log("��ư�� ���Ƚ��ϴ�!");
            }
        }
        // ��ư  Ȯ��
        if (buttonPushed != null && !buttonPushed.Button.activeSelf)
        {
            moveToTarget = true; // ��ǥ ��ġ�� �̵��� �����ϵ��� �÷��� ����
        }
        else
        {
            backToTarget = true;
        }

        // ��ǥ ��ġ�� �̵� ó��
        if (moveToTarget)
        {
            Vector2 targetPosition = target1.TransformPoint(new Vector2(0, 0));

            // �ε巴�� �̵�
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // ��ǥ ��ġ�� ���������� �̵� ����
            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                moveToTarget = false; // �̵� �Ϸ� �� �÷��� ����
                transform.position = targetPosition; // ����
            }
        }

        // ���ư���
        if (backToTarget)
        {
            Vector2 targetPosition = target2.TransformPoint(new Vector2(0, 0));

            // �ε巴�� �̵�
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // ��ǥ ��ġ�� ���������� �̵� ����
            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                backToTarget = false; // �̵� �Ϸ� �� �÷��� ����
                transform.position = targetPosition; // ����
            }
        }
    }
}
