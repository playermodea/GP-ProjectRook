using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour, IAttackSystem
{
    private EnemyMovement movement;

    [field: SerializeField] public bool isAttack { get; private set; }

    private void Awake()
    {
        isAttack = false;
        movement = GetComponent<EnemyMovement>();
    }

    public void MoveAttack(int row, int column)
    {
        GameObject target = movement.floorTile.tileInfo[row, column].tileObj;

        if (target.tag == "Player")
        {
            float damage = GetComponent<Status>().attackDamage;
            target.GetComponent<IStatus>().OnDamage(movement.cellRow, movement.cellColumn, damage);
        }
        else
        {
            GetComponent<Enemy>().CheckEnemyAvailablePoint();
        }
    }

    public void Attack(float damage, AttackRangeInfo[] attackRangeInfo)
    {
        GameObject target = null;

        for (int i = 0; i < attackRangeInfo.Length; i++)
        {
            for (int j = 0; j < attackRangeInfo[i].isAttack.Length; j++)
            {
                FloorTileInfo targetTile = movement.floorTile.CheckDestinationTile(movement.cellRow + attackRangeInfo[i].attackRow,
                                                                        movement.cellColumn + attackRangeInfo[i].attackColumn,
                                                                        attackRangeInfo[i].rangeDir,
                                                                        j);

                if (!targetTile.isPassable && targetTile.tileObj == null)
                {
                    break;
                }

                if (attackRangeInfo[i].isAttack[j] && (targetTile.row != movement.cellRow || targetTile.column != movement.cellColumn))
                {
                    GameObject obj = movement.floorTile.GetAttackObject(targetTile.row, targetTile.column, gameObject);

                    if (obj != null && obj.tag == "Player")
                    {
                        target = obj;
                    }
                }
            }
        }

        if (target != null && target.tag == "Player")
        {
            target.GetComponent<IStatus>().OnDamage(movement.cellRow, movement.cellColumn, damage);
        }

        isAttack = true;

        StartCoroutine(AttackTime());
    }

    private IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(1.0f);

        isAttack = false;
        //movement.floorTile.RefreshTile();
    }
}
