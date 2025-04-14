using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSignalLight : MonoBehaviour
{
    public Light[] signalLights;
    public SpriteRenderer[] spriteRenderers;
    public SwitchMode_edit switchMode_L;
    public SwitchMode_edit switchMode_R;
    public Arduino arduino;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (switchMode_R.gunMode)  // right gun
        {
            spriteRenderers[2].color = Color.gray;

            if (arduino.bulletCount_R < switchMode_R.maxBullet * 0.5)
                spriteRenderers[0].color = Color.green;
            else if (arduino.bulletCount_R < switchMode_R.maxBullet * 0.75)
                spriteRenderers[0].color = Color.yellow;
            else if (arduino.bulletCount_R == switchMode_R.maxBullet)
                spriteRenderers[0].color = Color.red;
        }
        else  // right punch
        {
            spriteRenderers[0].color = Color.gray;
            spriteRenderers[2].color = Color.green;
        }


        if (switchMode_L.gunMode)  // left gun
        {
            spriteRenderers[3].color = Color.gray;

            if (arduino.bulletCount_L < switchMode_L.maxBullet * 0.5)
                spriteRenderers[1].color = Color.green;
            else if (arduino.bulletCount_L < switchMode_L.maxBullet * 0.75)
                spriteRenderers[1].color = Color.yellow;
            else if (arduino.bulletCount_L == switchMode_L.maxBullet)
                spriteRenderers[1].color = Color.red;
        }
        else  // left punch
        {
            spriteRenderers[1].color = Color.gray;
            spriteRenderers[3].color = Color.green;
        }

        // if (switchMode_R.gunMode)  // right gun
        // {
        //     signalLights[2].color = Color.gray;

        //     if (arduino.bulletCount_R < switchMode_R.maxBullet * 0.5)
        //         signalLights[0].color = Color.green;
        //     else if (arduino.bulletCount_R < switchMode_R.maxBullet * 0.75)
        //         signalLights[0].color = Color.yellow;
        //     else if (arduino.bulletCount_R == switchMode_R.maxBullet)
        //         signalLights[0].color = Color.red;
        // }
        // else  // right punch
        // {
        //     signalLights[0].color = Color.gray;
        //     signalLights[2].color = Color.green;
        // }


        // if (switchMode_L.gunMode)  // left gun
        // {
        //     signalLights[3].color = Color.gray;

        //     if (arduino.bulletCount_L < switchMode_L.maxBullet * 0.5)
        //         signalLights[1].color = Color.green;
        //     else if (arduino.bulletCount_L < switchMode_L.maxBullet * 0.75)
        //         signalLights[1].color = Color.yellow;
        //     else if (arduino.bulletCount_L == switchMode_L.maxBullet)
        //         signalLights[1].color = Color.red;
        // }
        // else  // left punch
        // {
        //     signalLights[1].color = Color.gray;
        //     signalLights[3].color = Color.green;
        // }
    }
}
