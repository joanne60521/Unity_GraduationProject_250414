using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnRobotByThumbstick : MonoBehaviour
{
    [SerializeField] InputActionReference rightThumbstickReference;
    [SerializeField] float turnValue = 20;

    [HideInInspector]public float thumbstickX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thumbstickX = rightThumbstickReference.action.ReadValue<Vector2>().x;

        if (Mathf.Abs(thumbstickX) > 0.2)
        {
            transform.eulerAngles += new Vector3(0, turnValue * Time.deltaTime * thumbstickX, 0);
        }
    }
}
