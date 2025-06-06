using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.AI;

public class CountdownTimer : MonoBehaviour
{
    public EmissionPowerOff emissionPowerOff;
    public TMP_Text timerText; // 拖入 TextMeshPro UI
    public float countdownTime = 600f; // 10 分鐘 = 600 秒
    private float timer;
    public BulletScript bulletScript;
    public PlayableDirector loseTimeline;
    public FunctionManage functionManage;
    public Enemy enemy;
    public NavMeshAgent navMeshAgent;
    public Enemy enemy1;
    public NavMeshAgent navMeshAgent1;
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

            if (timer <= 10)
            {
                timerText.color = Color.red;
            }
        }
        else
        {
            timerText.text = "00:00:00";
            audioSourceTikTok.Stop();
            
            // lose
            emissionPowerOff.powerOff = true;
            loseTimeline.Play();
            functionManage.AllFunctionOff();
            if (enemy != null)
            {
                enemy.attackRange = -1;
                navMeshAgent.speed = 0;
            }
            if (enemy1 != null)
            {
                enemy1.attackRange = -1;
                enemy1.shootRange = -1;
                navMeshAgent1.speed = 0;
            }
        }
    }
}
