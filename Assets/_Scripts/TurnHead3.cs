using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnHead3 : MonoBehaviour
{
    [SerializeField] InputActionReference leftControllerReference;
    [SerializeField] float turnValue = 20;
    private float thumbstickX;
    private float thumbstickY;
    public float yMin = 30;
    public float yMax = 330;
    public bool turnVertical = true;



    void Start()
    {

    }

    void Update()
    {
        //Debug.Log(leftControllerReference.action.ReadValue<Vector2>());
        thumbstickX = leftControllerReference.action.ReadValue<Vector2>().x;
        thumbstickY = leftControllerReference.action.ReadValue<Vector2>().y;


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
            transform.eulerAngles += new Vector3(0, turnValue * Time.deltaTime * thumbstickX, 0);
        }
        if (Mathf.Abs(thumbstickY) > 0.2 && turnVertical)
        {
            // Debug.Log(transform.rotation.x);
            transform.eulerAngles += new Vector3(-turnValue * Time.deltaTime * thumbstickY, 0, 0);
        }
    }
}
