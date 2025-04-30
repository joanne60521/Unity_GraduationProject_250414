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
    
 
    // Update is called once per frame
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
            if (timePassed >= attackCD)
            {
                if (distance <= attackRange)
                {
                    animator.SetTrigger("attack");
                    timePassed = 0;
                    Vector3 direction = player.transform.position - transform.position;
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
                }
            }

            timePassed += Time.deltaTime;
    
            if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange && distance >= attackRange)
            {
                newDestinationCD = 0.5f;
                agent.SetDestination(player.transform.position);
                lookAtTargetPos = agent.velocity;
                Quaternion targetRotation = Quaternion.LookRotation(lookAtTargetPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }
            newDestinationCD -= Time.deltaTime;
        }
 
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
