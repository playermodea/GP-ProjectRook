using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    //public Button NewGame;
    //public Button Continue;
    //public Button Option;
    //public Button Exit;
    public void OnClick_New_Game()
    {
            SceneManager.LoadScene("YJHTESTScene");
            Debug.Log("새 게임을 시작합니다.");
    }
    public void OnClick_Continue()
    {
            Debug.Log("게임을 계속 진행합니다.");
    }
    public void OnClick_Option()
    {
        SceneManager.LoadScene("Option_Scene");
        Debug.Log("옵션 창을 엽니다.");
    }
    public void OnClick_Exit()
    {
            Application.Quit();
            Debug.Log("게임을 종료합니다.");
    }
}
