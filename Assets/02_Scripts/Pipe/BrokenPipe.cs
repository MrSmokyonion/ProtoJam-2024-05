using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenPipe : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private string targetTag;

    [Header("UI References")]
    [SerializeField] private Slider ui_repairGauge;

    [Header("Parent PipeSpawner")]
    [SerializeField] private PipeSpawner parents;

    [Header("Values")]
    [SerializeField] private float repairRange;
    [SerializeField] private float repairTime;

    private bool isRepairing;
    private bool isCompleted;
    private bool canSpawn;
    private float timer;

    //--------------
    public float spawnTime = 2.0f;

    PoolObjectType SpawnEnemyType
    {
        get
        {
            if(GameManager.Ins.Player.level < 5)
            {
                return PoolObjectType.EnemyPoop;
            }
            else
            {
                return Random.value > 0.5f ? PoolObjectType.EnemyPoop : PoolObjectType.EnemyDuck;
            }
        }
    }

    public void InitPipe(float _repairRange, float _repairTime, PipeSpawner _parents)
    {
        repairRange = _repairRange;
        repairTime = _repairTime;
        parents = _parents;
        isRepairing = false;
        isCompleted = false;
        canSpawn = true;
        timer = 0f;

        GetComponent<CircleCollider2D>().radius = repairRange;
        ui_repairGauge.maxValue = repairTime;
        ui_repairGauge.value = 0f;
        ui_repairGauge.gameObject.SetActive(false);

        if(GameManager.Ins.Player.level > 5)
        { 
            StartCoroutine(SpawnCrocodileCoroutine());
        }

        StartCoroutine(SpawnCoroutine());
    }


    private void Update()
    {
        OnRepairing();
    }

    
    private void RepairStart()
    {
        isRepairing = true;
        if(!ui_repairGauge.gameObject.activeInHierarchy)
        {
            ui_repairGauge.gameObject.SetActive(true);
        }
        SoundManager.instance.PlaySFXLoop(SoundManager.SOUND_LIST.SFX_PIPE_FIXING);
    }

    private void OnRepairing()
    {
        if (!isRepairing) { canSpawn = true; return; }

        timer += Time.deltaTime;
        ui_repairGauge.value = timer;
        canSpawn = false;

        if (timer > repairTime)
        {
            RepairFinish();
        }
    }

    private void RepairPause()
    {
        SoundManager.instance.StopSFXLoop(SoundManager.SOUND_LIST.SFX_PIPE_FIXING);
        isRepairing = false;
    }

    private void RepairFinish()
    {
        isRepairing = false;
        isCompleted = true;
        ui_repairGauge.gameObject.SetActive(false);
        GameManager.Ins.GetCoin += 50;

        SoundManager.instance.StopSFXLoop(SoundManager.SOUND_LIST.SFX_PIPE_FIXING);
        //수리 완료�瑛뻑� 그래픽 연출


        parents.RemovePipe(this);
        Invoke("DestroySelf", 3f);
    }

    IEnumerator SpawnCoroutine()
    {
        while (isCompleted != true)
        {
            yield return new WaitForSeconds(spawnTime);
            if(!canSpawn || isCompleted) { continue; }
            SpawnEnemy(SpawnEnemyType);
        }
    }

    IEnumerator SpawnCrocodileCoroutine()
    {
        while (isCompleted != true)
        {
            yield return new WaitForSeconds(spawnTime * 2);
            if (!canSpawn || isCompleted) { continue; }
            SpawnEnemy(PoolObjectType.EnemyCrocodile);
        }
    }

    void SpawnEnemy(PoolObjectType type)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject temp = Factory.Ins.GetObject(type, transform.position + (Vector3)Random.insideUnitCircle);
            EnemyBase enemy = temp.GetComponent<EnemyBase>();
            //enemy.SettingState() 여기서 스텟 설정
            SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_ENEMY_SPAWN, 0.05f);
        }
    }





    private void DestroySelf()
    {
        Destroy(gameObject);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(this.transform.position, repairRange);
        Gizmos.DrawWireSphere(this.transform.position, repairRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            if (isCompleted) { return; }
            RepairStart();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            if(isCompleted) { return; }
            RepairPause();
        }
    }
}
