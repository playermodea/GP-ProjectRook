using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private int enemyTurnCount;
    [SerializeField] private int totalTurnCount;
    [SerializeField] private int maxTurnCount;
    [SerializeField] private int currentTurnEnemyCount;
    [SerializeField] private int currentTurnTrapCount;
    [SerializeField] private int enemyDeadCount;
    [SerializeField] private int enemyMaxDead;
    [SerializeField] private bool enemyDeadLock;

    private GameObject player;
    private GameObject camera;
    private FloorTile currentMap;

    private static TurnManager instance;

    public static TurnManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<TurnManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object TurnManager");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        enemyTurnCount = 0;
        totalTurnCount = 0;
        maxTurnCount = 0;
        currentTurnEnemyCount = 0;
        currentTurnTrapCount = 0;
        enemyDeadCount = 0;
        enemyMaxDead = 0;
        enemyDeadLock = false;

        camera = GameObject.FindWithTag("MainCamera");
    }

    // Player 찾기
    public void SetPlayer()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Player 계획 시작
    public void PlayerPlanTurn()
    {
        Player playerSet = player.GetComponent<Player>();

        UIManager.Instance.SetEnergySlot(playerSet.maxActionPoint, playerSet.maxActionPoint);
        playerSet.curActionPoint = 0;
        playerSet.isPlaning = true;
        playerSet.isMovePlaning = true;
        playerSet.isTurn = false;
        playerSet.selectTile.SetActive(true);
        //playerSet.isDamaged = false;
    }

    // Player 턴 시작
    public void PlayerTurnStart()
    {
        Player playerSet = player.GetComponent<Player>();

        if (totalTurnCount == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                playerSet.afterimage[i].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                playerSet.afterimage[i].SetActive(false);
            }

            playerSet.selectTile.SetActive(false);
            maxTurnCount = playerSet.curActionPoint;
        }
        //playerSet.isDamaged = false;
        playerSet.isTurn = true;
        playerSet.Action();
    }

    // Player 턴 종료
    public void PlayerTurnEnd()
    {
        Player playerSet = player.GetComponent<Player>();

        playerSet.isTurn = false;

        if (!enemyDeadLock)
        {
            EnemyTurnStart();
        }
        else
        {
            StartCoroutine(EnemyDeadCheck());
        }
    }

    // Enemy 리스트 턴 시작
    public void EnemyTurnStart()
    {
        Player playerSet = player.GetComponent<Player>();

        CheckEnemyDie();
        currentTurnTrapCount = currentMap.TrapCount();
        currentTurnEnemyCount = currentMap.EnemyCount();

        if (currentMap.TrapCount() != 0)
        {
            for (int i = 0; i < currentMap.TrapCount(); i++)
            {
                currentMap.trapList[i].GetComponent<Trap>().isTurn = true;
                currentMap.trapList[i].GetComponent<ITrap>().Action();
            }
        }
        if (currentMap.EnemyCount() != 0)
        {
            for (int i = 0; i < currentMap.EnemyCount(); i++)
            {
                currentMap.enemyList[i].GetComponent<Enemy>().isTurn = true;
                currentMap.enemyList[i].GetComponent<Enemy>().isDamaged = false;
                currentMap.enemyList[i].GetComponent<IEnemy>().Action();
            }
        }
        else if (currentMap.TrapCount() == 0)
        {
            totalTurnCount++;
            if (totalTurnCount < maxTurnCount)
            {
                PlayerTurnStart();
            }
            else
            {
                totalTurnCount = 0;
                PlayerPlanTurn();
            }
        }
    }

    // Enemy 리스트 턴 종료
    public void EnemyTurnEnd()
    {
        Player playerSet = player.GetComponent<Player>();

        if (currentMap.EnemyCount() != 0 || currentMap.TrapCount() != 0)
        {
            enemyTurnCount++;

            if (enemyTurnCount == (currentTurnEnemyCount + currentTurnTrapCount))
            {
                enemyTurnCount = 0;
                totalTurnCount++;
                CheckEnemyDie();
                CheckTrapDestroy();
                currentMap.RefreshTile();

                if (totalTurnCount < maxTurnCount)
                {
                    StartCoroutine(NextTurnStart());
                }
                else
                {
                    totalTurnCount = 0;
                    PlayerPlanTurn();
                }
            }
        }
    }

    // 죽은 Enemy 확인 후 삭제
    public void CheckEnemyDie()
    {
        for (int i = currentMap.EnemyCount() - 1; i >= 0; i--)
        {
            if (currentMap.enemyList[i].GetComponent<Enemy>().isDead)
            {
                if (currentMap.enemyList[i].transform.parent != null && currentMap.enemyList[i].transform.parent.tag == "Boss")
                {
                    Destroy(currentMap.enemyList[i].transform.parent.gameObject);
                }
                else
                {
                    Destroy(currentMap.enemyList[i]);
                }
                currentMap.enemyList.RemoveAt(i);
                ScoreManager.Instance.SetTotalKillCount();
            }
        }

        if (currentMap.EnemyCount() == 0)
        {
            currentMap.DoorAction();
        }
    }

    // 파괴된 함정 체크
    public void CheckTrapDestroy()
    {
        for (int i = currentMap.TrapCount() - 1; i >= 0; i--)
        {
            if (currentMap.trapList[i].GetComponent<Trap>().isDestory)
            {
                Destroy(currentMap.trapList[i]);
                currentMap.trapList.RemoveAt(i);
            }
        }
    }

    // 현재 Map 변경
    public void MapSwitching(FloorTile map, bool isSwitchingStage = false)
    {
        currentMap = map;
        currentMap.DoorAction();
        totalTurnCount = 0;
        enemyTurnCount = 0;
        camera.GetComponent<MainCamera>().CameraMove(currentMap.transform.position + new Vector3(12.7f, -7.5f, -10.0f));
    }

    // Stage 이동
    public void StageSwitching()
    {
        player.transform.position = new Vector3(8.5f, -8.5f, 0.0f);
        camera.transform.position = new Vector3(12.7f, -7.5f, -10.0f);
        player.GetComponent<Player>().StageSwitching();
    }

    // 카메라 이동 완료 후 현재 맵에서 다시 플레이어 계획턴 시작
    public void MapSwitchingComplete()
    {
        PlayerPlanTurn();
    }

    // Enemy Dead 애니메이션이 실행 중일 때 Enemy 턴을 조금 지연시킴
    public void EnemyDeadAnimationDelay()
    {
        enemyDeadLock = true;
        enemyMaxDead++;
    }

    // Enemy Dead 애니메이션 전부 재생 후 Enemy 턴 실행
    public void EnemyDeadAnimationComplete()
    {
        enemyDeadCount++;

        if (enemyDeadCount == enemyMaxDead)
        {
            enemyDeadCount = 0;
            enemyMaxDead = 0;
            enemyDeadLock = false;
            //EnemyTurnStart();
        }
    }

    private IEnumerator NextTurnStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);

            if (player.GetComponent<Player>().isDead)
            {
                break;
            }

            if (!player.GetComponent<Player>().isDamaged)
            {
                PlayerTurnStart();
                break;
            }
        }
    }

    private IEnumerator EnemyDeadCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);

            if (!enemyDeadLock)
            {
                EnemyTurnStart();
                break;
            }
        }
    }
}
