using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class DataEditor
{
    private const string key_item_Plunger = "Plunger";
    private const string key_item_Manhole = "Manhole";
    private const string key_item_Wrench = "Wrench";
    private const string key_item_CoffeeCan = "CoffeeCan";
    private const string key_item_TurtleShell = "TurtleShell";
    private const string key_item_TrafficCone = "TrafficCone";
    private const string key_item_Health = "Health";
    private const string key_item_Damage = "Damage";
    private const string key_item_ExperienceRate = "ExperienceRate";
    private const string key_item_Regeneration = "Regeneration";
    private const string key_item_Movement = "Movement";
    private const string key_item_CoolTime = "CoolTime";


    private const string key_CurrentUpgrade = "CurrentUpgrade";
    private const string key_Money = "Money";

    public static void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void SaveItemCurrentUpgrade(ItemType _type, int _curUpgrade)
    {
        string key_ItemName = GetItemName(_type);

        if (key_ItemName == null) { return; }

        PlayerPrefs.SetInt(key_ItemName + key_CurrentUpgrade, _curUpgrade);
        Debug.Log("DataEditor : " + key_ItemName + " Current Upgrade Value " + _curUpgrade.ToString() + " is Saved.");
    }

    public static int LoadItemCurrentUpgrade(ItemType _type)
    {
        string key_ItemName = GetItemName(_type);

        if (key_ItemName == null) { return -1; }

        if (!PlayerPrefs.HasKey(key_ItemName + key_CurrentUpgrade))
        {
            Debug.LogWarning("DataEditor : No Data exist");
            return 0;
        }

        int _curUpgreade = PlayerPrefs.GetInt(key_ItemName + key_CurrentUpgrade);
        return _curUpgreade;
    }

    private static string GetItemName(ItemType _type)
    {
        string itemName;

        switch (_type)
        {
            case ItemType.Health:
                itemName = key_item_Health;
                break;
            case ItemType.Damage:
                itemName = key_item_Damage;
                break;
            case ItemType.ExperienceRate:
                itemName = key_item_ExperienceRate;
                break;
            case ItemType.Regeneration:
                itemName = key_item_Regeneration;
                break;
            case ItemType.Movement:
                itemName = key_item_Movement;
                break;
            case ItemType.CoolTime:
                itemName = key_item_CoolTime;
                break;
            default:
                Debug.LogWarning("DataEditor : Unavailbable ItemType.");
                return null;
        }

        return itemName;
    }

    public static void SaveMoney(int _value)
    {
        //int _currentMoney = LoadMoney();

        PlayerPrefs.SetInt(key_Money, _value);
        Debug.Log($"DataEditor : {key_Money} is Saved. +{_value}");
    }
    
    public static int LoadMoney()
    {
        if (!PlayerPrefs.HasKey(key_Money))
        {
            Debug.LogWarning("DataEditor : No Data exist");
            return 0;
        }

        int _money = PlayerPrefs.GetInt(key_Money);
        return _money;
    }
}
