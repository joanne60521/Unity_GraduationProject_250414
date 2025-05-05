using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertWhenTryToSwitch : MonoBehaviour
{
    public TutorialManage tutorialManage;
    public SwitchMode_edit switchMode_edit;
    public AudioSource audioSource;

    void Update()
    {
        if (switchMode_edit.vrInput && tutorialManage.currentStep == TutorialManage.TutorialState.V_Backward)
        {
            Debug.Log("Please follow the instruction.");
            audioSource.Play();
        }
    }
}
