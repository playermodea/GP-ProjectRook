using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectTile1 : PlayerStatus
{
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition =
            new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));

        transform.position = mousePosition;

        //숫자값을 status의 행동력값으로 바꿔보자.
        if (Mathf.Abs(transform.localPosition.x) > 2.0f ||
            Mathf.Abs(transform.localPosition.y) > 2.0f)
        {
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.blue; 
        }
    }


}
