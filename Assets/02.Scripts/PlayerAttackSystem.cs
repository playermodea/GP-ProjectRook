using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour, IAttackSystem
{
    private PlayerMovement movement;

    [field: SerializeField] public bool isAttack { get; private set; }

    private void Awake()
    {
        isAttack = false;
        movement = GetComponent<PlayerMovement>();
    }

    public void ShowAttackRange(int row, int column, AttackRangeInfo[] attackRangeInfo)
    {
        for (int i = 0; i < attackRangeInfo.Length; i++)
        {
            for (int j = 0; j < attackRangeInfo[i].isAttack.Length; j++)
            {
                FloorTileInfo targetTile = movement.floorTile.CheckDestinationTile(row + attackRangeInfo[i].attackRow,
                                                                        column + attackRangeInfo[i].attackColumn,
                                                                        attackRangeInfo[i].rangeDir,
                                                                        j);

                if (attackRangeInfo[i].isAttack[j] && (targetTile.row != row || targetTile.column != column))
                {
                    movement.floorTile.ShowPlayerAttackTileColor(targetTile.row, targetTile.column);
                }
            }
        }
    }

    public void Attack(float damage, AttackRangeInfo[] attackRangeInfo)
    {
        for (int i = 0; i < attackRangeInfo.Length; i++)
        {
            for (int j = 0; j < attackRangeInfo[i].isAttack.Length; j++)
            {
                FloorTileInfo targetTile = movement.floorTile.CheckDestinationTile(movement.cellRow + attackRangeInfo[i].attackRow,
                                                                        movement.cellColumn + attackRangeInfo[i].attackColumn,
                                                                        attackRangeInfo[i].rangeDir,
                                                                        j);

                if (attackRangeInfo[i].isAttack[j] && (targetTile.row != movement.cellRow || targetTile.column != movement.cellColumn))
                {
                    GameObject obj = movement.floorTile.GetAttackObject(targetTile.row, targetTile.column, gameObject, true);

                    if (obj != null && obj.tag == "Enemy" && !obj.GetComponent<Enemy>().isDamaged)
                    {
                        obj.GetComponent<IStatus>().OnDamage(movement.cellRow, movement.cellColumn, damage);
                    }
                }
            }
        }

        isAttack = true;

        StartCoroutine(AttackTime());
    }

    private IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(1.0f);

        isAttack = false;
    }
}