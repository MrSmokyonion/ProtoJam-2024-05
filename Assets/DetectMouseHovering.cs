using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectMouseHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemDetailUIController Controller;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Controller.StartFollowMouse();
        Debug.Log("start");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Controller.EndFollowMouse();
        Debug.Log("end");

    }
}
