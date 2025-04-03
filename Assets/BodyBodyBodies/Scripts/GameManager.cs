using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public GameObject respawn;
    public bool isStageComplete = false;

    public int stageIndex;
    public GameObject[] Stages;

    private Door currentDoor;

    void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

        }

        else
        {
            Debug.Log("GameManager 중복 발생 - 삭제됨");
            Destroy(gameObject);
            return;
        }
        ActivateCurrentStage();

        PlayerReposition();
    }

    void ActivateCurrentStage()
    {
        // 모든 스테이지 비활성화
        for (int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(false);
        }

        // 현재 스테이지 활성화
        if (stageIndex >= 0 && stageIndex < Stages.Length)
        {
            Stages[stageIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("잘못된 stageIndex 값! 배열 범위를 벗어남");
        }
    }


    public void NextStage()
    {
        DestroyAllCorpse();
        RemoveAllArrows();   // 기존 화살 제거
        DeactivateCannons(); // 기존 대포 비활성화

        if (stageIndex < Stages.Length)
        {
            Stages[stageIndex].SetActive(false);
        }

        //Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

        }

        //스테이지 변경 후 초기화
        currentDoor = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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

        foreach (Item item in items)
        {
            item.transform.position = item.initialPosition;
            item.gameObject.SetActive(true);
        }
    }

    public void SetCurrentDoor(Door door)
    {
        currentDoor = door;
    }


    void DeactivateCannons()
    {
        GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");
        foreach (GameObject cannon in cannons)
        {
            cannon.SetActive(false); // 모든 대포 비활성화
        }
    }

    void RemoveAllArrows()
    {

        GameObject arrowManager = GameObject.Find("Arrow Manager");

        if (arrowManager == null) return; // 부모 오브젝트 없으면 그냥 리턴

        foreach (Transform arrow in arrowManager.transform)
        {
            Destroy(arrow.gameObject); // 자식 오브젝트(화살) 제거
        }

    }

    //원하는 스테이지로 이동
    public void GoToStage(int index)
    {
        Debug.Log("GoToStage 실행: " + index);
        DestroyAllCorpse();
        RemoveAllArrows();
        DeactivateCannons();

        // 기존 스테이지 비활성화 (첫 시작일 때는 실행하지 않음)
        if (stageIndex >= 0 && stageIndex < Stages.Length && stageIndex != index)
        {
            Stages[stageIndex].SetActive(false);
        }

        // 새로운 스테이지로 이동
        if (index >= 0 && index < Stages.Length)
        {
            stageIndex = index;
            Stages[stageIndex].SetActive(true); // 새로운 스테이지 활성화
            PlayerReposition();
        }
        else
        {
            Debug.LogError("잘못된 스테이지 인덱스! 범위를 벗어남.");
        }

    }
}
