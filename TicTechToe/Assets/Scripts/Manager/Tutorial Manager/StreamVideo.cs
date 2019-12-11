using System.Collections;
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
    }
  
    public IEnumerator PlayVideo()
    {
        videoPlayer.EnableAudioTrack(0, false);

        videoPlayer.Prepare();
        WaitForSeconds waitforSeconds = new WaitForSeconds(0.2f);

        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return waitforSeconds;
            break;
        }

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();

        Debug.Log("Playing Video");

        while (videoPlayer.isPlaying)
        {
            Debug.Log("Is Playing");
            yield return null;
        }
        Debug.Log("Done Playing!");
    }
}
