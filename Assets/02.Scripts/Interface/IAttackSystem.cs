using UnityEngine;

public interface IAttackSystem
{
    void Attack(float damage, AttackRangeInfo[] attackRangeInfo);
}
