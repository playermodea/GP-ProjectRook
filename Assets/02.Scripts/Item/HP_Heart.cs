using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Heart : Item
{
    private float hpValue;

    private void Awake()
    {
        hpValue = 2.0f;
        price = 25;
        text = "체력 한칸 회복";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (sell && collision.GetComponent<Player>().gold < price)
            {
                ShowText(notEnoughGoldText);
                return;
            }
            else if (sell)
            {
                ShowText(enoughGoldText);
                AudioPlay.Instance.PlaySound("Buy_Item");
                collision.GetComponent<Player>().SetGold(-price);
            }
            else if (!sell)
            {
                AudioPlay.Instance.PlaySound("Get_Heart");
            }

            ShowText(text);
            collision.gameObject.GetComponent<Player>().SetHP(hpValue);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
