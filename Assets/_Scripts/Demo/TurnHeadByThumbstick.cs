using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnHeadByThumbstick : MonoBehaviour
{
    [SerializeField] InputActionReference rightThumbstickReference;
    [SerializeField] float turnValue = 20;
    [HideInInspector]public float thumbstickX;
    [HideInInspector]public float thumbstickY;
    // public GameObject RobotOrigin;
    public float yMin = 30;
    public float yMax = 330;
    public bool turnVertical = true;



    void Start()
    {

    }

    void Update()
    {
        thumbstickX = rightThumbstickReference.action.ReadValue<Vector2>().x;
        thumbstickY = rightThumbstickReference.action.ReadValue<Vector2>().y;


        if (transform.eulerAngles.x > yMin && transform.eulerAngles.x < 90)
        {
            turnVertical = false;
            transform.eulerAngles += new Vector3(turnValue * Time.deltaTime * -1, 0, 0);
        }else
        {
            turnVertical = true;
        }
        if (transform.eulerAngles.x < yMax && transform.eulerAngles.x > 270)
        {
            turnVertical = false;
            transform.eulerAngles += new Vector3(turnValue * Time.deltaTime * 1, 0, 0);
        }
        else
        {
            turnVertical = true;
        }


        if (Mathf.Abs(thumbstickX) > 0.2)
        {
            // transform.eulerAngles += new Vector3(0, turnValue * Time.deltaTime * thumbstickX, 0);
        }
        if (Mathf.Abs(thumbstickY) > 0.2 && turnVertical)
        {
            // Debug.Log(transform.rotation.x);
            transform.eulerAngles += new Vector3(-turnValue * Time.deltaTime * thumbstickY, 0, 0);
        }

        // transform.position = RobotOrigin.transform.position;
    }
}
