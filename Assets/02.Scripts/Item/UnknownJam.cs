using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownJam : Item
{
    private float attackDamageValue;
    private int movePointValue;

    private void Awake()
    {
        attackDamageValue = 2.0f;
        movePointValue = 1;
        price = 50;
        text = "알 수 없는 잼 획득 \n공격력 +2, [↘] +1";
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
            collision.gameObject.GetComponent<Player>().SetDamage(attackDamageValue);
            collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.RIGHT_DOWN);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
