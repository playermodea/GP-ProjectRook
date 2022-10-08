using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour, IStatus
{
    [SerializeField] protected float startHP;                                           // 초기 체력
    [SerializeField] protected float hp;                                                // 체력
    [field: SerializeField] public float attackDamage { get; protected set; }          // 공격력
    public bool isDead { get; protected set; }

    protected virtual void Start()
    {
        hp = startHP;
    }

    public virtual void OnDamage(int row, int column, float damage)
    {
        hp -= damage;
    }

    public virtual void SetHP(float value)
    {
        hp += value;
    }

    public virtual void SetDamage(float value)
    {
        attackDamage += value;
    }

    public virtual void OnDie()
    {
        Destroy(gameObject);
    }
}
