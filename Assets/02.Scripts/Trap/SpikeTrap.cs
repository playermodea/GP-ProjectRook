using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap, ITrap
{
    [SerializeField] private Sprite[] spikeSprite;

    private SpriteRenderer renderer;

    protected override void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();

        base.Awake();
    }

    private void CheckPlayerOnThisTrap()
    {
        if (floorTile.tileInfo[cellRow, cellColumn].tileObj != null && floorTile.tileInfo[cellRow, cellColumn].tileObj.tag == "Player")
        {
            Attack();
        }
        else
        {
            manager.GetComponent<TurnManager>().EnemyTurnEnd();
        }
    }

    public virtual void Action()
    {
        if (isTurn)
        {
            switch (state)
            {
                case TrapState.STOP:
                    CheckPlayerOnThisTrap();
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
        renderer.sprite = spikeSprite[1];

        if (floorTile.tileInfo[cellRow, cellColumn].tileObj != null && floorTile.tileInfo[cellRow, cellColumn].tileObj.tag == "Player")
        {
            floorTile.tileInfo[cellRow, cellColumn].tileObj.GetComponent<Player>().OnDamage(cellRow, cellColumn, attackDamage);
        }

        StartCoroutine(AttackCheck());
        StartCoroutine(SpriteCheck());
    }

    private IEnumerator AttackCheck()
    {
        yield return new WaitForSeconds(0.5f);

        isTurn = false;
        manager.GetComponent<TurnManager>().EnemyTurnEnd();
    }

    private IEnumerator SpriteCheck()
    {
        yield return new WaitForSeconds(1.0f);

        renderer.sprite = spikeSprite[0];
    }
}
