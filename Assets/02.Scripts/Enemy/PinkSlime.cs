using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkSlime : Enemy, IEnemy
{
    protected override void Start()
    {
        startHP = 25.0f;

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

        if (!movement.Move(EnemyPatternManager.Instance.pinkSlimeMovePatternInfo[moveCount], 2))
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
        animator.SetBool("isWalk", true);
        attack.Attack(attackDamage, EnemyPatternManager.Instance.slimeBasicAttackRangeInfo);
        StartCoroutine(AttackCheck());
    }

    protected override void SetAnimation()
    {
        animator.SetBool("isWalk", false);
    }

    public override void OnDamage(int row, int column, float damage)
    {
        animator.SetBool("isWalk", true);

        base.OnDamage(row, column, damage);
    }
}
