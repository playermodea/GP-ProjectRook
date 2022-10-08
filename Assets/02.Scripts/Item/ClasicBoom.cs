﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClasicBoom : Item
{
    private float attackDamageValue;

    private void Awake()
    {
        attackDamageValue = 3.0f;
        price = 50;
        text = "폭탄 획득 \n공격력 +3";
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
            UIManager.Instance.SetSlot(PlayerSkillType.ThrowingBoom);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
