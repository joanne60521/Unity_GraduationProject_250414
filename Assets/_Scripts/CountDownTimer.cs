using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.AI;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText; // 拖入 TextMeshPro UI
    public float countdownTime = 600f; // 10 分鐘 = 600 秒
    private float timer;
    public BulletScript bulletScript;
    public PlayableDirector loseTimeline;
    public FunctionManage functionManage;
    public Enemy enemy;
    public NavMeshAgent navMeshAgent;
    public AudioSource audioSourceTikTok;

    void Start()
    {
        timer = countdownTime;
        audioSourceTikTok.Play();
        bulletScript.damage = 20;
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            int milliseconds = Mathf.FloorToInt((timer * 100f) % 100f); // 取兩位數毫秒

            timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
        }
        else
        {
            timerText.text = "00:00:00";
            audioSourceTikTok.Stop();
            
            // lose
            loseTimeline.Play();
            functionManage.AllFunctionOff();
            enemy.attackRange = -1;
            navMeshAgent.speed = 0;
        }
    }
}
