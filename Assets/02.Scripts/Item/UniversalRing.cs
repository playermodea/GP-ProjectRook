using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalRing : Item
{
    private float hpValue;
    private float attackDamageValue;
    private float evaPointValue;
    private int movePointValue;

    private void Awake()
    {
        hpValue = 2.0f;
        attackDamageValue = 2.0f;
        evaPointValue = 10.0f;
        movePointValue = 1;
        price = 50;
        text = "만능 반지 획득 \n체력 +1, 공격력 +2, 회피력 +10%, [전방향] +1";
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
            collision.gameObject.GetComponent<Player>().SetEvasion(evaPointValue);
            for (int i = 0; i < 8; i++)
            {
                collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.UP + i);
            }
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
