using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionUI : MonoBehaviour
{
    [SerializeField] ItemType targetItemType;

    [SerializeField] private TMP_Text ui_nameText;
    [SerializeField] private TMP_Text ui_descriptionText;
    [SerializeField] private TMP_Text ui_maxLevelText;
    [SerializeField] private TMP_Text ui_costText;

    public void PrintItemInfo(ItemInfo _info, ItemSlotUIController _caller = null)
    {
        ui_nameText.text = _info.Name;
        ui_descriptionText.text = _info.Description + "\n" + "Current Level : " + _info.CurrentUpgradeLevel.ToString();
        ui_maxLevelText.text = "MaxLv : " + _info.MaxUpgradeLevel.ToString();
        if (_info.CurrentUpgradeLevel >= 5)
        {
            ui_costText.text = "MAX";
        }
        else
        {
            ui_costText.text = _info.UpgradeCost[_info.CurrentUpgradeLevel].ToString();
        }
        
        if( _caller != null )   //진짜 이렇게 코드짜면 안되는데..
        {
            _caller.UpdateSlotUI(_info.CurrentUpgradeLevel);
        }

        targetItemType = _info.Type;
        //SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_UI_BUTTON);
    }

    public void OnUpgradeButtonClicked()
    {
        SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_UI_BUTTON);
        ItemInfo _info = null;
        bool result = ItemManager.Ins.UpgradeItem(targetItemType, out _info);
        if(result)
        {
            PrintItemInfo(_info);
        }
        else
        {
            return;
        }
    }
}
