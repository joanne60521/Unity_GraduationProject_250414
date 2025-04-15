using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FunctionManage : MonoBehaviour
{
    public MoveForwardByThumbstick moveForwardByThumbstick;
    public TurnHeadByThumbstick turnHeadByThumbstick;
    public TurnRobotByThumbstick turnRobotByThumbstick;
    public ControlRobotByGyroscope controlRobotByGyroscope;
    public VRRig_test vRRig_Test;
    public Arduino arduino;
    public SwitchMode_edit switchMode_L;
    public SwitchMode_edit switchMode_R;
    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;

    public float fadeDuration = 1f; // 淡入淡出時間

    void Start()
    {
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 0; // 預設為正常顏色
        }
    }

    public void CanTurnOff()
    {controlRobotByGyroscope.canTurn = false;}
    public void CanTurnOn()
    {controlRobotByGyroscope.canTurn = true;}

    public void CanMoveOff()
    {controlRobotByGyroscope.canMove = false;}
    public void CanMoveOn()
    {controlRobotByGyroscope.canMove = true;}

    public void BothHandCanSwitchOff()
    {switchMode_L.canSwitch = false;
    switchMode_R.canSwitch = false;}
    public void BothHandCanSwitchOn()
    {switchMode_L.canSwitch = true;
    switchMode_R.canSwitch = true;}

    public void CanShootOff()
    {arduino.canShoot = false;}
    public void CanShootOn()
    {arduino.canShoot = true;}

    public void CanPunchOff()
    {vRRig_Test.leftHand.attackMode = false;
    vRRig_Test.rightHand.attackMode = false;}
    public void CanPunchOn()
    {vRRig_Test.leftHand.attackMode = true;
    vRRig_Test.rightHand.attackMode = true;}

    public void ControlRobotHandOff()
    {vRRig_Test.enabled = false;}
    public void ControlRobotHandOn()
    {vRRig_Test.enabled = true;}

    public void AllFunctionOff()
    {
        // moveForwardByThumbstick.enabled = false;
        // turnHeadByThumbstick.enabled = false;
        // turnRobotByThumbstick.enabled = false;
        controlRobotByGyroscope.canTurn = false;
        controlRobotByGyroscope.canMove = false;
        switchMode_L.canSwitch = false;
        switchMode_R.canSwitch = false;
        arduino.canShoot = false;
        // vRRig_Test.enabled = false;
        vRRig_Test.leftHand.attackMode = false;
        vRRig_Test.rightHand.attackMode = false;
        vRRig_Test.leftHand.brake = false;
        vRRig_Test.rightHand.brake = false;
        Debug.Log("AllFunctionOff");
    }
    public void AllFunctionOn()
    {
        // moveForwardByThumbstick.enabled = true;
        // turnHeadByThumbstick.enabled = true;
        // turnRobotByThumbstick.enabled = true;
        controlRobotByGyroscope.canTurn = true;
        controlRobotByGyroscope.canMove = true;
        switchMode_L.canSwitch = true;
        switchMode_R.canSwitch = true;
        arduino.canShoot = true;
        // vRRig_Test.enabled = true;
        vRRig_Test.leftHand.attackMode = true;
        vRRig_Test.rightHand.attackMode = true;
        Debug.Log("AllFunctionOn");
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float lerpedExposure = Mathf.Lerp(-20, 0, t / fadeDuration);
            colorAdjustments.postExposure.value = lerpedExposure;
            yield return null;
        }
    }
    public IEnumerator FadeOut()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float lerpedExposure = Mathf.Lerp(0, -20, t / fadeDuration);
            colorAdjustments.postExposure.value = lerpedExposure;
            yield return null;
        }
    }
}
