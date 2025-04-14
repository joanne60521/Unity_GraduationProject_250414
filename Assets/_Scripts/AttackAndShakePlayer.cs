using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndShakePlayer : MonoBehaviour
{
    public CameraShakeWhenFire cameraShakeWhenFire;
    private Enemy enemy;
    public AudioSourceControl audioSourceControl;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimEventShake()
    {
        if (enemy.distance <= enemy.attackRange)
        {
            cameraShakeWhenFire.TriggerShake();
            audioSourceControl.audioSources[2].Play();
            audioSourceControl.audioSources[3].Play();
        }
    }
}
