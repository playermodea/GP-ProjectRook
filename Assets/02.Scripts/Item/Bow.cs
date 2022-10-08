using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Item
{
    private float attackDamageValue;
    private float evaPointValue;

    private void Awake()
    {
        attackDamageValue = 1.0f;
        evaPointValue = 10.0f;
        price = 50;
        text = "얼음 지팡이 획득 \n공격력 +1, 회피력 +10%";
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
            collision.gameObject.GetComponent<Player>().SetEvasion(evaPointValue);
            UIManager.Instance.SetSlot(PlayerSkillType.HitTheMark);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
