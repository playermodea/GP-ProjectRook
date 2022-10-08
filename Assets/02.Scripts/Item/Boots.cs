using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Item
{
    private float hpValue;
    private int movePointValue;

    private void Awake()
    {
        hpValue = 2.0f;
        movePointValue = 2;
        price = 50;
        text = "부츠 획득 \n체력 +1, [↓] +2";
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
            collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.DOWN);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
