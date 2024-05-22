using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerSpawner : SkillSpawner
{
    protected override IEnumerator StartAttack()
    {
        while(player.CurrentHp > 0)
        {
            UpdateAttackPower();
            UpdateAttackSpeed();

            SpawnSkill();
            if(SpawnerLevel > 0)
            {
                yield return new WaitForSeconds(skillData.FireRate);
                SpawnSkill();
            }

            if (SpawnerLevel > 1)
            {
                yield return new WaitForSeconds(skillData.FireRate);
                SpawnSkill();
            }

            yield return new WaitForSeconds(finalAttackSpeed);
        }
    }
    public override void SpawnSkill()
    {
        Vector3 randomPos = Random.insideUnitCircle * 0.5f;

        Factory.Ins.GetObject(skillData.skillType, player.transform.position + randomPos, player.GetFireAngle());
    }

}
