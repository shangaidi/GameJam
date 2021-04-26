using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;//背景尺寸
    public Slider slider;//loading条
    public Text text;//loading文字
    public string NextSceneName;//下一场景名
    /// <summary>
    /// 异步加载
    /// </summary>
    public void LoadNextLevel()
    {
        StartCoroutine(Loadlevel());
    }

    IEnumerator Loadlevel()
    {
        loadScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(NextSceneName);

        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            slider.value = operation.progress;
            text.text = operation.progress * 100 + "%";
            if (operation.progress >= 0.9f)
            {
                slider.value = 1;

                text.text = "Press AnyKey to continue";

                if (Input.anyKeyDown)
                {
                    //TODO: jump

                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    } 
}
