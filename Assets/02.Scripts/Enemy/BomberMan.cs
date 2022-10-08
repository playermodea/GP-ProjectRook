using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMan : Enemy, IEnemy
{
    private int countdown;
    [SerializeField] private Sprite[] countdownSprite;

    protected override void Start()
    {
        startHP = 20.0f;
        countdown = 6;

        base.Start();
    }

    public virtual void Action()
    {
        if (isTurn && !isDead)
        {
            switch (state)
            {
                case EnemyState.MOVE:
                    Move();
                    break;
                case EnemyState.ATTACK:
                    Attack();
                    break;
                default:
                    break;
            }
        }
    }

    public virtual void Move()
    {
        animator.SetBool("isWalk", true);
        movement.Move(EnemyPatternManager.Instance.bomberManMovePateernInfo[moveCount], 1);
        StartCoroutine(MoveCheck());
        moveCount++;

        if (moveCount >= EnemyPatternManager.Instance.bomberManMovePateernInfo.Length)
        {
            moveCount = 0;
        }
    }

    public virtual void Attack()
    {
        movement.isShake = true;
        movement.originPos = transform.position;
        countdown--;
        renderer.sprite = countdownSprite[countdown];

        if (countdown > 0)
        {
            StartCoroutine(ShakeCheck());
        }
        else
        {
            AudioPlay.Instance.PlaySound("boomman");
            EffectManager.Instance.CreateExplosionEffect(transform.position);
            renderer.enabled = false;
            attack.Attack(attackDamage, EnemyPatternManager.Instance.bomberManAttackRangeInfo);
            StartCoroutine(AttackCheck());
        }
    }

    public override void OnDamage(int row, int column, float damage)
    {
        if (hp > 0.0f)
        {
            hp -= damage;

            UpdateHealthUI();
            EffectManager.Instance.CreateHitEffect(transform.position);
            AudioPlay.Instance.PlaySound("Attack");

            animator.SetBool("OnDamage", true);
            renderer.color = new Color(255.0f, 0.0f, 0.0f);
            movement.isShake = true;
            movement.originPos = transform.position;
            StartCoroutine(ShakeCheck());

            if (hp <= 0.0f)
            {
                animator.enabled = false;
                state = EnemyState.ATTACK;
                renderer.sprite = countdownSprite[countdown];
            }
        }
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
    }

    protected override IEnumerator MoveCheck()
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
                    TurnManager.Instance.EnemyTurnEnd();
                    break;
                }
                else
                {
                    if (movement.availablePoint == 0)
                    {
                        SetAnimation();
                        CheckEnemyAvailablePoint();
                    }
                    break;
                }
            }
        }
    }

    protected override IEnumerator AttackCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (!attack.isAttack)
            {
                OnDie();
                TurnManager.Instance.EnemyTurnEnd();
                break;
            }
        }
    }

    protected override IEnumerator ShakeCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (!movement.isShake)
            {
                if (hp > 0.0f)
                {
                    SetAnimation();
                }
                if (isTurn)
                {
                    TurnManager.Instance.EnemyTurnEnd();
                }
                break;
            }
        }
    }

    protected override void SetAnimation()
    {
        animator.SetBool("isWalk", false);
        animator.SetBool("OnDamage", false);
    }
}
