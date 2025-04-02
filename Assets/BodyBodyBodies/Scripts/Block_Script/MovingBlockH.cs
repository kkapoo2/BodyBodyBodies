using System.Collections;
using UnityEngine;
/* ����ġ�� ������ �⺻ ��ġ���� ��ǥ ��ġ������ ������
   Player�� ����ġ���� ����� �⺻��ġ�� ���ư� */
public class MovingBlockH : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float moveSpeed = 2.0f;

    Vector3 defPos;
    Vector3 targetPos;

    bool isMoving = false;   // ����� ���� �̵� ������ ����
    bool isGoingUp = false;  // true�� ��ǥ ��ġ�� �̵�, false�� ���� ��ġ�� �̵�

    void Start()
    {
        defPos = transform.position;
        targetPos = defPos + new Vector3(moveX, moveY, 0);
    }

    private void FixedUpdate()
    {
        if(isGoingUp && !isMoving)
        {
            StopAllCoroutines();
            StartCoroutine(MoveBlock(targetPos));   // ��ǥ ��ġ�� �̵�
        }
        else if (!isGoingUp && !isMoving)
        {
            StopAllCoroutines();
            StartCoroutine(MoveBlock(defPos));  // ���� ��ġ�� �̵�
        }

    }


    IEnumerator MoveBlock(Vector3 destination)
    {
        isMoving= true;

        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;

            // �̵� ���߿� ����ġ�� ������ ��� ���� ��ġ�� ����
            if (!isGoingUp && destination != defPos)
            {
                StartCoroutine(MoveBlock(defPos));
                yield break;
            }
        }

        transform.position = destination;
        isMoving = false;
    }

    public void SetCanMove(bool canMove)
    {
        isGoingUp = canMove;

        // �̵� ���߿� ����ġ�� ������ ��� ���� ��ġ�� ����
        if (!canMove && isMoving)
        {
            StopAllCoroutines();
            StartCoroutine(MoveBlock(defPos));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
        else if (collision.CompareTag("Switch"))
        {
            collision.transform.SetParent(transform);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Switch") || collision.CompareTag("Corpse"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }
    }
}
