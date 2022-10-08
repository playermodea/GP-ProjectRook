using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : Item
{
    private float hpValue;
    private float attackDamageValue;
    private int actionPointValue;
    private int movePointValue;

    private void Awake()
    {
        hpValue = 4.0f;
        attackDamageValue = 4.0f;
        actionPointValue = 1;
        movePointValue = 1;
        price = 50;
        text = "짐 가방 획득 \n체력 +2, 공격력 +4, 행동력 +1, [전방향] +1";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!CheckInteraction(collision.GetComponent<Player>()))
            {
                return;
            }

            ShowText(text);
            collision.gameObject.GetComponent<Player>().SetMaxHP(hpValue);
            collision.gameObject.GetComponent<Player>().SetDamage(attackDamageValue);
            collision.gameObject.GetComponent<Player>().SetMaxActionPoint(actionPointValue);
            for (int i = 0; i < 8; i++)
            {
                collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.UP + i);
            }
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
