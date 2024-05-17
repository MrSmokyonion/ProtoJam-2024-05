using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public Action<EnemyBase> onEnemyIn;
    public Action<EnemyBase> onEnemyOut;

    SpriteRenderer spriteRenderer;
    public Color attackStartColor = Color.red;
    public Color attackEndColor = Color.clear;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = attackEndColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyBase enemy))
        {
            onEnemyIn?.Invoke(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyBase enemy))
        {
            onEnemyOut?.Invoke(enemy);
        }
    }

    public void AttackEffect()
    {
        spriteRenderer.color = attackStartColor;
        spriteRenderer.DOColor(attackEndColor, 0.5f);
    }

}
