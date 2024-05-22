using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUIController : MonoBehaviour
{
    //�ش� UI�� ����Ű�� ������ ������
    [SerializeField] ItemType itemType;

    //UI ���� ������
    [SerializeField] Image ui_itemImage;
    [SerializeField] TMP_Text ui_currentLevelText;



    //���� �ʱ�ȭ
    public void InitSlotUI(ItemInfo _itemInfo)
    {
        itemType = _itemInfo.Type;
        //ui_itemImage.sprite = ;
        ui_currentLevelText.text = "Lv" + _itemInfo.CurrentUpgradeLevel.ToString();
    }

    //���� ������Ʈ
    public void UpdateSlotUI(int _CurrentUpgradeLevel)
    {
        ui_currentLevelText.text = "Lv" + _CurrentUpgradeLevel.ToString();
    }

    //���� Ŭ�������� �۵�

    public void OnClickSlot()
    {
        //TODO : ������ ���� ȿ��?
        UpdateItemDescriptionUI();
    }

    public void UpdateItemDescriptionUI()
    {
        transform.parent.GetComponent<ItemUpgradeUIController>().PrintItemInfo(itemType);
    }

    //���� ��Ŀ�� �������� �� �۵�
    public void OnExitFocusSlotButton()
    {

    }
}
