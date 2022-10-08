using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyWoodWaveAttack : MonoBehaviour
{
    [SerializeField] public int cellRow;
    [SerializeField] public int cellColumn;
    [field: SerializeField] public bool isActive { get; set; }
    private float attackDamage;

    [SerializeField] public Direction attackDir;
    [SerializeField] public Direction moveDir;

    [field: SerializeField] public EnemyAttackSystem attack { get; private set; }
    [field: SerializeField] public EnemyMovement movement { get; private set; }

    private void Awake()
    {
        isActive = false;
        attackDamage = 1.0f;

        attack = GetComponent<EnemyAttackSystem>();
        movement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        movement.cellRow = cellRow;
        movement.cellColumn = cellColumn;
    }

    public void Action()
    {
        if (isActive)
        {
            gameObject.tag = "Enemy";
            Attack();
        }
    }

    private void Move()
    {
        switch(moveDir)
        {
            case Direction.UP:
                movement.cellRow--;
                break;
            case Direction.RIGHT:
                movement.cellColumn++;
                break;
            case Direction.DOWN:
                movement.cellRow++;
                break;
            case Direction.LEFT:
                movement.cellColumn--;
                break;
            default:
                break;
        }

        if (movement.cellRow > 15 && moveDir == Direction.DOWN)
        {
            movement.cellRow = 1;
            isActive = false;
        }
        else if (movement.cellRow < 1 && moveDir == Direction.UP)
        {
            movement.cellRow = 15;
            isActive = false;
        }
        if (movement.cellColumn > 15 && moveDir == Direction.RIGHT)
        {
            movement.cellColumn = 1;
            isActive = false;
        }
        else if (movement.cellColumn < 1 && moveDir == Direction.LEFT)
        {
            movement.cellColumn = 15;
            isActive = false;
        }
    }

    private void Attack()
    {
        attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.FairyWoodAttack_1_RangeInfo, attackDir));
        StartCoroutine(AttackCheck());
        Move();
    }

    private IEnumerator AttackCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (!attack.isAttack)
            {
                transform.parent.GetComponent<FairyWood>().CheckWaveAttack();
                break;
            }
        }
    }
}
