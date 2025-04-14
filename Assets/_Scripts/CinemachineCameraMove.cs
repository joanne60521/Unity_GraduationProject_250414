using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

public class CinemachineCameraMove : MonoBehaviour
{
    public CountdownTimer countdownTimer;
    public GameObject PlayerRobotModel;
    public GameObject Cockpit;
    public FunctionManage functionManage;
    public Transform cameraRig; // VR 相機的父物件
    public CinemachineSmoothPath dollyTrack; // Cinemachine 路徑
    public float cutsceneDuration = 8f;
    public ResetCamera resetCamera;
    public float fadeDuration = 1f; // 淡入淡出時間
    public Volume globalVolume; // 拖入場景中的 Global Volume
    public TutorialManage tutorialManage;
    public GameObject[] gameObjectsUnactive;
    public SwitchMode_edit[] switchModes;
    public GameObject robotModel;
    public Transform lookAtRobotHead;
    private ColorAdjustments colorAdjustments;

    private float elapsedTime = 0f;
    private float t = 0;

    void Awake()
    {
        // tutorialManage.cutsceneOn = true;
        int i = 0;
        while (i < gameObjectsUnactive.Length)
        {
            gameObjectsUnactive[i].SetActive(false);
            i++;
        }
        int j = 0;
        while (j < switchModes.Length)
        {
            switchModes[j].enabled = false;
            j++;
        }
    }

    void Start()
    {
        // 嘗試從 Volume Profile 獲取 Color Adjustments
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = 0; // 預設為正常曝光
        }
        functionManage.AllFunctionOff();
        PlayerRobotModel.SetActive(false);
        Cockpit.SetActive(false);
        countdownTimer.enabled = false;
    }

    void Update()
    {
        if (elapsedTime < cutsceneDuration)
        {
            elapsedTime += Time.deltaTime;
            float pathPos = elapsedTime / cutsceneDuration;
            cameraRig.position = dollyTrack.EvaluatePosition(pathPos);
        }
        if (elapsedTime > cutsceneDuration - 2)
        {
            if (t < fadeDuration)
            {
                t += Time.deltaTime;
                float lerpedExposure = Mathf.Lerp(0, -20, t / fadeDuration);
                colorAdjustments.postExposure.value = lerpedExposure;
            }
            else
            {
                int i = 0;
                while (i < gameObjectsUnactive.Length)
                {
                    gameObjectsUnactive[i].SetActive(true);
                    i++;
                }
                int j = 0;
                while (j < switchModes.Length)
                {
                    switchModes[j].enabled = true;
                    j++;
                }
                resetCamera.ResetMainCamPos();
                PlayerRobotModel.SetActive(true);
                Cockpit.SetActive(true);
                functionManage.StartFadeIn();
                functionManage.AllFunctionOn();
                countdownTimer.enabled = true;
                // tutorialManage.StartTutorial();
                Destroy(robotModel);
                Destroy(gameObject);
            }
        }
    }

    void LateUpdate()
    {
        cameraRig.LookAt(lookAtRobotHead);
    }
}
