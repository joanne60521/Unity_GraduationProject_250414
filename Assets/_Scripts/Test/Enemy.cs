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
 
    public GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;


    public Target target;
    [SerializeField] public ParticleSystem explode;
    [HideInInspector]public float distance;
    [HideInInspector]public Vector3 lookAtTargetPos;



 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("PlayerRobot");
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
                }
            }
            timePassed += Time.deltaTime;
    
            if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange && distance >= attackRange)
            {
                newDestinationCD = 0.5f;
                agent.SetDestination(player.transform.position);
            }
            newDestinationCD -= Time.deltaTime;
            lookAtTargetPos = player.transform.position;
            lookAtTargetPos.y = transform.position.y;
            transform.LookAt(lookAtTargetPos);
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
