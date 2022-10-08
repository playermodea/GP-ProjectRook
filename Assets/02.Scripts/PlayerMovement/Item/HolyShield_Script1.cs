using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyShield_Script1 : MonoBehaviour //HP+1, 회피력+0.1
{
    private GameObject manager;
    private GameObject holyshield;
    private GameObject player;

    private float attackDamage;
    private float eva;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        manager = GameObject.Find("Manager");
        holyshield = GameObject.Find("HolyShield");
        player = GameObject.Find("Player");
    }
    void OnTriggerEnter(Collider item)
    {
        if (item.tag == "Player")
        {
            Debug.Log("아이템획득! HolyShield");

            eva += 10.0f;
            //임시로 UI만 바뀌는거 확인

            //eva = player.GetComponent<Player>().evaPoint + 0.1f; 
            //evaPoint가 private로 지정되어있어서 값을 불러올수없음.
            //manager.GetComponent<UIManager>().SetPlayerDMGAndEva(player.GetComponent<Player>().attackDamage, eva);
            Destroy(holyshield);
            //아령먹고 홀리쉴드먹었을때 공격력이 다시 0으로 돌아감.

            //HP UI바뀌게 새로 만들어야함.
        }
    }
}
