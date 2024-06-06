using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Canvas titleCanvas;
    public Canvas coinCanvas;

    public void CoinStoreUIOpen()
    {
        titleCanvas.gameObject.SetActive(false);
        coinCanvas.gameObject.SetActive(true);
        SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_UI_BUTTON);
    }

    public void TitleUIOpen()
    {
        coinCanvas.gameObject.SetActive(false);
        titleCanvas.gameObject.SetActive(true);
        SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_UI_BUTTON);
    }

    public void LoadIngame()
    {
        SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_UI_BUTTON);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
