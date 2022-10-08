using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{

    public void OnClick_Sound()
    {

    }

    public void OnClick_End()
    {
        SceneManager.LoadScene("Lobby_Scene");
    }

    public void OnClick_Exit()
    {

    }

}
