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
            Debug.Log("GameManager �ߺ� �߻� - ������");
            Destroy(gameObject);
            return;
        }
        ActivateCurrentStage();

        PlayerReposition();
    }

    void ActivateCurrentStage()
    {
        // ��� �������� ��Ȱ��ȭ
        for (int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(false);
        }

        // ���� �������� Ȱ��ȭ
        if (stageIndex >= 0 && stageIndex < Stages.Length)
        {
            Stages[stageIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("�߸��� stageIndex ��! �迭 ������ ���");
        }
    }


    public void NextStage()
    {
        DestroyAllCorpse();
        RemoveAllArrows();   // ���� ȭ�� ����
        DeactivateCannons(); // ���� ���� ��Ȱ��ȭ

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

        //�������� ���� �� �ʱ�ȭ
        currentDoor = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
            cannon.SetActive(false); // ��� ���� ��Ȱ��ȭ
        }
    }

    void RemoveAllArrows()
    {

        GameObject arrowManager = GameObject.Find("Arrow Manager");

        if (arrowManager == null) return; // �θ� ������Ʈ ������ �׳� ����

        foreach (Transform arrow in arrowManager.transform)
        {
            Destroy(arrow.gameObject); // �ڽ� ������Ʈ(ȭ��) ����
        }

    }

    //���ϴ� ���������� �̵�
    public void GoToStage(int index)
    {
        Debug.Log("GoToStage ����: " + index);
        DestroyAllCorpse();
        RemoveAllArrows();
        DeactivateCannons();

        // ���� �������� ��Ȱ��ȭ (ù ������ ���� �������� ����)
        if (stageIndex >= 0 && stageIndex < Stages.Length && stageIndex != index)
        {
            Stages[stageIndex].SetActive(false);
        }

        // ���ο� ���������� �̵�
        if (index >= 0 && index < Stages.Length)
        {
            stageIndex = index;
            Stages[stageIndex].SetActive(true); // ���ο� �������� Ȱ��ȭ
            PlayerReposition();
        }
        else
        {
            Debug.LogError("�߸��� �������� �ε���! ������ ���.");
        }

    }
}
