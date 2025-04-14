using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    private AudioSource audioSource;
    public float[] samples;
    public LineRenderer lineRenderer;

    private readonly int LINERENDER_POINT_CNT = 32;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        samples = new float[1024];
        lineRenderer.positionCount = LINERENDER_POINT_CNT;
        lineRenderer.startWidth = 0.002f;
        lineRenderer.endWidth = 0.002f;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
        for (int i = 0, cnt = LINERENDER_POINT_CNT; i < cnt; ++i)
        {
            var v = samples[i];
            Vector3 localPos = new Vector3(
            (i - LINERENDER_POINT_CNT / 2) * 0.01f, 
            v * 0.25f, 
            v * 0.25f / 2.5f
            );
            Vector3 worldPos = transform.position + transform.rotation * localPos;
            lineRenderer.SetPosition(i, worldPos);
        }
    }
}
