using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerStatus //캐릭터이동
{
    public float MoveSpeed;
    GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    Vector3 x;
    Vector3 inputPos;
    Vector2 dir;
    // Update is called once per frame
    void Update()
    {
        x = player.transform.position;

        //if (Input.GetMouseButton(0))
        //{ }
        inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = inputPos - transform.position;

        inputPos =
           new Vector2(Mathf.Round(inputPos.x), Mathf.Round(inputPos.y));

        //플레이어 좌표값 구하는법 찾기
        if (Input.GetMouseButton(0))
        {
            //숫자값을 status의 행동력값으로 바꿔보자.
            if (x.x < x.x + 2.1f ||
                    x.y < x.y + 2.1f)
            {
                transform.position = Vector2.MoveTowards
                    (transform.position, inputPos, Time.deltaTime * MoveSpeed);
            }
            //if (Mathf.Abs(transform.localPosition.x) < x.x+2.1f ||
            //    Mathf.Abs(transform.localPosition.y) < x.y+2.1f)

        }
    }
}
