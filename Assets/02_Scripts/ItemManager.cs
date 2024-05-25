using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템 데이터를 가지는 싱글톤 매니저
public class ItemManager : Singleton<ItemManager>
{
    /*
    #region Singleton
    public static ItemManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    */

    [SerializeField]
    private ItemInfo[] itemInfoData;
    public ItemInfo[] ItemInfoDatas => itemInfoData;

    [SerializeField]
    private int money;
    public int Money => money;

    private void Start()
    {
        InitItemInfo();
    }

    private void InitItemInfo()
    {
        money = DataEditor.LoadMoney();
        for(int i = 0; i < itemInfoData.Length; i++)
        {
            ItemInfo _target = itemInfoData[i];
            _target.CurrentUpgradeLevel = DataEditor.LoadItemCurrentUpgrade(_target.Type);
        }
    }

    public ItemInfo GetItemData(ItemType _type)
    {
        ItemInfo _itemInfo = null;

        switch (_type)
        {
            case ItemType.Health:
                _itemInfo = ItemInfoDatas[0];
                break;
            case ItemType.Damage:
                _itemInfo = ItemInfoDatas[1];
                break;
            case ItemType.ExperienceRate:
                _itemInfo = ItemInfoDatas[2];
                break;
            case ItemType.Regeneration:
                _itemInfo = ItemInfoDatas[3];
                break;
            case ItemType.Movement:
                _itemInfo = ItemInfoDatas[4];
                break;
            case ItemType.CoolTime:
                _itemInfo = ItemInfoDatas[5];
                break;
            default:
                Debug.LogWarning("ItemManager : Unavailable ItemType");
                break;
        }
        return _itemInfo;
    }

    public bool UpgradeItem(ItemType _targetItem, out ItemInfo _item)
    {
        _item = GetItemData(_targetItem);
        int cost = _item.UpgradeCost[_item.CurrentUpgradeLevel];

        if (money < cost)
        {
            return false;   //돈 부족
        }

        money -= cost;
        _item.CurrentUpgradeLevel++;
        DataEditor.SaveItemCurrentUpgrade(_targetItem, _item.CurrentUpgradeLevel);
        DataEditor.SaveMoney(money);
        return true;
    }
}
