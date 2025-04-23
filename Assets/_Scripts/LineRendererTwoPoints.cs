using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTwoPoints : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform point0;
    public Vector3 offset0;
    public Transform point1;
    public Vector3 offset1;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, point0.position + offset0);
        lineRenderer.SetPosition(1, point1.position + offset1);
    }
}
