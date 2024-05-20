using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSlider : MonoBehaviour
{

    Slider slider;
    Player target;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = 1.0f;
    }
    
    void Start()
    {
        // 나중에 GameManager로 바꿈
        target = FindAnyObjectByType<Player>();
        target.onHealthChange += ChangeValue;
    }

    private void ChangeValue(float hp, float maxHp)
    {
        if(maxHp > 0)
        {
            slider.value = hp / maxHp;
        }
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
    }
}
