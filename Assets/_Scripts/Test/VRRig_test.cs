using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


[System.Serializable]  //讓 class VRMap 顯示在 Inspector
public class VRMap_Hand
{
    public temp temp;
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public Transform playerOriginMainCam;
    public float scaleUp = 12.5f;
    public float delay = 2.5f;
    private Vector3 moveTargetPos;



    // attack mode
    public InputActionReference velocityReference;
    public GameObject cubeEnemy;
    public EnemyCollider EnemyCollider;


    public float velocityValueZThreshold = 0.5f;
    public float distanceThreshold = 20f;
    public float attackDelay = 1f;


    public float velocityValueZ = 0;
    [HideInInspector]public bool attacking = false;
    public bool attackMode = false;

    private float distance;
    public CameraVisibilityWithViewport cameraVisibilityWithViewport;

    [SerializeField]private float velContinueTime = 0;
    public float velContinueTimeThreshold = 0.2f;
    private float distancePunchEnemy;
    [SerializeField]private float distancePunchEnemyThreshold = 0.8f;
    public bool reachEnemy;
    public AudioSourceControl audioSourceControl;
    private float nextunchTime = 0;
    public float punchRate = 0.5f;
    [HideInInspector]public bool brake = false;
    [HideInInspector]public bool braked = false;
    public float velocityMag;



    public void Map()
    {
        velocityMag = Vector3.Magnitude(velocityReference.action.ReadValue<Vector3>());
        velocityValueZ = velocityReference.action.ReadValue<Vector3>().z;

        if (attackMode && cubeEnemy != null)
        {
            distance = (cubeEnemy.transform.position - playerOriginMainCam.transform.position).magnitude;

            if (velocityValueZ > velocityValueZThreshold)
            {
                velContinueTime = velContinueTime + Time.deltaTime;
            }else
            {
                velContinueTime = 0;
            }

            if (velContinueTime >= velContinueTimeThreshold && distance < distanceThreshold && cameraVisibilityWithViewport.isInView && Time.time > nextunchTime)
            {
                attacking = true;
            }

            if (attacking)
            {
                // attack
                if (!braked)
                {
                    brake = true;
                    braked = true;
                }else
                {
                    brake = false;
                }
                nextunchTime = Time.time + punchRate;
                rigTarget.position = Vector3.Lerp(rigTarget.position, cubeEnemy.transform.position, attackDelay * Time.deltaTime);
                rigTarget.localRotation = Quaternion.Lerp(rigTarget.localRotation, vrTarget.localRotation * Quaternion.Euler(trackingRotationOffset), delay * Time.deltaTime);
                distancePunchEnemy = (rigTarget.position - cubeEnemy.transform.position).magnitude;
                reachEnemy = false;
                if (distancePunchEnemy <= distancePunchEnemyThreshold)  // if (EnemyCollider.reachedEnemy)
                {
                    Debug.Log("reached enemy");
                    reachEnemy = true;
                    // attackMode = false;
                    attacking = false;
                    // EnemyCollider.reachedEnemy = false;
                    moveTargetPos = rigTarget.parent.InverseTransformDirection((vrTarget.position - playerOriginMainCam.position) * scaleUp);
                    if (velocityMag > 0.01f)
                    {
                        rigTarget.localPosition = Vector3.Lerp(rigTarget.localPosition, moveTargetPos, delay * Time.deltaTime);
                    }
                    else
                    {
                        rigTarget.localPosition = rigTarget.localPosition;
                    }
                    rigTarget.localRotation = Quaternion.Lerp(rigTarget.localRotation, vrTarget.localRotation * Quaternion.Euler(trackingRotationOffset), delay * Time.deltaTime);
                    EnemyCollider.PlayEffects();
                    audioSourceControl.PlayPunch();
                }
            }else
            {
                // normal move when attack mode 
                moveTargetPos = rigTarget.parent.InverseTransformDirection((vrTarget.position - playerOriginMainCam.position) * scaleUp);
                if (velocityMag > 0.01f)    
                {
                    rigTarget.localPosition = Vector3.Lerp(rigTarget.localPosition, moveTargetPos, delay * Time.deltaTime);
                }
                else
                {
                    rigTarget.localPosition = rigTarget.localPosition;
                }
                rigTarget.localRotation = Quaternion.Lerp(rigTarget.localRotation, vrTarget.localRotation * Quaternion.Euler(trackingRotationOffset), delay * Time.deltaTime);
                reachEnemy = false;
                braked = false;
            }
        }else
        {
            moveTargetPos = rigTarget.parent.InverseTransformDirection((vrTarget.position - playerOriginMainCam.position) * scaleUp);
            if (velocityMag > 0.01f)    
            {
                rigTarget.localPosition = Vector3.Lerp(rigTarget.localPosition, moveTargetPos, delay * Time.deltaTime);
            }
            else
            {
                rigTarget.localPosition = rigTarget.localPosition;
            }
            rigTarget.localRotation = Quaternion.Lerp(rigTarget.localRotation, vrTarget.localRotation * Quaternion.Euler(trackingRotationOffset), delay * Time.deltaTime);
            // moveTargetPos = (vrTarget.position - playerOriginMainCam.position) * scaleUp + playerOriginMainCam.position;
            // rb.Move(moveTargetPos, vrTarget.rotation);
            reachEnemy = false;
        }
    }
}

[System.Serializable]
public class VRMap_Head
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;

    public void MapHead()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
    }
}

public class VRRig_test : MonoBehaviour
{
    public float turnSmoothness = 5;

    public VRMap_Head head;
    public VRMap_Hand leftHand;
    public VRMap_Hand rightHand;


    public Transform headConstraint;
    private Vector3 headBodyOffset;
    void Start()
    {
        // headBodyOffset = transform.position - headConstraint.position;
    }

    void FixedUpdate()
    {
        // transform.position = headConstraint.position + headBodyOffset;
        // transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;
        // transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        // head.MapHead();
        leftHand.Map();
        rightHand.Map();

    }
}