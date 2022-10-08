using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : Item
{
    private int movePointValue;
    private float attackDamageValue;

    private void Awake()
    {
        movePointValue = 1;
        attackDamageValue = 1.0f;
        price = 50;
        text = "낚시대 획득 \n공격력 +2, [↗] +1";
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
            collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.UP_RIGHT);
            collision.gameObject.GetComponent<Player>().SetDamage(attackDamageValue);
            UIManager.Instance.SetSlot(PlayerSkillType.ThrowingBobber);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }

}
