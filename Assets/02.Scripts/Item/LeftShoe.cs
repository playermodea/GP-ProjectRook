using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftShoe : Item
{
    private float evaPointValue;
    private int movePointValue;

    private void Awake()
    {
        evaPointValue = 1.0f;
        movePointValue = 2;
        price = 50;
        text = "L-신발 획득 \n회피력 +1%, [←] +2";
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
            collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.LEFT);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
