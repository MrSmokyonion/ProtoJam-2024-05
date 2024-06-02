using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillCoolDownController : MonoBehaviour
{
    public CoolDownSlider slider_Plunger;
    public CoolDownSlider slider_Wrench;
    public CoolDownSlider slider_Manhole;

    private void Start()
    {
        slider_Plunger.gameObject.SetActive(false);
        slider_Wrench.gameObject.SetActive(false);
        slider_Manhole.gameObject.SetActive(false);
    }

    public void SetSkillCoolDownUI(AttackSkillData.SkillType _skillType, float _maxTime)
    {
        ShowUI(_skillType);
        InitUI(_skillType, _maxTime);
    }

    private void ShowUI(AttackSkillData.SkillType _skillType)
    {
        switch (_skillType)
        {
            case AttackSkillData.SkillType.Plunger:
                slider_Plunger.gameObject.SetActive(true);
                break;
            case AttackSkillData.SkillType.Wrench:
                slider_Wrench.gameObject.SetActive(true);
                break;
            case AttackSkillData.SkillType.ManHole:
                slider_Manhole.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void InitUI(AttackSkillData.SkillType _skillType, float _maxTime)
    {
        switch (_skillType)
        {
            case AttackSkillData.SkillType.Plunger:
                slider_Plunger.Initialize(_maxTime);
                break;
            case AttackSkillData.SkillType.Wrench:
                slider_Wrench.Initialize(_maxTime);
                break;
            case AttackSkillData.SkillType.ManHole:
                slider_Manhole.Initialize(_maxTime);
                break;
            default:
                break;
        }
    }
}
