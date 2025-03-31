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
            // 버튼이 활성화되었는지 확인
            if (!buttonPushed.Button.activeSelf)
            {
                Debug.Log("버튼이 눌렸습니다!");
            }
        }
        // 버튼  확인
        if (buttonPushed != null && !buttonPushed.Button.activeSelf)
        {
            moveToTarget = true; // 목표 위치로 이동을 시작하도록 플래그 설정
        }
        else
        {
            backToTarget = true;
        }

        // 목표 위치로 이동 처리
        if (moveToTarget)
        {
            Vector2 targetPosition = target1.TransformPoint(new Vector2(0, 0));

            // 부드럽게 이동
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // 목표 위치에 도달했으면 이동 중지
            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                moveToTarget = false; // 이동 완료 후 플래그 해제
                transform.position = targetPosition; // 고정
            }
        }

        // 돌아가기
        if (backToTarget)
        {
            Vector2 targetPosition = target2.TransformPoint(new Vector2(0, 0));

            // 부드럽게 이동
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // 목표 위치에 도달했으면 이동 중지
            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                backToTarget = false; // 이동 완료 후 플래그 해제
                transform.position = targetPosition; // 고정
            }
        }
    }
}
