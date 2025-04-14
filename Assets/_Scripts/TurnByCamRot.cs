using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnByCamRot : MonoBehaviour
{
    [SerializeField] GameObject Cameras;
    // Start is called before the first frame update
    void Start()
    {
        Cameras = GameObject.Find("Cameras");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y = Cameras.transform.eulerAngles.y;
        transform.eulerAngles = newRotation;
    }
}
