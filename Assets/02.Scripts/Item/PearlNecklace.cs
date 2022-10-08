using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlNecklace : Item
{
    private float hpValue;
    private int actionPointValue;

    private void Awake()
    {
        hpValue = 2.0f;
        actionPointValue = -1;
        price = 50;
        text = "진주목걸이 획득 \n체력 +1, 행동력 -1";
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
            collision.gameObject.GetComponent<Player>().SetMaxActionPoint(actionPointValue);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}