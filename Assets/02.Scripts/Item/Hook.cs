using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : Item
{
    private int actionPointValue;
    private int movePointValue;

    private void Awake()
    {
        actionPointValue = -1;
        movePointValue = 2;
        price = 50;
        text = "갈고리 획득 \n행동력 -1, [↖] +2";
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
            collision.gameObject.GetComponent<Player>().SetMaxActionPoint(actionPointValue);
            collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.LEFT_UP);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
