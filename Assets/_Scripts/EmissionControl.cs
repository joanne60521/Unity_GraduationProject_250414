using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionControl : MonoBehaviour
{
    public Color emissionColor = Color.white;  // 可調整的 HDR 顏色
    public float intensity = 2.0f;             // HDR 強度

    public Renderer rend;
    public int matInt;
    private Material mat;
    [SerializeField] float blinkSpeed = 2f; 
    public bool emissionBlink;

    void Start()
    {
        if (rend != null)
        {
            mat = rend.materials[matInt]; // 取得材質
            EnableEmission();    // 啟用 Emission
            // UpdateEmissionColor(); // 設置 HDR 顏色
        }
    }

    void EnableEmission()
    {
        if (mat != null)
        {
            mat.EnableKeyword("_EMISSION");  // 確保 Emission 屬性啟用
        }
    }

    void UpdateEmissionColor()
    {
        if (mat != null)
        {
            Color finalColor = emissionColor * intensity;  // 乘上 HDR 強度
            mat.SetColor("_EmissionColor", finalColor);    // 設置 Emission 顏色
        }
    }

    // 測試用：按鍵改變 Emission 顏色
    void Update()
    {
        if (emissionBlink)
        {
            float emission = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
            Color finalColor = emissionColor * Mathf.LinearToGammaSpace(emission);
            mat.SetColor("_EmissionColor", finalColor);
        }
    }
}
