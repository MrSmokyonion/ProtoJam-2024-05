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
            SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_WRENCH_THROW);
            for (int i = 1; i < spawnCount; i++)
            {
                yield return new WaitForSeconds(skillData.FireRate);
                SpawnSkill();
                SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_WRENCH_THROW);
            }

            GameManager.Ins.DoSkillCoolDownUI(AttackSkillData.SkillType.Wrench, finalSpawnSpeed);
            yield return new WaitForSeconds(finalSpawnSpeed);
        }
    }
}
