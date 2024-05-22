using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManHole : Projectile
{
    /// <summary>
    /// ������Ʈ�� Ǯ�� ������(���丮 ������ �ִ� Ǯ)
    /// </summary>
    Transform pool = null;

    /// <summary>
    /// ������Ʈ�� Ǯ�� �ѹ��� ������
    /// </summary>
    public Transform Pool
    {
        set
        {
            if (pool == null)
            {
                pool = value;
            }
        }
    }

    Transform target;

    /// <summary>
    /// Ǯ���� ��ȯ �����ٰ� �˸��� ��������Ʈ
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
        
        transform.SetParent(pool);
        onDone?.Invoke();
        onDone = null;

        gameObject.SetActive(false);
    }

}
