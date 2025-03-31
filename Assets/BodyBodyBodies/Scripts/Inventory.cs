using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;  // Inventory가 표시될 패널
    public GameObject itemSlotPrefab;  // 아이템 슬롯 프리팹 (Canvas에 미리 설정)

    public void AddItem(Item item)
    {
        // Inventory 패널에 새로운 슬롯 생성
        GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryPanel.transform);

        // 슬롯에 아이템 이미지 및 이름 설정
        Image itemImage = itemSlot.transform.Find("ItemImage").GetComponent<Image>();
        itemImage.sprite = item.itemSprite;
    }
}
