using UnityEngine;

public class AutoBlock : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float moveSpeed = 2.0f;

    Vector3 defPos;     // 기본 위치
    Vector3 targetPos;  // 목표 위치

    public bool isCanMove = false;  // 블록이 움직일 수 있는 상태인지 확인
    public bool isMoving = false;   // 현재 블록이 이동 중인지 확인
    bool isGoingUp = false;         // 현재 블록이 올라가는 중인지 확인

    
    void Start()
    {
        defPos = transform.position;    // 초기 위치 저장
        targetPos = defPos + new Vector3(moveX, moveY, 0);  //목표 위치 설정
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
            Vector3 destination = isGoingUp ? targetPos : defPos;   // 올라갈 때오 내려올 때 목적지 설정

            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                if(!isCanMove)
                    yield break;

                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }

            if (!isCanMove)
                yield break;

            // 방향 전환
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
            isMoving = false;   //이동을 멈출 때 이동 중 상태도 해제
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
