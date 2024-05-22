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

    public void PrintItemInfo(ItemInfo _info)
    {
        ui_nameText.text = _info.Name;
        ui_descriptionText.text = _info.Description + "\n" + "Current Level : " + _info.CurrentUpgradeLevel.ToString();
        ui_maxLevelText.text = "MaxLv : " + _info.MaxUpgradeLevel.ToString();
        ui_costText.text = _info.UpgradeCost[_info.CurrentUpgradeLevel].ToString();

        targetItemType = _info.Type;
    }

    public void OnUpgradeButtonClicked()
    {
        ItemInfo _info = null;
        bool result = ItemManager.Instance.UpgradeItem(targetItemType, out _info);
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
