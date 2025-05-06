using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertWhenTryToSwitch : MonoBehaviour
{
    public TutorialManage tutorialManage;
    public SwitchMode_edit switchMode_edit;
    public ControlRobotByGyroscope controlRobotByGyroscope;
    public AudioSource audioSource;

    void Update()
    {
        if (tutorialManage.currentStep == TutorialManage.TutorialState.Shoot1 || tutorialManage.currentStep == TutorialManage.TutorialState.V_Shoot2)
        {
            if (switchMode_edit.vrInput)
            {
                Debug.Log("Please follow the instruction.");
                audioSource.Play();
            }
        }
        else if (tutorialManage.currentStep == TutorialManage.TutorialState.V_Forward)
        {
            if (controlRobotByGyroscope.tryToBackward)
            {
                Debug.Log("Please follow the instruction.");
                audioSource.Play();
            }
        }
    }
}
