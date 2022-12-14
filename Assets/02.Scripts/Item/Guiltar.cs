using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guiltar : Item
{
    private int movePointValue;

    private void Awake()
    {
        movePointValue = 1;
        price = 50;
        text = "기타 획득 \n[전방향] +1";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            if (!CheckInteraction(collision.GetComponent<Player>()))
            {
                return;
            }

            ShowText(text);
            for (int i = 0; i < 8; i++)
            {
                collision.gameObject.GetComponent<Player>().SetMovePoint(movePointValue, Direction.UP + i);
            }
            UIManager.Instance.SetSlot(PlayerSkillType.SonicWave);
            ScoreManager.Instance.SetTotalItem();
            Destroy(gameObject);
        }
    }

}
