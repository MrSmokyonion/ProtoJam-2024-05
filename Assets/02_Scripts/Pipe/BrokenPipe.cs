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
    private float timer;

    public void InitPipe(float _repairRange, float _repairTime, PipeSpawner _parents)
    {
        repairRange = _repairRange;
        repairTime = _repairTime;
        parents = _parents;
        isRepairing = false;
        isCompleted = false;
        timer = 0f;

        GetComponent<CircleCollider2D>().radius = repairRange;
        ui_repairGauge.maxValue = repairTime;
        ui_repairGauge.value = 0f;
        ui_repairGauge.gameObject.SetActive(false);
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
    }

    private void OnRepairing()
    {
        if (!isRepairing) { return; }

        timer += Time.deltaTime;
        ui_repairGauge.value = timer;

        if(timer > repairTime)
        {
            RepairFinish();
        }
    }

    private void RepairPause()
    {
        isRepairing = false;
    }

    private void RepairFinish()
    {
        isRepairing = false;
        isCompleted = true;
        ui_repairGauge.gameObject.SetActive(false);

        //수리 완료�瑛뻑� 그래픽 연출


        parents.RemovePipe(this);
        Invoke("DestroySelf", 3f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(this.transform.position, repairRange);
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
