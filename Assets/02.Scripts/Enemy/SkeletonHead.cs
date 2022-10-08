using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHead : Enemy, IEnemy
{
    [SerializeField] private EnemyType skeletonHeadType;
    [SerializeField] private Direction dir;

    protected override void Start()
    {
        startHP = 30.0f;
        state = EnemyState.ATTACK;

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

    }

    public virtual void Attack()
    {
        AttackAnimation(dir);
        if (skeletonHeadType == EnemyType.A)
        {
            attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.skeletonHeadAttackRangeInfo, dir));
            dir += 2;
            if (dir > Direction.LEFT_UP)
            {
                dir = Direction.UP;
            }
        }
        else if (skeletonHeadType == EnemyType.B)
        {
            attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.skeletonHeadAttackRangeInfo, dir));
            dir -= 2;
            if (dir < Direction.UP)
            {
                dir = Direction.LEFT;
            }
        }
        StartCoroutine(AttackCheck());
    }

    private void AttackAnimation(Direction dir)
    {
        animator.SetBool("isBack", false);
        animator.SetBool("isRight", false);
        animator.SetBool("isForward", false);
        animator.SetBool("isLeft", false);

        switch (dir)
        {
            case Direction.UP:
                animator.SetBool("isBack", true);
                break;
            case Direction.RIGHT:
                animator.SetBool("isRight", true);
                break;
            case Direction.DOWN:
                animator.SetBool("isForward", true);
                break;
            case Direction.LEFT:
                animator.SetBool("isLeft", true);
                break;
            default:
                break;
        }
    }

    protected override IEnumerator AttackCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (!attack.isAttack)
            {
                TurnManager.Instance.EnemyTurnEnd();
                break;
            }
        }
    }
}
