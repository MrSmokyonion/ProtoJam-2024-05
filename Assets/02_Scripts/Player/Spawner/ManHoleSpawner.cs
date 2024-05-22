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
            // 3����

            Vector3 spawnPosition = new Vector3();
            switch(i)
            {
                case 0:     // ù��° ��ȯ, �׻� ���ʿ� ���´�
                    spawnPosition = new Vector3(0, 1);
                    break;
                case 1:     // �ι�° ��ȯ, spawnCount�� 2�̸� �Ʒ���ȯ, 3�̸� �밢�� ��ȯ�̴�
                    if (spawnCount < 3)
                    {
                        spawnPosition = new Vector3(0, -1, 0);
                    }
                    else
                    {
                        spawnPosition = new Vector3(0.71f, -0.71f, 0);
                    }
                    break;
                case 2:     // ����° ��ȯ
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
