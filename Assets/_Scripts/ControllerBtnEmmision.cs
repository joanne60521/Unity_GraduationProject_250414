using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBtnEmmision : MonoBehaviour
{
    [SerializeField] Renderer rendererBtn_L;
    [SerializeField] Renderer rendererBtn1_L;
    [SerializeField] Renderer rendererBtn_R;
    [SerializeField] Renderer rendererBtn1_R;
    [SerializeField] float blinkSpeed = 2f; 
    public bool emmisionBlink = false;
    [SerializeField] Color emmisionColor = Color.red;
    private Material material_L;
    private Material material1_L;
    private Material material_R;
    private Material material1_R;
    // Start is called before the first frame update
    void Start()
    {
        material_L = rendererBtn_L.material;
        material1_L = rendererBtn1_L.material;
        material_R = rendererBtn_R.material;
        material1_R = rendererBtn1_R.material;

        material_L.EnableKeyword("_EMISSION");
        material1_L.EnableKeyword("_EMISSION");
        material_R.EnableKeyword("_EMISSION");
        material1_R.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (emmisionBlink)
        {
            float emission = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
            Color finalColor = emmisionColor * Mathf.LinearToGammaSpace(emission);
            material_L.SetColor("_EmissionColor", finalColor);
            material1_L.SetColor("_EmissionColor", finalColor);
            material_R.SetColor("_EmissionColor", finalColor);
            material1_R.SetColor("_EmissionColor", finalColor);
        }
    }

    public void SetBlinking(bool state)
    {
        emmisionBlink = state;
        if (!state)
        {
            material_L.SetColor("_EmissionColor", Color.black);
            material1_L.SetColor("_EmissionColor", Color.black);
            material_R.SetColor("_EmissionColor", Color.black);
            material1_R.SetColor("_EmissionColor", Color.black);
        }
    }
}
