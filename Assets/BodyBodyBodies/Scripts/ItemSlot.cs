using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item; //슬롯에 담긴 아이템
    public bool isUsed = false; //아이템 사용 여부
    public Image itemImage; //아이템 이미지 표시

    //아이템 정보 설정 메소드
    public void SelfItem(Item newItem)
    {
        item = newItem;
        isUsed = false;

        //이미지 설정
        itemImage.sprite = newItem.itemSprite;
        itemImage.enabled = true;
    }

    //아이템 사용 메소드
    public void UseItem()
    {
        if (item != null && !isUsed)
        {
            isUsed = true;
            // 아이템 사용 효과 처리(추가 구현)
        }
    }

}
