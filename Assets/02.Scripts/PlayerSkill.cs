using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSkill : MonoBehaviour
{
    // 기본 스킬
    public AttackRangeInfo[,] risingSlash { get; private set; }             // 세로베기
    public AttackRangeInfo[] stampFeetRangeInfo { get; private set; }       // 발구르기

    // 추가 스킬
    public AttackRangeInfo[,] swingRangeInfo { get; private set; }          // 휘두르기
    public AttackRangeInfo[] sonicWaveRangeInfo { get; private set; }       // 음파           
    public AttackRangeInfo[,] throwingBobberRangeInfo { get; private set; } // 찌 투척
    public AttackRangeInfo[] crackTheSkyRangeInfo { get; private set; }     // Crack The Sky
    public AttackRangeInfo[,] forwardSlash { get; private set; }            // 찌르기
    public AttackRangeInfo[,] diggingRangeInfo { get; private set; }        // 앞찍기
    public AttackRangeInfo[,] throwingBoomRangeInfo { get; private set; }   // 폭탄투척
    public AttackRangeInfo[,] sideSlashRangeInfo { get; private set; }      // 가로베기
    public AttackRangeInfo[] spinningSlashRangeInfo { get; private set; }   // 회전베기
    public AttackRangeInfo[,] smashSlamRangeInfo { get; private set; }      // 내려찍기
    public AttackRangeInfo[,] hitTheMarkRangeInfo { get; private set; }     // 적중
    public AttackRangeInfo[,] slingShotRangeInfo { get; private set; }       // 화염
    public AttackRangeInfo[,] diffusionExplosionRangeInfo { get; private set; }  // 확산폭발

    private void Awake()
    {
        risingSlash = new AttackRangeInfo[4, 3];
        swingRangeInfo = new AttackRangeInfo[4, 2];
        stampFeetRangeInfo = new AttackRangeInfo[5];
        sonicWaveRangeInfo = new AttackRangeInfo[4];
        throwingBobberRangeInfo = new AttackRangeInfo[4, 1];
        crackTheSkyRangeInfo = new AttackRangeInfo[4];
        forwardSlash = new AttackRangeInfo[4, 1];
        diggingRangeInfo = new AttackRangeInfo[4, 2];
        throwingBoomRangeInfo = new AttackRangeInfo[4, 2];
        sideSlashRangeInfo = new AttackRangeInfo[4, 1];
        spinningSlashRangeInfo = new AttackRangeInfo[8];
        smashSlamRangeInfo = new AttackRangeInfo[4, 1];
        hitTheMarkRangeInfo = new AttackRangeInfo[4, 1];
        slingShotRangeInfo = new AttackRangeInfo[4, 1];
        diffusionExplosionRangeInfo = new AttackRangeInfo[4, 2];

        InitRisingSlash();
        InitSwing();
        InitStampFeet();
        InitSonicWave();
        InitThrowingBobber();
        InitCrackTheSky();
        InitForwardSlash();
        InitDigging();
        InitThrowingBoom();
        InitSideSlash();
        InitSpinningSlash();
        InitSmashSlam();
        InitHitTheMark();
        InitSlingShot();
        InitDiffusionExplosion();
    }

    private void InitRisingSlash()
    {
        // 위
        risingSlash[0, 0].Initialize(-1, 0, Direction.UP, new bool[4]);
        risingSlash[0, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();
        risingSlash[0, 1].Initialize(-1, 1, Direction.UP, new bool[4]);
        risingSlash[0, 1].isAttack = Enumerable.Repeat(true, 4).ToArray();
        risingSlash[0, 2].Initialize(-3, 2, Direction.UP, new bool[2]);
        risingSlash[0, 2].isAttack = Enumerable.Repeat(true, 2).ToArray();

        // 오른쪽
        risingSlash[1, 0].Initialize(0, 1, Direction.RIGHT, new bool[4]);
        risingSlash[1, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();
        risingSlash[1, 1].Initialize(1, 1, Direction.RIGHT, new bool[4]);
        risingSlash[1, 1].isAttack = Enumerable.Repeat(true, 4).ToArray();
        risingSlash[1, 2].Initialize(2, 3, Direction.RIGHT, new bool[2]);
        risingSlash[1, 2].isAttack = Enumerable.Repeat(true, 2).ToArray();

        // 아래
        risingSlash[2, 0].Initialize(1, 0, Direction.DOWN, new bool[4]);
        risingSlash[2, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();
        risingSlash[2, 1].Initialize(1, -1, Direction.DOWN, new bool[4]);
        risingSlash[2, 1].isAttack = Enumerable.Repeat(true, 4).ToArray();
        risingSlash[2, 2].Initialize(3, -2, Direction.DOWN, new bool[2]);
        risingSlash[2, 2].isAttack = Enumerable.Repeat(true, 2).ToArray();

        // 왼쪽
        risingSlash[3, 0].Initialize(0, -1, Direction.LEFT, new bool[4]);
        risingSlash[3, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();
        risingSlash[3, 1].Initialize(-1, -1, Direction.LEFT, new bool[4]);
        risingSlash[3, 1].isAttack = Enumerable.Repeat(true, 4).ToArray();
        risingSlash[3, 2].Initialize(-2, -3, Direction.LEFT, new bool[2]);
        risingSlash[3, 2].isAttack = Enumerable.Repeat(true, 2).ToArray();
    }

    private void InitSwing()
    {
        // 위
        swingRangeInfo[0, 0].Initialize(-1, -2, Direction.RIGHT, new bool[5]);
        swingRangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 5).ToArray();
        swingRangeInfo[0, 1].Initialize(-2, -1, Direction.RIGHT, new bool[3]);
        swingRangeInfo[0, 1].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 오른쪽
        swingRangeInfo[1, 0].Initialize(-2, 1, Direction.DOWN, new bool[5]);
        swingRangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 5).ToArray();
        swingRangeInfo[1, 1].Initialize(-1, 2, Direction.DOWN, new bool[3]);
        swingRangeInfo[1, 1].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 아래
        swingRangeInfo[2, 0].Initialize(1, -2, Direction.RIGHT, new bool[5]);
        swingRangeInfo[2, 0].isAttack = Enumerable.Repeat(true, 5).ToArray();
        swingRangeInfo[2, 1].Initialize(2, -1, Direction.RIGHT, new bool[3]);
        swingRangeInfo[2, 1].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 왼쪽
        swingRangeInfo[3, 0].Initialize(-2, -1, Direction.DOWN, new bool[5]);
        swingRangeInfo[3, 0].isAttack = Enumerable.Repeat(true, 5).ToArray();
        swingRangeInfo[3, 1].Initialize(-1, -2, Direction.DOWN, new bool[3]);
        swingRangeInfo[3, 1].isAttack = Enumerable.Repeat(true, 3).ToArray();
    }

    private void InitStampFeet()
    {
        stampFeetRangeInfo[0].Initialize(0, -2, Direction.RIGHT, new bool[5] { true, true, false, true, true });

        for (int i = 1; i < 3; i++)
        {
            stampFeetRangeInfo[i].Initialize(i, -2 + i, Direction.RIGHT, new bool[5 - 2 * i]);
            stampFeetRangeInfo[i].isAttack = Enumerable.Repeat(true, 5 - 2 * i).ToArray();

            stampFeetRangeInfo[i + 2].Initialize(-i, -2 + i, Direction.RIGHT, new bool[5 - 2 * i]);
            stampFeetRangeInfo[i + 2].isAttack = Enumerable.Repeat(true, 5 - 2 * i).ToArray();
        }
    }

    private void InitSonicWave()
    {
        sonicWaveRangeInfo[0].Initialize(-3, 0, Direction.RIGHT_DOWN, new bool[3] { true, true, true });
        sonicWaveRangeInfo[1].Initialize(0, 3, Direction.DOWN_LEFT, new bool[3] { true, true, true });
        sonicWaveRangeInfo[2].Initialize(3, 0, Direction.LEFT_UP, new bool[3] { true, true, true });
        sonicWaveRangeInfo[3].Initialize(0, -3, Direction.UP_RIGHT, new bool[3] { true, true, true });
    }

    private void InitThrowingBobber()
    {
        throwingBobberRangeInfo[0, 0].Initialize(0, 0, Direction.UP, new bool[6] { false, false, false, false, false, true });
        throwingBobberRangeInfo[1, 0].Initialize(0, 0, Direction.RIGHT, new bool[6] { false, false, false, false, false, true });
        throwingBobberRangeInfo[2, 0].Initialize(0, 0, Direction.DOWN, new bool[6] { false, false, false, false, false, true });
        throwingBobberRangeInfo[3, 0].Initialize(0, 0, Direction.LEFT, new bool[6] { false, false, false, false, false, true });
    }

    private void InitCrackTheSky()
    {
        crackTheSkyRangeInfo[0].Initialize(-3, -1, Direction.UP, new bool[1] { true });
        crackTheSkyRangeInfo[1].Initialize(-1, 3, Direction.UP, new bool[1] { true });
        crackTheSkyRangeInfo[2].Initialize(3, 1, Direction.UP, new bool[1] { true });
        crackTheSkyRangeInfo[3].Initialize(1, -3, Direction.UP, new bool[1] { true });
    }

    private void InitForwardSlash()
    {
        // 위
        forwardSlash[0, 0].Initialize(-1, 0, Direction.UP, new bool[4]);
        forwardSlash[0, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();

        // 오른쪽
        forwardSlash[1, 0].Initialize(0, 1, Direction.RIGHT, new bool[4]);
        forwardSlash[1, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();

        // 아래
        forwardSlash[2, 0].Initialize(1, 0, Direction.DOWN, new bool[4]);
        forwardSlash[2, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();

        // 왼쪽
        forwardSlash[3, 0].Initialize(0, -1, Direction.LEFT, new bool[4]);
        forwardSlash[3, 0].isAttack = Enumerable.Repeat(true, 4).ToArray();
    }

    private void InitDigging()
    {
        // 위
        diggingRangeInfo[0, 0].Initialize(-1, -1, Direction.RIGHT, new bool[3]);
        diggingRangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
        diggingRangeInfo[0, 1].Initialize(-2, -1, Direction.RIGHT, new bool[3]);
        diggingRangeInfo[0, 1].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 오른쪽
        diggingRangeInfo[1, 0].Initialize(-1, 1, Direction.DOWN, new bool[3]);
        diggingRangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
        diggingRangeInfo[1, 1].Initialize(-1, 2, Direction.DOWN, new bool[3]);
        diggingRangeInfo[1, 1].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 아래
        diggingRangeInfo[2, 0].Initialize(1, -1, Direction.RIGHT, new bool[3]);
        diggingRangeInfo[2, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
        diggingRangeInfo[2, 1].Initialize(2, -1, Direction.RIGHT, new bool[3]);
        diggingRangeInfo[2, 1].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 왼쪽
        diggingRangeInfo[3, 0].Initialize(-1, -1, Direction.DOWN, new bool[3]);
        diggingRangeInfo[3, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
        diggingRangeInfo[3, 1].Initialize(-1, -2, Direction.DOWN, new bool[3]);
        diggingRangeInfo[3, 1].isAttack = Enumerable.Repeat(true, 3).ToArray();
    }

    private void InitThrowingBoom()
    {
        // 위
        throwingBoomRangeInfo[0, 0].Initialize(-4, 0, Direction.UP, new bool[3]);
        throwingBoomRangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
        throwingBoomRangeInfo[0, 1].Initialize(-5, -1, Direction.RIGHT, new bool[3] { true, false, true });

        // 오른쪽
        throwingBoomRangeInfo[1, 0].Initialize(0, 4, Direction.RIGHT, new bool[3]);
        throwingBoomRangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
        throwingBoomRangeInfo[1, 1].Initialize(-1, 5, Direction.DOWN, new bool[3] { true, false, true });

        // 아래
        throwingBoomRangeInfo[2, 0].Initialize(4, 0, Direction.DOWN, new bool[3]);
        throwingBoomRangeInfo[2, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
        throwingBoomRangeInfo[2, 1].Initialize(5, -1, Direction.RIGHT, new bool[3] { true, false, true });

        // 왼쪽
        throwingBoomRangeInfo[3, 0].Initialize(0, -4, Direction.LEFT, new bool[3]);
        throwingBoomRangeInfo[3, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
        throwingBoomRangeInfo[3, 1].Initialize(-1, -5, Direction.DOWN, new bool[3] { true, false, true });
    }

    private void InitSideSlash()
    {
        // 위
        sideSlashRangeInfo[0, 0].Initialize(-1, -1, Direction.RIGHT, new bool[3]);
        sideSlashRangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 오른쪽
        sideSlashRangeInfo[1, 0].Initialize(-1, 1, Direction.DOWN, new bool[3]);
        sideSlashRangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 아래
        sideSlashRangeInfo[2, 0].Initialize(1, -1, Direction.RIGHT, new bool[3]);
        sideSlashRangeInfo[2, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 왼쪽
        sideSlashRangeInfo[3, 0].Initialize(-1, -1, Direction.DOWN, new bool[3]);
        sideSlashRangeInfo[3, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
    }

    private void InitSpinningSlash()
    {
        for (int i = 0; i < 8; i++)
        {
            spinningSlashRangeInfo[i].Initialize(0, 0, Direction.UP + i, new bool[2] { false, true });
        }
    }

    private void InitSmashSlam()
    {
        // 위
        smashSlamRangeInfo[0, 0].Initialize(-1, 0, Direction.UP, new bool[1] { true });

        // 오른쪽
        smashSlamRangeInfo[1, 0].Initialize(0, 1, Direction.UP, new bool[1] { true });

        // 아래
        smashSlamRangeInfo[2, 0].Initialize(1, 0, Direction.UP, new bool[1] { true });

        // 왼쪽
        smashSlamRangeInfo[3, 0].Initialize(0, -1, Direction.UP, new bool[1] { true });
    }

    private void InitHitTheMark()
    {
        // 위
        hitTheMarkRangeInfo[0, 0].Initialize(-4, 0, Direction.UP, new bool[3]);
        hitTheMarkRangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 오른쪽
        hitTheMarkRangeInfo[1, 0].Initialize(0, 4, Direction.RIGHT, new bool[3]);
        hitTheMarkRangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 아래
        hitTheMarkRangeInfo[2, 0].Initialize(4, 0, Direction.DOWN, new bool[3]);
        hitTheMarkRangeInfo[2, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();

        // 왼쪽
        hitTheMarkRangeInfo[3, 0].Initialize(0, -4, Direction.LEFT, new bool[3]);
        hitTheMarkRangeInfo[3, 0].isAttack = Enumerable.Repeat(true, 3).ToArray();
    }

    private void InitSlingShot()
    {
        // 위
        slingShotRangeInfo[0, 0].Initialize(-3, 0, Direction.UP, new bool[1] { true });

        // 오른쪽
        slingShotRangeInfo[1, 0].Initialize(0, 3, Direction.UP, new bool[1] { true });

        // 아래
        slingShotRangeInfo[2, 0].Initialize(3, 0, Direction.UP, new bool[1] { true });

        // 왼쪽
        slingShotRangeInfo[3, 0].Initialize(0, -3, Direction.UP, new bool[1] { true });
    }

    private void InitDiffusionExplosion()
    {
        // 위
        diffusionExplosionRangeInfo[0, 0].Initialize(-1, -2, Direction.UP_RIGHT, new bool[5]);
        diffusionExplosionRangeInfo[0, 0].isAttack = Enumerable.Repeat(true, 5).ToArray();
        diffusionExplosionRangeInfo[0, 1].Initialize(-1, 2, Direction.LEFT_UP, new bool[5]);
        diffusionExplosionRangeInfo[0, 1].isAttack = Enumerable.Repeat(true, 5).ToArray();
        diffusionExplosionRangeInfo[0, 1].isAttack[2] = false;

        // 오른쪽
        diffusionExplosionRangeInfo[1, 0].Initialize(-2, 1, Direction.RIGHT_DOWN, new bool[5]);
        diffusionExplosionRangeInfo[1, 0].isAttack = Enumerable.Repeat(true, 5).ToArray();
        diffusionExplosionRangeInfo[1, 1].Initialize(2, 1, Direction.UP_RIGHT, new bool[5]);
        diffusionExplosionRangeInfo[1, 1].isAttack = Enumerable.Repeat(true, 5).ToArray();
        diffusionExplosionRangeInfo[1, 1].isAttack[2] = false;

        // 아래
        diffusionExplosionRangeInfo[2, 0].Initialize(1, 2, Direction.DOWN_LEFT, new bool[5]);
        diffusionExplosionRangeInfo[2, 0].isAttack = Enumerable.Repeat(true, 5).ToArray();
        diffusionExplosionRangeInfo[2, 1].Initialize(1, -2, Direction.RIGHT_DOWN, new bool[5]);
        diffusionExplosionRangeInfo[2, 1].isAttack = Enumerable.Repeat(true, 5).ToArray();
        diffusionExplosionRangeInfo[2, 1].isAttack[2] = false;

        // 왼쪽
        diffusionExplosionRangeInfo[3, 0].Initialize(-2, -1, Direction.DOWN_LEFT, new bool[5]);
        diffusionExplosionRangeInfo[3, 0].isAttack = Enumerable.Repeat(true, 5).ToArray();
        diffusionExplosionRangeInfo[3, 1].Initialize(2, -1, Direction.LEFT_UP, new bool[5]);
        diffusionExplosionRangeInfo[3, 1].isAttack = Enumerable.Repeat(true, 5).ToArray();
        diffusionExplosionRangeInfo[3, 1].isAttack[2] = false;
    }

    private AttackRangeInfo[] ConvertArray(AttackRangeInfo[,] rangeInfo, Direction dir)
    {
        int dirNumber = 0;

        switch(dir)
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

    public float GetSkillDamage(float damage, PlayerSkillType type)
    {
        float dmg = 0.0f;

        switch (type)
        {
            case PlayerSkillType.Swing:
                dmg = 10.0f + (damage - 1.0f) * 1.2f;
                break;
            case PlayerSkillType.StampingFeet:
                dmg = 10.0f + (damage - 1.0f);
                break;
            case PlayerSkillType.SonicWave:
                dmg = 15.0f + (damage - 1.0f) * 1.5f;
                break;
            case PlayerSkillType.ThrowingBobber:
                dmg = 20.0f + (damage - 1.0f) * 2.0f;
                break;
            case PlayerSkillType.CrackTheSky:
                dmg = 20.0f + (damage - 1.0f) * 1.7f;
                break;
            case PlayerSkillType.RisingSlash:
                dmg = 9.0f + (damage - 1.0f) * 0.8f;
                break;
            case PlayerSkillType.ForwardSlash:
                dmg = 10.0f + (damage - 1.0f) * 1.8f;
                break;
            case PlayerSkillType.Digging:
                dmg = 5.0f + (damage - 1.0f) * 2.0f;
                break;
            case PlayerSkillType.ThrowingBoom:
                dmg = 15.0f + (damage - 1.0f) * 1.7f;
                break;
            case PlayerSkillType.SideSlash:
                dmg = 17.0f + (damage - 1.0f) * 1.6f;
                break;
            case PlayerSkillType.SpinningSlash:
                dmg = 10.0f + (damage - 1.0f) * 2.5f;
                break;
            case PlayerSkillType.SmashSlam:
                dmg = 25.0f + (damage - 1.0f) * 1.8f;
                break;
            case PlayerSkillType.HitTheMark:
                dmg = 13.0f + (damage - 1.0f) * 2.0f;
                break;
            case PlayerSkillType.SlingShot:
                dmg = 22.0f + (damage - 1.0f) * 2.0f;
                break;
            case PlayerSkillType.DiffusionExplosion:
                dmg = 13.0f + (damage - 1.0f) * 1.5f;
                break;
            default:
                break;
        }

        return Mathf.FloorToInt(dmg);
    }

    public AttackRangeInfo[] GetPlayerSkill(PlayerSkillType type, Direction dir)
    {
        AttackRangeInfo[] rangeInfo = null;

        switch (type)
        {
            case PlayerSkillType.Swing:
                rangeInfo = ConvertArray(swingRangeInfo, dir);
                break;
            case PlayerSkillType.StampingFeet:
                rangeInfo = stampFeetRangeInfo;
                break;
            case PlayerSkillType.SonicWave:
                rangeInfo = sonicWaveRangeInfo;
                break;
            case PlayerSkillType.ThrowingBobber:
                rangeInfo = ConvertArray(throwingBobberRangeInfo, dir);
                break;
            case PlayerSkillType.CrackTheSky:
                rangeInfo = crackTheSkyRangeInfo;
                break;
            case PlayerSkillType.RisingSlash:
                rangeInfo = ConvertArray(risingSlash, dir);
                break;
            case PlayerSkillType.ForwardSlash:
                rangeInfo = ConvertArray(forwardSlash, dir);
                break;
            case PlayerSkillType.Digging:
                rangeInfo = ConvertArray(diggingRangeInfo, dir);
                break;
            case PlayerSkillType.ThrowingBoom:
                rangeInfo = ConvertArray(throwingBoomRangeInfo, dir);
                break;
            case PlayerSkillType.SideSlash:
                rangeInfo = ConvertArray(sideSlashRangeInfo, dir);
                break;
            case PlayerSkillType.SpinningSlash:
                rangeInfo = spinningSlashRangeInfo;
                break;
            case PlayerSkillType.SmashSlam:
                rangeInfo = ConvertArray(smashSlamRangeInfo, dir);
                break;
            case PlayerSkillType.HitTheMark:
                rangeInfo = ConvertArray(hitTheMarkRangeInfo, dir);
                break;
            case PlayerSkillType.SlingShot:
                rangeInfo = ConvertArray(slingShotRangeInfo, dir);
                break;
            case PlayerSkillType.DiffusionExplosion:
                rangeInfo = ConvertArray(diffusionExplosionRangeInfo, dir);
                break;
            default:
                break;
        }

        return rangeInfo;
    }
}
