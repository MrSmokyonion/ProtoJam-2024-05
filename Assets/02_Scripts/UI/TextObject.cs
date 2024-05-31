using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextObject : MonoBehaviour
{
    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void LifeOver()
    {
        Destroy(this.gameObject);
    }

}
