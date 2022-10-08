using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickax : Item
{
    private int movePointValue;
    private float attackDamageValue;
    private int TurnValue;

    private void Awake()
    {
        movePointValue = 1;
        attackDamageValue = 2.0f;
        TurnValue = 1;
        price = 50;
        text = "곡괭이 획득 \n공격력 +2, 행동력 +1, [→] +1";
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
            collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.LEFT);
            collision.gameObject.GetComponent<Player>().SetDamage(attackDamageValue);
            collision.gameObject.GetComponent<Player>().SetMaxActionPoint(TurnValue);
            UIManager.Instance.SetSlot(PlayerSkillType.SmashSlam);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
