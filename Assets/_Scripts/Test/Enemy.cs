using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    public float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;
    public int enemyBulletNum = 0;
    public float shootRange = 1f;
    [Range(0.0f, -90.0f)] public float shootRotateY = -75;

 
    private GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;


    private Target target;
    [SerializeField] public ParticleSystem explode;
    public float distance;
    Vector3 lookAtTargetPos;

    private float originSpeed;

 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("PlayerRobot");
        originSpeed = agent.speed;
        target = GetComponent<Target>();
    }
    
    void Update()
    {
        // animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
        if (!target.died)
        {
            animator.SetFloat("speed", agent.velocity.magnitude);
        }else
        {
            animator.SetFloat("speed", 0);
            agent.speed = 0;
        }

        if (player != null && !target.died)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);

            if (enemyBulletNum > 0)  // long range attack
            {
                if (distance < shootRange)
                {
                    animator.SetBool("shoot", true);
                    timePassed = 0;
                    agent.SetDestination(transform.position);
                    Vector3 direction = player.transform.position - transform.position;
                    direction.y = 0f;
                    Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, shootRotateY, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50);
                }
                else
                {
                    animator.SetBool("shoot", false);
                }
            }
            else if (enemyBulletNum == 0)  // short range attack
            {
                animator.SetBool("shoot", false);
                if (timePassed > attackCD)
                {
                    if (distance <= attackRange)
                    {
                        animator.SetTrigger("attack");
                        timePassed = 0;
                        Vector3 direction = player.transform.position - transform.position;
                        direction.y = 0f;
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
                    }
                }
            }

            timePassed += Time.deltaTime;
    
            if (newDestinationCD <= 0 && distance <= aggroRange && distance >= shootRange && enemyBulletNum > 0)
            {
                newDestinationCD = 0.5f;
                agent.SetDestination(player.transform.position);
                lookAtTargetPos = agent.velocity;
                lookAtTargetPos.y = 0f;  // 讓 LookRotation 產生的方向僅作用在水平方向（Y 軸旋轉），不會抬頭或低頭
                Quaternion targetRotation = Quaternion.LookRotation(lookAtTargetPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }
            else if (newDestinationCD <= 0 && distance <= aggroRange && distance >= attackRange && enemyBulletNum == 0)
            {
                newDestinationCD = 0.5f;
                agent.SetDestination(player.transform.position);
                lookAtTargetPos = agent.velocity;
                lookAtTargetPos.y = 0f;
                Quaternion targetRotation = Quaternion.LookRotation(lookAtTargetPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }
            newDestinationCD -= Time.deltaTime;
        }
 
    }

    public void EnemyShoot()
    {
        enemyBulletNum--;
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
