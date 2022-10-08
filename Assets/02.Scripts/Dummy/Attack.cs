using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Movement movement;
    private GameObject target;

    [field: SerializeField] public bool isAttack { get; private set; }

    private void Start()
    {
        isAttack = false;
        movement = GetComponent<Movement>();
    }

    public void Attacking(float damage, AttackRangeInfo[] attackRangeInfo)
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
                    target = movement.floorTile.GetAttackObject(targetTile.row, targetTile.column, gameObject);

                    if (target != null)
                    {
                        target.GetComponent<IStatus>().OnDamage(movement.cellRow, movement.cellColumn, damage);
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
