using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyUIManager : MonoBehaviour
{
    private int index;
    private float alphaAmount;
    private bool effectTrigger;
    private bool skip;
    private bool next;
    private Vector2 pivot;

    [Header("Cursor")]
    public Texture2D noneClickCursor;
    public Texture2D clickCursor;

    [Header("Start Panel")]
    public GameObject startPanel;
    public GameObject nextText;
    public Text showText;
    public string[] startText;

    [Header("Lobby Panel")]
    public GameObject lobbyPanel;
    public Image logo;

    [Header("Option Panel")]
    public GameObject optionPanel;
    public GameObject optionMainPanel;
    public GameObject soundPanel;
    public GameObject musicsource;
    public GameObject effectsource;

    public bool isclick_start;
    public bool isclick_back;

    public static LobbyUIManager instance;

    public static LobbyUIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<LobbyUIManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object LobbyUIManager");
                }
            }

            return instance;
        }
    }

    private void Start()
    {
        index = 0;
        alphaAmount = 255.0f;
        effectTrigger = false;
        isclick_start = false;
        isclick_back = false;
        skip = false;
        next = false;
        pivot = new Vector2(7.0f, 4.0f);
        MusicPlay.Instance.PlaySound("LobbyScene");
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(clickCursor, pivot, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(noneClickCursor, pivot, CursorMode.ForceSoftware);
        }

        if (skip && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LoadingScene");
        }

        if(next && Input.GetKeyDown(KeyCode.Return))
        {
            next = false;
            index++;
            nextText.SetActive(false);
            StartCoroutine(ShowText(index));
        }

        if (effectTrigger)
        {
            alphaAmount += Time.deltaTime * 150.0f;

            if (alphaAmount / 255.0f >= 1.0f)
            {
                effectTrigger = false;
            }
        }
        else
        {
            alphaAmount -= Time.deltaTime * 150.0f;

            if (alphaAmount / 255.0f <= 0.0f)
            {
                effectTrigger = true;
            }
        }

        logo.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, alphaAmount / 255.0f);
    }

    [System.Obsolete]
    public void OnNewGameButton()
    {
        isclick_start = true;
        if (isclick_start)
        {
            AudioPlay.Instance.PlaySound("Option_Newgame");
            isclick_start = false;
        }
        Destroy(effectsource);
        MusicPlay.Instance.PlaySound("ProLog");

        skip = true;
        lobbyPanel.SetActive(false);
        startPanel.SetActive(true);
        StartCoroutine(ShowText(0));
    }

    public void OnOptionButton()
    {
        isclick_start = true;
        lobbyPanel.SetActive(false);
        optionPanel.SetActive(true);
        if (isclick_start)
        {
            AudioPlay.Instance.PlaySound("Option_Newgame");
            isclick_start = false;
        }
    }

    public void OnLobbyExitButton()
    {
        isclick_back = true;
        if (isclick_back)
        {
            AudioPlay.Instance.PlaySound("Exit_Back");
            isclick_back = false;
        }
            Application.Quit();
    }

    public void OnSoundButton()
    {
        isclick_start = true;
        optionMainPanel.SetActive(false);
        soundPanel.SetActive(true);
        if (isclick_start)
        {
            AudioPlay.Instance.PlaySound("Option_Newgame");
            isclick_start = false;
        }
    }

    public void OnOptionExitButton()
    {
        isclick_back = true;
        optionPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        if (isclick_back)
        {
            AudioPlay.Instance.PlaySound("Exit_Back");
            isclick_back = false;
        }
    }

    public void OnSoundExitButton()
    {
        isclick_back = true;
        soundPanel.SetActive(false);
        optionMainPanel.SetActive(true);
        if (isclick_back)
        {
            AudioPlay.Instance.PlaySound("Exit_Back");
            isclick_back = false;
        }
    }

    private IEnumerator ShowText(int index)
    {
        if (index >= startText.Length)
        {
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            if (index == 0)
            {
                yield return new WaitForSeconds(1.0f);
            }

            for (int i = 0; i < startText[index].Length; i++)
            {
                yield return new WaitForSeconds(0.05f);
                showText.text = startText[index].Substring(0, i + 1);
            }

            yield return new WaitForSeconds(0.5f);
            next = true;
            nextText.SetActive(true);
        }
    }
}
