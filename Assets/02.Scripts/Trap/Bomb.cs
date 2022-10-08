using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Trap, ITrap
{
    [SerializeField] private int count;
    [SerializeField] private Sprite[] countdownSprite;

    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        count = 4;

        base.Awake();

        state = TrapState.ATTACK;
    }

    public virtual void Action()
    {
        if (isTurn)
        {
            switch (state)
            {
                case TrapState.STOP:
                    break;
                case TrapState.ATTACK:
                    Attack();
                    break;
                default:
                    break;
            }
        }
    }

    public virtual void Attack()
    {
        if (count == 0)
        {
            EffectManager.Instance.CreateExplosionEffect(transform.position);

            isDestory = true;
            renderer.enabled = false;
            floorTile.ShowPlayerAttackTileColor(cellRow, cellColumn);
            floorTile.SetTrapDestroyTile(gameObject);

            if (floorTile.tileInfo[cellRow, cellColumn].tileObj != null && floorTile.tileInfo[cellRow, cellColumn].tileObj.tag == "Player")
            {
                floorTile.tileInfo[cellRow, cellColumn].tileObj.GetComponent<Player>().OnDamage(cellRow, cellColumn, attackDamage);
            }
        }
        else
        {
            count--;
            renderer.sprite = countdownSprite[count];
        }
        StartCoroutine(AttackCheck());
    }

    private IEnumerator AttackCheck()
    {
        yield return new WaitForSeconds(0.5f);

        isTurn = false;
        manager.GetComponent<TurnManager>().EnemyTurnEnd();
    }
}
