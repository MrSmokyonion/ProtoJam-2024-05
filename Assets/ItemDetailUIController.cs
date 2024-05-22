using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailUIController : MonoBehaviour
{
    [SerializeField] RectTransform ui_ItemDetailContainer;

    private float screenRatio;
    private Vector2 screenSize;
    private bool isOn;

    private void Start()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        screenRatio = 1280 / screenSize.x;
    }

    private void Update()
    {
        if(isOn)
        {
            FollowMouse();
        }
    }

    public void StartFollowMouse()
    {
        isOn = true;
        ui_ItemDetailContainer.gameObject.SetActive(true);
    }
    public void EndFollowMouse()
    {
        isOn = false;
        ui_ItemDetailContainer.gameObject.SetActive(false);
    }

    public void FollowMouse()
    {
        Vector2 dest = Input.mousePosition;
        dest *= screenRatio;
        dest.y -= screenSize.y * screenRatio;
        ui_ItemDetailContainer.anchoredPosition = dest;
    }
}
