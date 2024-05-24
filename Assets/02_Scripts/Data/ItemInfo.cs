using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "00_ItemType", menuName = "Scriptable Object/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    [SerializeField]
    private ItemType type;
    public ItemType Type => type;

    [SerializeField]
    protected string itemName;
    public string Name => itemName;

    [SerializeField]
    protected string description;
    public string Description => description;

    [SerializeField]
    protected int maxUpgradeLevel;
    public int MaxUpgradeLevel => maxUpgradeLevel;

    [SerializeField]
    protected int currentUpgradeLevel;
    public int CurrentUpgradeLevel
    {
        get { return currentUpgradeLevel; }
        set
        {
            if (value > maxUpgradeLevel)
            {
                currentUpgradeLevel = maxUpgradeLevel;
            }
            else if (value < 0)
            {
                currentUpgradeLevel = 0;
            }
        }
    }

    [SerializeField]
    protected int[] upgradeCost;
    public int[] UpgradeCost => upgradeCost;
}


public enum ItemType
{
    // 여기서 부터 스킬
    Plunger = 0,
    Manhole,
    Wrench,
    CoffeeCan,
    TurtleShell,
    TrafficCone,
    // 여기서부터 스텟
    Health,
    Damage,
    Experience,
    Regeneration,
    Movement,
    CoolTime

}