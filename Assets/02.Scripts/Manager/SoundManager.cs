using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 다수의 적이 맞았을 때 사운드가 겹치는게 좋을지 안겹치는게 좋을지 봐야함
    private GameObject source;

    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object SoundManager");
                }
            }

            return instance;
        }
    }

    private void Start()
    {
        GameObject source = GameObject.Find("MusicSource");
    }
}
