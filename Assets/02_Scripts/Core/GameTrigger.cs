using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 싱글톤이 컴파일 끝나고 늦게 트리거를 실행할 타이밍이 애메하여 준비 된 마지막 트리거로 사용할 클래스
/// </summary>
public class GameTrigger : MonoBehaviour
{
    private void Start()
    {
        OnTrigger();
    }

    protected void OnTrigger()
    {
        GameManager.Ins.StartGame();
    }
}
