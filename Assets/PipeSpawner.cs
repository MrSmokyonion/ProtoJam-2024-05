using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Spawn Target")]
    [SerializeField] private GameObject prefab_pipe;

    [Header("Spawn Point")]
    [SerializeField] private Transform spawnPoint_LeftBottom;
    [SerializeField] private Transform spawnPoint_RightTop;

    [Header("Debug")]
    [SerializeField] private Transform debug_player;

    [Header("Variable")]
    [SerializeField]
    [Tooltip("파이프가 스폰되는 시간")]
    private float spawnTime;
    [SerializeField]
    [Tooltip("파이프가 수리되기 위한 시간은 어느정도?")]
    private float pipeRepairTime;
    [SerializeField]
    [Tooltip("파이프를 수리할 수 있는 거리는 얼마나?")]
    private float pipeRepairRange;
    [SerializeField]
    [Tooltip("파이프 스폰할 때 플레이어와의 거리 오프셋")]
    private float pipeDistanceOffset;
    [SerializeField]
    [Tooltip("한번에 존재할 수 있는 파이프 개수")]
    private int maxPipeCountAtSameTime;
    [SerializeField]
    [Tooltip("목표 파이프 스폰 개수")]
    private int targetPipeCount;


    private List<BrokenPipe> brokenPipes;

    private bool isOn = false;


    public System.Action onFixedPipe;


    //private void Start()
    //{
    //    isOn = false;
    //}

    [ContextMenu("Start Spawn")]
    public void StartSpawn()
    {
        StartSpawn(targetPipeCount, maxPipeCountAtSameTime, spawnTime);
    }

    public void StartSpawn(int _targetPipeCount = 10, int _maxPipeCountAtSameTime = 3, float _spawnTime = 10f)
    {
        isOn = true;
        targetPipeCount = _targetPipeCount;
        maxPipeCountAtSameTime = _maxPipeCountAtSameTime;
        spawnTime = _spawnTime;
        brokenPipes = new List<BrokenPipe>();
        StartCoroutine(OnStartSpawn());
    }

    [ContextMenu("End Spawn")]
    public void EndSpawn()
    {
        isOn = false;
    }

    public void RemovePipe(BrokenPipe _target)
    {
        onFixedPipe?.Invoke();
        brokenPipes.Remove(_target);
    }

    private IEnumerator OnStartSpawn()
    {
        float _timer = spawnTime;

        while(true)
        {
            yield return new WaitForEndOfFrame();

            _timer += Time.deltaTime;
            if(brokenPipes.Count >= targetPipeCount ) { isOn = false; break; }

            if (_timer > spawnTime)
            {
                if (!isOn) { break; }
                if(brokenPipes.Count >= maxPipeCountAtSameTime) { continue; }

                _timer = 0f;
                for (int i = brokenPipes.Count; i < maxPipeCountAtSameTime; i++)
                {
                    SpawnBrokenPipe();
                }
            }
        }
    }

    private void SpawnBrokenPipe()
    {
        Vector3 spawnPosition = GetSpawnCoordinate();

        GameObject _obj = Instantiate(prefab_pipe, spawnPosition, Quaternion.identity);
        BrokenPipe _brokenPipe = _obj.GetComponent<BrokenPipe>();
        _brokenPipe.InitPipe(pipeRepairRange, pipeRepairTime, this);

        brokenPipes.Add(_brokenPipe);
    }

    private Vector3 GetSpawnCoordinate()
    {
        Vector3 spawnPosition = Vector3.zero;
        while (true)
        {
            spawnPosition.x = UnityEngine.Random.Range(spawnPoint_LeftBottom.position.x, spawnPoint_RightTop.position.x);
            spawnPosition.y = UnityEngine.Random.Range(spawnPoint_LeftBottom.position.y, spawnPoint_RightTop.position.y);

            float distance = Vector3.Distance(spawnPosition, debug_player.position);
            if(distance > pipeRepairRange + pipeDistanceOffset) { break; }
            else { continue; }
        }
        return spawnPosition;
    }
}
