using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public Canvas titleCanvas;
    public Canvas coinCanvas;

    public void CoinStoreUIOpen()
    {
        titleCanvas.gameObject.SetActive(false);
        coinCanvas.gameObject.SetActive(true);
    }

    public void TitleUIOpen()
    {
        coinCanvas.gameObject.SetActive(false);
        titleCanvas.gameObject.SetActive(true);
    }
}