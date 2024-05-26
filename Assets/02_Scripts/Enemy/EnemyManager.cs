using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    readonly List<Transform> _enemyList = new();

    protected override void OnInitalize()
    {
        _enemyList.Clear();
    }

    public void RegisterEnemy(Transform enemy)
    {
        _enemyList.Add(enemy);
    }

    public void RemoveEnemy(Transform enemy)
    {
        _enemyList.Remove(enemy);
    }

    public IEnumerable<Transform> IterateEnemyTransforms()
    {
        return _enemyList;
    }
}