using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clawhammer : Item
{
    private float value;

    private void Awake()
    {
        value = 4.0f;
        price = 50;
        text = "장도리 획득 \n공격력 +4";
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
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
