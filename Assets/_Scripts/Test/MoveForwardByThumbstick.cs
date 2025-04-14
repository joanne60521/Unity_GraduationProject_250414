using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveForwardByThumbstick : MonoBehaviour
{
    [SerializeField] InputActionReference leftThumbstickReference;
    [SerializeField] float speed = 10;
    [HideInInspector]public float thumbstickX;
    [HideInInspector]public float thumbstickY;
    private CharacterController characterController;

    [SerializeField] bool groundedPlayer;

    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        thumbstickY = leftThumbstickReference.action.ReadValue<Vector2>().y;
        thumbstickX = leftThumbstickReference.action.ReadValue<Vector2>().x;


        if (thumbstickY > 0.5 || Input.GetKey(KeyCode.UpArrow))
        {
            characterController.Move(transform.forward * speed * Time.deltaTime);
        }
        if (thumbstickY < -0.5 || Input.GetKey(KeyCode.DownArrow))
        {
            characterController.Move(transform.forward * -1 * speed * Time.deltaTime);
        }
        if (thumbstickX > 0.5 || Input.GetKey(KeyCode.RightArrow))
        {
            characterController.Move(transform.right * speed * Time.deltaTime);
        }
        if (thumbstickX < -0.5 || Input.GetKey(KeyCode.LeftArrow))
        {
            characterController.Move(-transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey("t"))
        {
            characterController.Move(transform.forward * speed * Time.deltaTime);
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
