/*
 *FileName:      ScoreList.cs
 *Author:        MC
 *Date:          2021/04/26 01:06:39
 *UnityVersion:  2020.3.0f1c1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreList : MonoBehaviour
{
    public Button AgainBtn;
    public Button ExitBtn;

    public Text HighestScoreText;
    public Text CurrentScoreText;

    public float HighestScore = 0;
    public float CurrentScore = 0;
    private void OnEnable()
    {
        AgainBtn.onClick.AddListener(AgainBtnClick);
        ExitBtn.onClick.AddListener(ExitBtnClick);
    }

    void Update()
    {
        if (TimeManager.Instance.timer >= PlayerPrefs.GetFloat("HighestScore"))
        {
            HighestScore = TimeManager.Instance.timer;
            CurrentScore = TimeManager.Instance.timer;
            PlayerPrefs.SetFloat("HighestScore", TimeManager.Instance.timer);
        }
        else
        {
            HighestScore = PlayerPrefs.GetFloat("HighestScore");
            CurrentScore = TimeManager.Instance.timer;
        }
        HighestScoreText.text = HighestScore.ToString("F2");
        CurrentScoreText.text = CurrentScore.ToString("F2");
    }

    void AgainBtnClick()
    {
        SceneManager.LoadScene("Main");
    }
    void ExitBtnClick()
    {
        Application.Quit();
    }
}
