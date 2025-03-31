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

        //�������� ���� �� �ʱ�ȭ
        currentDoor = null;
    }

    //��ü ����
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
        // ���� UI�ʱ�ȭ ��, �κ��丮 �������� UI�� �ٽ� ǥ��
    }
    public void ResetItems()
    {
        GameObject currentStage = Stages[stageIndex]; // ���� Ȱ��ȭ�� ��������
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
