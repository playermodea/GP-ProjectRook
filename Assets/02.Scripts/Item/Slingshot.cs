using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : Item
{
    private float attackDamageValue;
    private int movePointValue;

    private void Awake()
    {
        attackDamageValue = 2.0f;
        movePointValue = 1;
        price = 50;
        text = "화염 지팡이 획득 \n공격력 +2, [↑] +1";
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
            collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.UP);
            UIManager.Instance.SetSlot(PlayerSkillType.SlingShot);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
