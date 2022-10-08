using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HolyShield : Item
{
    private float hpValue;
    private float evaValue;

    public Text dialog;

    private void Awake()
    {
        hpValue = 2.0f;
        evaValue = 10.0f;
        price = 50;
        text = "신성한 방패 획득 \n체력 +1, 회피력 +10%";
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
            collision.gameObject.GetComponent<Player>().SetEvasion(evaValue);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }
}
