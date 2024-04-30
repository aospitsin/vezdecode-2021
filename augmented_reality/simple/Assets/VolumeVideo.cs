using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VolumeVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if(spriteRenderer.enabled)
            if(videoPlayer.canSetDirectAudioVolume)
                videoPlayer.SetDirectAudioVolume(0, 0.5f);
        if (spriteRenderer.enabled == false)
            if(videoPlayer.canSetDirectAudioVolume)
                videoPlayer.SetDirectAudioVolume(0, 0f);
    }
}
