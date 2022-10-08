using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    private Player player;

    private bool cheat = false;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCheat();
        }

        if (cheat)
        {
            // 스탯 및 현재 맵 적 전부 죽이기 관련 치트
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    player.SetDamage(10.0f);
                    UIManager.Instance.Dialog_Text("치트:데미지 +10");
                    UIManager.Instance.Full_Dialog_Text("치트:데미지 +10");
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    player.SetDamage(-10.0f);
                    UIManager.Instance.Dialog_Text("치트:데미지 -10");
                    UIManager.Instance.Full_Dialog_Text("치트:데미지 -10");
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    player.SetGold(10);
                    UIManager.Instance.Dialog_Text("치트:골드 +10");
                    UIManager.Instance.Full_Dialog_Text("치트:골드 +10");
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    player.SetGold(-10);
                    UIManager.Instance.Dialog_Text("치트:골드 -10");
                    UIManager.Instance.Full_Dialog_Text("치트:골드 -10");
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        player.SetMovePoint(1, Direction.UP + i);
                    }
                    UIManager.Instance.Dialog_Text("치트:전방향 이동력 +1");
                    UIManager.Instance.Full_Dialog_Text("치트:전방향 이동력 +1");
                }
                if (Input.GetKeyDown(KeyCode.N))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        player.SetMovePoint(-1, Direction.UP + i);
                    }
                    UIManager.Instance.Dialog_Text("치트:전방향 이동력 -1");
                    UIManager.Instance.Full_Dialog_Text("치트:전방향 이동력 -1");
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    player.SetEvasion(10.0f, true);
                    UIManager.Instance.Dialog_Text("치트:회피력 +10%");
                    UIManager.Instance.Full_Dialog_Text("치트:회피력 +10%");
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    player.SetEvasion(-10.0f, true);
                    UIManager.Instance.Dialog_Text("치트:회피력 -10%");
                    UIManager.Instance.Full_Dialog_Text("치트:회피력 -10%");
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    player.movement.floorTile.CheatKillAllEnemy();
                    UIManager.Instance.Dialog_Text("치트:현재 맵 적 전부 죽이기");
                    UIManager.Instance.Full_Dialog_Text("치트:현재 맵 적 전부 죽이기");
                }
                if (Input.GetKeyDown(KeyCode.J))
                {
                    player.SetMaxHP(100.0f);
                    UIManager.Instance.Dialog_Text("치트:풀피 회복");
                    UIManager.Instance.Full_Dialog_Text("치트:풀피 회복");
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    player.OnDamage(0, 0, 1000.0f);
                    UIManager.Instance.Dialog_Text("치트:플레이어에게 1000데미지");
                    UIManager.Instance.Full_Dialog_Text("치트:플레이어 1000데미지");
                }
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    player.SetMaxActionPoint(1);
                    UIManager.Instance.Dialog_Text("치트:행동력 +1");
                    UIManager.Instance.Full_Dialog_Text("치트:행동력 +1");
                    UIManager.Instance.SetEnergySlot(player.maxActionPoint - player.curActionPoint, player.maxActionPoint);
                }
                if (Input.GetKeyDown(KeyCode.T))
                {
                    player.SetMaxActionPoint(-1);
                    UIManager.Instance.Dialog_Text("치트:행동력 -1");
                    UIManager.Instance.Full_Dialog_Text("치트:행동력 -1");
                    UIManager.Instance.SetEnergySlot(player.maxActionPoint - player.curActionPoint, player.maxActionPoint);
                }
            }

            // 스킬 관련 치트
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad0))
                {
                    UIManager.Instance.Dialog_Text("치트:회전베기 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:회전베기 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.SpinningSlash);
                    return;
                }
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad1))
                {
                    UIManager.Instance.Dialog_Text("치트:발구르기 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:발구르기 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.StampingFeet);
                    return;
                }
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad2))
                {
                    UIManager.Instance.Dialog_Text("치트:휘두르기 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:휘두르기 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.Swing);
                    return;
                }
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad3))
                {
                    UIManager.Instance.Dialog_Text("치트:찌 투척 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:찌 투척 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.ThrowingBobber);
                    return;
                }
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad4))
                {
                    UIManager.Instance.Dialog_Text("치트:폭탄투척 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:폭탄투척 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.ThrowingBoom);
                    return;
                }
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    UIManager.Instance.Dialog_Text("치트:낙뢰 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:낙뢰 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.CrackTheSky);
                }
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    UIManager.Instance.Dialog_Text("치트:확산폭발 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:확산폭발 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.DiffusionExplosion);
                }
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    UIManager.Instance.Dialog_Text("치트:앞찍기 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:앞찍기 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.Digging);
                }
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    UIManager.Instance.Dialog_Text("치트:찌르기 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:찌르기 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.ForwardSlash);
                }
                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    UIManager.Instance.Dialog_Text("치트:적중 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:적중 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.HitTheMark);
                }
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    UIManager.Instance.Dialog_Text("치트:세로베기 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:세로베기 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.RisingSlash);
                }
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    UIManager.Instance.Dialog_Text("치트:가로베기 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:가로베기 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.SideSlash);
                }
                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    UIManager.Instance.Dialog_Text("치트:화염 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:화염 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.SlingShot);
                }
                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    UIManager.Instance.Dialog_Text("치트:내려찍기 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:내려찍기 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.SmashSlam);
                }
                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    UIManager.Instance.Dialog_Text("치트:음파 스킬 얻기");
                    UIManager.Instance.Full_Dialog_Text("치트:음파 스킬 얻기");
                    UIManager.Instance.SetSlot(PlayerSkillType.SonicWave);
                }
            }
        }
    }

    private void SetCheat()
    {
        if (cheat)
        {
            cheat = false;
            UIManager.Instance.Dialog_Text("치트모드가 비활성화 되었습니다.");
            UIManager.Instance.Full_Dialog_Text("치트모드가 비활성화 되었습니다.");
        }
        else
        {
            cheat = true;
            UIManager.Instance.Dialog_Text("치트모드가 활성화 되었습니다.");
            UIManager.Instance.Full_Dialog_Text("치트모드가 활성화 되었습니다.");
        }
    }
}
