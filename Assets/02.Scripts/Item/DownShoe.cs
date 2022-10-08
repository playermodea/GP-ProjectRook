using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownShoe : Item
{
    private float evaPointValue;
    private int movePointValue;

    private void Awake()
    {
        evaPointValue = 1.0f;
        movePointValue = 2;
        price = 50;
        text = "D-신발 획득 \n회피력 +1%, [↓] +2";
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
            collision.gameObject.GetComponent<Player>().SetEvasion(evaPointValue);
            collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.DOWN);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
