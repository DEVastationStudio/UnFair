using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoTutorialScript : MonoBehaviour
{
    #region Variables

    [SerializeField] private VideoPlayer[] tutorial;
    [SerializeField] private GameObject videoContainer;
    private int numberVideo;

    #endregion

    #region Metodos

    public void StartVideo()
    {
        videoContainer.SetActive(true);
        tutorial[numberVideo].gameObject.SetActive(true);
        tutorial[numberVideo].Stop();
        tutorial[numberVideo].Play();
    }
    private void PauseVideo()
    {
        tutorial[numberVideo].Pause();
        tutorial[numberVideo].gameObject.SetActive(false);
    }

    public void NextVideo()
    {
        if (numberVideo < tutorial.Length -1)
        {
            PauseVideo();
            numberVideo++;
            StartVideo();
        }
        else
        {
            PauseVideo();
            videoContainer.SetActive(false);
            numberVideo = 0;
        }
    }

    #endregion




}
