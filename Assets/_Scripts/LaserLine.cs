using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLine : MonoBehaviour
{
    private LineRenderer laserLine;
    public Transform muzzle;
    public Transform hitWhere;
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        laserLine.SetPosition(0, muzzle.position);
        laserLine.SetPosition(1, hitWhere.position);
    }
}
