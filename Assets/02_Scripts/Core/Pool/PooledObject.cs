using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ������Ʈ Ǯ�� �� ������Ʈ���� ��ӹ��� Ŭ����
/// </summary>
public class PooledObject : MonoBehaviour
{
    /// <summary>
    /// �� ���� ������Ʈ�� ��Ȱ��ȭ �� �� ����Ǵ� ��������Ʈ
    /// </summary>
    public System.Action onDisable;

    protected virtual void OnEnable()
    {
        transform.localPosition = Vector3.zero;                 // ��ġ�� ȸ�� �ʱ�ȭ
        transform.localRotation = Quaternion.identity;

    }

    protected virtual void OnDisable()
    {
        onDisable?.Invoke();
    }

    /// <summary>
    /// �����ð� �� ������Ʈ ��Ȱ��ȭ
    /// </summary>
    /// <param name="delay">��Ȱ��ȭ ������ų �ð�</param>
    /// <returns></returns>
    protected virtual IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
