using UnityEngine;

public class AutoBlock : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float moveSpeed = 2.0f;

    Vector3 defPos;     // �⺻ ��ġ
    Vector3 targetPos;  // ��ǥ ��ġ

    public bool isCanMove = false;  // ����� ������ �� �ִ� �������� Ȯ��
    public bool isMoving = false;   // ���� ����� �̵� ������ Ȯ��
    bool isGoingUp = false;         // ���� ����� �ö󰡴� ������ Ȯ��

    
    void Start()
    {
        defPos = transform.position;    // �ʱ� ��ġ ����
        targetPos = defPos + new Vector3(moveX, moveY, 0);  //��ǥ ��ġ ����
    }

    private void FixedUpdate()
    {
        if (isCanMove && !isMoving)
        {
            StartCoroutine(MoveBlock());
        }

    }

    System.Collections.IEnumerator MoveBlock()
    {
        isMoving = true;

        while (isCanMove)
        {
            Vector3 destination = isGoingUp ? targetPos : defPos;   // �ö� ���� ������ �� ������ ����

            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                if(!isCanMove)
                    yield break;

                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }

            if (!isCanMove)
                yield break;

            // ���� ��ȯ
            isGoingUp = !isGoingUp;

            yield return new WaitForSeconds(0.5f);
        }

        isMoving = false;
        
    }

    public void SetCanMove(bool canMove)
    {
        isCanMove = canMove;

        if (!canMove)
        {
            isMoving = false;   //�̵��� ���� �� �̵� �� ���µ� ����
            StopAllCoroutines();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
