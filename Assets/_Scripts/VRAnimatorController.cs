using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    public float speedThreshold = 0.1f;
    public Transform RobotOrigin;
    public float robotSpeed;
    private Animator animator;
    private Vector3 previousPos;

    public ControlRobotByGyroscope controlRobotByGyroscope;

    void Start()
    {
        animator = GetComponent<Animator>();
        previousPos = RobotOrigin.position;
    }

    void Update()
    {
        Vector3 RobotOriginSpeed = (RobotOrigin.position - previousPos) / Time.deltaTime;
        RobotOriginSpeed.y = 0;
        previousPos = RobotOrigin.position;
        // animator.SetBool("isMoving", controlRobotByGyroscope.isMoving);
        animator.SetBool("isMoving", RobotOriginSpeed.magnitude >= speedThreshold);
        robotSpeed = RobotOriginSpeed.magnitude;
    }
}
