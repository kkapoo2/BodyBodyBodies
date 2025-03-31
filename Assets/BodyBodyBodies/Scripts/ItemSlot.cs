using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item; //���Կ� ��� ������
    public bool isUsed = false; //������ ��� ����
    public Image itemImage; //������ �̹��� ǥ��

    //������ ���� ���� �޼ҵ�
    public void SelfItem(Item newItem)
    {
        item = newItem;
        isUsed = false;

        //�̹��� ����
        itemImage.sprite = newItem.itemSprite;
        itemImage.enabled = true;
    }

    //������ ��� �޼ҵ�
    public void UseItem()
    {
        if (item != null && !isUsed)
        {
            isUsed = true;
            // ������ ��� ȿ�� ó��(�߰� ����)
        }
    }

}
