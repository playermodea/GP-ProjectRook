using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbbell_script : MonoBehaviour //공격력 +2
{
    private GameObject manager;
    private GameObject dumbbell;
    private GameObject player;

    private float attackDamage;
    private readonly float eva;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        manager = GameObject.Find("Manager");
        dumbbell = GameObject.Find("Dumbbell");
        player = GameObject.Find("Player");
    }
    void OnTriggerEnter(Collider item)
    {
        if (item.tag == "Player")
        {
            Debug.Log("아이템획득! Dumbbell");
            attackDamage = player.GetComponent<Player>().attackDamage + 2.0f;
            //manager.GetComponent<UIManager>().SetPlayerDMGAndEva(attackDamage, eva);
            Destroy(dumbbell);
        }
    }
}
