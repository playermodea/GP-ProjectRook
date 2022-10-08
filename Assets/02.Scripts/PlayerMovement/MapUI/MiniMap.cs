using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject Mini_Map_Sprite;
    public Material inPlayer_Room;
    public Material Visited_Room;
    public Material UnVisited_Room;

    public FloorTile floorTile;

    private void Awake()
    {
        floorTile = GetComponentInParent<FloorTile>();
        Mini_Map_Sprite.gameObject.GetComponent<SpriteRenderer>().material = UnVisited_Room;
    }

    public void VisitiedCurrRoom()
    { 
        if(floorTile.PlayerCheck()) 
        {
            Mini_Map_Sprite.gameObject.GetComponent<SpriteRenderer>().material
            = inPlayer_Room;
        }
        else
        {
            if (floorTile.ActiveCheck())
            {
                Mini_Map_Sprite.gameObject.GetComponent<SpriteRenderer>().material
                = Visited_Room;
            }
            else
            {
                Mini_Map_Sprite.gameObject.GetComponent<SpriteRenderer>().material
                = UnVisited_Room;
            }
        }
    }
}//카메라가보는맵 = inPlayer_Room
//카메라가 지나간맵 = VIsited_Room
//카메라가 간적없는맵 = UnVisited_Room
//or 플레이어 위치체크해주는 함수있으면 좋을지도?
