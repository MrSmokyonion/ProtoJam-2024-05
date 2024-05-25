using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExSlider : MonoBehaviour
{
    TextMeshProUGUI levelText;
    Slider slider;
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = 1.0f;

        levelText = GetComponentInChildren<TextMeshProUGUI>();
        levelText.text = $"Lv 1";
    }

    // Start is called before the first frame update
    void Start()
    {
        // 나중에 GameManager로 바꿈
        GameManager.Ins.Player.onExChange += ChangeExValue;
        GameManager.Ins.Player.onLevelChange += ChageLevelValue;

    }

    void ChangeExValue(float current, float max)
    {
        if(max == 0)
        {
            slider.value = 0;
            return;
        }
        
        slider.value = current / max;   
    }

    void ChageLevelValue(int level)
    {
        levelText.text = $"Lv {level}";
    }
}
