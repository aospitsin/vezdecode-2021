using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Ссылка на AudioMixer")][SerializeField]
    private AudioMixerGroup Mixer;

    private bool stateAllMusic;
    private Coroutine cor;

    private void Start()
    {
        stateAllMusic = true;
    }

    public void OnMusicToggle()
    {
        stateAllMusic = !stateAllMusic;

        SmoothlyChangeVolume("Master", stateAllMusic, 2f, 0.5f);
    }

    private void SmoothlyChangeVolume(string nameSound, bool enabled, float speedUp, float speedDown)
    { 
        if(cor != null)
            StopCoroutine(cor);
        
        if (enabled)
            cor = StartCoroutine(volumeUpMusic(nameSound, 0f, speedUp));
        else
            cor = StartCoroutine(volumeDownMusic(nameSound, -80f, speedDown));
    }

    IEnumerator volumeUpMusic(string nameAudio, float volume, float speed)
    {
        float volumeMixer;
        Mixer.audioMixer.GetFloat(nameAudio, out volumeMixer);
        while(volumeMixer < volume)
        {
            if (volumeMixer > volume - 10f)
            {
                Mixer.audioMixer.SetFloat(nameAudio, -10f);
                break;
            }

            volumeMixer = Mathf.Lerp(volumeMixer, volume, Time.deltaTime * speed);
            Mixer.audioMixer.SetFloat(nameAudio, volumeMixer);

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator volumeDownMusic(string nameAudio, float volume, float speed)
    {
        float volumeMixer;
        Mixer.audioMixer.GetFloat(nameAudio, out volumeMixer);
        while (volumeMixer > volume)
        {
            if (volumeMixer < volume + 30f)
            {
                Mixer.audioMixer.SetFloat(nameAudio, -80f);
                break;
            }

            volumeMixer = Mathf.Lerp(volumeMixer, volume, Time.deltaTime * speed);
            Mixer.audioMixer.SetFloat(nameAudio, volumeMixer);

            yield return new WaitForFixedUpdate();
        }
    }
}
