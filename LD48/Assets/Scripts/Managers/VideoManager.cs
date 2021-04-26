using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class VideoManager : MonoBehaviour
{
    private bool isVideoPlay = false;//视频进度控制器
    //public
    public GameObject Video;//视频界面
    public GameObject UI;//右方UI   SelectInteraction
    public GameObject PlayBtn;//Play按钮
    public CanvasGroup PlayCanvasGroup;//Play按钮下的Image
    public GameObject PauseBtn;//Psuse按钮
    public CanvasGroup PauseCanvasGroup;//Psuse按钮下的Image
    public Slider _slider;//视频进度条
    public VideoPlayer _videoPlayer;//视频
    void Update()
    {
        VideoSliderController();//视频进度条
    }
    //视频按钮
    bool isVideoBtn = false;
    public virtual void VideoBtn()
    {
        isVideoBtn = !isVideoBtn;
        if (isVideoBtn)
        {
            isVideoPlay = true;//关闭进度条控制
            Video.SetActive(true);
            UI.SetActive(false);
        }
        else
        {
            isVideoPlay = false;//关闭进度条控制
            Video.SetActive(false);
            UI.SetActive(true);
        }
    }    
    //视频进度条
    private void VideoSliderController()
    {
        if (isVideoPlay == true)//开启时
        {
            _slider.value = (float)(_videoPlayer.time / _videoPlayer.clip.length);
            if (_videoPlayer.isPlaying == false)
            {
                PlayBtn.SetActive(true);
                PauseBtn.SetActive(false);
            }
            else if (_videoPlayer.isPlaying == true)
            {
                PlayBtn.SetActive(false);
                PauseBtn.SetActive(true);
            }
        }
    }
    //Play 按钮
    public void VideoPlayBtn()
    {
        _videoPlayer.Play();
    }
    //Play 按钮渐变
    public void VideoPlayCanvasGroup()
    {
        PauseCanvasGroup.alpha = 1;
        DOTween.To(() => PauseCanvasGroup.alpha, x => PauseCanvasGroup.alpha = x, 0, 1f);
        if (PauseCanvasGroup.alpha < 0.1f)
        {
            PauseCanvasGroup.alpha = 1;
        }
    }
    //Video_Pause  按钮
    public void VideoPauseBtn()
    {
        if (_videoPlayer.isPlaying == true)
        {
            _videoPlayer.Pause();
        }
    }
    //视频初始化
    public void InitVideoManager()
    {
        isVideoBtn = false;//初始化按钮
        isVideoPlay = false;//初始化视频进度条
        PauseCanvasGroup.alpha = 1;
        PlayBtn.SetActive(false);
        PauseBtn.SetActive(true);
        _videoPlayer.Stop();
        Debug.Log("videoManager");
    }
}
