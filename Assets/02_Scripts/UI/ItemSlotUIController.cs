using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUIController : MonoBehaviour
{
    //해당 UI가 가리키는 아이템 데이터
    [SerializeField] ItemType itemType;

    //UI 참조 데이터
    [SerializeField] Image ui_itemImage;
    [SerializeField] TMP_Text ui_currentLevelText;



    //슬롯 초기화
    public void InitSlotUI(ItemInfo _itemInfo)
    {
        itemType = _itemInfo.Type;
        //ui_itemImage.sprite = ;
        ui_currentLevelText.text = "Lv" + _itemInfo.CurrentUpgradeLevel.ToString();
    }

    //슬롯 업데이트
    public void UpdateSlotUI(int _CurrentUpgradeLevel)
    {
        ui_currentLevelText.text = "Lv" + _CurrentUpgradeLevel.ToString();
    }

    //슬롯 클릭했을때 작동

    public void OnClickSlot()
    {
        //TODO : 아이템 강조 효과?
        UpdateItemDescriptionUI();
    }

    public void UpdateItemDescriptionUI()
    {
        transform.parent.GetComponent<ItemUpgradeUIController>().PrintItemInfo(itemType);
    }

    //슬롯 포커싱 해제됬을 때 작동
    public void OnExitFocusSlotButton()
    {

    }
}
