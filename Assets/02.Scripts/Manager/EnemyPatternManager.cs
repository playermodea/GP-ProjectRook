using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPatternManager : MonoBehaviour
{
    // 이동 패턴
    public Direction[] redSlimeMovePatternInfo;
    public Direction[] blueSlimeMovePatternInfo;
    public Direction[] skyBlueSlimeMovePatternInfo;
    public Direction[] pinkSlimeMovePatternInfo;
    public Direction[] blackHornbugMovePatternInfo;
    public Direction[] whiteHornbugMovePatternInfo;
    public Direction[] cordycepsMovePatternInfo;
    public Direction[] bomberManMovePateernInfo;

    // 공격 범위
    public AttackRangeInfo[] slimeBasicAttackRangeInfo;
    public AttackRangeInfo[] skyBlueSlimeSpecialAttackInfo;
    public AttackRangeInfo[] pinkSlimeSpecialAttackRangeInfo;
    public AttackRangeInfo[] hornbugBasicAttackRangeInfo;
    public AttackRangeInfo[] cordycepsAttackRangeInfo;
    public AttackRangeInfo[,] skeletonHeadAttackRangeInfo;
    public AttackRangeInfo[] bomberManAttackRangeInfo;

    public AttackRangeInfo[,] BossBasicAttackRangeInfo;
    public AttackRangeInfo[,] FairyQueenAttackRangeInfo;
    public AttackRangeInfo[,] FairyWoodAttack_1_RangeInfo;
    public AttackRangeInfo[,] FairyWoodAttack_2_RangeInfo;

    private static EnemyPatternManager instance;

    public static EnemyPatternManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<EnemyPatternManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object EnemyPatternManager");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        //redSlimeMovePatternInfo = new Direction[8];
        //blueSlimeMovePatternInfo = new Direction[8];
        //skyBlueSlimeMovePatternInfo = new Direction[4];
        //pinkSlimeMovePatternInfo = new Direction[4];
        //blackHornbugMovePatternInfo = new Direction[8];
        //whiteHornbugMovePatternInfo = new Direction[8];
        //cordycepsMovePatternInfo = new Direction[28];
        //bomberManMovePateernInfo = new Direction[12];
        redSlimeMovePatternInfo = new Direction[2];
        blueSlimeMovePatternInfo = new Direction[2];
        skyBlueSlimeMovePatternInfo = new Direction[2];
        pinkSlimeMovePatternInfo = new Direction[2];
        blackHornbugMovePatternInfo = new Direction[2];
        whiteHornbugMovePatternInfo = new Direction[2];
        cordycepsMovePatternInfo = new Direction[6];
        bomberManMovePateernInfo = new Direction[12];

        //slimeBasicAttackRangeInfo = new AttackRangeInfo[3];
        slimeBasicAttackRangeInfo = new AttackRangeInfo[4];
        skyBlueSlimeSpecialAttackInfo = new AttackRangeInfo[6];
        pinkSlimeSpecialAttackRangeInfo = new AttackRangeInfo[6];
        //hornbugBasicAttackRangeInfo = new AttackRangeInfo[2];
        hornbugBasicAttackRangeInfo = new AttackRangeInfo[4];
        cordycepsAttackRangeInfo = new AttackRangeInfo[2];
        skeletonHeadAttackRangeInfo = new AttackRangeInfo[4, 3];
        bomberManAttackRangeInfo = new AttackRangeInfo[24];

        BossBasicAttackRangeInfo = new AttackRangeInfo[4, 2];
        FairyQueenAttackRangeInfo = new AttackRangeInfo[2, 4];
        FairyWoodAttack_1_RangeInfo = new AttackRangeInfo[4, 1];
        FairyWoodAttack_2_RangeInfo = new AttackRangeInfo[3, 4];

        InitRedSlimeMovePattern();
        InitBlueSlimeMovePattern();
        InitSkyBlueSlimeMovePattern();
        InitPinkSlimeMovePattern();
        InitBlackHornbugMovePattern();
        InitWhiteHornbugMovePattern();
        InitCordycepsMovePattern();
        InitBomberManMovePattern();

        InitSlimeBasicAttackRangeInfo();
        InitSkyBlueSlimeSpecialAttackRangeInfo();
        InitPinkSlimeSpecialAttackRangeInfo();
        InitHornbugBasicAttackRangeInfo();
        InitCordycepsBasicAttackRangeInfo();
        InitSkeletonHeadBasicAttackRangeInfo();
        InitBomberManAttackRangeInfo();
        InitBossBasicAttackRangeInfo();
        InitFairyQueenAttackRangeInfo();
        InitFairyWoodAttack_1_RangeInfo();
        InitFairyWoodAttack_2_RangeInfo();
    }

    // 이동 패턴 초기화 함수들
    private void InitRedSlimeMovePattern()
    {
        redSlimeMovePatternInfo[0] = Direction.LEFT;
        redSlimeMovePatternInfo[1] = Direction.RIGHT;
    }

    private void InitBlueSlimeMovePattern()
    {
        blueSlimeMovePatternInfo[0] = Direction.UP;
        blueSlimeMovePatternInfo[1] = Direction.DOWN;
    }

    private void InitSkyBlueSlimeMovePattern()
    {
        skyBlueSlimeMovePatternInfo[0] = Direction.DOWN;
        skyBlueSlimeMovePatternInfo[1] = Direction.UP;
    }

    private void InitPinkSlimeMovePattern()
    {
        pinkSlimeMovePatternInfo[0] = Direction.RIGHT;
        pinkSlimeMovePatternInfo[1] = Direction.LEFT;
    }

    private void InitBlackHornbugMovePattern()
    {
        blackHornbugMovePatternInfo[0] = Direction.UP_RIGHT;
        blackHornbugMovePatternInfo[1] = Direction.DOWN_LEFT;
    }

    private void InitWhiteHornbugMovePattern()
    {
        whiteHornbugMovePatternInfo[0] = Direction.LEFT_UP;
        whiteHornbugMovePatternInfo[1] = Direction.RIGHT_DOWN;
    }

    private void InitCordycepsMovePattern()
    {
        for (int i = 0; i < 3; i++)
        {
            cordycepsMovePatternInfo[i] = Direction.RIGHT;
        }
        for (int i = 3; i < 6; i++)
        {
            cordycepsMovePatternInfo[i] = Direction.LEFT;
        }
    }

    private void InitBomberManMovePattern()
    {
        for (int i = 0; i < 3; i++)
        {
            bomberManMovePateernInfo[i] = Direction.LEFT;
        }
        for (int i = 0; i < 3; i++)
        {
            bomberManMovePateernInfo[i + 3] = Direction.DOWN;
        }
        for (int i = 0; i < 3; i++)
        {
            bomberManMovePateernInfo[i + 6] = Direction.RIGHT;
        }
        for (int i = 0; i < 3; i++)
        {
            bomberManMovePateernInfo[i + 9] = Direction.UP;
        }
    }

    // 공격 범위 초기화 함수들
    private void InitSlimeBasicAttackRangeInfo()
    {
        slimeBasicAttackRangeInfo[0].Initialize(-1, 0, Direction.UP, new bool[16]);
        slimeBasicAttackRangeInfo[0].isAttack = Enumerable.Repeat(true, 16).ToArray();
        slimeBasicAttackRangeInfo[1].Initialize(0, 1, Direction.RIGHT, new bool[16]);
        slimeBasicAttackRangeInfo[1].isAttack = Enumerable.Repeat(true, 16).ToArray();
        slimeBasicAttackRangeInfo[2].Initialize(1, 0, Direction.DOWN, new bool[16]);
        slimeBasicAttackRangeInfo[2].isAttack = Enumerable.Repeat(true, 16).ToArray();
        slimeBasicAttackRangeInfo[3].Initialize(0, -1, Direction.LEFT, new bool[16]);
        slimeBasicAttackRangeInfo[3].isAttack = Enumerable.Repeat(true, 16).ToArray();
    }

    private void InitSkyBlueSlimeSpecialAttackRangeInfo()
    {
        for (int i = 0; i < 2; i++)
        {
            skyBlueSlimeSpecialAttackInfo[i].Initialize(-3 + i, -3, Direction.RIGHT, new bool[7] { true, true, true, false, true, true, true });
        }

        for (int i = 0; i < 2; i++)
        {
            skyBlueSlimeSpecialAttackInfo[i + 2].Initialize(-1 + i * 2, -3, Direction.RIGHT, new bool[7] { true, true, false, false, false, true, true });
        }

        for (int i = 0; i < 2; i++)
        {
            skyBlueSlimeSpecialAttackInfo[i + 4].Initialize(2 + i, -3, Direction.RIGHT, new bool[7] { true, true, true, false, true, true, true });
        }
    }

    private void InitPinkSlimeSpecialAttackRangeInfo()
    {
        for (int i = 0; i < 2; i++)
        {
            pinkSlimeSpecialAttackRangeInfo[i].Initialize(2, -2 + i * 4, Direction.UP, new bool[5]);
            pinkSlimeSpecialAttackRangeInfo[i].isAttack = Enumerable.Repeat(true, 5).ToArray();
        }
        pinkSlimeSpecialAttackRangeInfo[2].Initialize(3, 0, Direction.UP, new bool[7] { true, true, false, false, false, true, true });

        for (int i = 0; i < 2; i++)
        {
            pinkSlimeSpecialAttackRangeInfo[i + 3].Initialize(-2 + i * 4, -1, Direction.RIGHT, new bool[3] { true, false, true });
        }
        pinkSlimeSpecialAttackRangeInfo[5].Initialize(0, -3, Direction.RIGHT, new bool[7] { true, false, false, false, false, false, true });
    }

    private void InitHornbugBasicAttackRangeInfo()
    {
        hornbugBasicAttackRangeInfo[0].Initialize(-1, 1, Direction.UP_RIGHT, new bool[16]);
        hornbugBasicAttackRangeInfo[0].isAttack = Enumerable.Repeat(true, 16).ToArray();
        hornbugBasicAttackRangeInfo[1].Initialize(1, 1, Direction.RIGHT_DOWN, new bool[16]);
        hornbugBasicAttackRangeInfo[1].isAttack = Enumerable.Repeat(true, 16).ToArray();
        hornbugBasicAttackRangeInfo[2].Initialize(1, -1, Direction.DOWN_LEFT, new bool[16]);
        hornbugBasicAttackRangeInfo[2].isAttack = Enumerable.Repeat(true, 16).ToArray();
        hornbugBasicAttackRangeInfo[3].Initialize(-1, -1, Direction.LEFT_UP, new bool[16]);
        hornbugBasicAttackRangeInfo[3].isAttack = Enumerable.Repeat(true, 16).ToArray();
    }

    private void InitCordycepsBasicAttackRangeInfo()
    {
        cordycepsAttackRangeInfo[0].Initialize(2, 0, Direction.UP, new bool[5] { true, true, false, true, true });
        cordycepsAttackRangeInfo[1].Initialize(0, -2, Direction.RIGHT, new bool[5] { true, true, false, true, true });
    }

    private void InitSkeletonHeadBasicAttackRangeInfo()
    {
        int count = 0;
        int reverseNumber = 1;
        Direction dir = Direction.UP;

        // 위, 아래 범위
        for (int i = 0; i < 2; i++)
        {
            skeletonHeadAttackRangeInfo[count, 0].Initialize(-1 * reverseNumber, 0, dir, new bool[16]);
            skeletonHeadAttackRangeInfo[count, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();

            skeletonHeadAttackRangeInfo[count, 1].Initialize(-1 * reverseNumber, 1, dir, new bool[16]);
            skeletonHeadAttackRangeInfo[count, 1].isAttack = Enumerable.Repeat(true, 16).ToArray();
            skeletonHeadAttackRangeInfo[count, 1].isAttack[0] = false;

            skeletonHeadAttackRangeInfo[count, 2].Initialize(-1 * reverseNumber, -1, dir, new bool[16]);
            skeletonHeadAttackRangeInfo[count, 2].isAttack = Enumerable.Repeat(true, 16).ToArray();
            skeletonHeadAttackRangeInfo[count, 2].isAttack[0] = false;

            count += 2;
            dir = Direction.DOWN;
            reverseNumber = -1;
        }

        count = 1;
        dir = Direction.RIGHT;
        reverseNumber = 1;

        // 오른쪽, 왼쪽 범위
        for (int i = 0; i < 2; i++)
        {
            skeletonHeadAttackRangeInfo[count, 0].Initialize(0, 1 * reverseNumber, dir, new bool[16]);
            skeletonHeadAttackRangeInfo[count, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();

            skeletonHeadAttackRangeInfo[count, 1].Initialize(1, 1 * reverseNumber, dir, new bool[16]);
            skeletonHeadAttackRangeInfo[count, 1].isAttack = Enumerable.Repeat(true, 16).ToArray();
            skeletonHeadAttackRangeInfo[count, 1].isAttack[0] = false;

            skeletonHeadAttackRangeInfo[count, 2].Initialize(-1, 1 * reverseNumber, dir, new bool[5]);
            skeletonHeadAttackRangeInfo[count, 2].isAttack = Enumerable.Repeat(true, 16).ToArray();
            skeletonHeadAttackRangeInfo[count, 2].isAttack[0] = false;

            count += 2;
            dir = Direction.LEFT;
            reverseNumber = -1;
        }
    }

    private void InitBomberManAttackRangeInfo()
    {
        for (int i = 0; i < 8; i++)
        {
            bomberManAttackRangeInfo[i].Initialize(0, 0, Direction.UP + i, new bool[2] { false, true });
        }

        for (int i = 0; i < 5; i++)
        {
            bomberManAttackRangeInfo[i + 8].Initialize(-2, -2 + i, Direction.UP, new bool[1] { true });
            bomberManAttackRangeInfo[i + 13].Initialize(2, -2 + i, Direction.UP, new bool[1] { true });
        }

        for (int i = 0; i < 3; i++)
        {
            bomberManAttackRangeInfo[i + 18].Initialize(-1 + i, -2, Direction.UP, new bool[1] { true });
            bomberManAttackRangeInfo[i + 21].Initialize(-1 + i, 2, Direction.UP, new bool[1] { true });
        }
    }

    private void InitBossBasicAttackRangeInfo()
    {
        BossBasicAttackRangeInfo[0, 0].Initialize(-1, 0, Direction.UP, new bool[16]);
        BossBasicAttackRangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();
        BossBasicAttackRangeInfo[0, 1].Initialize(-1, 1, Direction.UP, new bool[16]);
        BossBasicAttackRangeInfo[0, 1].isAttack = Enumerable.Repeat(true, 16).ToArray();

        BossBasicAttackRangeInfo[1, 0].Initialize(0, 2, Direction.RIGHT, new bool[16]);
        BossBasicAttackRangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();
        BossBasicAttackRangeInfo[1, 1].Initialize(1, 2, Direction.RIGHT, new bool[16]);
        BossBasicAttackRangeInfo[1, 1].isAttack = Enumerable.Repeat(true, 16).ToArray();

        BossBasicAttackRangeInfo[2, 0].Initialize(2, 0, Direction.DOWN, new bool[16]);
        BossBasicAttackRangeInfo[2, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();
        BossBasicAttackRangeInfo[2, 1].Initialize(2, 1, Direction.DOWN, new bool[16]);
        BossBasicAttackRangeInfo[2, 1].isAttack = Enumerable.Repeat(true, 16).ToArray();

        BossBasicAttackRangeInfo[3, 0].Initialize(0, -1, Direction.LEFT, new bool[16]);
        BossBasicAttackRangeInfo[3, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();
        BossBasicAttackRangeInfo[3, 1].Initialize(1, -1, Direction.LEFT, new bool[16]);
        BossBasicAttackRangeInfo[3, 1].isAttack = Enumerable.Repeat(true, 16).ToArray();
    }

    private void InitFairyQueenAttackRangeInfo()
    {
        FairyQueenAttackRangeInfo[0, 0].Initialize(-1, 0, Direction.UP, new bool[4]);
        FairyQueenAttackRangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();
        FairyQueenAttackRangeInfo[0, 1].Initialize(-1, 1, Direction.UP, new bool[4]);
        FairyQueenAttackRangeInfo[0, 1].isAttack = Enumerable.Repeat(true, 4).ToArray();
        FairyQueenAttackRangeInfo[0, 2].Initialize(2, 0, Direction.DOWN, new bool[4]);
        FairyQueenAttackRangeInfo[0, 2].isAttack = Enumerable.Repeat(true, 4).ToArray();
        FairyQueenAttackRangeInfo[0, 3].Initialize(2, 1, Direction.DOWN, new bool[4]);
        FairyQueenAttackRangeInfo[0, 3].isAttack = Enumerable.Repeat(true, 4).ToArray();

        FairyQueenAttackRangeInfo[1, 0].Initialize(0, 2, Direction.RIGHT, new bool[4]);
        FairyQueenAttackRangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();
        FairyQueenAttackRangeInfo[1, 1].Initialize(1, 2, Direction.RIGHT, new bool[4]);
        FairyQueenAttackRangeInfo[1, 1].isAttack = Enumerable.Repeat(true, 4).ToArray();
        FairyQueenAttackRangeInfo[1, 2].Initialize(0, -1, Direction.LEFT, new bool[4]);
        FairyQueenAttackRangeInfo[1, 2].isAttack = Enumerable.Repeat(true, 4).ToArray();
        FairyQueenAttackRangeInfo[1, 3].Initialize(1, -1, Direction.LEFT, new bool[4]);
        FairyQueenAttackRangeInfo[1, 3].isAttack = Enumerable.Repeat(true, 4).ToArray();
    }

    private void InitFairyWoodAttack_1_RangeInfo()
    {
        FairyWoodAttack_1_RangeInfo[0, 0].Initialize(-1, 0, Direction.UP, new bool[16]);
        FairyWoodAttack_1_RangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();
        FairyWoodAttack_1_RangeInfo[1, 0].Initialize(0, 1, Direction.RIGHT, new bool[16]);
        FairyWoodAttack_1_RangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();
        FairyWoodAttack_1_RangeInfo[2, 0].Initialize(1, 0, Direction.DOWN, new bool[16]);
        FairyWoodAttack_1_RangeInfo[2, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();
        FairyWoodAttack_1_RangeInfo[3, 0].Initialize(0, -1, Direction.LEFT, new bool[16]);
        FairyWoodAttack_1_RangeInfo[3, 0].isAttack = Enumerable.Repeat(true, 16).ToArray();
    }

    private void InitFairyWoodAttack_2_RangeInfo()
    {
        FairyWoodAttack_2_RangeInfo[0, 0].Initialize(-1, -1, Direction.RIGHT, new bool[4]);
        FairyWoodAttack_2_RangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();
        FairyWoodAttack_2_RangeInfo[0, 1].Initialize(2, 2, Direction.UP, new bool[3]);
        FairyWoodAttack_2_RangeInfo[0, 1].isAttack = Enumerable.Repeat(true, 3).ToArray();
        FairyWoodAttack_2_RangeInfo[0, 2].Initialize(2, 1, Direction.LEFT, new bool[3]);
        FairyWoodAttack_2_RangeInfo[0, 2].isAttack = Enumerable.Repeat(true, 3).ToArray();
        FairyWoodAttack_2_RangeInfo[0, 3].Initialize(1, -1, Direction.UP, new bool[2]);
        FairyWoodAttack_2_RangeInfo[0, 3].isAttack = Enumerable.Repeat(true, 2).ToArray();

        FairyWoodAttack_2_RangeInfo[1, 0].Initialize(-2, -2, Direction.RIGHT, new bool[6]);
        FairyWoodAttack_2_RangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 6).ToArray();
        FairyWoodAttack_2_RangeInfo[1, 1].Initialize(3, 3, Direction.UP, new bool[5]);
        FairyWoodAttack_2_RangeInfo[1, 1].isAttack = Enumerable.Repeat(true, 5).ToArray();
        FairyWoodAttack_2_RangeInfo[1, 2].Initialize(3, 2, Direction.LEFT, new bool[5]);
        FairyWoodAttack_2_RangeInfo[1, 2].isAttack = Enumerable.Repeat(true, 5).ToArray();
        FairyWoodAttack_2_RangeInfo[1, 3].Initialize(2, -2, Direction.UP, new bool[4]);
        FairyWoodAttack_2_RangeInfo[1, 3].isAttack = Enumerable.Repeat(true, 4).ToArray();

        FairyWoodAttack_2_RangeInfo[2, 0].Initialize(-3, -3, Direction.RIGHT, new bool[8]);
        FairyWoodAttack_2_RangeInfo[2, 0].isAttack = Enumerable.Repeat(true, 8).ToArray();
        FairyWoodAttack_2_RangeInfo[2, 1].Initialize(4, 4, Direction.UP, new bool[7]);
        FairyWoodAttack_2_RangeInfo[2, 1].isAttack = Enumerable.Repeat(true, 7).ToArray();
        FairyWoodAttack_2_RangeInfo[2, 2].Initialize(4, 3, Direction.LEFT, new bool[7]);
        FairyWoodAttack_2_RangeInfo[2, 2].isAttack = Enumerable.Repeat(true, 7).ToArray();
        FairyWoodAttack_2_RangeInfo[2, 3].Initialize(3, -3, Direction.UP, new bool[6]);
        FairyWoodAttack_2_RangeInfo[2, 3].isAttack = Enumerable.Repeat(true, 6).ToArray();
    }

    // 해당 방향에 맞는 공격범위 변환 후 반환 함수 / 1 - UP, 2 - RIGHT, 3 - DOWN, 4 - LEFT
    public AttackRangeInfo[] ConvertArray(AttackRangeInfo[,] rangeInfo, Direction dir)
    {
        int dirNumber = 0;

        switch (dir)
        {
            case Direction.UP:
                dirNumber = 0;
                break;
            case Direction.RIGHT:
                dirNumber = 1;
                break;
            case Direction.DOWN:
                dirNumber = 2;
                break;
            case Direction.LEFT:
                dirNumber = 3;
                break;
            default:
                break;
        }

        AttackRangeInfo[] tempInfo = new AttackRangeInfo[rangeInfo.GetLength(1)];

        for (int i = 0; i < rangeInfo.GetLength(1); i++)
        {
            tempInfo[i] = rangeInfo[dirNumber, i];
        }

        return tempInfo;
    }
}
