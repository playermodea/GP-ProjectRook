using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Item
{
    private int value;

    private void Awake()
    {
        value = -1;
        price = 50;
        text = "행동수칙 책 획득 \n행동력 -1";
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
            collision.gameObject.GetComponent<Player>().SetMaxActionPoint(value);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
