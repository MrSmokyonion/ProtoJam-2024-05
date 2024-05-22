using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManHoleSpawner : SkillSpawner
{
    bool projectileDone = false;

    protected override IEnumerator StartAttack()
    {
        while (player.CurrentHp > 0)
        {
            UpdateAttackPower();
            UpdateAttackSpeed();

            SpawnSkill();
            //for (int i = 1; i < spawnCount; i++)
            //{
            //    yield return new WaitForSeconds(skillData.FireRate);
            //    SpawnSkill();
            //}
            yield return new WaitUntil(() => projectileDone);

            

            yield return new WaitForSeconds(finalSpawnSpeed);
        }
    }

    public override void SpawnSkill()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            // 0, 1
            // 0, -1
            // 3방향

            Vector3 spawnPosition = new Vector3();
            switch(i)
            {
                case 0:     // 첫번째 소환, 항상 위쪽에 나온다
                    spawnPosition = new Vector3(0, 1);
                    break;
                case 1:     // 두번째 소환, spawnCount가 2이면 아래소환, 3이면 대각선 소환이다
                    if (spawnCount < 3)
                    {
                        spawnPosition = new Vector3(0, -1, 0);
                    }
                    else
                    {
                        spawnPosition = new Vector3(0.71f, -0.71f, 0);
                    }
                    break;
                case 2:     // 세번째 소환
                    spawnPosition = new Vector3(-0.71f, -0.71f, 0);
                    break;
                default:
                    break;
            }

            GameObject temp = Factory.Ins.GetObject(skillData.GetPoolType(), player.transform.position + spawnPosition * 1.3f, 0);
            ManHole manHole = temp.GetComponent<ManHole>();
            manHole.OnInitialize(skillData, finalDamage, lifeTime);
            manHole.transform.SetParent(transform);
            if (i == 0)
            {
                projectileDone = false;
                manHole.onDone = () =>
                {
                    projectileDone = true;
                };
            }
        }
    }
}
