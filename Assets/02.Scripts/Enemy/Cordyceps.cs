using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cordyceps : Enemy, IEnemy
{
    private int attackCount;

    protected override void Start()
    {
        startHP = 30.0f;
        attackCount = 0;

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
        if (EnemyPatternManager.Instance.cordycepsMovePatternInfo[moveCount] == Direction.LEFT)
        {
            animator.SetBool("isLeft", true);
        }
        else if (EnemyPatternManager.Instance.cordycepsMovePatternInfo[moveCount] == Direction.RIGHT)
        {
            animator.SetBool("isRight", true);
        }

        movement.Move(EnemyPatternManager.Instance.cordycepsMovePatternInfo[moveCount], 1);
        moveCount++;
        StartCoroutine(MoveCheck());

        if (moveCount >= EnemyPatternManager.Instance.cordycepsMovePatternInfo.Length)
        {
            moveCount = 0;
        }
    }

    public virtual void Attack()
    {
        animator.SetBool("isAttack", true);

        if (attackCount == 0)
        {
            attack.Attack(attackDamage, EnemyPatternManager.Instance.slimeBasicAttackRangeInfo);
        }
        else
        {
            attack.Attack(attackDamage, EnemyPatternManager.Instance.hornbugBasicAttackRangeInfo);
        }
        StartCoroutine(AttackCheck());
        attackCount++;
    }

    public override void OnDamage(int row, int column, float damage)
    {
        animator.SetBool("isAttack", true);

        base.OnDamage(row, column, damage);
    }

    protected override void SetAnimation()
    {
        animator.SetBool("isRight", false);
        animator.SetBool("isLeft", false);
        animator.SetBool("isAttack", false);
    }

    protected override IEnumerator AttackCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (!attack.isAttack)
            {
                SetAnimation();
                if (attackCount >= 2)
                {
                    state = EnemyState.MOVE;
                    attackCount = 0;
                }
                isTurn = false;
                TurnManager.Instance.EnemyTurnEnd();
                break;
            }
        }
    }
}
