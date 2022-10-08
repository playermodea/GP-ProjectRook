using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffusionExplosion : Item
{
    private float value;

    private void Awake()
    {
        value = 2.0f;
        price = 50;
        text = "확산 수류탄 획득 \n공격력 +2";
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
            collision.gameObject.GetComponent<Player>().SetDamage(value);
            UIManager.Instance.SetSlot(PlayerSkillType.DiffusionExplosion);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
