using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchVision : MonoBehaviour
{
    public Transform XrOrigin;
    public bool moveHead = true;

    public InputActionReference moveHeadInputReference;
    public float moveHeadValue;
    public bool btnUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(moveHeadInputReference.action.ReadValue<float>());
        moveHeadValue = moveHeadInputReference.action.ReadValue<float>();
        if (moveHeadValue == 1.0f && btnUp == true) {
            moveHead = !moveHead;
            Debug.Log("moveHead = " + moveHead);
            btnUp = false;
        }
        else if (moveHeadValue != 1.0f && btnUp != true) {
            btnUp = true;
        }
        if (moveHead) {
            transform.localRotation = Quaternion.Euler(XrOrigin.localRotation.eulerAngles);
            //transform.localPosition = XrOrigin.localPosition;
        }
    }
}
