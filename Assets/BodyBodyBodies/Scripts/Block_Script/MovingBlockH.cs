using System.Collections;
using UnityEngine;
/* 스위치를 누르면 기본 위치에서 목표 위치까지만 움직임
   Player가 스위치에서 벗어나면 기본위치로 돌아감 */
public class MovingBlockH : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float moveSpeed = 2.0f;

    Vector3 defPos;
    Vector3 targetPos;

    bool isMoving = false;   // 블록이 현재 이동 중인지 여부
    bool isGoingUp = false;  // true면 목표 위치로 이동, false면 원래 위치로 이동

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
            StartCoroutine(MoveBlock(targetPos));   // 목표 위치로 이동
        }
        else if (!isGoingUp && !isMoving)
        {
            StopAllCoroutines();
            StartCoroutine(MoveBlock(defPos));  // 원래 위치로 이동
        }

    }


    IEnumerator MoveBlock(Vector3 destination)
    {
        isMoving= true;

        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;

            // 이동 도중에 스위치가 꺼지면 즉시 원래 위치로 복귀
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

        // 이동 도중에 스위치가 꺼지면 즉시 원래 위치로 복귀
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
