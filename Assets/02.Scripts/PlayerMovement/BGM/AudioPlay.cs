using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Effect_Sound")]
    public AudioClip Attack1;//몬스터 피격 효과음
    public AudioClip Attack2;//몬스터 피격 효과음
    public AudioClip Attack3;//몬스터 피격 효과음
    public AudioClip Get_Heart;//하트 획득 효과음
    public AudioClip Get_Coin;//동전 획득 효과음
    public AudioClip Under_Attack1;//플레이어 피격 효과음
    public AudioClip Under_Attack2;//플레이어 피격 효과음
    public AudioClip Under_Attack3;//플레이어 피격 효과음
    public AudioClip Move;//플레이어 이동 효과음
    public AudioClip Boss_Dead;//보스 사망 효과음
    public AudioClip Stage_Clear;//보스 사망 후 사다리와 동시에 나타나는 효과음
    public AudioClip Player_Dead;//플레이어 사망 효과음
    public AudioClip Get_Item;//패시브 아이템 획득 효과음
    public AudioClip Next_Floor;//다음층 이동 효과음
    public AudioClip Boss_Attack1;//보스 피격 효과음
    public AudioClip Boss_Attack2;//보스 피격 효과음
    public AudioClip Boss_Attack3;//보스 피격 효과음
    public AudioClip Lobby_Option_Newgame;//설정, 뉴게임선택시 효과음
    public AudioClip Lobby_Exit_Back;//나가기, 설정 뒤로가기 효과음
    public AudioClip Buy_Item;//상점 구매 효과음
    public AudioClip Explosion_Sound;//자폭병 자폭 효과음

    [Header("Skill_Effect_Sound")]
    public AudioClip Fire_Effect_Sound;//화염 효과음
    public AudioClip Boom_Effect_Sound;//폭탄투척, 확산폭발 효과음
    public AudioClip Slide_Effect_Sound;//앞찍기, 가로베기, 휘두르기, 낙뢰소환 효과음
    public AudioClip SoundWave_Effect_Sound;//음파 효과음
    public AudioClip ThrowAFloat_Effect_Sound;//찌 투척 효과음
    public AudioClip Stabbing_Effect_Sound;//발구르기, 내려찍기, 세로베기, 찌르기, 회전베기 효과음
    public AudioClip Hit_Effect_Sound;//적중 효과음


    private static AudioPlay instance;

    public static AudioPlay Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<AudioPlay>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object AudioPlay");
                }
            }

            return instance;
        }
    }
    private void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
        this.audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        int random;
        switch (action)
        {
            case "Attack":
                random = Random.Range(1, 3);
                if (random == 1)
                {
                    audioSource.clip = Attack1;
                }
                else if (random == 2)
                {
                    audioSource.clip = Attack2;
                }
                else if (random == 3)
                {
                    audioSource.clip = Attack3;
                }
                break;
            case "Get_Heart":
                audioSource.clip = Get_Heart;
                break;
            case "Get_Coin":
                audioSource.clip = Get_Coin;
                break;
            case "Under_Attack":
                random = Random.Range(1, 4);
                if(random == 1)
                {
                    audioSource.clip = Under_Attack1;
                }
                else if(random == 2)
                {
                    audioSource.clip = Under_Attack2;
                }
                else if(random == 3)
                {
                    audioSource.clip = Under_Attack3;
                }
                break;
            case "Move":
                audioSource.clip = Move;
                break;
            case "Boss_Dead":
                audioSource.clip = Boss_Dead;
                break;
            case "Stage_Clear":
                audioSource.clip = Stage_Clear;
                break;
            case "Player_Dead":
                audioSource.clip = Player_Dead;
                break;
            case "Get_Item":
                audioSource.clip = Get_Item;
                break;
            case "Next_Floor":
                audioSource.clip = Next_Floor;
                break;
            case "Attack_Boss":
                random = Random.Range(1, 4);
                if (random == 1)
                {
                    audioSource.clip = Boss_Attack1;
                }
                else if (random == 2)
                {
                    audioSource.clip = Boss_Attack2;
                }
                else if (random == 3)
                {
                    audioSource.clip = Boss_Attack3;
                }
                break;
            case "Option_Newgame":
                audioSource.clip = Lobby_Option_Newgame;
                break;
            case "Exit_Back":
                audioSource.clip = Lobby_Exit_Back;
                break;
            case "Buy_Item":
                audioSource.clip = Buy_Item;
                break;

            case "boomman": //폭탄병
                audioSource.clip = Explosion_Sound;
                break;
            case "FireEffect":// 화염
                audioSource.clip = Fire_Effect_Sound;
                break;
            case "BoomEffect"://폭탄 확산폭발
                audioSource.clip = Boom_Effect_Sound;
                break;
            case "SlideEffect":// 앞찍,가로베기,휘두르기,낙뢰
                audioSource.clip = Slide_Effect_Sound;
                break;
            case "SoundWave"://음파
                audioSource.clip = SoundWave_Effect_Sound;
                break;
            case "ThrowFloatEffcet"://찌 투척
                audioSource.clip = ThrowAFloat_Effect_Sound;
                break;
            case "StabbingEffect"://발구르기, 내려찍기, 세로베기, 찌르기, 회전베기
                audioSource.clip = Stabbing_Effect_Sound;
                break;
            case "Hit"://적중
                audioSource.clip = Hit_Effect_Sound;
                break;
        }
        audioSource.Play();
}
    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
