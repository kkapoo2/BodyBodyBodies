using UnityEngine;

public class AutoBlock : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;

    public float times = 0.0f;
    public float weight = 0.0f;

    public bool isCanMove = false;

    float perDX;
    float perDY;
    Vector3 defPos;
    bool isReverse = false;
    void Start()
    {
        defPos = transform.position;    // 초기 위치 저장
        float timestep = Time.fixedDeltaTime;
        perDX = moveX / (1.0f / timestep * times); // 이동 속도 설정
        perDY = moveY / (1.0f / timestep * times);

    }

    private void FixedUpdate()
    {

        if (isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;

            if (isReverse)
            {
                if ((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                {
                    endX = true;
                }
                if ((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f && y >= defPos.y))
                {
                    endY = true;
                }
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z)); //블럭 이동
            }
            else
            {
                if ((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x <= defPos.x + moveX))
                {
                    endX = true;
                }
                if ((perDY >= 0.0f && y >= defPos.y + moveY) || (perDY < 0.0f && y <= defPos.y + moveY))
                {
                    endY = true;
                }
                Vector3 v = new Vector3(perDX, perDY, defPos.z);
                transform.Translate(v);
            }

            if (endX && endY)
            {
                if (isReverse)
                {
                    transform.position = defPos;
                }
                isReverse = !isReverse;
                isCanMove = false;
            }
        }

    }

    public void Move()
    {
        isCanMove = true;
    }

    public void Stop()
    {
        isCanMove = false;
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
