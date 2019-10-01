﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
   

    void Update()
    {
        StartCoroutine(PlayVideo());
        //PlayTutorialVideo();
    }
  
    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitforSeconds = new WaitForSeconds(0.2f);
        while (!videoPlayer.isPrepared)
        {
            yield return waitforSeconds;
            break;
        }

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    //void PlayTutorialVideo()
    //{
    //    videoPlayer.Prepare();
    //    rawImage.texture = videoPlayer.texture;
    //    videoPlayer.Play();
    //}

}