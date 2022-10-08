using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private int endTextIndex;
    private float shakeTime;
    private float shakeAmount;
    private float fadeAmount;
    private bool isShake;
    private bool isFadeOut;
    private bool isFadeIn;
    private bool next;
    private bool skip;
    private Vector2 cursorPivot;
    private Vector3 camPos;
    private GameObject camera;
    private GameObject player;

    private int changeSlotNumber;
    private PlayerSkillType changeSkillType;
    private bool isGameover;
    private bool[] availableSlot;
    private PlayerSkillType[] slotType;

    internal void Dialog_Text()
    {
        throw new System.NotImplementedException();
    }

    private const string swingText = "휘두르기";
    private const string stampingFeetText = "발구르기";
    private const string sonicWaveText = "음파";
    private const string throwingBobberText = "찌 투척";
    private const string crackTheSkyText = "낙뢰";
    private const string risingSlashText = "세로베기";
    private const string forwardSlashText = "찌르기";
    private const string diggingText = "앞찍기";
    private const string throwingBoomText = "폭탄투척";
    private const string sideSlashText = "가로베기";
    private const string spinningSlashText = "회전베기";
    private const string smashSlamText = "내려찍기";
    private const string hitTheMarkText = "적중";
    private const string slingShotText = "화염";
    private const string diffusionExplosionText = "확산폭발";

    private const string changeSkillText = "교체할 스킬을 선택하여 주십시오.";
    private const string notChangeSkillText = "스킬을 교체하지 않았습니다.";

    [Header("Panel")]
    public GameObject AllInGamePanel;
    public GameObject GameoverPanel;
    public GameObject FadeEffectPanel;
    public GameObject SkillChangePanel;
    public GameObject SkillChangeCheckPanel;
    public GameObject EndingPanel;

    [Header("Ending")]
    public GameObject nextText;
    public Text showEndingText;
    public string[] endingText;

    [Header("Skill Change")]
    public Text changeSkillNameText;
    public Text slot_1_text;
    public Text slot_2_text;
    public Text slot_3_text;
    public Text slot_4_text;

    [Header("Minimap")]
    public Text floornumber;

    [Header("Pause")]
    public GameObject PausePanel;
    public GameObject Continue;
    public GameObject MainMenu;

    [Header("Player Skill")]
    public Text skillName_1;
    public Text skillName_2;
    public Text skillName_3;
    public Text skillName_4;
    public Text skillDamage_1;
    public Text skillDamage_2;
    public Text skillDamage_3;
    public Text skillDamage_4;
    public Text playerDmg;
    public Text playerEva;
    public Text playerGold;
    public GameObject slot_3;
    public GameObject slot_4;

    [Header("Dialog")]
    public GameObject fullDialog;
    public Text[] infoSmallDialog;
    public Text[] infoFullDialog;

    [Header("Player HP")]
    public GameObject[] maxHPImage;
    public GameObject[] hpImage;

    [Header("Player Energy")]
    public GameObject[] maxEnergyImage;
    public GameObject[] energyImage;

    [Header("Cursor")]
    public Texture2D noneClickCursor;
    public Texture2D clickCursor;

    [Header("Audio")]
    public GameObject musicsource;
    public GameObject effectsource;

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object UIManager");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        endTextIndex = 0;
        shakeTime = 0.5f;
        shakeAmount = 0.1f;
        fadeAmount = 0.1f;
        isShake = false;
        isGameover = false;
        isFadeOut = false;
        isFadeIn = false;
        next = false;
        skip = false;
        cursorPivot = new Vector2(7.0f, 4.0f);

        availableSlot = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            availableSlot[i] = true;
        }

        slotType = new PlayerSkillType[4];
        for (int i = 0; i < 4; i++)
        {
            slotType[i] = PlayerSkillType.None;
        }
    }

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(clickCursor, cursorPivot, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(noneClickCursor, cursorPivot, CursorMode.ForceSoftware);
        }

        if (isShake)
        {
            if (shakeTime >= 0.0f)
            {
                camera.transform.position = Random.insideUnitSphere * shakeAmount + camPos;
                shakeTime -= Time.deltaTime;
            }
            else
            {
                isShake = false;
                shakeTime = 0.5f;
                camera.transform.position = camPos;
            }
        }

        if (isFadeOut)
        {
            fadeAmount += 520.0f * Time.deltaTime;
            FadeEffectPanel.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, fadeAmount / 255.0f);

            if (fadeAmount / 255.0f >= 1.0f)
            {
                isFadeOut = false;
                if (MapManager.Instance.stageNumber == MapManager.Instance.endStageNumber)
                {
                    MapManager.Instance.DestroyGameScene();
                    SetEnding();
                }
                else
                {
                    MapManager.Instance.CreateNextStage();
                }
                isFadeIn = true;
            }
        }

        if (isFadeIn)
        {
            fadeAmount -= 520.0f * Time.deltaTime;
            FadeEffectPanel.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, fadeAmount / 255.0f);

            if (fadeAmount / 255.0f <= 0.0f)
            {
                isFadeIn = false;
                if (MapManager.Instance.end)
                {
                    ShowEnding();
                }
            }
        }

        if (isGameover)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //AudioPlay.Instance.StopMusic();
                SceneManager.LoadScene("LobbyScene");
            }
        }

        if (skip && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LobbyScene");
        }

        if (next && Input.GetKeyDown(KeyCode.Return))
        {
            endTextIndex++;
            next = false;
            nextText.SetActive(false);
            StartCoroutine(ShowEndingText(endTextIndex));
        }

        Pause();
    }

    // Skill 슬롯 텍스트 설정
    private string SetSlotText(Text skillName, Text skillDamage, PlayerSkillType type)
    {
        Player playerSet = player.GetComponent<Player>();

        switch (type)
        {
            case PlayerSkillType.Swing:
                skillName.text = swingText;
                break;
            case PlayerSkillType.StampingFeet:
                skillName.text = stampingFeetText;
                break;
            case PlayerSkillType.SonicWave:
                skillName.text = sonicWaveText;
                break;
            case PlayerSkillType.ThrowingBobber:
                skillName.text = throwingBobberText;
                break;
            case PlayerSkillType.CrackTheSky:
                skillName.text = crackTheSkyText;
                break;
            case PlayerSkillType.RisingSlash:
                skillName.text = risingSlashText;
                break;
            case PlayerSkillType.ForwardSlash:
                skillName.text = forwardSlashText;
                break;
            case PlayerSkillType.Digging:
                skillName.text = diggingText;
                break;
            case PlayerSkillType.ThrowingBoom:
                skillName.text = throwingBoomText;
                break;
            case PlayerSkillType.SideSlash:
                skillName.text = sideSlashText;
                break;
            case PlayerSkillType.SpinningSlash:
                skillName.text = spinningSlashText;
                break;
            case PlayerSkillType.SmashSlam:
                skillName.text = smashSlamText;
                break;
            case PlayerSkillType.HitTheMark:
                skillName.text = hitTheMarkText;
                break;
            case PlayerSkillType.SlingShot:
                skillName.text = slingShotText;
                break;
            case PlayerSkillType.DiffusionExplosion:
                skillName.text = diffusionExplosionText;
                break;
            default:
                break;
        }

        if (skillDamage != null)
        {
            skillDamage.text = playerSet.skill.GetSkillDamage(playerSet.attackDamage, type) + "";
        }

        return skillName.text;
    }

    // Skill 슬롯 1번 버튼 액션
    public void OnSkillSlot_1()
    {
        Player playerSet = player.GetComponent<Player>();

        if (playerSet.isPlaning)
        {
            if (playerSet.isMovePlaning || playerSet.curSkillType != slotType[0]) 
            {
                playerSet.isMovePlaning = false;
                playerSet.isAttackPlaning = true;
                playerSet.curSkillType = slotType[0];
            }
            else
            {
                playerSet.isMovePlaning = true;
                playerSet.isAttackPlaning = false;
            }
        }
    }

    // Skill 슬롯 2번 버튼 액션
    public void OnSkillSlot_2()
    {
        Player playerSet = player.GetComponent<Player>();

        if (playerSet.isPlaning)
        {
            if (playerSet.isMovePlaning || playerSet.curSkillType != slotType[1])
            {
                playerSet.isMovePlaning = false;
                playerSet.isAttackPlaning = true;
                playerSet.curSkillType = slotType[1];
            }
            else
            {
                playerSet.isMovePlaning = true;
                playerSet.isAttackPlaning = false;
            }
        }
    }

    // Skill 슬롯 3번 버튼 액션
    public void OnSkillSlot_3()
    {
        Player playerSet = player.GetComponent<Player>();

        if (playerSet.isPlaning && slotType[2] != PlayerSkillType.None)
        {
            if (playerSet.isMovePlaning || playerSet.curSkillType != slotType[2])
            {
                playerSet.isMovePlaning = false;
                playerSet.isAttackPlaning = true;
                playerSet.curSkillType = slotType[2];
            }
            else
            {
                playerSet.isMovePlaning = true;
                playerSet.isAttackPlaning = false;
            }
        }
    }

    // Skill 슬롯 4번 버튼 액션
    public void OnSkillSlot_4()
    {
        Player playerSet = player.GetComponent<Player>();

        if (playerSet.isPlaning && slotType[3] != PlayerSkillType.None)
        {
            if (playerSet.isMovePlaning || playerSet.curSkillType != slotType[3])
            {
                playerSet.isMovePlaning = false;
                playerSet.isAttackPlaning = true;
                playerSet.curSkillType = slotType[3];
            }
            else
            {
                playerSet.isMovePlaning = true;
                playerSet.isAttackPlaning = false;
            }
        }
    }

    // 턴 종료 버튼 액션
    public void EndPlayerPlaning()
    {
        Player playerSet = player.GetComponent<Player>();

        if (playerSet.isPlaning && playerSet.curActionPoint == playerSet.maxActionPoint)
        {
            playerSet.isPlaning = false;
            playerSet.isAttackPlaning = false;
            playerSet.isMovePlaning = false;
            playerSet.movement.floorTile.RefreshTile();
            playerSet.SortingPlanStack();
            TurnManager.Instance.PlayerTurnStart();
        }
    }

    // 되돌리기 버튼 액션
    public void UndoPlayerPlan()
    {
        Player playerSet = player.GetComponent<Player>();

        if (playerSet.isPlaning)
        {
            playerSet.isMovePlaning = true;
            playerSet.isAttackPlaning = false;
            playerSet.UndoPlan();
        }
    }

    // Player 데미지 텍스트 설정
    public void SetPlayerDMG(float value)
    {
        playerDmg.text = value + "";

        for (int i = 0; i < 4; i++)
        {
            if (slotType[i] != PlayerSkillType.None)
            {
                SetSlot(slotType[i], i + 1, true);
            }
        }
    }

    // Player 회피력 텍스트 설정
    public void SetPlayerEva(float value)
    {
        playerEva.text = value + "";
    }

    // Player 골드 설정
    public void SetPlayerGold(float value)
    {
        playerGold.text = value + "";
    }

    // Skill 슬롯 설정
    public void SetSlot(PlayerSkillType type, int slotNumber = -1, bool refresh = false)
    {
        int slot = slotNumber;
        if (slot == -1)
        {
            for (int i = 0; i < 4; i++)
            {
                if (availableSlot[i])
                {
                    slot = i + 1;
                    break;
                }
            }
        }

        switch (slot)
        {
            case -1:
                ChangeSkill(type);
                return;
            case 1:
                slotType[0] = type;
                availableSlot[0] = false;
                SetSlotText(skillName_1, skillDamage_1, slotType[0]);
                break;
            case 2:
                slotType[1] = type;
                availableSlot[1] = false;
                SetSlotText(skillName_2, skillDamage_2, slotType[1]);
                break;
            case 3:
                if (!slot_3.active)
                {
                    slot_3.SetActive(true);
                }
                slotType[2] = type;
                availableSlot[2] = false;
                SetSlotText(skillName_3, skillDamage_3, slotType[2]);
                break;
            case 4:
                if (!slot_4.active)
                {
                    slot_4.SetActive(true);
                }
                slotType[3] = type;
                availableSlot[3] = false;
                SetSlotText(skillName_4, skillDamage_4, slotType[3]);
                break;
            default:
                break;
        }

        if (!refresh)
        {
            string msg = "스킬 [" + SetSlotText(changeSkillNameText, null, type) + "] 획득";

            Dialog_Text(msg);
            Full_Dialog_Text(msg);
        }
    }

    // SkillChange Panel On
    private void ChangeSkill(PlayerSkillType type)
    {
        Player playerSet = player.GetComponent<Player>();
        string msg;

        msg = SetSlotText(changeSkillNameText, null, type) + " " + playerSet.skill.GetSkillDamage(playerSet.attackDamage, type) + " DMG";
        changeSkillNameText.text = msg;
        SkillChangePanel.SetActive(true);
        changeSkillType = type;
        slot_1_text.text = skillName_1.text;
        slot_2_text.text = skillName_2.text;
        slot_3_text.text = skillName_3.text;
        slot_4_text.text = skillName_4.text;
        Dialog_Text(changeSkillText);
        Full_Dialog_Text(changeSkillText);
    }

    // SkillChange Cancel
    public void CancelSkillChange()
    {
        Dialog_Text(notChangeSkillText);
        Full_Dialog_Text(notChangeSkillText);
        SkillChangeCheckPanel.SetActive(false);
        SkillChangePanel.SetActive(false);
    }

    // SkillChangeCheck Panel On
    public void ChangeSkillCheck(int slotNumber)
    {
        changeSlotNumber = slotNumber;
        SkillChangeCheckPanel.SetActive(true);
    }

    // SkillChangeCheck Cancel
    public void CancelChangeSkillCheck()
    {
        SkillChangeCheckPanel.SetActive(false);
    }

    // SkillChange
    public void ChangeSkill()
    {
        SetSlot(changeSkillType, changeSlotNumber);
        SkillChangePanel.SetActive(false);
        SkillChangeCheckPanel.SetActive(false);
    }

    // HP UI 설정
    public void SetHPSlot(float hp, float maxHP)
    {
        for (int i = 0; i < maxHPImage.Length; i++)
        {
            maxHPImage[i].SetActive(false);
        }

        for (int i = 0; i < maxHP / 2; i++)
        {
            maxHPImage[i].SetActive(true);
        }

        for (int i = 0; i < hpImage.Length; i++)
        {
            hpImage[i].SetActive(false);
        }

        for (int i = 0; i < hp; i++)
        {
            hpImage[i].SetActive(true);
        }
    }

    // Energy UI 설정
    public void SetEnergySlot(int energy, int maxEnergy)
    {
        for (int i = 0; i < energyImage.Length; i++)
        {
            maxEnergyImage[i].SetActive(false);
        }

        for (int i = 0; i < maxEnergy; i++)
        {
            maxEnergyImage[i].SetActive(true);
        }

        for (int i = 0; i < energyImage.Length; i++)
        {
            energyImage[i].SetActive(false);
        }

        for (int i = 0; i < energy; i++)
        {
            energyImage[i].SetActive(true);
        }
    }

    // Player 설정
    public void SetPlayer()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Player 피격시 Camera 흔들기
    public void CameraShake()
    {
        isShake = true;
        camPos = camera.transform.position;
    }

    // Stage 넘어갈 시 Fade Out, In 효과
    public void StageFadeOut()
    {
        isFadeOut = true;
    }

    // 게임오버 UI
    public void Gameover()
    {
        isGameover = true;

        if(isGameover)
        {
            //AudioPlay.Instance.StopMusic();
            //MusicPlay.Instance.StopMusic();
            MusicPlay.Instance.PlaySound("GameOver");
            AudioPlay.Instance.PlaySound("Player_Dead");
        }

        GameoverPanel.SetActive(true);
    }

    public void ChangeDialogue()
    {
        if (fullDialog.active)
        {
            fullDialog.SetActive(false);
        }
        else
        {
            fullDialog.SetActive(true);
        }
    }

    //반시계 방향 회전
    public void CounterClockwise_RotatingSkill()
    {
        //.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
        player.GetComponent<Player>().curSkillDir -= 2;
        if((int)player.GetComponent<Player>().curSkillDir == -2)
        {
            player.GetComponent<Player>().curSkillDir = (Direction)6;
        }
        if ((int)player.GetComponent<Player>().curSkillDir == -4)
        {
            player.GetComponent<Player>().curSkillDir = (Direction)4;
        }
        if ((int)player.GetComponent<Player>().curSkillDir == -6)
        {
            player.GetComponent<Player>().curSkillDir = (Direction)2;
        }
        if ((int)player.GetComponent<Player>().curSkillDir <= -7)
        {
            player.GetComponent<Player>().curSkillDir = 0;
        }
    }

    //시계 방향 회전
    public void Clockwise_RotatingSkill()
    {
        player.GetComponent<Player>().curSkillDir += 2;
        if ((int)player.GetComponent<Player>().curSkillDir >= 7)
        {
            player.GetComponent<Player>().curSkillDir = 0;
        }
    }

    //다이얼로그
    public void Dialog_Text(string dialog_text)
    {
        SmallDialogUpdate(dialog_text);
        //dialog.text += dialog_text;
        //Dialog_Scroll.verticalNormalizedPosition = 0.0f;
    }

    //풀 다이얼로그
    public void Full_Dialog_Text(string full_dialog_text)
    {
        FullDialogUpdate(full_dialog_text);
        //full_dialog.text += full_dialog_text;
        //FullDialog_Scroll.verticalNormalizedPosition = 0.0f;
    }

    // 작은 다이얼로그 업데이트
    private void SmallDialogUpdate(string msg)
    {
        int index = msg.IndexOf("\n");
        bool isInput = false;

        for (int i = 0; i < infoSmallDialog.Length; i++)
        {
            if (infoSmallDialog[i].text == "")
            {
                isInput = true;
                if (index == -1)
                {
                    infoSmallDialog[i].text = msg;
                }
                else
                {
                    infoSmallDialog[i].text = msg.Substring(0, index - 1);
                }
                break;
            }
        }

        if (!isInput)
        {
            for (int i = 1; i < infoSmallDialog.Length; i++)
            {
                infoSmallDialog[i - 1].text = infoSmallDialog[i].text;
            }

            if (index == -1)
            {
                infoSmallDialog[infoSmallDialog.Length - 1].text = msg;
            }
            else
            {
                infoSmallDialog[infoSmallDialog.Length - 1].text = msg.Substring(0, index - 1);
            }
        }

        if (index != -1)
        {
            SmallDialogUpdate(msg.Substring(index + 1));
        }
    }

    // 큰 다이얼로그 업데이트
    private void FullDialogUpdate(string msg)
    {
        int index = msg.IndexOf("\n");
        bool isInput = false;

        for (int i = 0; i < infoFullDialog.Length; i++)
        {
            if (infoFullDialog[i].text == "")
            {
                isInput = true;
                if (index == -1)
                {
                    infoFullDialog[i].text = msg;
                }
                else
                {
                    infoFullDialog[i].text = msg.Substring(0, index - 1);
                }
                break;
            }
        }

        if (!isInput)
        {
            for (int i = 1; i < infoFullDialog.Length; i++)
            {
                infoFullDialog[i - 1].text = infoFullDialog[i].text;
            }

            if (index == -1)
            {
                infoFullDialog[infoFullDialog.Length - 1].text = msg;
            }
            else
            {
                infoFullDialog[infoFullDialog.Length - 1].text = msg.Substring(0, index - 1);
            }
        }

        if (index != -1)
        {
            FullDialogUpdate(msg.Substring(index + 1));
        }
    }

    public void Stage_Number()
    {
        floornumber.text = MapManager.Instance.stageNumber.ToString();
    }

    public void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanel.SetActive(true);
        }
    }

    public void ContinueButton()
    {
        PausePanel.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    // 터치 방지
    public bool IsPointerOverUIObject()
    {
        if (SkillChangePanel.active || PausePanel.active)
        {
            return false;
        }

        return true;
    }

    // 엔딩 문구 설정
    private void SetEnding()
    {
        MusicPlay.Instance.PlaySound("Ending");
        endingText[5] = "\n" + ScoreManager.Instance.totalDamagedCount + "번의 공격을 받아내며,";
        endingText[6] = "\n" + ScoreManager.Instance.totalKillCount + "마리의 몬스터를 해치웠습니다.";
        endingText[7] = "\n전리품으로는 " + ScoreManager.Instance.totalItemCount + "개의 아이템을 얻었고,";
        endingText[8] = "\n총 " + ScoreManager.Instance.totalGoldCount + "골드를 얻었습니다.";

        AllInGamePanel.SetActive(false);
}

    // 엔딩 출력
    private void ShowEnding()
    {
        skip = true;
        EndingPanel.SetActive(true);
        StartCoroutine(ShowEndingText(endTextIndex));
    }

    // 엔딩 텍스트 출력
    private IEnumerator ShowEndingText(int index)
    {
        if (index >= endingText.Length)
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            if (index == 0)
            {
                yield return new WaitForSeconds(1.0f);
            }

            string tempText = "";

            for (int i = 0; i < endingText[index].Length; i++)
            {
                yield return new WaitForSeconds(0.05f);

                if (index < 5 || index > 8)
                {
                    showEndingText.horizontalOverflow = HorizontalWrapMode.Wrap;
                    showEndingText.verticalOverflow = VerticalWrapMode.Truncate;
                    showEndingText.text = endingText[index].Substring(0, i + 1);
                }
                else
                {
                    if (i == 0)
                    {
                        tempText = showEndingText.text;
                    }
                    showEndingText.horizontalOverflow = HorizontalWrapMode.Overflow;
                    showEndingText.verticalOverflow = VerticalWrapMode.Overflow;
                    showEndingText.text = tempText + endingText[index].Substring(0, i + 1);
                }
            }

            yield return new WaitForSeconds(0.5f);
            next = true;
            nextText.SetActive(true);
        }
    }
}