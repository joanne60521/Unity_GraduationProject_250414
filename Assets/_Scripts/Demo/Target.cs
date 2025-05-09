using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Target : MonoBehaviour
{
    public float health = 50f;
    [SerializeField] private AudioClip audioo;
    public GameObject particlePrefab;
    private Animator animator;
    [HideInInspector]public bool died = false;

    public CountdownTimer countdownTimer;
    public AudioClip audioClipDie;
    public PlayableDirector winTimeline;
    public AudioSource audioSourceTikTok;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void TakeDamage (float amount)
    {
        if (!died)
        {
            health -= amount;
            animator.SetTrigger("damage");
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    public void Die ()
    {
        GameObject particleInstance = Instantiate(particlePrefab, new(transform.position.x, 10, transform.position.z), Quaternion.identity);
        Destroy(particleInstance, 0.83f); // 秒後銷毀

        // AudioSource.PlayClipAtPoint(audioo, new(transform.position.normalized.x, -6, transform.position.normalized.z), 1f);
        // turnOnLight.destroyCount++;
        
        animator.SetTrigger("hp=0");
        AudioSource.PlayClipAtPoint(audioClipDie, transform.position, 1f);
        died = true;
        // Destroy(gameObject, 1);

        // win
        // countdownTimer.enabled = false;
        audioSourceTikTok.Stop();
        // winTimeline.Play();
    }
}
