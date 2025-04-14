using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class temp2 : MonoBehaviour
{
    public Rigidbody rb;
    public Transform controllerSim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 targetPos = transform.position - transform.forward * 5 * Time.deltaTime;
        // rb.Move(targetPos, transform.rotation);

        Vector3 targetPos = controllerSim.position;
        targetPos.y = transform.position.y;
        rb.Move(targetPos, transform.rotation);
    }
}
