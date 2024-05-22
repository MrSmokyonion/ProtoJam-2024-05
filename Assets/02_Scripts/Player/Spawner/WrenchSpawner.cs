using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchSpawner : SkillSpawner
{
    protected override IEnumerator StartAttack()
    {
        while (player.CurrentHp > 0)
        {
            UpdateAttackPower();
            UpdateAttackSpeed();

            SpawnSkill();
            for (int i = 1; i < spawnCount; i++)
            {
                yield return new WaitForSeconds(skillData.FireRate);
                SpawnSkill();
            }

            yield return new WaitForSeconds(finalSpawnSpeed);
        }
    }
}
