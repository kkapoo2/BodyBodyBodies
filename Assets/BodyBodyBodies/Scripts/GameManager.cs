using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public GameObject respawn;

    public int stageIndex;
    public GameObject[] Stages;

    private Door currentDoor;

    void Awake()
    {
        //플레이어 리스폰 위치로 이동
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        player.transform.position = respawn.transform.position;

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void NextStage()
    {
        DestroyAllCorpse();
        //Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

        }

        //스테이지 변경 후 초기화
        currentDoor = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerReposition();
        }
    }

    //시체 제거
    public void DestroyAllCorpse()
    {
        GameObject corpseManager = GameObject.Find("Corpse Manager"); // Corpse Manager 찾기
        if (corpseManager == null) return; // Corpse Manager가 없으면 그냥 리턴

        foreach (Transform corpse in corpseManager.transform) // Corpse Manager의 모든 자식 확인
        {
            Destroy(corpse.gameObject); // 자식 오브젝트만 삭제
        }
    }

    public void PlayerReposition()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        if (respawn != null)
        {
            player.transform.position = respawn.transform.position;
        }
        player.VelocityZero();

        

        Invoke("ResetJumpState", 0.1f);
    }

    void ResetJumpState()
    {
        player.anim.SetBool("isJumping", false);
    }

    public void UpdateInventoryUI()
    {
        // 기존 UI초기화 후, 인벤토리 아이템을 UI에 다시 표시
    }

    public void ResetItems()
    {
        GameObject currentStage = Stages[stageIndex]; // 현재 활성화된 스테이지
        Item[] items = currentStage.GetComponentsInChildren<Item>();

        foreach(Item item in items)
        {
            item.transform.position = item.initialPosition;
            item.gameObject.SetActive(true);
        }
    }

    public void SetCurrentDoor(Door door)
    {
        currentDoor = door;
    }

}
