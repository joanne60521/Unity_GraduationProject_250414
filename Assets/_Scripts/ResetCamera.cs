using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetCamera : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;
    private float firstResetTime = 1f;
    private bool firstReset = true;

    public InputActionReference leftResetValueRef;
    public InputActionReference rightResetValueRef;
    private bool reset = false;
    private float resetCD = 1f;
    private float nextResetTime = 0;

    public void ResetMainCamPos()
    {
        var rotationAngleY = resetTransform.rotation.eulerAngles.y - playerHead.transform.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngleY, 0);
        player.transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);

        var distancdDiff = resetTransform.position - playerHead.transform.position;
        player.transform.position += distancdDiff;
    }


    void Update()
    {
        float rightResetRead = rightResetValueRef.action.ReadValue<float>();
        float leftResetRead = leftResetValueRef.action.ReadValue<float>();

        if (leftResetRead > 0.5 && rightResetRead > 0.5)
            reset = true;
        else
            reset = false;


        if (Time.time >= firstResetTime && firstReset)
        {
            ResetMainCamPos();
            firstReset = false;
        }
        if (Input.GetKeyDown("r"))
        {
            ResetMainCamPos();
        }
        if (Time.time > nextResetTime && reset)
        {
            nextResetTime = Time.time + resetCD;
            ResetMainCamPos();
            Debug.Log("reset");
        }
    }
}
