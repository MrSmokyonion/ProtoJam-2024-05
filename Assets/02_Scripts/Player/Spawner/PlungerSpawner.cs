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
            for(int i = 1; i < spawnCount; i++)
            {
                yield return new WaitForSeconds(skillData.FireRate);
                SpawnSkill();
            }
            yield return new WaitForSeconds(finalSpawnSpeed);
        }
    }
    public override void SpawnSkill()
    {
        Vector3 randomPos = Random.insideUnitCircle * 0.5f;

        GameObject temp = Factory.Ins.GetObject(skillData.GetPoolType(), player.transform.position + randomPos, player.GetFireAngle());
        temp.GetComponent<Projectile>().OnInitialize(skillData, finalDamage, lifeTime);
    }

}
