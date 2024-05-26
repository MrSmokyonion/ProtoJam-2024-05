using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemUpgradeUIController : MonoBehaviour
{
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] ItemDescriptionUI itemDescriptionUI;
    [SerializeField] TMP_Text ui_CurrentMoneyText;

    private void Start()
    {
        InitStoreUI();
    }

    public void InitStoreUI()
    {
        ui_CurrentMoneyText.text = "Coin:" + ItemManager.Ins.Money.ToString();
        InitItemSlotActive(ItemManager.Ins.ItemInfoDatas);
    }

    public void InitItemSlotActive(ItemInfo[] _itemInfos)
    {
        for(int i = 0; i < _itemInfos.Length; i++)
        {
            GameObject _gameObject = Instantiate(itemSlotPrefab, transform);
            ItemSlotUIController controller = _gameObject.GetComponent<ItemSlotUIController>();
            controller.InitSlotUI(_itemInfos[i]);
        }

        PrintItemInfo(_itemInfos[0].Type);
    }

    public void PrintItemInfo(ItemType _type)
    {
        ItemInfo itemInfo = ItemManager.Ins.GetItemData(_type);
        itemDescriptionUI.PrintItemInfo(itemInfo);
    }
}
