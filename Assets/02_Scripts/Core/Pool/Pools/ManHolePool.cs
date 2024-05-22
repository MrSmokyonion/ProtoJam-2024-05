using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManHolePool : ObjectPool<ManHole>
{
    protected override void OnGenerateObjects(ManHole comp)
    {
        comp.Pool = comp.transform.parent;
    }
}
