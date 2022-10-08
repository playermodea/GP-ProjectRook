using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floor_script : MonoBehaviour
{
    public Text Floor;
    // Start is called before the first frame update
    void Start()
    {
        Floor.text = ("1");
        //if 플레이어가 층이동을 하지 않았다면
        //Floor.text = ("1");

        //if 플레이어가 층이동을 했다면
        //한 횟수 출력
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
