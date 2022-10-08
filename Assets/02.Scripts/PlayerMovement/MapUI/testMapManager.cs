using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMapManager : MonoBehaviour
{
    private int maxMapNumber = 6;
    private float interval = 34.0f;

    public Material DefaultBackground; //한번도 가본적없음
    public Material VisitedBack; //방문했지만 지금있는곳 아님
    public Material PlayerInRoom; //지금 있는곳
    // DefaultRoom
    [Header("Player Spawn Map")]
    public GameObject defaultDMap;
    public GameObject defaultLMap;
    public GameObject defaultRMap;
    public GameObject defaultUMap;
    public GameObject defaultLDMap;
    public GameObject defaultLRMap;
    public GameObject defaultLUMap;
    public GameObject defaultRDMap;
    public GameObject defaultRUMap;
    public GameObject defaultUDMap;
    public GameObject defaultLRDMap;
    public GameObject defaultLRUMap;
    public GameObject defaultLUDMap;
    public GameObject defaultRUDMap;
    public GameObject defaultAllDirectionMap;

    public GameObject MiniMap_Room;

    [Header("Treasure Map")]
    public GameObject defaultTreasureDMap;
    public GameObject defaultTreasureLMap;
    public GameObject defaultTreasureRMap;
    public GameObject defaultTreasureUMap;

    public GameObject TreasureMinimap_Room;

    [Header("Basic Map")]
    /////////////////////// 1개
    // 위쪽 1개
    public GameObject[] UMap;
    // 아래 1개
    public GameObject[] DMap;
    // 오른쪽 1개
    public GameObject[] RMap;
    // 왼쪽 1개
    public GameObject[] LMap;
    /////////////////////// 1개

    /////////////////////// 2개
    // 위쪽 2개
    public GameObject[] UDMap;
    // 왼쪽 2개
    public GameObject[] LDMap;
    public GameObject[] LRMap;
    public GameObject[] LUMap;
    // 오른쪽 2개
    public GameObject[] RDMap;
    public GameObject[] RUMap;
    /////////////////////// 2개

    /////////////////////// 3개
    // 왼쪽 3개
    public GameObject[] LRDMap;
    public GameObject[] LRUMap;
    public GameObject[] LUDMap;
    // 오른쪽 3개
    public GameObject[] RUDMap;
    /////////////////////// 3개

    // 사방면
    public GameObject[] AllDirectionMap;

    public bool isKey = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isKey)
        {
            CreateRandomMap();
            isKey = true;
        }
        if (Input.GetKeyDown(KeyCode.H) && isKey)
        {
            isKey = false;
        }
    }

    private void CreateRandomMap()
    {
        MapInfo[] tempArray = new MapInfo[maxMapNumber];
        tempArray[0].x = 0.0f;
        tempArray[0].y = 0.0f;
        tempArray[0].posReady = true;

        SetMapInfo(tempArray);
        SetSpecialMap(tempArray);
        CheckPassableDirection(tempArray);

        CreateFirstMap(tempArray[0]);
        for (int i = 1; i < tempArray.Length; i++)
        {
            if (!tempArray[i].isTreasureMap)
            {
                CreateMap(tempArray[i]);
            }
            else
            {
                CreateTreasureMap(tempArray[i]);
            }
        }
    }

    private void CreateFirstMap(MapInfo map)
    {
        if (map.passableUp && map.passableRight && map.passableDown && map.passableLeft)
        {
            Instantiate(defaultAllDirectionMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableUp && map.passableRight && map.passableDown)
        {
            Instantiate(defaultRUDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableRight && map.passableDown && map.passableLeft)
        {
            Instantiate(defaultLRDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableUp && map.passableRight && map.passableLeft)
        {
            Instantiate(defaultLRUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableUp && map.passableDown && map.passableLeft)
        {
            Instantiate(defaultLUDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableUp && map.passableDown)
        {
            Instantiate(defaultUDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableDown && map.passableLeft)
        {
            Instantiate(defaultLDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableRight && map.passableLeft)
        {
            Instantiate(defaultLRMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableUp && map.passableLeft)
        {
            Instantiate(defaultLUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableDown && map.passableRight)
        {
            Instantiate(defaultRDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableUp && map.passableRight)
        {
            Instantiate(defaultRUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableUp)
        {
            Instantiate(defaultUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableRight)
        {
            Instantiate(defaultRMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableDown)
        {
            Instantiate(defaultDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        if (map.passableLeft)
        {
            Instantiate(defaultLMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            //MiniMap_Room.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            return;
        }
        MiniMap_Room.GetComponent<SpriteRenderer>().material = PlayerInRoom;
    }

    private void CreateTreasureMap(MapInfo map)
    {
        if (map.passableUp)
        {
            Instantiate(defaultTreasureUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(TreasureMinimap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableRight)
        {
            Instantiate(defaultTreasureRMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(TreasureMinimap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableDown)
        {
            Instantiate(defaultTreasureDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(TreasureMinimap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableLeft)
        {
            Instantiate(defaultTreasureLMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(TreasureMinimap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
    }

    private void CreateMap(MapInfo map)
    {
        if (map.passableUp && map.passableRight && map.passableDown && map.passableLeft)
        {
            int randomIndex = Random.Range(0, AllDirectionMap.Length);
            Instantiate(AllDirectionMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableUp && map.passableRight && map.passableDown)
        {
            int randomIndex = Random.Range(0, RUDMap.Length);
            Instantiate(RUDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableRight && map.passableDown && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LRDMap.Length);
            Instantiate(LRDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableUp && map.passableRight && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LRUMap.Length);
            Instantiate(LRUMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableUp && map.passableDown && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LUDMap.Length);
            Instantiate(LUDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableUp && map.passableDown)
        {
            int randomIndex = Random.Range(0, UDMap.Length);
            Instantiate(UDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableDown && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LDMap.Length);
            Instantiate(LDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableRight && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LRMap.Length);
            Instantiate(LRMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableUp && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LUMap.Length);
            Instantiate(LUMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableDown && map.passableRight)
        {
            int randomIndex = Random.Range(0, RDMap.Length);
            Instantiate(RDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableUp && map.passableRight)
        {
            int randomIndex = Random.Range(0, RUMap.Length);
            Instantiate(RUMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableUp)
        {
            int randomIndex = Random.Range(0, UMap.Length);
            Instantiate(UMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableRight)
        {
            int randomIndex = Random.Range(0, RMap.Length);
            Instantiate(RMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableDown)
        {
            int randomIndex = Random.Range(0, DMap.Length);
            Instantiate(DMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
        if (map.passableLeft)
        {
            int randomIndex = Random.Range(0, LMap.Length);
            Instantiate(LMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity);
            Instantiate(MiniMap_Room, new Vector3(map.x + 500, map.y - 500, 0.0f), Quaternion.identity);
            return;
        }
    }

    private void SetMapInfo(MapInfo[] mapArray)
    {
        int count = 1;
        while (count != maxMapNumber)
        {
            for (int i = 0; i < mapArray.Length; i++)
            {
                bool create = (Random.value > 0.5f);
                int dir = Random.Range(0, 4);

                if (mapArray[i].posReady && create)
                {
                    if (dir == 0)
                    {
                        if (CheckOverlapMap(mapArray[i].x, mapArray[i].y + interval, mapArray))
                        {
                            for (int j = 0; j < mapArray.Length; j++)
                            {
                                if (!mapArray[j].posReady)
                                {
                                    mapArray[j].x = mapArray[i].x;
                                    mapArray[j].y = mapArray[i].y + interval;
                                    mapArray[j].posReady = true;
                                    count++;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if (dir == 1)
                    {
                        if (CheckOverlapMap(mapArray[i].x + interval, mapArray[i].y, mapArray))
                        {
                            for (int j = 0; j < mapArray.Length; j++)
                            {
                                if (!mapArray[j].posReady)
                                {
                                    mapArray[j].x = mapArray[i].x + interval;
                                    mapArray[j].y = mapArray[i].y;
                                    mapArray[j].posReady = true;
                                    count++;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if (dir == 2)
                    {
                        if (CheckOverlapMap(mapArray[i].x, mapArray[i].y - interval, mapArray))
                        {
                            for (int j = 0; j < mapArray.Length; j++)
                            {
                                if (!mapArray[j].posReady)
                                {
                                    mapArray[j].x = mapArray[i].x;
                                    mapArray[j].y = mapArray[i].y - interval;
                                    mapArray[j].posReady = true;
                                    count++;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if (dir == 3)
                    {
                        if (CheckOverlapMap(mapArray[i].x - interval, mapArray[i].y, mapArray))
                        {
                            for (int j = 0; j < mapArray.Length; j++)
                            {
                                if (!mapArray[j].posReady)
                                {
                                    mapArray[j].x = mapArray[i].x - interval;
                                    mapArray[j].y = mapArray[i].y;
                                    mapArray[j].posReady = true;
                                    count++;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

            }
        }
    }

    private void SetSpecialMap(MapInfo[] mapArray)
    {
        int treasureMapPassableDir = Random.Range(0, 4);
        float getXpos = 0.0f;
        float getYpos = 0.0f;
        int getIndex = 0;

        while (getIndex == 0)
        {
            treasureMapPassableDir = Random.Range(0, 4);

            if (treasureMapPassableDir == 0)
            {
                for (int i = 1; i < mapArray.Length; i++)
                {
                    if (getYpos >= mapArray[i].y)
                    {
                        getYpos = mapArray[i].y;
                        getIndex = i;
                    }
                }
                mapArray[getIndex].isTreasureMap = true;
                mapArray[getIndex].passableUp = true;
            }
            else if (treasureMapPassableDir == 1)
            {
                for (int i = 1; i < mapArray.Length; i++)
                {
                    if (getXpos >= mapArray[i].x)
                    {
                        getXpos = mapArray[i].x;
                        getIndex = i;
                    }
                }
                mapArray[getIndex].isTreasureMap = true;
                mapArray[getIndex].passableRight = true;
            }
            else if (treasureMapPassableDir == 2)
            {
                for (int i = 1; i < mapArray.Length; i++)
                {
                    if (getYpos <= mapArray[i].y)
                    {
                        getYpos = mapArray[i].y;
                        getIndex = i;
                    }
                }
                mapArray[getIndex].isTreasureMap = true;
                mapArray[getIndex].passableDown = true;
            }
            else if (treasureMapPassableDir == 3)
            {
                for (int i = 1; i < mapArray.Length; i++)
                {
                    if (getXpos <= mapArray[i].x)
                    {
                        getXpos = mapArray[i].x;
                        getIndex = i;
                    }
                }
                mapArray[getIndex].isTreasureMap = true;
                mapArray[getIndex].passableLeft = true;
            }
        }
    }

    private void CheckPassableDirection(MapInfo[] mapArray)
    {
        for (int i = 0; i < mapArray.Length; i++)
        {
            for (int j = 0; j < mapArray.Length; j++)
            {
                if (mapArray[i].isTreasureMap)
                {
                    break;
                }

                if (i != j)
                {
                    if (mapArray[i].x - interval == mapArray[j].x && mapArray[i].y == mapArray[j].y)
                    {
                        if ((mapArray[j].isTreasureMap && mapArray[j].passableRight) || !mapArray[j].isTreasureMap)
                        {
                            mapArray[i].passableLeft = true;
                        }
                    }
                    else if (mapArray[i].x + interval == mapArray[j].x && mapArray[i].y == mapArray[j].y)
                    {
                        if ((mapArray[j].isTreasureMap && mapArray[j].passableLeft) || !mapArray[j].isTreasureMap)
                        {
                            mapArray[i].passableRight = true;
                        }
                    }
                    else if (mapArray[i].x == mapArray[j].x && mapArray[i].y - interval == mapArray[j].y)
                    {
                        if ((mapArray[j].isTreasureMap && mapArray[j].passableUp) || !mapArray[j].isTreasureMap)
                        {
                            mapArray[i].passableDown = true;
                        }
                    }
                    else if (mapArray[i].x == mapArray[j].x && mapArray[i].y + interval == mapArray[j].y)
                    {
                        if ((mapArray[j].isTreasureMap && mapArray[j].passableDown) || !mapArray[j].isTreasureMap)
                        {
                            mapArray[i].passableUp = true;
                        }
                    }
                }
            }
        }
    }

    private bool CheckOverlapMap(float x, float y, MapInfo[] mapArray)
    {
        for (int i = 0; i < mapArray.Length; i++)
        {
            if (mapArray[i].posReady && x == mapArray[i].x && y == mapArray[i].y)
            {
                return false;
            }
        }

        return true;
    }
}
