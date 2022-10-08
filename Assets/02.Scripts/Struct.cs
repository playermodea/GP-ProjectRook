using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Floor 타일맵의 각 타일 정보 구조체
public struct FloorTileInfo
{
    public int row, column; // 현재 해당 타일이 저장된 배열의 행렬 인덱스         - 처음 초기화되면 바뀌지 않음
    public Vector3 tilePos; // 현재 해당 타일의 중앙 좌표                        - 처음 초기화되면 바뀌지 않음
    public bool underPlayer; // 현재 해당 타일이 플레이어 뒤 쪽에 생성되는지 여부  - 처음 초기화 이 후 바뀔 수 없음
    public bool isPassable; // 현재 해당 타일로 이동할 수 있는지 여부             - 처음 초기화 이 후 바뀔 수 있음
    public bool isTrap;     // 현재 해당 타일이 함정인지 여부                    - 처음 초기화 이 후 바뀔 수 있음
    public GameObject tileObj;  // 현재 해당 타일 위에 존재하는 게임 오브젝트     - 처음 초기화 이 후 바뀔 수 있음
}

// 각 객체의 공격 범위 구조체
public struct AttackRangeInfo
{
    public int attackRow, attackColumn;
    public Direction rangeDir;
    public bool[] isAttack;

    public void Initialize(int row, int column, Direction dir, bool[] var)
    {
        attackRow = row;
        attackColumn = column;
        rangeDir = dir;
        isAttack = var;
    }
}

// TempTile 좌표 구조체
public struct TempTileInfo
{
    public int row, column;
}

// Player 목적지 정보 구조체
public struct PlayerDestinationInfo
{
    public int row, column;
    public Direction dir;
    public int movePoint;
}

// Player Skill 정보 구조체
public struct PlayerAttackPlanInfo
{
    public PlayerSkillType type;
    public Direction dir;
}

// Player Skill 이펙트를 생성 정보 구조체
public struct PlayerSkillEffectInfo
{
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;

    public void Initialize(Vector3 pPos, Vector3 pRot, Vector3 pScale)
    {
        pos = pPos;
        rot = pRot;
        scale = pScale;
    }
}

// 랜덤 Map 생성시 필요한 정보 구조체
public struct MapInfo
{
    public float x;
    public float y;
    public bool posReady;
    public bool passableUp;
    public bool passableRight;
    public bool passableDown;
    public bool passableLeft;
    public bool isTreasureMap;
    public bool isBossMap;
    public bool isStoreMap;
}