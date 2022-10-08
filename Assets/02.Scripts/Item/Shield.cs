using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{
    private float attackDamageValue;
    private float hpValue;

    private void Awake()
    {
        hpValue = 2.0f;
        attackDamageValue = 1.0f;
        price = 50;
        text = "원형 방패 획득 \n체력 +1, 공격력 +1";
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
            collision.gameObject.GetComponent<Player>().SetMaxHP(hpValue);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
