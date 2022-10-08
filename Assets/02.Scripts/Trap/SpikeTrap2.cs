using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap2 : Trap, ITrap
{
    private int stopCount;
    private int attackCount;

    [SerializeField] private Sprite[] spikeSprite;

    [SerializeField] private GameObject attackObj;
    private SpriteRenderer renderer;

    protected override void Awake()
    {
        stopCount = 0;
        attackCount = 0;

        renderer = GetComponent<SpriteRenderer>();

        base.Awake();
    }

    private void CheckPlayerOnThisTrap()
    {
        if (floorTile.tileInfo[cellRow, cellColumn].tileObj != null && floorTile.tileInfo[cellRow, cellColumn].tileObj.tag == "Player")
        {
            state = TrapState.ATTACK;
            Attack();
        }
    }

    public virtual void Action()
    {
        if (isTurn)
        {
            switch (state)
            {
                case TrapState.STOP:
                    Stop();
                    break;
                case TrapState.ATTACK:
                    Attack();
                    break;
                default:
                    break;
            }
        }
    }

    private void Stop()
    {
        if (stopCount == 0)
        {
            renderer.sprite = spikeSprite[0];
        }

        stopCount++;

        if (stopCount >= 2)
        {
            stopCount = 0;
            state = TrapState.ATTACK;
        }
        manager.GetComponent<TurnManager>().EnemyTurnEnd();
    }

    public virtual void Attack()
    {
        if (attackCount == 0)
        {
            renderer.sprite = spikeSprite[1];

            if (floorTile.tileInfo[cellRow, cellColumn].tileObj != null && floorTile.tileInfo[cellRow, cellColumn].tileObj.tag == "Player")
            {
                floorTile.tileInfo[cellRow, cellColumn].tileObj.GetComponent<Player>().OnDamage(cellRow, cellColumn, attackDamage);
            }

            floorTile.tileInfo[cellRow, cellColumn].isPassable = false;
            floorTile.tileInfo[cellRow, cellColumn].tileObj = attackObj;
        }

        attackCount++;

        if (attackCount >= 2)
        {
            attackCount = 0;
            state = TrapState.STOP;
            floorTile.tileInfo[cellRow, cellColumn].isPassable = true;
            floorTile.tileInfo[cellRow, cellColumn].tileObj = null;
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
