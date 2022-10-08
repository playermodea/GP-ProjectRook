using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private GameObject player;

    [Header("Hit/Die Effect")]
    public GameObject hitEffect;                // Player, Enemy 피격시 이펙트
    public GameObject explosionEffect;          // FairyQueen의 폭탄 패턴, BomberMan 폭발 시 이펙트

    [Header("Player Skill")]
    public GameObject normalSlashEffect;
    public GameObject hitTheMarkEffect;
    public GameObject throwingBobberEffect;
    public GameObject diggingEffect;
    public GameObject spinningSlashEffect;
    public GameObject normalFireExplosionEffect;
    public GameObject normalSlamEffect;
    public GameObject sonicWaveEffect;
    public GameObject crackTheSkyEffect;
    public GameObject forwardSlashEffect;

    private static EffectManager instance;

    public static EffectManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<EffectManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object EffectManager");
                }
            }

            return instance;
        }
    }

    public void SetPlayer()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void CreateHitEffect(Vector3 pos)
    {
        StartCoroutine(CheckDestroy(Instantiate(hitEffect, pos, Quaternion.identity)));
    }

    public void CreateExplosionEffect(Vector3 pos)
    {
        StartCoroutine(CheckDestroy(Instantiate(explosionEffect, pos, Quaternion.identity)));
    }

    private void CreateNormalSlashEffect(Vector3 pos, PlayerSkillEffectInfo info)
    {
        Vector3 plusPos = pos + info.pos;
        Quaternion plusRot = Quaternion.Euler(info.rot);
        Vector3 plusScale = info.scale;
        GameObject effect = Instantiate(normalSlashEffect, plusPos, plusRot);
        effect.transform.localScale += plusScale;

        StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
    }

    private void CreateHitTheMarkEffect(Vector3 pos, PlayerSkillEffectInfo[] info)
    {
        Vector3 plusPos;
        Quaternion plusRot;
        Vector3 plusScale;
        GameObject effect;

        for (int i = 0; i < info.Length; i++)
        {
            plusPos = pos + info[i].pos;
            plusRot = Quaternion.Euler(info[i].rot);
            plusScale = info[i].scale;
            effect = Instantiate(hitTheMarkEffect, plusPos, plusRot);
            effect.transform.localScale += plusScale;

            if (i == info.Length - 1)
            {
                StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
            }
            else
            {
                StartCoroutine(CheckPlayerSkillEffectDestroy(effect, false));
            }
        }
    }

    private void CreateThrowingBobberEffect(Vector3 pos, PlayerSkillEffectInfo info)
    {
        Vector3 plusPos = pos + info.pos;
        Quaternion plusRot = Quaternion.Euler(info.rot);
        Vector3 plusScale = info.scale;
        GameObject effect = Instantiate(throwingBobberEffect, plusPos, plusRot);
        effect.transform.localScale += plusScale;

        StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
    }

    private void CreateDiggingEffect(Vector3 pos, PlayerSkillEffectInfo info)
    {
        Vector3 plusPos = pos + info.pos;
        Quaternion plusRot = Quaternion.Euler(info.rot);
        Vector3 plusScale = info.scale;
        GameObject effect = Instantiate(diggingEffect, plusPos, plusRot);
        effect.transform.localScale += plusScale;

        StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
    }

    private void CreateSpinningSlashEffect(Vector3 pos)
    {
        GameObject effect = Instantiate(spinningSlashEffect, pos + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
        effect.transform.localScale += new Vector3(0.5f, 0.5f, 0.0f);
        StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
    }

    private void CreateNormalFireExplosionEffect(Vector3 pos, PlayerSkillEffectInfo[] info)
    {
        Vector3 plusPos;
        Quaternion plusRot;
        Vector3 plusScale;
        GameObject effect;

        for (int i = 0; i < info.Length; i++)
        {
            plusPos = pos + info[i].pos;
            plusRot = Quaternion.Euler(info[i].rot);
            plusScale = info[i].scale;
            effect = Instantiate(normalFireExplosionEffect, plusPos, plusRot);
            effect.transform.localScale += plusScale;

            if (i == info.Length - 1)
            {
                StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
            }
            else
            {
                StartCoroutine(CheckPlayerSkillEffectDestroy(effect, false));
            }
        }
    }

    private void CreateNormalSlamEffect(Vector3 pos, PlayerSkillEffectInfo info)
    {
        Vector3 plusPos = pos + info.pos;
        Quaternion plusRot = Quaternion.Euler(info.rot);
        Vector3 plusScale = info.scale;
        GameObject effect = Instantiate(normalSlamEffect, plusPos, plusRot);
        effect.transform.localScale += plusScale;

        StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
    }

    private void CreateSonicWaveEffect(Vector3 pos)
    {
        GameObject effect = Instantiate(sonicWaveEffect, pos, Quaternion.identity);
        effect.transform.localScale += new Vector3(0.5f, 0.5f, 0.0f);
        StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
    }

    private void CreateCrackTheSkyEffect(Vector3 pos, PlayerSkillEffectInfo[] info)
    {
        Vector3 plusPos;
        Quaternion plusRot;
        Vector3 plusScale;
        GameObject effect;

        for (int i = 0; i < info.Length; i++)
        {
            plusPos = pos + info[i].pos;
            plusRot = Quaternion.Euler(info[i].rot);
            plusScale = info[i].scale;
            effect = Instantiate(crackTheSkyEffect, plusPos, plusRot);
            effect.transform.localScale += plusScale;

            if (i == info.Length - 1)
            {
                StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
            }
            else
            {
                StartCoroutine(CheckPlayerSkillEffectDestroy(effect, false));
            }
        }
    }

    private void CreateForwardSlashEffect(Vector3 pos, PlayerSkillEffectInfo info)
    {
        Vector3 plusPos = pos + info.pos;
        Quaternion plusRot = Quaternion.Euler(info.rot);
        Vector3 plusScale = info.scale;
        GameObject effect = Instantiate(forwardSlashEffect, plusPos, plusRot);
        effect.transform.localScale += plusScale;

        StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
    }

    private void CreateDiffusionExplosionEffect(Vector3 pos, PlayerSkillEffectInfo[] info)
    {
        Vector3 plusPos;
        Quaternion plusRot;
        Vector3 plusScale;
        GameObject effect;

        for (int i = 0; i < info.Length; i++)
        {
            plusPos = pos + info[i].pos;
            plusRot = Quaternion.Euler(info[i].rot);
            plusScale = info[i].scale;
            effect = Instantiate(normalFireExplosionEffect, plusPos, plusRot);
            effect.transform.localScale += plusScale;

            if (i == info.Length - 1)
            {
                StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
            }
            else
            {
                StartCoroutine(CheckPlayerSkillEffectDestroy(effect, false));
            }
        }
    }

    private void CreateSlingShotEffect(Vector3 pos, PlayerSkillEffectInfo info)
    {
        Vector3 plusPos = pos + info.pos;
        Quaternion plusRot = Quaternion.Euler(info.rot);
        Vector3 plusScale = info.scale;
        GameObject effect = Instantiate(normalFireExplosionEffect, plusPos, plusRot);
        effect.transform.localScale += plusScale;

        StartCoroutine(CheckPlayerSkillEffectDestroy(effect));
    }

    public void CreatePlayerSkillEffect(Vector3 pos, PlayerSkillType type, Direction dir)
    {
        switch (type)
        {
            case PlayerSkillType.Swing:
                AudioPlay.Instance.PlaySound("SlideEffect");
                CreateNormalSlashEffect(pos, PlayerSkillInfoCheck(type, dir));
                break;
            case PlayerSkillType.StampingFeet:
                AudioPlay.Instance.PlaySound("StabbingEffect");
                CreateNormalSlamEffect(pos, PlayerSkillInfoCheck(type, dir));
                break;
            case PlayerSkillType.SonicWave:
                AudioPlay.Instance.PlaySound("SonicWave");
                CreateSonicWaveEffect(pos);
                break;
            case PlayerSkillType.ThrowingBobber:
                AudioPlay.Instance.PlaySound("ThrowFloatEffcet");
                CreateThrowingBobberEffect(pos, PlayerSkillInfoCheck(type, dir));
                break;
            case PlayerSkillType.CrackTheSky:
                AudioPlay.Instance.PlaySound("SlideEffect");
                CreateCrackTheSkyEffect(pos, PlayerSkillInfoCheck2(type, dir));
                break;
            case PlayerSkillType.RisingSlash:
                AudioPlay.Instance.PlaySound("StabbingEffect");
                CreateNormalSlashEffect(pos, PlayerSkillInfoCheck(type, dir));
                break;
            case PlayerSkillType.ForwardSlash:
                AudioPlay.Instance.PlaySound("StabbingEffect");
                CreateForwardSlashEffect(pos, PlayerSkillInfoCheck(type, dir));
                break;
            case PlayerSkillType.Digging:
                AudioPlay.Instance.PlaySound("SlideEffect");
                CreateDiggingEffect(pos, PlayerSkillInfoCheck(type, dir));
                break;
            case PlayerSkillType.ThrowingBoom:
                AudioPlay.Instance.PlaySound("BoomEffect");
                CreateNormalFireExplosionEffect(pos, PlayerSkillInfoCheck2(type, dir));
                break;
            case PlayerSkillType.SideSlash:
                AudioPlay.Instance.PlaySound("SlideEffect");
                CreateNormalSlashEffect(pos, PlayerSkillInfoCheck(type, dir));
                break;
            case PlayerSkillType.SpinningSlash:
                AudioPlay.Instance.PlaySound("StabbingEffect");
                CreateSpinningSlashEffect(pos);
                break;
            case PlayerSkillType.SmashSlam:
                AudioPlay.Instance.PlaySound("StabbingEffect");
                CreateNormalSlamEffect(pos, PlayerSkillInfoCheck(type, dir));
                break;
            case PlayerSkillType.HitTheMark:
                AudioPlay.Instance.PlaySound("Hit");
                CreateHitTheMarkEffect(pos, PlayerSkillInfoCheck2(type, dir));
                break;
            case PlayerSkillType.SlingShot:
                AudioPlay.Instance.PlaySound("FireEffect");
                CreateSlingShotEffect(pos, PlayerSkillInfoCheck(type, dir));
                break;
            case PlayerSkillType.DiffusionExplosion:
                AudioPlay.Instance.PlaySound("BoomEffect");
                CreateDiffusionExplosionEffect(pos, PlayerSkillInfoCheck2(type, dir));
                break;
            default:
                break;
        }
    }

    private PlayerSkillEffectInfo PlayerSkillInfoCheck(PlayerSkillType type, Direction dir)
    {
        PlayerSkillEffectInfo info = new PlayerSkillEffectInfo();

        switch (type)
        {
            case PlayerSkillType.Swing:
                {
                    switch (dir)
                    {
                        case Direction.UP:
                            info.Initialize(new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.5f, 0.5f, 0.0f));
                            break;
                        case Direction.RIGHT:
                            info.Initialize(new Vector3(1.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 270.0f), new Vector3(0.5f, 0.5f, 0.0f));
                            break;
                        case Direction.DOWN:
                            info.Initialize(new Vector3(0.5f, -1.0f, 0.0f), new Vector3(180.0f, 180.0f, 0.0f), new Vector3(0.5f, 0.5f, 0.0f));
                            break;
                        case Direction.LEFT:
                            info.Initialize(new Vector3(-1.0f, 0.0f, 0.0f), new Vector3(0.0f, 180.0f, 270.0f), new Vector3(0.5f, 0.5f, 0.0f));
                            break;
                        default:
                            break;
                    }
                }
                break;
            case PlayerSkillType.StampingFeet:
                info.Initialize(new Vector3(0.0f, 1.0f, 0.0f), Vector3.zero, Vector3.zero);
                break;
            case PlayerSkillType.ThrowingBobber:
                {
                    switch (dir)
                    {
                        case Direction.UP:
                            info.Initialize(new Vector3(0.0f, 2.0f, 0.0f), new Vector3(0.0f, 0.0f, 90.0f), new Vector3(0.5f, 0.0f, 0.0f));
                            break;
                        case Direction.RIGHT:
                            info.Initialize(new Vector3(2.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.5f, 0.0f, 0.0f));
                            break;
                        case Direction.DOWN:
                            info.Initialize(new Vector3(0.0f, -2.0f, 0.0f), new Vector3(0.0f, 0.0f, 270.0f), new Vector3(0.5f, 0.0f, 0.0f));
                            break;
                        case Direction.LEFT:
                            info.Initialize(new Vector3(-2.0f, 0.0f, 0.0f), new Vector3(0.0f, 180.0f, 0.0f), new Vector3(0.5f, 0.0f, 0.0f));
                            break;
                        default:
                            break;
                    }
                }
                break;
            case PlayerSkillType.RisingSlash:
                {
                    switch(dir)
                    {
                        case Direction.UP:
                            info.Initialize(new Vector3(0.5f, 1.5f, 0.0f), new Vector3(180.0f, 0.0f, 270.0f), new Vector3(1.0f, 0.5f, 0.0f));
                            break;
                        case Direction.RIGHT:
                            info.Initialize(new Vector3(1.5f, -0.5f, 0.0f), new Vector3(180.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.5f, 0.0f));
                            break;
                        case Direction.DOWN:
                            info.Initialize(new Vector3(-0.5f, -1.5f, 0.0f), new Vector3(0.0f, 180.0f, 270.0f), new Vector3(1.0f, 0.5f, 0.0f));
                            break;
                        case Direction.LEFT:
                            info.Initialize(new Vector3(-1.5f, 0.5f, 0.0f), new Vector3(0.0f, 180.0f, 0.0f), new Vector3(1.0f, 0.5f, 0.0f));
                            break;
                        default:
                            break;
                    }
                }
                break;
            case PlayerSkillType.ForwardSlash:
                {
                    switch (dir)
                    {
                        case Direction.UP:
                            info.Initialize(new Vector3(-0.5f, 2.5f, 0.0f), new Vector3(0.0f, 0.0f, 90.0f), new Vector3(0.5f, 0.0f, 0.0f));
                            break;
                        case Direction.RIGHT:
                            info.Initialize(new Vector3(3.0f, 0.5f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.5f, 0.0f, 0.0f));
                            break;
                        case Direction.DOWN:
                            info.Initialize(new Vector3(-0.5f, -2.5f, 0.0f), new Vector3(0.0f, 180.0f, 270.0f), new Vector3(0.5f, 0.0f, 0.0f));
                            break;
                        case Direction.LEFT:
                            info.Initialize(new Vector3(-3.5f, 0.5f, 0.0f), new Vector3(0.0f, 180.0f, 0.0f), new Vector3(0.5f, 0.0f, 0.0f));
                            break;
                        default:
                            break;
                    }
                }
                break;
            case PlayerSkillType.Digging:
                {
                    switch (dir)
                    {
                        case Direction.UP:
                            info.Initialize(new Vector3(0.0f, 2.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        case Direction.RIGHT:
                            info.Initialize(new Vector3(1.5f, 0.5f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        case Direction.DOWN:
                            info.Initialize(new Vector3(0.0f, -1.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        case Direction.LEFT:
                            info.Initialize(new Vector3(-1.5f, 0.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        default:
                            break;
                    }
                }
                break;
            case PlayerSkillType.ThrowingBoom:
                break;
            case PlayerSkillType.SideSlash:
                {
                    switch (dir)
                    {
                        case Direction.UP:
                            info.Initialize(new Vector3(0.0f, 0.5f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.zero);
                            break;
                        case Direction.RIGHT:
                            info.Initialize(new Vector3(0.5f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 270.0f), Vector3.zero);
                            break;
                        case Direction.DOWN:
                            info.Initialize(new Vector3(0.0f, -0.5f, 0.0f), new Vector3(180.0f, 0.0f, 0.0f), Vector3.zero);
                            break;
                        case Direction.LEFT:
                            info.Initialize(new Vector3(-0.5f, 0.0f, 0.0f), new Vector3(0.0f, 180.0f, 270.0f), Vector3.zero);
                            break;
                        default:
                            break;
                    }
                }
                break;
            case PlayerSkillType.SmashSlam:
                {
                    switch (dir)
                    {
                        case Direction.UP:
                            info.Initialize(new Vector3(0.0f, 1.3f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(-0.5f, -0.5f, 0.0f));
                            break;
                        case Direction.RIGHT:
                            info.Initialize(new Vector3(1.0f, 0.3f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(-0.5f, -0.5f, 0.0f));
                            break;
                        case Direction.DOWN:
                            info.Initialize(new Vector3(0.0f, -0.7f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(-0.5f, -0.5f, 0.0f));
                            break;
                        case Direction.LEFT:
                            info.Initialize(new Vector3(-1.0f, 0.3f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(-0.5f, -0.5f, 0.0f));
                            break;
                        default:
                            break;
                    }
                }
                break;
            case PlayerSkillType.SlingShot:
                {
                    switch (dir)
                    {
                        case Direction.UP:
                            info.Initialize(new Vector3(0.0f, 4.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        case Direction.RIGHT:
                            info.Initialize(new Vector3(3.0f, 1.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        case Direction.DOWN:
                            info.Initialize(new Vector3(0.0f, -2.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        case Direction.LEFT:
                            info.Initialize(new Vector3(-3.0f, 1.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        default:
                            break;
                    }
                }
                break;
            default:
                break;
        }

        return info;
    }

    private PlayerSkillEffectInfo[] PlayerSkillInfoCheck2(PlayerSkillType type, Direction dir)
    {
        PlayerSkillEffectInfo[] infos = new PlayerSkillEffectInfo[1];

        switch(type)
        {
            case PlayerSkillType.HitTheMark:
                {
                    PlayerSkillEffectInfo[] info = new PlayerSkillEffectInfo[3];
                    switch (dir)
                    {
                        case Direction.UP:
                            info[0].Initialize(new Vector3(0.0f, 6.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            info[1].Initialize(new Vector3(0.0f, 5.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            info[2].Initialize(new Vector3(0.0f, 4.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            break;
                        case Direction.RIGHT:
                            info[0].Initialize(new Vector3(4.0f, 0.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            info[1].Initialize(new Vector3(5.0f, 0.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            info[2].Initialize(new Vector3(6.0f, 0.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            break;
                        case Direction.DOWN:
                            info[0].Initialize(new Vector3(0.0f, -5.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            info[1].Initialize(new Vector3(0.0f, -4.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            info[2].Initialize(new Vector3(0.0f, -3.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            break;
                        case Direction.LEFT:
                            info[0].Initialize(new Vector3(-4.0f, 0.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            info[1].Initialize(new Vector3(-5.0f, 0.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            info[2].Initialize(new Vector3(-6.0f, 0.5f, 0.0f), Vector3.zero, new Vector3(-0.3f, -0.3f, 0.0f));
                            break;
                        default:
                            break;
                    }
                    return info;
                }
            case PlayerSkillType.ThrowingBoom:
                {
                    PlayerSkillEffectInfo[] info = new PlayerSkillEffectInfo[5];
                    switch (dir)
                    {
                        case Direction.UP:
                            info[0].Initialize(new Vector3(0.0f, 5.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[1].Initialize(new Vector3(0.0f, 6.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[2].Initialize(new Vector3(0.0f, 7.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[3].Initialize(new Vector3(-1.0f, 6.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[4].Initialize(new Vector3(1.0f, 6.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        case Direction.RIGHT:
                            info[0].Initialize(new Vector3(4.0f, 1.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[1].Initialize(new Vector3(5.0f, 1.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[2].Initialize(new Vector3(6.0f, 1.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[3].Initialize(new Vector3(5.0f, 2.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[4].Initialize(new Vector3(5.0f, 0.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        case Direction.DOWN:
                            info[0].Initialize(new Vector3(0.0f, -3.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[1].Initialize(new Vector3(0.0f, -4.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[2].Initialize(new Vector3(0.0f, -5.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[3].Initialize(new Vector3(-1.0f, -4.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[4].Initialize(new Vector3(1.0f, -4.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        case Direction.LEFT:
                            info[0].Initialize(new Vector3(-4.0f, 1.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[1].Initialize(new Vector3(-5.0f, 1.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[2].Initialize(new Vector3(-6.0f, 1.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[3].Initialize(new Vector3(-5.0f, 2.0f, 0.0f), Vector3.zero, Vector3.zero);
                            info[4].Initialize(new Vector3(-5.0f, 0.0f, 0.0f), Vector3.zero, Vector3.zero);
                            break;
                        default:
                            break;
                    }
                    return info;
                }
            case PlayerSkillType.DiffusionExplosion:
                {
                    PlayerSkillEffectInfo[] info = new PlayerSkillEffectInfo[10];
                    switch (dir)
                    {
                        case Direction.UP:
                            for (int i = 0; i < 5; i++)
                            {
                                info[i].Initialize(new Vector3(2.0f - i, 2.0f + i, 0.0f), Vector3.zero, Vector3.zero);
                            }
                            for (int i = 0; i < 5; i++)
                            {
                                info[i + 5].Initialize(new Vector3(-2.0f + i, 2.0f + i, 0.0f), Vector3.zero, Vector3.zero);
                            }
                            break;
                        case Direction.RIGHT:
                            for (int i = 0; i < 5; i++)
                            {
                                info[i].Initialize(new Vector3(1.0f + i, 3.0f - i, 0.0f), Vector3.zero, Vector3.zero);
                            }
                            for (int i = 0; i < 5; i++)
                            {
                                info[i + 5].Initialize(new Vector3(1.0f + i, -1.0f + i, 0.0f), Vector3.zero, Vector3.zero);
                            }
                            break;
                        case Direction.DOWN:
                            for (int i = 0; i < 5; i++)
                            {
                                info[i].Initialize(new Vector3(-2.0f + i, 0.0f - i, 0.0f), Vector3.zero, Vector3.zero);
                            }
                            for (int i = 0; i < 5; i++)
                            {
                                info[i + 5].Initialize(new Vector3(2.0f - i, 0.0f - i, 0.0f), Vector3.zero, Vector3.zero);
                            }
                            break;
                        case Direction.LEFT:
                            for (int i = 0; i < 5; i++)
                            {
                                info[i].Initialize(new Vector3(-1.0f - i, -1.0f + i, 0.0f), Vector3.zero, Vector3.zero);
                            }
                            for (int i = 0; i < 5; i++)
                            {
                                info[i + 5].Initialize(new Vector3(-1.0f - i, 3.0f - i, 0.0f), Vector3.zero, Vector3.zero);
                            }
                            break;
                        default:
                            break;
                    }
                    return info;
                }
            case PlayerSkillType.CrackTheSky:
                {
                    PlayerSkillEffectInfo[] info = new PlayerSkillEffectInfo[4];
                    {
                        info[0].Initialize(new Vector3(3.0f, 2.5f, 0.0f), Vector3.zero, Vector3.zero);
                        info[1].Initialize(new Vector3(-1.0f, 4.5f, 0.0f), Vector3.zero, Vector3.zero);
                        info[2].Initialize(new Vector3(-3.0f, 0.5f, 0.0f), Vector3.zero, Vector3.zero);
                        info[3].Initialize(new Vector3(1.0f, -1.5f, 0.0f), Vector3.zero, Vector3.zero);
                    }
                    return info;
                }
            default:
                break;
        }

        return infos;
    }

    private IEnumerator CheckPlayerSkillEffectDestroy(GameObject obj, bool playerAttackCheck = true)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            if (obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                if (playerAttackCheck)
                {
                    player.GetComponent<Player>().Attack();
                }
                Destroy(obj);
                break;
            }
        }
    }

    private IEnumerator CheckDestroy(GameObject obj)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(obj);
                break;
            }
        }
    }
}
