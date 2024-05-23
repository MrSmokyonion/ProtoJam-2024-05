using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �����͸� ������ �̱��� �Ŵ���
public class ItemManager : MonoBehaviour
{
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
            case ItemType.Plunger:
                _itemInfo = ItemInfoDatas[0];
                break;
            case ItemType.Manhole:
                _itemInfo = ItemInfoDatas[1];
                break;
            case ItemType.Wrench:
                _itemInfo = ItemInfoDatas[2];
                break;
            case ItemType.CoffeeCan:
                _itemInfo = ItemInfoDatas[3];
                break;
            case ItemType.TurtleShell:
                _itemInfo = ItemInfoDatas[4];
                break;
            case ItemType.TrafficCone:
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
            return false;   //�� ����
        }

        money -= cost;
        _item.CurrentUpgradeLevel++;
        DataEditor.SaveItemCurrentUpgrade(_targetItem, _item.CurrentUpgradeLevel);
        DataEditor.SaveMoney(money);
        return true;
    }
}