using UnityEngine;

public interface IStatus
{
    void OnDamage(int row, int column, float damage);
    void SetHP(float healingPoint);
    void SetDamage(float value);
    void OnDie();
}
