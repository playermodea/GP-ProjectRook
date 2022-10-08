using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallJar : Item
{
    private float value;

    private void Awake()
    {
        value = 2.0f;
        price = 50;
        text = "작은 항아리 획득 \n체력 +1";
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
            collision.gameObject.GetComponent<Player>().SetMaxHP(value);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
