using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDuck : EnemyBase
{
    protected override Vector2 Dir
    {
        get => dir;
        set
        {
            dir = value;
            if (dir.x > 0)
            {
                animator.transform.localScale = new Vector2(1, 1);
            }
            else
            {
                animator.transform.localScale = new Vector2(-1, 1);
            }
        }
    }
}
