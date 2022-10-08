using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword : Item
{
    private float attackDamageValue;

    private void Awake()
    {
        attackDamageValue = 2.0f;
        price = 50;
        text = "장검 획득 \n공격력 +2";
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
            UIManager.Instance.SetSlot(PlayerSkillType.SpinningSlash);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
