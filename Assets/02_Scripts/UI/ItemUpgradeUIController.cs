using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemUpgradeUIController : MonoBehaviour
{
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] ItemDescriptionUI itemDescriptionUI;
    [SerializeField] TMP_Text ui_CurrentMoneyText;

    public ItemSlotUIController[] itemSlots;

    private void Awake()
    {
        InitStoreUI();
    }

    public void InitStoreUI()
    {
        RefreshCoinUI();
        InitItemSlotActive(ItemManager.Ins.ItemInfoDatas);
    }

    public void RefreshCoinUI()
    {
        ui_CurrentMoneyText.text = "Coin:" + ItemManager.Ins.Money.ToString();
    }

    public void InitItemSlotActive(ItemInfo[] _itemInfos)
    {
        itemSlots = new ItemSlotUIController[_itemInfos.Length];
        for (int i = 0; i < _itemInfos.Length; i++)
        {
            GameObject _gameObject = Instantiate(itemSlotPrefab, transform);
            ItemSlotUIController controller = _gameObject.GetComponent<ItemSlotUIController>();
            controller.InitSlotUI(_itemInfos[i]);
            if (i == 0) PrintItemInfo(_itemInfos[0].Type, controller);

            itemSlots[i] = controller;
        }
    }

    public void PrintItemInfo(ItemType _type, ItemSlotUIController _caller)
    {
        ItemInfo itemInfo = ItemManager.Ins.GetItemData(_type);
        itemDescriptionUI.PrintItemInfo(itemInfo, _caller);
    }
}
