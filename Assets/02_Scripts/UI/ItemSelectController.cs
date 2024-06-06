using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static AttackSkillData;

public class ItemSelectController : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private TMP_Text ui_PlungerLevelText;
    [SerializeField] private TMP_Text ui_ManholeLevelText;
    [SerializeField] private TMP_Text ui_WrenchLevelText;
    [SerializeField] private TMP_Text ui_PlayerStatusValueText;

    Player player;

    private void Start()
    {
        OnInitialized();
    }

    public void OnInitialized()
    {
        player = GameManager.Ins.Player;
        player.onLevelChange += InitItemSelectUI;
    }

    public void InitItemSelectUI(int _playerLevel)
    {
        GameManager.Ins.PauseGame();

        int _level = 0;
        if(player.SkillInventory.TryGetValue(AttackSkillData.SkillType.Plunger, out _level))
        {
            ui_PlungerLevelText.text = "Lv " + _level.ToString();
        }
        else
        {
            ui_PlungerLevelText.text = "Lv 0";
        }


        if (player.SkillInventory.TryGetValue(AttackSkillData.SkillType.ManHole, out _level))
        {
            ui_ManholeLevelText.text = "Lv " + _level.ToString();
        }
        else
        {
            ui_ManholeLevelText.text = "Lv 0";
        }


        if (player.SkillInventory.TryGetValue(AttackSkillData.SkillType.Wrench, out _level))
        {
            ui_WrenchLevelText.text = "Lv " + _level.ToString();
        }
        else
        {
            ui_WrenchLevelText.text = "Lv 0";
        }



        ShowUI();
    }

    public void OnSelectItem(string _skillType)
    {
        AttackSkillData.SkillType _type;
        switch (_skillType)
        {
            case "Plunger": _type = AttackSkillData.SkillType.Plunger; break;
            case "Manhole": _type = AttackSkillData.SkillType.ManHole; break;
            case "Wrench" : _type = AttackSkillData.SkillType.Wrench; break;
            default:
                return;
        }

        int _level = 0;
        player.SkillInventory.TryGetValue(_type, out _level);
        if (_level >= 5) 
        {
            return;
        }

        GameManager.Ins.Player.AddSkill(_type);

        GameManager.Ins.ResumeGame();
        HideUI();
    }

    void SetPlayerState()
    {
        string log = $"";

        log += $"{player.MaxHp - player.extraMaxHp} + {player.extraMaxHp}\n";
        log += $"{player.AttackDamage}%\n";
        log += $"{player.extraPaymentRate}%\n";
        log += $"5초당 {player.Regenration}%\n";
        log += $"{player.moveSpeed}배\n";
        log += $"{player.SkillCoolTimeRate}%\n";

        ui_PlayerStatusValueText.text = log;
    }

    private void ShowUI()
    {
        SetPlayerState();
        container.SetActive(true);
    }

    private void HideUI()
    {
        container.SetActive(false);
    }
}