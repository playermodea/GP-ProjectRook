using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    private AudioSource audioSource;

    private GameObject[] musics;

    [Header("Main_Sound")]
    public AudioClip Lobby_Scene;
    public AudioClip First_Floor;
    public AudioClip Second_Floor;
    public AudioClip ProLog_Scene;
    public AudioClip Ending_Scene;
    public AudioClip GameOver_Scene;

    [Header("Boss")]
    public AudioClip First_Floor_Boss;
    public AudioClip Second_Floor_Boss;



    private static MusicPlay instance;

    public static MusicPlay Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<MusicPlay>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object MusicPlay");
                }
            }

            return instance;
        }
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBackgroundMusic(int stageNumber)
    {
        switch(stageNumber)
        {
            case 1:
                audioSource.clip = First_Floor;
                break;
            case 2:
                audioSource.clip = Second_Floor;
                break;
            default:
                break;
        }

        PlayMusic();
    }

    public void PlayBossMapBackgroundMusic(int stageNumber)
    {
        switch(stageNumber)
        {
            case 1:
                audioSource.clip = First_Floor_Boss;
                break;
            case 2:
                audioSource.clip = Second_Floor_Boss;
                break;
            default:
                break;
        }

        PlayMusic();
    }

    public void PlaySound(string action)
    {
        switch (action)
        {
            case "LobbyScene":
                audioSource.clip = Lobby_Scene;
                break;
            case "ProLog":
                audioSource.clip = ProLog_Scene;
                break;
            case "Ending":
                audioSource.clip = Ending_Scene;
                break;
            case "GameOver":
                audioSource.clip = GameOver_Scene;
                break;
        }

        PlayMusic();
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
