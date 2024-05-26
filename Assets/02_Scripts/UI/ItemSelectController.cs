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
        switch (_skillType)
        {
            case "Plunger": GameManager.Ins.Player.AddSkill(AttackSkillData.SkillType.Plunger); break;
            case "Manhole": GameManager.Ins.Player.AddSkill(AttackSkillData.SkillType.ManHole); break;
            case "Wrench" : GameManager.Ins.Player.AddSkill(AttackSkillData.SkillType.Wrench); break;
            default:
                break;
        }

        GameManager.Ins.ResumeGame();
        HideUI();
    }

    private void ShowUI()
    {
        container.SetActive(true);
    }

    private void HideUI()
    {
        container.SetActive(false);
    }
}