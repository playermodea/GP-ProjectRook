using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected string text;
    protected string enoughGoldText;
    protected string notEnoughGoldText;
    public int price { get; protected set; }
    public bool sell { get; set; } = false;

    private void Start()
    {
        enoughGoldText = price + " 골드를 내고 아이템을 구매했습니다.";
        notEnoughGoldText = "골드가 부족합니다.";
    }

    protected virtual void ShowText(string msg)
    {
        UIManager.Instance.Dialog_Text(msg);
        UIManager.Instance.Full_Dialog_Text(msg);
    }

    protected virtual bool CheckInteraction(Player player)
    {
        if (sell && player.gold < price)
        {
            ShowText(notEnoughGoldText);
            return false;
        }
        else if (sell)
        {
            ShowText(enoughGoldText);
            AudioPlay.Instance.PlaySound("Buy_Item");
            player.SetGold(-price);
        }
        else if (!sell)
        {
            AudioPlay.Instance.PlaySound("Get_Item");
        }

        return true;
    }
}