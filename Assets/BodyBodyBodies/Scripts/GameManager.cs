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
        //�÷��̾� ������ ��ġ�� �̵�
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

        //�������� ���� �� �ʱ�ȭ
        currentDoor = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerReposition();
        }
    }

    //��ü ����
    public void DestroyAllCorpse()
    {
        GameObject corpseManager = GameObject.Find("Corpse Manager"); // Corpse Manager ã��
        if (corpseManager == null) return; // Corpse Manager�� ������ �׳� ����

        foreach (Transform corpse in corpseManager.transform) // Corpse Manager�� ��� �ڽ� Ȯ��
        {
            Destroy(corpse.gameObject); // �ڽ� ������Ʈ�� ����
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
