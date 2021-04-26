
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AniCallBack();
public class AnimationManager : MonoBehaviour {
    public static AnimationManager Instance;

    //public Animator _Animator;
    public void Awake()
    {
        Instance = this;
    }
    //Public
    public void AniPlay(Animator animator, AniCallBack aniCallBack)
    {
        StartCoroutine(DelayAni(animator, aniCallBack));
    }
    IEnumerator DelayAni(Animator animator, AniCallBack aniCallBack)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        aniCallBack();
    }


    public void AniPlay(Animator animator, AniCallBack aniCallBack,float DelayTime)
    {
        StartCoroutine(DelayAni(animator, aniCallBack, DelayTime));
    }
    IEnumerator DelayAni(Animator animator, AniCallBack aniCallBack,float DelayTime)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length+ DelayTime);
        aniCallBack();
    }


    //void Start()
    //{
    //    AniPlay(_Animator, (()=> { Debug.Log("执行完动画后。。。。。。"); }));
    //}
}
