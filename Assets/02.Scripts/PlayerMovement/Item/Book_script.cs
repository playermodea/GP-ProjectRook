using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_script : MonoBehaviour
{
    private int value;
    private GameObject player;

    private void Awake()
    {
        value = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Debug.Log("아이템획득 - Book");
            collision.gameObject.GetComponent<Player>().SetMaxActionPoint(value);
            //턴넘기기전에 이미 차있음. 턴종료시 값이 올라가게 바꿔줘야할듯?
            Destroy(gameObject);
        }
    }
}
