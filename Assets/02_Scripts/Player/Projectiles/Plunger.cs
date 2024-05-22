using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : Projectile
{
    protected override void OnMoveUpdate(float time)
    {
        transform.Translate(speed * time * dir);
    }
}
