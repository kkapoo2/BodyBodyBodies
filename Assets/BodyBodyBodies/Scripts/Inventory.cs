using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;  // Inventory�� ǥ�õ� �г�
    public GameObject itemSlotPrefab;  // ������ ���� ������ (Canvas�� �̸� ����)

    public void AddItem(Item item)
    {
        // Inventory �гο� ���ο� ���� ����
        GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryPanel.transform);

        // ���Կ� ������ �̹��� �� �̸� ����
        Image itemImage = itemSlot.transform.Find("ItemImage").GetComponent<Image>();
        itemImage.sprite = item.itemSprite;
    }
}
