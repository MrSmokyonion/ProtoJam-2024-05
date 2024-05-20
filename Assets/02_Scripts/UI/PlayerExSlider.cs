using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExSlider : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 나중에 GameManager로 바꿈
        FindAnyObjectByType<Player>().onExChange += ChangeValue;
    }

    void ChangeValue(int current, int max)
    {
        if(max == 0)
        {
            slider.value = 0;
            return;
        }
        
        slider.value = (float)current / (float)max;

        
    }
}
