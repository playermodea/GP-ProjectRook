using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Status
{
    protected IEnumerator healthCoroutine;

    public EnemyState state { get; protected set; }
    public int moveCount { get; protected set; }
    public bool isTurn;
    public bool isDamaged;

    public Animator animator { get; protected set; }
    public SpriteRenderer renderer { get; protected set; }
    public EnemyMovement movement { get; protected set; }
    public EnemyAttackSystem attack { get; protected set; }
    public Canvas healthCanvas;
    public RectTransform healthFill;

    public GameObject[] dropItem;

    private void Awake()
    {
        state = EnemyState.MOVE;
        healthCoroutine = UICheck();

        moveCount = 0;
        attackDamage = 1.0f;

        isTurn = false;
        isDead = false;
        isDamaged = false;
        
        healthCanvas.gameObject.GetComponent<Canvas>().enabled = false;

        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttackSystem>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnDamage(int row, int column, float damage)
    {
        base.OnDamage(row, column, damage);

        UpdateHealthUI();
        EffectManager.Instance.CreateHitEffect(transform.position);

        isDamaged = true;
        renderer.color = new Color(255.0f / 255.0f, 0.0f, 0.0f);
        AudioPlay.Instance.PlaySound("Attack");
        movement.isShake = true;
        movement.originPos = transform.position;
        StartCoroutine(ShakeCheck());
    }

    public override void OnDie()
    {
        bool drop = (Random.value >= 0.3f);

        if (drop)
        {
            GameObject dropI;
            bool dropGold = (Random.value >= 0.25f);

            if (dropGold)
            {
                dropI = Instantiate(dropItem[0], movement.floorTile.tileInfo[movement.cellRow, movement.cellColumn].tilePos, Quaternion.identity);
            }
            else
            {
                dropI = Instantiate(dropItem[1], movement.floorTile.tileInfo[movement.cellRow, movement.cellColumn].tilePos, Quaternion.identity);
            }

            dropI.transform.parent = transform.parent;
        }

        isDead = true;
        movement.floorTile.SetEnemyDieTile(gameObject);
        renderer.enabled = false;
        TurnManager.Instance.EnemyDeadAnimationComplete();
    }

    public void CheckEnemyAvailablePoint()
    {
        movement.isMoveAttack = false;

        if (movement.availablePoint > 0)
        {
            movement.Move(movement.moveDirection, movement.availablePoint);
            StartCoroutine(MoveCheck());
        }
        else
        {
            isTurn = false;
            TurnManager.Instance.EnemyTurnEnd();
        }
    }

    protected virtual void UpdateHealthUI()
    {
        healthCanvas.gameObject.GetComponent<Canvas>().enabled = true;
        float barPercent = hp / startHP;
        if (barPercent < 0.0f)
        {
            barPercent = 0.0f;
        }
        healthFill.localScale = new Vector3(barPercent, 1.0f, 1.0f);
        StopCoroutine(healthCoroutine);
        StartCoroutine(healthCoroutine);
    }

    protected virtual IEnumerator UICheck()
    {
        yield return new WaitForSeconds(3.0f);

        healthCanvas.gameObject.GetComponent<Canvas>().enabled = false;
    }

    protected virtual IEnumerator MoveCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (!movement.isMove)
            {
                if (!movement.isMoveAttack)
                {
                    isTurn = false;
                    SetAnimation();
                    state = EnemyState.ATTACK;
                    TurnManager.Instance.EnemyTurnEnd();
                    break;
                }
                else
                {
                    if (movement.availablePoint == 0)
                    {
                        SetAnimation();
                        state = EnemyState.ATTACK;
                        CheckEnemyAvailablePoint();
                    }
                    break;
                }
            }
        }
    }

    protected virtual IEnumerator AttackCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (!attack.isAttack)
            {
                isTurn = false;
                SetAnimation();
                state = EnemyState.MOVE;
                TurnManager.Instance.EnemyTurnEnd();
                break;
            }
        }
    }

    protected virtual IEnumerator ShakeCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (!movement.isShake)
            {
                if (hp <= 0.0f)
                {
                    TurnManager.Instance.EnemyDeadAnimationDelay();
                    movement.isDead = true;
                    StartCoroutine(AlphaCheck());
                }
                else
                {
                    SetAnimation();
                }
                break;
            }
        }
    }

    protected virtual IEnumerator AlphaCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (!movement.isDead)
            {
                OnDie();
                break;
            }
        }
    }

    protected virtual void SetAnimation()
    {

    }
}
