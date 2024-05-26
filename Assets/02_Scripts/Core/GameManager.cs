using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [SerializeField]
    private ResultUIController resultUIController;

    Player player;
    public Player Player
    {
        get
        {
            if(player == null)
            {
                player = FindAnyObjectByType<Player>();
            }    
            return player;
        }
        private set
        {
            player = value;
        }
    }

    int fixedPipeCount = 0;
    public int FixedPipeCount
    {
        get => fixedPipeCount;
        set
        {
            fixedPipeCount = value;
            OnFixedPipeCount?.Invoke(fixedPipeCount);
        }
    }

    public System.Action<int> OnFixedPipeCount;

    int spawnedPipeCount = 0;
    public int SpawnedPipeCount
    {
        get => spawnedPipeCount;
        set
        {
            spawnedPipeCount = value;
        }
    }

    int getCoin;
    public int GetCoin
    {
        get => getCoin;
        set
        {
            getCoin = value;
        }
    }

    PipeSpawner pipeSpawner;
    public PipeSpawner PipeSpawner
    {
        get
        {
            if(pipeSpawner == null)
            {
                pipeSpawner = FindAnyObjectByType<PipeSpawner>();
            }
            return pipeSpawner;
        }
        private set { pipeSpawner = value; }
    }

    protected override void OnInitalize()
    {
        base.OnInitalize();


        if (true)       // 나중에 씬 마다 enum 추가하면 구별할 것
        {
            FixedPipeCount = 0;

            pipeSpawner = FindAnyObjectByType<PipeSpawner>();

            if(pipeSpawner != null )
            {
                pipeSpawner.onFixedPipe = () => FixedPipeCount++;

                pipeSpawner.StartSpawn();       // 게임 시작
            }
        }

    }

    public void ShowResultUI()
    {
        resultUIController.InitResultValueToText(FixedPipeCount, spawnedPipeCount, GetCoin);
    }
}
