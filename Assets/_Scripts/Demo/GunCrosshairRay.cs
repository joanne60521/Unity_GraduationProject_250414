using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCrosshairRay : MonoBehaviour
{
    public LayerMask ui;
    public Transform hitWhere;
    public RaycastHit crosshair;
    public RaycastHit gunfire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(hitWhere);
        if (Physics.Raycast(transform.position, transform.forward, out crosshair, ui))
        {
            Debug.DrawRay(transform.position, transform.forward * crosshair.distance, Color.yellow);
        }
    }
}
