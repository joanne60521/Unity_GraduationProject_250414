using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSignalLight : MonoBehaviour
{
    public Renderer[] signalRenderers;
    // public SpriteRenderer[] spriteRenderers;
    public Material[] signalLights;
    public SwitchMode_edit switchMode_L;
    public SwitchMode_edit switchMode_R;
    public Arduino arduino;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < signalRenderers.Length; i++)
        {
            signalLights[i] = signalRenderers[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (switchMode_R.gunMode)  // right gun
        {
            signalLights[2].SetColor("_EmissionColor", Color.gray * 1.5f);

            if (arduino.bulletCount_R < switchMode_R.maxBullet * 0.5)
                signalLights[0].SetColor("_EmissionColor", Color.green * 1.5f);
            else if (arduino.bulletCount_R < switchMode_R.maxBullet * 0.75)
                signalLights[0].SetColor("_EmissionColor", Color.yellow * 1.5f);
            else if (arduino.bulletCount_R == switchMode_R.maxBullet)
                signalLights[0].SetColor("_EmissionColor", Color.red * 1.5f);
        }
        else  // right punch
        {
            signalLights[0].SetColor("_EmissionColor", Color.gray * 1.5f);
            signalLights[2].SetColor("_EmissionColor", Color.green * 1.5f);
        }


        if (switchMode_L.gunMode)  // left gun
        {
            signalLights[3].SetColor("_EmissionColor", Color.gray * 1.5f);

            if (arduino.bulletCount_L < switchMode_L.maxBullet * 0.5)
                signalLights[1].SetColor("_EmissionColor", Color.green * 1.5f);
            else if (arduino.bulletCount_L < switchMode_L.maxBullet * 0.75)
                signalLights[1].SetColor("_EmissionColor", Color.yellow * 1.5f);
            else if (arduino.bulletCount_L == switchMode_L.maxBullet)
                signalLights[1].SetColor("_EmissionColor", Color.red * 1.5f);
        }
        else  // left punch
        {
            signalLights[1].SetColor("_EmissionColor", Color.gray * 1.5f);
            signalLights[3].SetColor("_EmissionColor", Color.green * 1.5f);
        }
    }
}
