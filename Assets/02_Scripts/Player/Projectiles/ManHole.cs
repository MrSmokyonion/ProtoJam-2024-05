using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManHole : Projectile
{

    Transform target;

    /// <summary>
    /// 스포너에게 디스폰 되었다고 알리는 델리게이트(끝난 기점으로 쿨타임 진행)
    /// </summary>
    public System.Action onDone;

    const float rotateSpeed = 200.0f;

    public override void OnInitialize(AttackSkillData data, float damage, float lifeTime)
    {
        base.OnInitialize(data, damage, lifeTime);
        target = GameManager.Ins.Player.transform;
    }

    protected override void OnMoveUpdate(float time)
    {
        transform.RotateAround(target.position, new Vector3(0f, 0f, 1f), rotateSpeed * time);
        transform.Rotate(new Vector3(0, 0, -1), rotateSpeed * time);
    }

    protected override IEnumerator LifeOver(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        
        onDone?.Invoke();
        onDone = null;

        gameObject.SetActive(false);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            // enemy에게 데미지 주기
            enemy.OnHitted(damage);
        }

        
    }

}
