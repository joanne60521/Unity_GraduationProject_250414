using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GunCrosshairMove : MonoBehaviour
{
    public GunCrosshairRay GunCrosshairRay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GunCrosshairRay.crosshair.point;
    }
}
