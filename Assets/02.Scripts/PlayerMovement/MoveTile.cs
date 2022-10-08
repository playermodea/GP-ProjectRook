using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MoveTile : PlayerStatus//플레이어 행동력만큼 하이라이트
{
    public Tilemap tilemap;
    public TileBase changeTile;
    public TileBase selectTile;

    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //숫자값을 status의 행동력값으로 바꿔보자.
        if (Mathf.Abs(transform.localPosition.x) > 2.1f ||
            Mathf.Abs(transform.localPosition.y) > 2.1f)
        {
            sr.color = Color.black;
            
        }
        else
        {
            tilemap.SetTile(new Vector3Int((int)(transform.position.x + 2),
                (int)(transform.position.y + 2), 0), changeTile);
        }//지금은 움직일때마다 +2,+2위에 생성됨. 이걸 주변 행동력에 맞게 생성되게 바꿔보자.
    }


}
