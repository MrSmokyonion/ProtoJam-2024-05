using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    public static TextSpawner instance;
    public static TextSpawner Ins
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<TextSpawner>();
            }

            return instance;
        }
    }

    public TextObject text;

    public void GetText(Vector3 position, string textValue)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        GameObject temp = Instantiate(text.gameObject, transform);
        temp.transform.position = screenPos;
        temp.GetComponent<TextObject>().SetText(textValue);
        temp.SetActive(true);
    }
}
