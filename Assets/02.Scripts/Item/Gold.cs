using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Item
{
    private int value;

    private void Awake()
    {
        value = 10;
        text = "10골드 획득";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ShowText(text);
            collision.gameObject.GetComponent<Player>().SetGold(value);
            AudioPlay.Instance.PlaySound("Get_Coin");
            ScoreManager.Instance.SetTotalGold();
            Destroy(gameObject);
        }
    }
}
