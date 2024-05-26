using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text ui_RepairPipeValue;
    [SerializeField] private TMP_Text ui_GetCoinValue;

    public void InitResultValueToText(int _repairPipe, int _targetPipe, int _getCoin)
    {
        ui_RepairPipeValue.text = _repairPipe.ToString() + '/' + _targetPipe.ToString();
        ui_GetCoinValue.text = _getCoin.ToString();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
