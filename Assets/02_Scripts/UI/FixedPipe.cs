using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FixedPipe : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = $"Fixed Pipe : 0";
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Ins.OnFixedPipeCount += ChangeValue;
    }

    void ChangeValue(int count)
    {
        text.text = $"Fixed Pipe : {count}";
    }
}
