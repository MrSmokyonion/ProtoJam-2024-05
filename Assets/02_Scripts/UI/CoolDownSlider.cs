using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownSlider : MonoBehaviour
{
    [SerializeField] Slider ui_Slider;

    private float maxTime;
    private bool pauseTrigger;

    private void Start()
    {
        ui_Slider = GetComponent<Slider>();
    }

    public void Initialize(float _maxValue)
    {
        ui_Slider.maxValue = maxTime = _maxValue;
        ui_Slider.value = 0;
        pauseTrigger = false;
        StartCoolDown();
    }

    Coroutine _co;
    public void StartCoolDown()
    {
        if(_co != null)
        {
            StopCoroutine(_co);
        }
        _co = StartCoroutine(CoolDownTimer());
    }

    public void PauseCoolDown()
    {
        pauseTrigger = true;
    }

    public void ResumeCoolDown()
    {
        pauseTrigger = false;
    }

    private void FinishCoolDown()
    {
        //TODO : 스킬 사용하는 코드 추가해야 함.
    }

    private IEnumerator CoolDownTimer()
    {
        float _timer = 0;

        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (pauseTrigger)
            {
                continue;
            }

            _timer += Time.deltaTime;
            ui_Slider.value = _timer;
            if( _timer > maxTime) 
            {
                FinishCoolDown();
                continue;   //쿨타임 UI가 알아서 작동하게 할 것인가, 아니면 다른곳에서 이 코루틴을 반복실행하게 하는 코드가 필요.
            }
        }
    }
}
