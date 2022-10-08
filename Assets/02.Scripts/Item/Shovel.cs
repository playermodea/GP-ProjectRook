using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : Item
{
    private float hpValue;
    private int TurnValue;

    private void Awake()
    {
        hpValue = 2.0f;
        TurnValue = -1;
        price = 50;
        text = "마법 삽 획득 \n체력 +1, 행동력 -1";
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
            collision.gameObject.GetComponent<Player>().SetMaxHP(hpValue);
            collision.gameObject.GetComponent<Player>().SetMaxActionPoint(TurnValue);
            UIManager.Instance.SetSlot(PlayerSkillType.Digging);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
