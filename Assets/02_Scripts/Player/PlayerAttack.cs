using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PooledObject
{
    public Color attackStartColor = Color.red;
    public Color attackEndColor = Color.clear;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = attackEndColor;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        spriteRenderer.color = attackStartColor;
        spriteRenderer.DOColor(attackEndColor, 0.5f);
        StartCoroutine(LifeOver(0.5f));
    }
}
