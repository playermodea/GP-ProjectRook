using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dumbbell : Item
{
    private float value;

    private void Awake()
    {
        value = 6.0f;
        price = 50;
        text = "아령 획득\n 공격력 +6";
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
