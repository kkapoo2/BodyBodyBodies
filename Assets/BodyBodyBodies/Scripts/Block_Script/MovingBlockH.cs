using UnityEngine;

public class MovingBlockH : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float times = 0.0f;
    public float weight = 0.0f;
    public bool isMoveWhenOn = false;

    private ButtonPushed buttonPushed;
    public GameObject Button;

    public bool isCanMove = true;
    float perDX;
    float perDY;
    Vector3 defPos;
    bool isReverse = false;
    void Start()
    {
        if (Button != null)
        {
            buttonPushed = Button.GetComponent<ButtonPushed>();
        }

        defPos = transform.position;
        float timestep = Time.fixedDeltaTime;
        perDX = moveX / (1.0f / timestep * times);
        perDY = moveY / (1.0f / timestep * times);

        if (isMoveWhenOn)
            isCanMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPushed != null)
        {
            //버튼이 활성화되었는지 확인
            if (!buttonPushed.Button.activeSelf)
            {
                Debug.Log("버튼이 눌렸습니다!");
            }
        }

    }

    private void FixedUpdate()
    {
        if (buttonPushed != null && !buttonPushed.Button.activeSelf)
        {
            isCanMove = true; // 버튼이 눌렸다면 움직임을 활성화
        }
        else
        {
            isCanMove = false;
        }


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
                if (isMoveWhenOn == false)
                {
                    Invoke("Move", weight);
                }
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
            if (isMoveWhenOn)
            {
                isCanMove = true;
            }
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
