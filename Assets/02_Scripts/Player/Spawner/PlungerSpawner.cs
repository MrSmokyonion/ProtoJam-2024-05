using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlungerSpawner : SkillSpawner
{
    readonly List<Transform> _cache = new();

    protected override IEnumerator StartAttack()
    {
        spawnCount = 3; // 뚫어뻥은 기본 3개부터 시작

        while (player.CurrentHp > 0)
        {
            UpdateAttackPower();
            UpdateAttackSpeed();

            var targets = EnemyManager.Ins.IterateEnemyTransforms()
                .OrderBy(t => Vector2.SqrMagnitude((Vector2) t.position - (Vector2) transform.position))
                .Take(spawnCount);
            _cache.Clear();
            _cache.AddRange(targets);
            foreach (var targetTransform in _cache)
            {
                var targetPosition = targetTransform.position;
                SpawnPlunger(targetPosition);
                SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_PLUNGER_THROW);
                yield return new WaitForSeconds(skillData.FireRate);
            }

            GameManager.Ins.DoSkillCoolDownUI(AttackSkillData.SkillType.Plunger, finalSpawnSpeed);
            yield return new WaitForSeconds(finalSpawnSpeed);
        }
    }

    void SpawnPlunger(Vector3 targetPosition)
    {
        Vector3 randomPos = Random.insideUnitCircle * 0.5f;

        var delta = (Vector2) targetPosition - (Vector2) transform.position;
        var rotation = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        GameObject temp =
            Factory.Ins.GetObject(skillData.GetPoolType(), player.transform.position + randomPos, rotation);
        temp.GetComponent<Projectile>().OnInitialize(skillData, finalDamage, lifeTime);
    }
}