using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // +++ ���� ���� +++ --------------------------------
    [Header("���� ����")]

    // ü�� ���� =========
    /// <summary>
    /// �ִ� ü��
    /// </summary>
    public float maxHp = 100.0f;

    /// <summary>
    /// ���� ü��
    /// </summary>
    public float currentHp = 100.0f;

    public float CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = value;
            if(currentHp > maxHp)
            {
                currentHp = maxHp;
            }
            onHealthChange?.Invoke(currentHp, maxHp);

            if (currentHp <= 0)
            {
                currentHp = 0;
                OnDie();        // ���
            }
        }
    }

    /// <summary>
    /// �ʴ� ü�� �����
    /// </summary>
    public float regeneration = 0.0f;

    // ���� ���� =====================
    /// <summary>
    /// ���ݷ�
    /// </summary>
    public float attackDamage = 10.0f;

    /// <summary>
    /// ����
    /// </summary>
    public float defence = 0.0f;

    /// <summary>
    /// �ڵ����� �ð�(�������� ������)
    /// </summary>
    public float attackDelayTime = 1.0f;

    /// <summary>
    /// ȸ����
    /// </summary>
    public float dodgeRate = 0.0f;

    /// <summary>
    /// ����� ������
    /// </summary>
    public float debuffGuard = 0.0f;

    /// <summary>
    /// ��� ��ġ�� �ð�
    /// </summary>
    public float HealingSpeed = 1.0f;

    // ����ġ ���� -----------------------------
    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public int level = 1;

    /// <summary>
    /// ���� ����ġ
    /// </summary>
    public int currentEx = 0;

    public int CurrentEx
    {
        get => currentEx;
        set
        {
            currentEx = value;
            if(currentEx >= maxEx)
            {
                // ������, current �ʱ�ȭ, max ����
            }
            onExChange?.Invoke(currentEx, maxEx);
        }
    }

    /// <summary>
    /// �ִ� ����ġ
    /// </summary>
    public int maxEx = 5;   

    /// <summary>
    /// ����ġ ������(�⺻ 1.0)
    /// </summary>
    public float exIncreaseRate = 1.0f;

    // �̵� ���� --------------------------------

    /// <summary>
    /// �÷��̾��� �̵� �ӵ�
    /// </summary>
    public float moveSpeed = 10.0f;

    /// <summary>
    /// �÷��̾��� �̵� ����
    /// </summary>
    Vector2 dir;

    Vector2 Dir
    {
        get => dir;
        set
        {
            dir = value;
            if(dir.x != 0)
            {
                if(dir.x > 0)
                {
                    attackAxie.rotation = Quaternion.Euler(0, 0, -90.0f);
                }
                else
                {
                    attackAxie.rotation = Quaternion.Euler(0, 0, 90.0f);
                }
            }
        }
    }
    



    // +++��������Ʈ ����+++ ----------------------------------

    /// <summary>
    /// ü�� �� �ٲ𶧸��� �Ҹ��� ��������Ʈ
    /// </summary>
    public System.Action<float, float> onHealthChange;

    /// <summary>
    /// ����ġ �� �ٲ𶧸��� �Ҹ��� ��������Ʈ
    /// </summary>
    public System.Action<int, int> onExChange;

    /// <summary>
    /// ���� �� �ٲ𶧸��� �Ҹ��� ��������Ʈ
    /// </summary>
    public System.Action<int> onLevelChange;

    // +++��Ÿ �ڷ�+++ --------------------------------

    /// <summary>
    /// ���� ��
    /// </summary>
    Transform attackAxie;

    /// <summary>
    /// �⺻ ���� ��ġ
    /// </summary>
    Transform attackArea;

    // +++ ������Ʈ ���� +++ -------------------------------------
    Rigidbody2D rb;
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        attackAxie = transform.GetChild(0);
        attackArea = attackAxie.GetChild(0);
    }

    private void Start()
    {
        CurrentEx = 0;

        StartCoroutine(AutoAttack());
    }

    private void OnEnable()
    {
        playerController.onMove = OnMove;
    }

    private void OnMove(Vector2 input)
    {
        Dir = input;
    }

    void FixedUpdate()
    {
        rb.MovePosition(moveSpeed * Time.fixedDeltaTime * Dir + rb.position);
    }

    /// <summary>
    /// �����ð� �������� �ڵ������ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator AutoAttack()
    {
        while(currentHp > 0)
        {
            yield return new WaitForSeconds(attackDelayTime);
            Attack();
        }
    }

    /// <summary>
    /// ���� ���� �ȿ� �ִ� ���� �����ϱ�(��� ���� ����ü�� ��ȯ�ϴ� ���)
    /// </summary>
    void Attack()
    {
        Debug.Log($"�÷��̾� �ڵ� ����");
        Factory.Ins.GetObject(PoolObjectType.PlayerAttack, attackArea.position);
    }

    /// <summary>
    /// ����� �����ϴ� �ڵ�
    /// </summary>
    void OnDie()
    {

    }
    
}
