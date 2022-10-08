using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorTile : MonoBehaviour
{
    private int minSize = 0;
    private int maxSize = 17;
    private int minMatrixSize = 0;
    private int maxMatrixSize = 16;
    private bool isActive = false;
    private bool inPlayer = false;
    private bool inBoss = false;

    private Tilemap floorTileMap;
    private Tilemap wallTileMap;
    private Tilemap underPlayerTileMap;

    public FloorTileInfo[,] tileInfo;
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> trapList = new List<GameObject>();
    public List<GameObject> doorList = new List<GameObject>();

    // 타일 정보 초기화
    private void Awake()
    {
        floorTileMap = GetComponent<Tilemap>();
        wallTileMap = transform.parent.transform.GetChild(2).GetComponent<Tilemap>();
        underPlayerTileMap = transform.parent.transform.GetChild(1).GetComponent<Tilemap>();

        // 각 맵의 바닥은 무조건 17 x 17
        tileInfo = new FloorTileInfo[maxSize, maxSize];

        // 바닥 기본 설정
        for (int i = minSize; i < maxSize; i++)
        {
            for (int j = minSize; j < maxSize; j++)
            {
                Vector3Int floorTilePos = floorTileMap.LocalToCell(new Vector3(j, 0.5f - i, 0));
                tileInfo[i, j].tilePos = floorTileMap.GetCellCenterWorld(floorTilePos);
                tileInfo[i, j].row = i;
                tileInfo[i, j].column = j;
                tileInfo[i, j].isTrap = false;

                if (wallTileMap.HasTile(floorTilePos))
                {
                    tileInfo[i, j].isPassable = false;
                    tileInfo[i, j].underPlayer = false;
                    tileInfo[i, j].tileObj = null;
                }
                else
                {
                    if (underPlayerTileMap.HasTile(floorTilePos))
                    {
                        tileInfo[i, j].underPlayer = true;
                    }
                    tileInfo[i, j].isPassable = true;
                    tileInfo[i, j].tileObj = null;
                }
            }
        }
    }

    // 타일 색상 변경
    private void SetTileColor(int row, int column, Color color)
    {
        Vector3Int vec3Int = floorTileMap.WorldToCell(tileInfo[row, column].tilePos);
        floorTileMap.SetTileFlags(vec3Int, TileFlags.None);
        floorTileMap.SetColor(vec3Int, color);
    }

    // underPlayer 타일 색상 변경
    private void SetUnderPlayerTileColor(int row, int column, Color color)
    {
        Vector3Int vec3Int = underPlayerTileMap.WorldToCell(tileInfo[row, column].tilePos);
        underPlayerTileMap.SetTileFlags(vec3Int, TileFlags.None);
        underPlayerTileMap.SetColor(vec3Int, color);
    }

    // 최대 타일 벗어나는 범위 선택 방지
    private TempTileInfo OverTilePrevention(int row, int column, Direction dir)
    {
        int tempCount;

        if (row < minMatrixSize)
        {
            tempCount = row;
            row = minMatrixSize;

            if (dir == Direction.LEFT_UP)
            {
                column -= tempCount;
            }
            else if (dir == Direction.UP_RIGHT)
            {
                column += tempCount;
            }
        }
        else if (row > maxMatrixSize)
        {
            tempCount = row - maxMatrixSize;
            row = maxMatrixSize;

            if (dir == Direction.DOWN_LEFT)
            {
                column += tempCount;
            }
            else if (dir == Direction.RIGHT_DOWN)
            {
                column -= tempCount;
            }
        }
        if (column < minMatrixSize)
        {
            tempCount = column;
            column = minMatrixSize;

            if (dir == Direction.LEFT_UP)
            {
                row -= tempCount;
            }
            else if (dir == Direction.DOWN_LEFT)
            {
                row += tempCount;
            }
        }
        else if (column > maxMatrixSize)
        {
            tempCount = column - maxMatrixSize;
            column = maxMatrixSize;

            if (dir == Direction.UP_RIGHT)
            {
                row += tempCount;
            }
            else if (dir == Direction.RIGHT_DOWN)
            {
                row -= tempCount;
            }
        }

        TempTileInfo tileInfo;
        tileInfo.row = row;
        tileInfo.column = column;

        return tileInfo;
    }

    // Player, Enemy 생성 시 현재 객체가 있는 타일들에 대한 변수 초기화
    public void InitTileObject(GameObject obj)
    {
        for (int i = minSize; i < maxSize; i++)
        {
            for (int j = minSize; j < maxSize; j++)
            {
                if (obj.transform.position == tileInfo[i, j].tilePos)
                {
                    if (obj.tag == "Player")
                    {
                        isActive = true;
                        inPlayer = true;
                        obj.GetComponent<Player>().movement.cellRow = i;
                        obj.GetComponent<Player>().movement.cellColumn = j;
                        tileInfo[i, j].isPassable = false;
                        tileInfo[i, j].isTrap = false;
                        tileInfo[i, j].tileObj = obj;
                    }
                    else if (obj.tag == "Enemy")
                    {
                        obj.GetComponent<Enemy>().movement.cellRow = i;
                        obj.GetComponent<Enemy>().movement.cellColumn = j;
                        tileInfo[i, j].isPassable = false;
                        tileInfo[i, j].isTrap = false;
                        tileInfo[i, j].tileObj = obj;
                        enemyList.Add(obj);
                    }
                    else if (obj.tag == "Trap")
                    {
                        obj.GetComponent<Trap>().cellRow = i;
                        obj.GetComponent<Trap>().cellColumn = j;
                        tileInfo[i, j].isTrap = true;
                        trapList.Add(obj);
                    }
                    else if (obj.tag == "Door")
                    {
                        obj.GetComponent<Door>().cellRow = i;
                        obj.GetComponent<Door>().cellColumn = j;
                        doorList.Add(obj);
                    }
                }
            }
        }
    }

    // Boss 생성 시 현재 객체가 있는 타일들에 대한 변수 초기화
    public void InitTileBossObject(GameObject obj)
    {
        for (int i = minSize; i < maxSize; i++)
        {
            for (int j = minSize; j < maxSize; j++)
            {
                if (obj.transform.position == tileInfo[i, j].tilePos)
                {
                    if (obj.tag == "Enemy")
                    {
                        inBoss = true;
                        tileInfo[i, j + 1].isPassable = false;
                        tileInfo[i, j + 1].tileObj = obj;
                        tileInfo[i + 1, j].isPassable = false;
                        tileInfo[i + 1, j].tileObj = obj;
                        tileInfo[i + 1, j + 1].isPassable = false;
                        tileInfo[i + 1, j + 1].tileObj = obj;
                    }
                }
            }
        }
    }

    // Player, Enemy가 이동하고자 하는 방향의 다음 타일 체크 후 반환
    public FloorTileInfo CheckNextTile(int row, int column, Direction dir)
    {
        int desRow = row;
        int desColumn = column;

        switch (dir)
        {
            case Direction.UP:
                desRow = row - 1;
                desColumn = column;
                break;
            case Direction.UP_RIGHT:
                desRow = row - 1;
                desColumn = column + 1;
                break;
            case Direction.RIGHT:
                desRow = row;
                desColumn = column + 1;
                break;
            case Direction.RIGHT_DOWN:
                desRow = row + 1;
                desColumn = column + 1;
                break;
            case Direction.DOWN:
                desRow = row + 1;
                desColumn = column;
                break;
            case Direction.DOWN_LEFT:
                desRow = row + 1;
                desColumn = column - 1;
                break;
            case Direction.LEFT:
                desRow = row;
                desColumn = column - 1;
                break;
            case Direction.LEFT_UP:
                desRow = row - 1;
                desColumn = column - 1;
                break;
            default:
                break;
        }

        if (desRow < minMatrixSize || desRow > maxMatrixSize || desColumn < minMatrixSize || desColumn > maxMatrixSize)
        {
            desRow = row;
            desColumn = column;
        }

        return tileInfo[desRow, desColumn];
    }

    // Player, Enemy가 이동하고자 하는 최종 목적지의 타일 체크 후 반환
    public FloorTileInfo CheckDestinationTile(int row, int column, Direction dir, int point)
    {
        int desRow = -1;
        int desColumn = -1;

        switch (dir)
        {
            case Direction.UP:
                desRow = row - point;
                desColumn = column;
                break;
            case Direction.UP_RIGHT:
                desRow = row - point;
                desColumn = column + point;
                break;
            case Direction.RIGHT:
                desRow = row;
                desColumn = column + point;
                break;
            case Direction.RIGHT_DOWN:
                desRow = row + point;
                desColumn = column + point;
                break;
            case Direction.DOWN:
                desRow = row + point;
                desColumn = column;
                break;
            case Direction.DOWN_LEFT:
                desRow = row + point;
                desColumn = column - point;
                break;
            case Direction.LEFT:
                desRow = row;
                desColumn = column - point;
                break;
            case Direction.LEFT_UP:
                desRow = row - point;
                desColumn = column - point;
                break;
            default:
                break;
        }

        TempTileInfo tempTile = OverTilePrevention(desRow, desColumn, dir);

        return tileInfo[tempTile.row, tempTile.column];
    }

    // Player, Enemy 이동 시 현재 객체가 있는 타일과 이동할 다음 타일에 대한 설정 변경
    public void SetTileInformation(GameObject obj, FloorTileInfo floorTileInfo)
    {
        if (obj.tag == "Player")
        {
            Player player = obj.GetComponent<Player>();
            tileInfo[player.movement.cellRow, player.movement.cellColumn].isPassable = true;
            tileInfo[player.movement.cellRow, player.movement.cellColumn].tileObj = null;
            tileInfo[floorTileInfo.row, floorTileInfo.column].isPassable = false;
            tileInfo[floorTileInfo.row, floorTileInfo.column].tileObj = obj;
        }
        else if (obj.tag == "Enemy")
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            tileInfo[enemy.movement.cellRow, enemy.movement.cellColumn].isPassable = true;
            tileInfo[enemy.movement.cellRow, enemy.movement.cellColumn].tileObj = null;
            tileInfo[floorTileInfo.row, floorTileInfo.column].isPassable = false;
            tileInfo[floorTileInfo.row, floorTileInfo.column].tileObj = obj;
        }
    }

    // Enemy 사망시 해당 Enemy가 있던 타일 설정 변경
    public void SetEnemyDieTile(GameObject obj)
    {
        if (obj.tag == "Enemy")
        {
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            tileInfo[enemy.cellRow, enemy.cellColumn].isPassable = true;
            tileInfo[enemy.cellRow, enemy.cellColumn].tileObj = null;
        }
    }

    // Boss 사망시 해당 Boss 있던 타일 설정 변경
    public void SetBossDieTile(GameObject obj)
    {
        if (obj.tag == "Enemy")
        {
            inBoss = false;
            EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
            tileInfo[enemy.cellRow, enemy.cellColumn].isPassable = true;
            tileInfo[enemy.cellRow, enemy.cellColumn].tileObj = null;
            tileInfo[enemy.cellRow, enemy.cellColumn + 1].isPassable = true;
            tileInfo[enemy.cellRow, enemy.cellColumn + 1].tileObj = null;
            tileInfo[enemy.cellRow + 1, enemy.cellColumn].isPassable = true;
            tileInfo[enemy.cellRow + 1, enemy.cellColumn].tileObj = null;
            tileInfo[enemy.cellRow + 1, enemy.cellColumn + 1].isPassable = true;
            tileInfo[enemy.cellRow + 1, enemy.cellColumn + 1].tileObj = null;
        }
    }

    // Trap 파괴시 해당 Trap이 있던 타일 설정 변경
    public void SetTrapDestroyTile(GameObject obj)
    {
        if (obj.tag == "Trap")
        {
            Trap trap = obj.GetComponent<Trap>();
            tileInfo[trap.cellRow, trap.cellColumn].isTrap = false;
        }
    }

    // Door 오픈시 해당 Door가 있던 타일 설정 변경
    public void SetDoorOpenTile(GameObject obj)
    {
        if (obj.tag == "Door")
        {
            Door door = obj.GetComponent<Door>();
            tileInfo[door.cellRow, door.cellColumn].isPassable = true;
        }
    }

    // Door Close시 해당 Door가 있던 타일 설정 변경
    public void SetDoorCloseTile(GameObject obj)
    {
        if (obj.tag == "Door")
        {
            Door door = obj.GetComponent<Door>();
            tileInfo[door.cellRow, door.cellColumn].isPassable = false;
        }
    }

    // 현재 맵에서 Player가 나갈 때 타일 설정 변경
    public void ExitPlayer(int row, int column)
    {
        inPlayer = false;
        tileInfo[row, column].isPassable = true;
        tileInfo[row, column].tileObj = null;
    }

    // Player의 계획 턴일 때 8방향 Player 이동력 표시
    public void ShowPlayerMoveTile(int row, int column, Direction dir, int movePoint, Stack<PlayerDestinationInfo> movePlanStack)
    {
        FloorTileInfo showTile;
        FloorTileInfo nextTileInfo;

        int tempRow, tempColumn;

        if (movePlanStack.Count != 0)
        {
            tempRow = movePlanStack.Peek().row;
            tempColumn = movePlanStack.Peek().column;
            showTile = CheckDestinationTile(tempRow, tempColumn, dir, movePoint);
            nextTileInfo = CheckNextTile(tempRow, tempColumn, dir);
        }
        else
        {
            tempRow = row;
            tempColumn = column;
            showTile = CheckDestinationTile(tempRow, tempColumn, dir, movePoint);
            nextTileInfo = CheckNextTile(tempRow, tempColumn, dir);
        }

        if (showTile.row == tempRow && showTile.column == tempColumn)
        {
            return;
        }

        while (nextTileInfo.isPassable || nextTileInfo.tileObj)
        {
            if (nextTileInfo.underPlayer)
            {
                SetUnderPlayerTileColor(nextTileInfo.row, nextTileInfo.column, MaterialManager.Instance.moveTileMaterial.color);
            }
            SetTileColor(nextTileInfo.row, nextTileInfo.column, MaterialManager.Instance.moveTileMaterial.color);

            if ((nextTileInfo.row == showTile.row && nextTileInfo.column == showTile.column))
            {
                break;
            }

            tempRow = nextTileInfo.row;
            tempColumn = nextTileInfo.column;
            nextTileInfo = CheckNextTile(tempRow, tempColumn, dir);
        }
    }

    // Player의 이동 계획 턴일 때 마우스 조작
    public bool ShowMovePlanMousePoint(int row, int column, Stack<PlayerDestinationInfo> movePlanStack, Stack<PlayerState> statePlanStack)
    {
        if (UIManager.Instance.IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int tempX, tempY;
            int tempRow, tempColumn;

            tempX = floorTileMap.WorldToCell(ray.origin).x;
            tempY = floorTileMap.WorldToCell(ray.origin).y;

            if (movePlanStack.Count != 0)
            {
                tempRow = movePlanStack.Peek().row;
                tempColumn = movePlanStack.Peek().column;
            }
            else
            {
                tempRow = row;
                tempColumn = column;
            }

            Vector3Int v3Int = new Vector3Int(tempX, tempY, 0);
            //if (floorTileMap.GetColor(v3Int) == MaterialManager.Instance.moveTileMaterial.color)
            //{
            //    floorTileMap.SetTileFlags(v3Int, TileFlags.None);
            //    floorTileMap.SetColor(v3Int, (MaterialManager.Instance.attackTileMaterial.color));
            //}
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 tilePos = new Vector3(transform.position.x + (float)(tempX + 0.5), transform.position.y + (float)(tempY + 0.5), 0);

                for (int i = minSize; i < maxSize; i++)
                {
                    for (int j = minSize; j < maxSize; j++)
                    {
                        if (tilePos == tileInfo[i, j].tilePos)
                        {
                            if (floorTileMap.GetColor(v3Int) == MaterialManager.Instance.moveTileMaterial.color)
                            {
                                PlayerDestinationInfo desInfo = SetDirection(tempRow, tempColumn, i, j);
                                movePlanStack.Push(desInfo);
                                statePlanStack.Push(PlayerState.MOVE);
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    // Player의 공격 계획 턴일 때 마우스 조작
    public bool ShowAttackPlanMousePoint(PlayerSkillType type, Direction dir, Stack<PlayerAttackPlanInfo> attackPlanStack, Stack<PlayerState> statePlanStack)
    {
        if (UIManager.Instance.IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int tempX, tempY;

            tempX = floorTileMap.WorldToCell(ray.origin).x;
            tempY = floorTileMap.WorldToCell(ray.origin).y;

            Vector3Int v3Int = new Vector3Int(tempX, tempY, 0);
            //if (floorTileMap.GetColor(v3Int) == MaterialManager.Instance.attackTileMaterial.color)
            //{
            //    floorTileMap.SetTileFlags(v3Int, TileFlags.None);
            //    floorTileMap.SetColor(v3Int, (MaterialManager.Instance.moveTileMaterial.color));
            //}
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 tilePos = new Vector3(transform.position.x + (float)(tempX + 0.5), transform.position.y + (float)(tempY + 0.5), 0);

                for (int i = minSize; i < maxSize; i++)
                {
                    for (int j = minSize; j < maxSize; j++)
                    {
                        if (tilePos == tileInfo[i, j].tilePos)
                        {
                            if (floorTileMap.GetColor(v3Int) == MaterialManager.Instance.attackTileMaterial.color)
                            {
                                PlayerAttackPlanInfo attackInfo;
                                attackInfo.type = type;
                                attackInfo.dir = dir;
                                attackPlanStack.Push(attackInfo);
                                statePlanStack.Push(PlayerState.ATTACK);
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    // Player가 정해진 목적지를 향한 방향 및 몇 칸 이동할지 반환
    public PlayerDestinationInfo SetDirection(int objRow, int objColumn, int desRow, int desColumn)
    {
        Direction dir = Direction.NONE;
        int point = -1;

        if (objRow == desRow)
        {
            if (objColumn > desColumn)
            {
                point = objColumn - desColumn;
                dir = Direction.LEFT;
            }
            else if (objColumn < desColumn)
            {
                point = desColumn - objColumn;
                dir = Direction.RIGHT;
            }
        }

        if (objColumn == desColumn)
        {
            if (objRow > desRow)
            {
                point = objRow - desRow;
                dir = Direction.UP;
            }
            else if (objRow < desRow)
            {
                point = desRow - objRow;
                dir = Direction.DOWN;
            }
        }

        if (objRow > desRow)
        {
            if (objColumn > desColumn)
            {
                point = objColumn - desColumn;
                dir = Direction.LEFT_UP;
            }
            else if (objColumn < desColumn)
            {
                point = desColumn - objColumn;
                dir = Direction.UP_RIGHT;
            }
        }

        if (objRow < desRow)
        {
            if (objColumn > desColumn)
            {
                point = objColumn - desColumn;
                dir = Direction.DOWN_LEFT;
            }
            else if (objColumn < desColumn)
            {
                point = desColumn - objColumn;
                dir = Direction.RIGHT_DOWN;
            }
        }

        PlayerDestinationInfo desInfo;
        desInfo.row = desRow;
        desInfo.column = desColumn;
        desInfo.dir = dir;
        desInfo.movePoint = point;

        return desInfo;
    }

    // 공격 범위 타일 내에 있는 게임 오브젝트 반환 및 공격 범위 타일 색상 변경
    public GameObject GetAttackObject(int row, int column, GameObject obj, bool isPlayer = false)
    {
        if (!isPlayer)
        {
            SetTileColor(row, column, MaterialManager.Instance.enemyAttackTileMaterial.color);

            if (tileInfo[row, column].underPlayer)
            {
                SetUnderPlayerTileColor(row, column, MaterialManager.Instance.enemyAttackTileMaterial.color);
            }
        }
        else
        {
            SetTileColor(row, column, MaterialManager.Instance.attackTileMaterial.color);

            if (tileInfo[row, column].underPlayer)
            {
                SetUnderPlayerTileColor(row, column, MaterialManager.Instance.attackTileMaterial.color);
            }
        }

        if (tileInfo[row, column].tileObj != null)
        {
            if ((obj.tag == "Player" && tileInfo[row, column].tileObj.tag == "Enemy") || (obj.tag == "Enemy" && tileInfo[row, column].tileObj.tag == "Player"))
            {
                return tileInfo[row, column].tileObj;
            }
        }

        return null;
    }

    // Player 공격 타일 색상 변경
    public void ShowPlayerAttackTileColor(int row, int column)
    {
        SetTileColor(row, column, MaterialManager.Instance.attackTileMaterial.color);

        if (tileInfo[row, column].underPlayer)
        {
            SetUnderPlayerTileColor(row, column, MaterialManager.Instance.attackTileMaterial.color);
        }
    }

    // Door Action
    public void DoorAction()
    {
        for (int i = 0; i < doorList.Count; i++)
        {
            doorList[i].GetComponent<Door>().Action();
        }
    }

    // 현재 맵에 남은 적 카운팅
    public int EnemyCount()
    {
        return enemyList.Count;
    }

    // 현재 맵에 있는 함정 카운팅
    public int TrapCount()
    {
        return trapList.Count;
    }

    // Active Check
    public bool ActiveCheck()
    {
        return isActive;
    }

    // Player Check
    public bool PlayerCheck()
    {
        return inPlayer;
    }

    // Boss Check
    public bool BossCheck()
    {
        return inBoss;
    }

    // MiniMap Update
    public void MiniMapUpdate()
    {
        transform.GetChild(0).GetComponent<MiniMap>().VisitiedCurrRoom();
    }

    // 현재 타일맵 색상 복원
    public void RefreshTile()
    {
        //floorTileMap.RefreshAllTiles();

        for (int i = minSize; i < maxSize; i++)
        {
            for (int j = minSize; j < maxSize; j++)
            {
                if (floorTileMap.GetColor(floorTileMap.WorldToCell(tileInfo[i, j].tilePos)) != Color.white)
                {
                    floorTileMap.SetColor(floorTileMap.WorldToCell(tileInfo[i, j].tilePos), Color.white);
                    underPlayerTileMap.SetColor(underPlayerTileMap.WorldToCell(tileInfo[i, j].tilePos), Color.white);
                }
            }
        }
    }

    // 넉백 공격 받았을 때 넉백 방향 지정
    public Direction KnockbackDirection(int targetRow, int targetColumn, int attackRow, int attackColumn)
    {
        Direction dir = Direction.NONE;

        if (targetRow == attackRow)
        {
            if (targetColumn > attackColumn)
            {
                dir = Direction.RIGHT;
            }
            else if (targetColumn < attackColumn)
            {
                dir = Direction.LEFT;
            }
        }

        if (targetColumn == attackColumn)
        {
            if (targetRow > attackRow)
            {
                dir = Direction.DOWN;
            }
            else if (targetRow < attackRow)
            {
                dir = Direction.UP;
            }
        }

        if (targetRow > attackRow)
        {
            if (targetColumn > attackColumn)
            {
                dir = Direction.RIGHT_DOWN;
            }
            else if (targetColumn < attackColumn)
            {
                dir = Direction.DOWN_LEFT;
            }
        }

        if (targetRow < attackRow)
        {
            if (targetColumn > attackColumn)
            {
                dir = Direction.UP_RIGHT;
            }
            else if (targetColumn < attackColumn)
            {
                dir = Direction.LEFT_UP;
            }
        }

        return dir;
    }

    // 해당 타일맵 선택
    public bool SelectTile(GameObject obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int tempX = floorTileMap.WorldToCell(ray.origin).x;
        int tempY = floorTileMap.WorldToCell(ray.origin).y;
        Vector3Int v3Int = new Vector3Int(tempX, tempY, 0);
        Vector3 tilePos = new Vector3(transform.position.x + (float)(tempX + 0.5), transform.position.y + (float)(tempY + 0.5), 0);

        for (int i = minSize; i < maxSize; i++)
        {
            for (int j = minSize; j < maxSize; j++)
            {
                if (tilePos == tileInfo[i, j].tilePos)
                {
                    if ((floorTileMap.GetColor(v3Int) == MaterialManager.Instance.attackTileMaterial.color) ||
                        (floorTileMap.GetColor(v3Int) == MaterialManager.Instance.moveTileMaterial.color))
                    {
                        obj.transform.position = tilePos;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    // 치트: 맵 내부 적 전부 사망시키기
    public void CheatKillAllEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].GetComponent<Enemy>().OnDamage(0, 0, 1000.0f);
        }
    }
}
