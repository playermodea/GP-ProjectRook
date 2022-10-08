using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIManager.Instance.StageFadeOut();
            Destroy(gameObject);
        }
    }
}