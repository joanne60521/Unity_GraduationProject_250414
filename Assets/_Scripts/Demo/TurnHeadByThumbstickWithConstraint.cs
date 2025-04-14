using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnHeadByThumbstickWithConstraint : MonoBehaviour
{
    [SerializeField] InputActionReference leftControllerReference;
    [SerializeField] float turnValue = 20;
    private float thumbstickX;
    private float thumbstickY;
    public Transform mytransform;
    public float currentY;
    public float currentX;
    public float yMin = 30;
    public float yMax = 330;
    public float xMin = -10;
    public float xMax = 10;
    public bool turnHorozontal = true;
    public bool turnVertical = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(leftControllerReference.action.ReadValue<Vector2>());
        thumbstickX = leftControllerReference.action.ReadValue<Vector2>().x;
        thumbstickY = leftControllerReference.action.ReadValue<Vector2>().y;


        if (Mathf.Abs(thumbstickX) > 0.2 && turnHorozontal)
        {
            mytransform.eulerAngles += new Vector3(0, turnValue * Time.deltaTime * thumbstickX, 0);
        }
        if (Mathf.Abs(thumbstickY) > 0.2 && turnVertical)
        {
            // Debug.Log(mytransform.rotation.x);
            mytransform.eulerAngles += new Vector3(-turnValue * Time.deltaTime * thumbstickY, 0, 0);
        }


        if (mytransform.eulerAngles.y > yMin && mytransform.eulerAngles.y < 90)
        {
            turnHorozontal = false;
            mytransform.eulerAngles += new Vector3(0, turnValue * Time.deltaTime * -1, 0);
        }else
        {
            turnHorozontal = true;
        }
        if (mytransform.eulerAngles.y < yMax && mytransform.eulerAngles.y > 270)
        {
            turnHorozontal = false;
            mytransform.eulerAngles += new Vector3(0, turnValue * Time.deltaTime * 1, 0);
        }
        else
        {
            turnHorozontal = true;
        }



        if (mytransform.eulerAngles.x > yMin && mytransform.eulerAngles.x < 90)
        {
            turnVertical = false;
            mytransform.eulerAngles += new Vector3(turnValue * Time.deltaTime * -1, 0, 0);
        }else
        {
            turnVertical = true;
        }
        if (mytransform.eulerAngles.x < yMax && mytransform.eulerAngles.x > 270)
        {
            turnVertical = false;
            mytransform.eulerAngles += new Vector3(turnValue * Time.deltaTime * 1, 0, 0);
        }
        else
        {
            turnVertical = true;
        }

    }
}
