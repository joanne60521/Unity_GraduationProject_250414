using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void FootstepAudio()
    {
        StartCoroutine(PlayForOneSecond(audioSource));
    }

    IEnumerator PlayForOneSecond(AudioSource source)
    {
        source.Play();
        yield return new WaitForSeconds(1.0f);
        source.Stop();
    }
}
