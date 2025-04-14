using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceControl : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioClip audioClipFightBGM;
    public StartGameControl startGameControl;
    private bool b = true;

    void Start()
    {
        // audioSources = GetComponents<AudioSource>();
        // audioSources[0] = GetComponent<AudioSource>();

        // audioSources[0].time = 0.5f;

        // InvokeRepeating("PlayMachineNoise", 0f, 2f);
    }
    
    void Update()
    {
        if(b)  // if(startGameControl.startGame && b)
        {
            b = false;
            audioSources = GetComponents<AudioSource>();
            // audioSources[0] = GetComponent<AudioSource>();
            audioSources[1].enabled = true;

            audioSources[0].time = 0.5f;

            InvokeRepeating("PlayMachineNoise", 0f, 2f);
        }
    }
    
    void PlayMachineNoise()
    {
        audioSources[0].Play();
    }
    
    public void PlayPunch()
    {
        audioSources[2].Play();
    }
    public void PlayShoot()
    {
        audioSources[3].Play();
    }
    public void ChangeToFightBGM()
    {
        audioSources[1].clip = audioClipFightBGM;
        audioSources[1].Play();
    }
    public void StopBGM()
        {
            audioSources[1].Stop();
        }

}
