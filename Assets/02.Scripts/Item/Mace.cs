using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : Item
{
    private float attackDamageValue;
    private int TurnValue;

    private void Awake()
    {
        attackDamageValue = 3.0f;
        TurnValue = 1;
        price = 50;
        text = "메이스 획득 \n공격력 +3, 행동력 +1";
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
            collision.gameObject.GetComponent<Player>().SetMaxActionPoint(TurnValue);
            UIManager.Instance.SetSlot(PlayerSkillType.Swing);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
