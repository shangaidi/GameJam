/*
 *FileName:      StartUI.cs
 *Author:        MC
 *Date:          2021/04/26 00:27:58
 *UnityVersion:  2020.3.0f1c1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public Button StartBtn;
    public Button ExitBtn;
    public GameObject TeachBtn;
    public GameObject Bg;

    private void Start()
    {
        StartBtn.onClick.AddListener(OpenTeach);
        ExitBtn.onClick.AddListener(ExitGame);
        TeachBtn.GetComponent<Button>().onClick.AddListener(JumpScene);
    }

    private void JumpScene()
    {
        SceneManager.LoadScene("Main");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
    private void OpenTeach()
    {
        Bg.SetActive(false);
        TeachBtn.SetActive(true);
    }
}
