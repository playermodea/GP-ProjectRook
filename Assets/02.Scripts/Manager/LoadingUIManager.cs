using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingUIManager : MonoBehaviour
{
    public RectTransform loadingInBar;

    public static LoadingUIManager instance;

    public static LoadingUIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<LoadingUIManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object LoadingUIManager");
                }
            }

            return instance;
        }
    }

    private void Start()
    {
        loadingInBar.localScale = Vector3.zero;
        StartCoroutine(LoadAsynScene());
    }

    private void Update()
    {
        
    }

    private IEnumerator LoadAsynScene()
    {
        yield return null;

        float time = 0.0f;
        float barPercent = 0.0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync("InGameScene");
    
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            time += Time.deltaTime;
            barPercent = (operation.progress / 0.9f) * time;

            loadingInBar.localScale = new Vector3(barPercent, 1.0f, 1.0f);

            if (barPercent >= 1.0f)
            {
                operation.allowSceneActivation = true;
                yield break;
            }
        }

    }
}
