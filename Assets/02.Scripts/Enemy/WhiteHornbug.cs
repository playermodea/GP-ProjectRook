using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHornbug : Enemy, IEnemy
{
    protected override void Start()
    {
        startHP = 20.0f;

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
        if (EnemyPatternManager.Instance.whiteHornbugMovePatternInfo[moveCount] == Direction.RIGHT_DOWN)
        {
            animator.SetBool("isRight", true);
        }
        else if (EnemyPatternManager.Instance.whiteHornbugMovePatternInfo[moveCount] == Direction.LEFT_UP)
        {
            animator.SetBool("isLeft", true);
        }

        if (!movement.Move(EnemyPatternManager.Instance.whiteHornbugMovePatternInfo[moveCount], 1))
        {
            moveCount++;
        }
        StartCoroutine(MoveCheck());

        if (moveCount >= 2)
        {
            moveCount = 0;
        }
    }

    public virtual void Attack()
    {
        animator.SetBool("isAttack", true);
        attack.Attack(attackDamage, EnemyPatternManager.Instance.hornbugBasicAttackRangeInfo);
        StartCoroutine(AttackCheck());
    }

    protected override void SetAnimation()
    {
        animator.SetBool("isRight", false);
        animator.SetBool("isLeft", false);
        animator.SetBool("isAttack", false);
    }

    public override void OnDamage(int row, int column, float damage)
    {
        animator.SetBool("isAttack", true);

        base.OnDamage(row, column, damage);
    }
}
