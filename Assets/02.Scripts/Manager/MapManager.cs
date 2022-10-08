using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private float interval = 19.0f;
    private int specialMapNumber = 3;

    public int stageNumber;
    public int endStageNumber;
    public int maxMapNumber;
    public bool end;
    public GameObject minimapCam;

    private List<GameObject> mapList = new List<GameObject>();

    #region Player Spawn Map
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
    #endregion

    #region Treasure Map
    [Header("Treasure Map")]
    public GameObject defaultTreasureDMap;
    public GameObject defaultTreasureLMap;
    public GameObject defaultTreasureRMap;
    public GameObject defaultTreasureUMap;
    #endregion

    #region Basic Map
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
    #endregion

    #region Store Map
    //Store Map
    [Header("Store Map")]
    public GameObject defaultStoreDMap;
    public GameObject defaultStoreLMap;
    public GameObject defaultStoreRMap;
    public GameObject defaultStoreUMap;
    #endregion

    #region Boss Map
    //Boss Map
    [Header("Boss Map")]
    public GameObject[] defaultBossDMap;
    public GameObject[] defaultBossLMap;
    public GameObject[] defaultBossRMap;
    public GameObject[] defaultBossUMap;
    #endregion

    // Player
    [Header("Spawn Object")]
    public GameObject player;

    private static MapManager instance;

    public static MapManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<MapManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object MapManager");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        CreateRandomMap();
    }

    private void Start()
    {
        MusicPlay.Instance.PlayBackgroundMusic(stageNumber);
        Instantiate(player, new Vector3(8.5f, -8.5f, 0.0f), Quaternion.identity);
    }

    private void CreateRandomMap()
    {
        MapInfo[] tempArray = new MapInfo[maxMapNumber];
        tempArray[0].x = 0.0f;
        tempArray[0].y = 0.0f;
        tempArray[0].posReady = true;

        SetMonsterMapInfo(tempArray);
        SetBossMap(tempArray);
        SetTreasureMap(tempArray);
        SetStoreMap(tempArray);
        CheckPassableDirection(tempArray);

        CreateFirstMap(tempArray[0]);
        for (int i = 1; i < tempArray.Length; i++)
        {
            if (tempArray[i].isBossMap)
            {
                CreateBossMap(tempArray[i]);
            }
            else if (tempArray[i].isTreasureMap)
            {
                CreateTreasureMap(tempArray[i]);
            }
            else if (tempArray[i].isStoreMap)
            {
                CreateStoreMap(tempArray[i]);
            }
            else
            {
                CreateMonsterMap(tempArray[i]);
            }
        }

        SetMinimapCameraPosition();
        UIManager.Instance.Stage_Number();
    }

    private void SetMinimapCameraPosition()
    {
        float yMax = mapList[0].transform.GetChild(0).GetChild(0).position.y;
        float yMin = mapList[0].transform.GetChild(0).GetChild(0).position.y;
        float xMax = mapList[0].transform.GetChild(0).GetChild(0).position.x;
        float xMin = mapList[0].transform.GetChild(0).GetChild(0).position.x;

        for (int i = 1; i < mapList.Count; i++)
        {
            float miniX = mapList[i].transform.GetChild(0).GetChild(0).position.x;
            float miniY = mapList[i].transform.GetChild(0).GetChild(0).position.y;

            if (miniX > xMax)
            {
                xMax = miniX;
            }
            else if (miniX < xMin)
            {
                xMin = miniX;
            }

            if (miniY > yMax)
            {
                yMax = miniY;
            }
            else if (miniY < yMin)
            {
                yMin = miniY;
            }
        }

        float centerX = (xMax + xMin) / 2;
        float centerY = (yMax + yMin) / 2;

        minimapCam.transform.position = new Vector3(centerX, centerY, -10.0f);
    }

    private void CreateFirstMap(MapInfo map)
    {
        if (map.passableUp && map.passableRight && map.passableDown && map.passableLeft)
        {
            mapList.Add(Instantiate(defaultAllDirectionMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableRight && map.passableDown)
        {
            mapList.Add(Instantiate(defaultRUDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableRight && map.passableDown && map.passableLeft)
        {
            mapList.Add(Instantiate(defaultLRDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableRight && map.passableLeft)
        {
            mapList.Add(Instantiate(defaultLRUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableDown && map.passableLeft)
        {
            mapList.Add(Instantiate(defaultLUDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableDown)
        {
            mapList.Add(Instantiate(defaultUDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableDown && map.passableLeft)
        {
            mapList.Add(Instantiate(defaultLDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableRight && map.passableLeft)
        {
            mapList.Add(Instantiate(defaultLRMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableLeft)
        {
            mapList.Add(Instantiate(defaultLUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableDown && map.passableRight)
        {
            mapList.Add(Instantiate(defaultRDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableRight)
        {
            mapList.Add(Instantiate(defaultRUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp)
        {
            mapList.Add(Instantiate(defaultUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableRight)
        {
            mapList.Add(Instantiate(defaultRMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableDown)
        {
            mapList.Add(Instantiate(defaultDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableLeft)
        {
            mapList.Add(Instantiate(defaultLMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
    }

    private void CreateBossMap(MapInfo map)
    {
        if (map.passableUp)
        {
            int randomIndex = Random.Range(0, defaultBossUMap.Length);
            mapList.Add(Instantiate(defaultBossUMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableRight)
        {
            int randomIndex = Random.Range(0, defaultBossRMap.Length);
            mapList.Add(Instantiate(defaultBossRMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableDown)
        {
            int randomIndex = Random.Range(0, defaultBossDMap.Length);
            mapList.Add(Instantiate(defaultBossDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableLeft)
        {
            int randomIndex = Random.Range(0, defaultBossLMap.Length);
            mapList.Add(Instantiate(defaultBossLMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
    }

    private void CreateTreasureMap(MapInfo map)
    {
        if (map.passableUp)
        {
            mapList.Add(Instantiate(defaultTreasureUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableRight)
        {
            mapList.Add(Instantiate(defaultTreasureRMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableDown)
        {
            mapList.Add(Instantiate(defaultTreasureDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableLeft)
        {
            mapList.Add(Instantiate(defaultTreasureLMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
    }

    private void CreateStoreMap(MapInfo map)
    {
        if (map.passableUp)
        {
            mapList.Add(Instantiate(defaultStoreUMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableRight)
        {
            mapList.Add(Instantiate(defaultStoreRMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableDown)
        {
            mapList.Add(Instantiate(defaultStoreDMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableLeft)
        {
            mapList.Add(Instantiate(defaultStoreLMap, new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
    }

    private void CreateMonsterMap(MapInfo map)
    {
        if (map.passableUp && map.passableRight && map.passableDown && map.passableLeft)
        {
            int randomIndex = Random.Range(0, AllDirectionMap.Length);
            mapList.Add(Instantiate(AllDirectionMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableRight && map.passableDown)
        {
            int randomIndex = Random.Range(0, RUDMap.Length);
            mapList.Add(Instantiate(RUDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableRight && map.passableDown && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LRDMap.Length);
            mapList.Add(Instantiate(LRDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableRight && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LRUMap.Length);
            mapList.Add(Instantiate(LRUMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableDown && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LUDMap.Length);
            mapList.Add(Instantiate(LUDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableDown)
        {
            int randomIndex = Random.Range(0, UDMap.Length);
            mapList.Add(Instantiate(UDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableDown && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LDMap.Length);
            mapList.Add(Instantiate(LDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableRight && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LRMap.Length);
            mapList.Add(Instantiate(LRMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableLeft)
        {
            int randomIndex = Random.Range(0, LUMap.Length);
            mapList.Add(Instantiate(LUMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableDown && map.passableRight)
        {
            int randomIndex = Random.Range(0, RDMap.Length);
            mapList.Add(Instantiate(RDMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp && map.passableRight)
        {
            int randomIndex = Random.Range(0, RUMap.Length);
            mapList.Add(Instantiate(RUMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableUp)
        {
            int randomIndex = Random.Range(0, UMap.Length);
            mapList.Add(Instantiate(UMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableRight)
        {
            int randomIndex = Random.Range(0, RMap.Length);
            mapList.Add(Instantiate(RMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableDown)
        {
            int randomIndex = Random.Range(0, DMap.Length);
            mapList.Add(Instantiate(DMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
        if (map.passableLeft)
        {
            int randomIndex = Random.Range(0, LMap.Length);
            mapList.Add(Instantiate(LMap[randomIndex], new Vector3(map.x, map.y, 0.0f), Quaternion.identity));
            return;
        }
    }

    private void SetMonsterMapInfo(MapInfo[] mapArray)
    {
        int count = 1;
        while (count != maxMapNumber - specialMapNumber)
        {
            for (int i = 0; i < maxMapNumber - specialMapNumber; i++)
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

    private void SetBossMap(MapInfo[] mapArray)
    {
        int passableDir;
        int nearIndex;
        int readyMapNumber = maxMapNumber - specialMapNumber;

        while (true)
        {
            passableDir = Random.Range(0, 4);
            nearIndex = Random.Range(1, maxMapNumber - specialMapNumber);

            if (passableDir == 0 && stageNumber != 2)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x, mapArray[nearIndex].y - interval, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y - interval;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isBossMap = true;
                    mapArray[readyMapNumber].passableUp = true;
                    break;
                }
            }
            else if (passableDir == 1)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x - interval, mapArray[nearIndex].y, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x - interval;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isBossMap = true;
                    mapArray[readyMapNumber].passableRight = true;
                    break;
                }
            }
            else if (passableDir == 2)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x, mapArray[nearIndex].y + interval, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y + interval;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isBossMap = true;
                    mapArray[readyMapNumber].passableDown = true;
                    break;
                }
            }
            else if (passableDir == 3)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x + interval, mapArray[nearIndex].y, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x + interval;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isBossMap = true;
                    mapArray[readyMapNumber].passableLeft = true;
                    break;
                }
            }
        }
    }

    private void SetTreasureMap(MapInfo[] mapArray)
    {
        int passableDir;
        int nearIndex;
        int readyMapNumber = maxMapNumber - specialMapNumber + 1;

        while (true)
        {
            passableDir = Random.Range(0, 4);
            nearIndex = Random.Range(1, maxMapNumber - specialMapNumber);

            if (passableDir == 0)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x, mapArray[nearIndex].y - interval, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y - interval;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isTreasureMap = true;
                    mapArray[readyMapNumber].passableUp = true;
                    break;
                }
            }
            else if (passableDir == 1)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x - interval, mapArray[nearIndex].y, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x - interval;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isTreasureMap = true;
                    mapArray[readyMapNumber].passableRight = true;
                    break;
                }
            }
            else if (passableDir == 2)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x, mapArray[nearIndex].y + interval, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y + interval;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isTreasureMap = true;
                    mapArray[readyMapNumber].passableDown = true;
                    break;
                }
            }
            else if (passableDir == 3)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x + interval, mapArray[nearIndex].y, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x + interval;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isTreasureMap = true;
                    mapArray[readyMapNumber].passableLeft = true;
                    break;
                }
            }
        }
    }

    private void SetStoreMap(MapInfo[] mapArray)
    {
        int passableDir;
        int nearIndex;
        int readyMapNumber = maxMapNumber - specialMapNumber + 2;

        while (true)
        {
            passableDir = Random.Range(0, 4);
            nearIndex = Random.Range(1, maxMapNumber - specialMapNumber);

            if (passableDir == 0)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x, mapArray[nearIndex].y - interval, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y - interval;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isStoreMap = true;
                    mapArray[readyMapNumber].passableUp = true;
                    break;
                }
            }
            else if (passableDir == 1)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x - interval, mapArray[nearIndex].y, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x - interval;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isStoreMap = true;
                    mapArray[readyMapNumber].passableRight = true;
                    break;
                }
            }
            else if (passableDir == 2)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x, mapArray[nearIndex].y + interval, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y + interval;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isStoreMap = true;
                    mapArray[readyMapNumber].passableDown = true;
                    break;
                }
            }
            else if (passableDir == 3)
            {
                if (CheckOverlapMap(mapArray[nearIndex].x + interval, mapArray[nearIndex].y, mapArray))
                {
                    mapArray[readyMapNumber].x = mapArray[nearIndex].x + interval;
                    mapArray[readyMapNumber].y = mapArray[nearIndex].y;
                    mapArray[readyMapNumber].posReady = true;
                    mapArray[readyMapNumber].isStoreMap = true;
                    mapArray[readyMapNumber].passableLeft = true;
                    break;
                }
            }
        }
    }

    private void CheckPassableDirection(MapInfo[] mapArray)
    {
        for (int i = 0; i < mapArray.Length; i++)
        {
            for (int j = 0; j < mapArray.Length; j++)
            {
                if (mapArray[i].isTreasureMap || mapArray[i].isBossMap || mapArray[i].isStoreMap)
                {
                    break;
                }

                if (i != j)
                {
                    if (mapArray[i].x - interval == mapArray[j].x && mapArray[i].y == mapArray[j].y)
                    {
                        if (((mapArray[j].isBossMap && mapArray[j].passableRight) || !mapArray[j].isBossMap) &&
                            ((mapArray[j].isTreasureMap && mapArray[j].passableRight) || !mapArray[j].isTreasureMap) &&
                            ((mapArray[j].isStoreMap && mapArray[j].passableRight) || !mapArray[j].isStoreMap))
                        {
                            mapArray[i].passableLeft = true;
                        }
                    }
                    else if (mapArray[i].x + interval == mapArray[j].x && mapArray[i].y == mapArray[j].y)
                    {
                        if (((mapArray[j].isBossMap && mapArray[j].passableLeft) || !mapArray[j].isBossMap) &&
                            ((mapArray[j].isTreasureMap && mapArray[j].passableLeft) || !mapArray[j].isTreasureMap) &&
                            ((mapArray[j].isStoreMap && mapArray[j].passableLeft) || !mapArray[j].isStoreMap))
                        {
                            mapArray[i].passableRight = true;
                        }
                    }
                    else if (mapArray[i].x == mapArray[j].x && mapArray[i].y - interval == mapArray[j].y)
                    {
                        if (((mapArray[j].isBossMap && mapArray[j].passableUp) || !mapArray[j].isBossMap) &&
                            ((mapArray[j].isTreasureMap && mapArray[j].passableUp) || !mapArray[j].isTreasureMap) &&
                            ((mapArray[j].isStoreMap && mapArray[j].passableUp) || !mapArray[j].isStoreMap))
                        {
                            mapArray[i].passableDown = true;
                        }
                    }
                    else if (mapArray[i].x == mapArray[j].x && mapArray[i].y + interval == mapArray[j].y)
                    {
                        if (((mapArray[j].isBossMap && mapArray[j].passableDown) || !mapArray[j].isBossMap) &&
                            ((mapArray[j].isTreasureMap && mapArray[j].passableDown) || !mapArray[j].isTreasureMap) &&
                            ((mapArray[j].isStoreMap && mapArray[j].passableDown) || !mapArray[j].isStoreMap))
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

    private void DestroyCurrentStage()
    {
        for (int i = mapList.Count - 1; i >= 0; i--)
        {
            Destroy(mapList[i]);
            mapList.RemoveAt(i);
        }
    }

    public void CreateNextStage()
    {
        stageNumber++;
        maxMapNumber += 2;

        DestroyCurrentStage();
        CreateRandomMap();
        TurnManager.Instance.StageSwitching();
        MusicPlay.Instance.PlayBackgroundMusic(stageNumber);
    }

    public void DestroyGameScene()
    {
        for (int i = mapList.Count - 1; i >= 0; i--)
        {
            Destroy(mapList[i]);
            mapList.RemoveAt(i);
        }

        end = true;
        Destroy(GameObject.FindWithTag("Player"));
    }
}