using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRobotByGyroscope : MonoBehaviour
{
    public Gyroscope gy;
    public float forwardThreshold = 20f;
    public float backwardThreshold = -20f;

    [SerializeField] float turnValue = 20f;
    [SerializeField] float speed = 10f;

    private CharacterController characterController;
    [SerializeField] bool groundedPlayer;
    
    public enum GyState {Front, Back, Left, Right, Stop}
    public GyState gyState = GyState.Stop;
    [SerializeField] float[] gyroscpoeFlt = new float[2];

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canTurn = true;
    public bool isMoving = false;
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (gy.prFlt1[0] > forwardThreshold && gy.prFlt2[0] > forwardThreshold && canMove)
        {
            // move forward
            gyState = GyState.Front;
            characterController.Move(transform.forward * speed * Time.deltaTime);
            isMoving = true;
        }
        else if (gy.prFlt1[0] < backwardThreshold && gy.prFlt2[0] < backwardThreshold && canMove)
        {
            // move backward
            gyState = GyState.Back;
            characterController.Move(transform.forward * -1 * speed * Time.deltaTime);
            isMoving = true;
        }
        else if (gy.prFlt1[0] > forwardThreshold && gy.prFlt2[0] < backwardThreshold && canTurn)  // 左前右後
        {
            // turn left
            gyState = GyState.Left;
            transform.eulerAngles += new Vector3(0, -turnValue * Time.deltaTime, 0);
            isMoving = true;
        }
        else if (gy.prFlt1[0] < backwardThreshold && gy.prFlt2[0] > forwardThreshold && canTurn)  // 左後右前
        {
            // turn right
            gyState = GyState.Right;
            transform.eulerAngles += new Vector3(0, turnValue * Time.deltaTime, 0);
            isMoving = true;
        }
        else
        {
            // stop
            gyState = GyState.Stop;
            isMoving = false;
        }

        gyroscpoeFlt[0] = gy.prFlt1[0];
        gyroscpoeFlt[1] = gy.prFlt2[0];
        // Debug.Log("gyroscpoeFlt = " + gyroscpoeFlt[0] + "  ,  " + gyroscpoeFlt[1]);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // move forward
            gyState = GyState.Front;
            characterController.Move(transform.forward * speed * Time.deltaTime);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // move backward
            gyState = GyState.Back;
            characterController.Move(transform.forward * -1 * speed * Time.deltaTime);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))  // 左前右後
        {
            // turn left
            gyState = GyState.Left;
            transform.eulerAngles += new Vector3(0, -turnValue * Time.deltaTime, 0);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))  // 左後右前
        {
            // turn right
            gyState = GyState.Right;
            transform.eulerAngles += new Vector3(0, turnValue * Time.deltaTime, 0);
            isMoving = true;
        }
        else
        {
            // stop
            gyState = GyState.Stop;
            isMoving = false;
        }


        RaycastHit hit;
        Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 5f);
        groundedPlayer = Mathf.Abs(transform.position.y - hit.point.y) <= 0.1;
        if (!groundedPlayer)
        {
            characterController.Move(-transform.up * 9.8f * Time.deltaTime);
        }
    }
}
