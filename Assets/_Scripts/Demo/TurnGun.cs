using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TurnGun : MonoBehaviour
{
    [SerializeField] InputActionReference rightThumbstickReference;
    [SerializeField] float turnValue = 20;
    private float thumbstickX;
    private float thumbstickY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(leftControllerReference.action.ReadValue<Vector2>());
        thumbstickX = rightThumbstickReference.action.ReadValue<Vector2>().x;
        thumbstickY = rightThumbstickReference.action.ReadValue<Vector2>().y;
        if (Mathf.Abs(thumbstickX) > 0.2 | Mathf.Abs(thumbstickY) > 0.2)
        {
            transform.eulerAngles += new Vector3(turnValue * Time.deltaTime * thumbstickY, turnValue * Time.deltaTime * thumbstickX, 0);
        }
    }
}
