using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;

    public int stageIndex;
    public GameObject[] Stages;

    private Door currentDoor;

    void Awake()
    {
        if(Instance == null)
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

    //시체 제거
    void DestroyAllCorpse()
    {
        GameObject[] corpses = GameObject.FindGameObjectsWithTag("Corpse");
        foreach (GameObject corpse in corpses)
        {
            Destroy(corpse);
        }
    }

    public void PlayerReposition()
    {
        GameObject spawnObj = GameObject.FindGameObjectWithTag("Respawn");
        if (spawnObj != null)
        {
            player.transform.position = spawnObj.transform.position;
        }
        player.VelocityZero();
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
