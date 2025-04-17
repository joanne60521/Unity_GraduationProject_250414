using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionPowerOff : MonoBehaviour
{
    public ControlSignalLight controlSignalLight;
    public Renderer[] renderers;
    public Material[] materials;
    public bool powerOff = false;
    private float intensity = 2;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (powerOff)
        {
            controlSignalLight.enabled = false;
            for (int i = 0; i < materials.Length; i++)
            {
                intensity = intensity - Time.deltaTime * 0.5f;
                Color finalColor = materials[i].color * intensity;  // 乘上 HDR 強度
                materials[i].SetColor("_EmissionColor", finalColor);    // 設置 Emission 顏色
            }
        }
    }
}
