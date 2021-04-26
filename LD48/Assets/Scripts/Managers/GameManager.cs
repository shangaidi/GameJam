/*
 *FileName:      GameManager.cs
 *Author:        MC
 *Date:          2021/04/24 15:11:58
 *UnityVersion:  2020.3.0f1c1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("ËÙÂÊ")]
    public float GameSpeed;
    public int FishSpeed;
    //Spawn
    public float colddown = 2f;
    //o2
    public float O2colddown = 2f;
    //blood
    public GameObject blood_common;
    public GameObject blood_low;
    //End
    public GameObject ScoreList;
    private void Update()
    {
        if (TimeManager.Instance.timer <= 10)
        {
            GameSpeed *= 1;
        }
        else if (TimeManager.Instance.timer > 10 && TimeManager.Instance.timer < 20)
        {
            GameSpeed *= 1.5f;
        }
        else if (TimeManager.Instance.timer >= 20)
        {
            GameSpeed *= 2f;
        }

        if (CharacterControl.Instance.isdie==true)
        {
            ScoreList.SetActive(true);
        }
    }



    public void ShowBlood()
    {
        blood_common.SetActive(true);
        StartCoroutine(waitBlood());
    }
    public void HideBlood()
    {
        blood_common.SetActive(false);
    }
    IEnumerator waitBlood()
    {
        yield return new WaitForSeconds(0.3f);
        HideBlood();
    }

    public void ShowBloodAni()
    {
        blood_low.SetActive(true);
    }
    public void HideBloodAni()
    {
        blood_low.SetActive(false);
    }
}
