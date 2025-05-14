using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;
using UnityEngine.Video;
using UnityEngine.Playables;

public class TutorialManage : MonoBehaviour
{
    public TutorialBar tutorialBar;
    public Transform PlayerRobot;
    public Transform Enemy;
    public CameraVisibilityWithViewport cameraVisibilityWithViewport_L;
    public CameraVisibilityWithViewport cameraVisibilityWithViewport_R;
    public AudioSource audioSource;
    public AudioSource completeTutorAudioSource;
    public ControllerBtnEmmision controllerBtnEmmision;
    public PlayableDirector attackedTimeline;
    public VideoPlayer videoPlayer;
    public RawImage screenRawImage;
    public enum TutorialState {V_Hand1, Hand2, V_Vision1, V_Vision2, V_Forward, Punch1, V_Punch2, V_Backward, Switch1, Switch2, V_Switch3, Shoot1, V_Shoot2, Finish}
    public TutorialState currentStep = TutorialState.V_Hand1;

    public GameObject tutorialUI; // 放 UI 提示 (Text, Canvas)
    public GameObject tutorialObjects;
    public Text tutorialText; // 文字顯示
    public Image tutorialImage; // 拖入 UI Image
    // public Sprite[] tutorialSprites; // 放入所有教學示意圖
    public Texture[] tutorialTextures; // 放入所有教學示意圖
    public VideoClip[] tutorialVideoClips;
    public AudioClip[] tutorialAudioClips;
    public GameObject[] tutorialCards;
    public GameObject enemy; // 敵人
    public Volume globalVolume; // 拖入場景中的 Global Volume
    private ColorAdjustments colorAdjustments;
    public float fadeDuration = 1f; // 淡入淡出時間
    
    public MoveForwardByThumbstick moveForwardByThumbstick;
    public TurnRobotByThumbstick turnRobotByThumbstick;
    public TurnHeadByThumbstick turnHeadByThumbstick;
    public ControlRobotByGyroscope controlRobotByGyroscope;
    public VRRig_test vRRig_Test;
    public Arduino arduino;
    public SwitchMode_edit switchMode_L;
    public SwitchMode_edit switchMode_R;
    public BulletScript bulletScript;
    public MoveToEnemyTrigger moveToEnemyTrigger_Forward;

    public AudioClip tutorialAlertClip;

    private bool isHand = true;
    private bool isVision1 = true;
    private bool isVision2 = true;
    private bool isForwarded = true;
    private bool isSwitched = true;
    private bool isShooted = true;
    private bool isBackwarded = true;
    private bool isPunched = true;

    public bool cutsceneOn = false;

    private Coroutine shrinkCoroutine = null;

    private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 startScale;

    public float frequency = 0;
    private bool barCanMove = false;


    void Start()
    {
        // 關掉功能
        // turnHeadByThumbstick.enabled = false;
        // turnRobotByThumbstick.enabled = false;
        // moveForwardByThumbstick.enabled = false;
        controlRobotByGyroscope.canTurn = false;
        controlRobotByGyroscope.canMove = false;
        controlRobotByGyroscope.canBackward = false;
        switchMode_L.canSwitch = false;
        switchMode_R.canSwitch = false;
        arduino.canShoot = false;
        // vRRig_Test.enabled = false;
        vRRig_Test.leftHand.attackMode = false;
        vRRig_Test.rightHand.attackMode = false;
        bulletScript.damage = 0;

        startPos = tutorialObjects.transform.localPosition;
        startRot = tutorialObjects.transform.localEulerAngles;
        startScale = tutorialObjects.transform.localScale;

        tutorialUI.SetActive(false);

        // 嘗試從 Volume Profile 獲取 Color Adjustments
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 0; // 預設為正常顏色
        }

        if (!cutsceneOn)
            // 開始教學
            StartTutorial();
    }

    IEnumerator FadeFromBlack()
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

    IEnumerator AutoNextStep(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextStepWithDelay(0);
    }





    public void StartTutorial()
    {
        // StartCoroutine(FadeFromBlack());
        // PauseGame(true, 3); // 進入教學時，先暫停敵人
        ShowTutorial(0);
        StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
        vRRig_Test.enabled = true;
        isHand = false;
        barCanMove = true;

        tutorialCards[0].SetActive(true);
        tutorialCards[1].SetActive(true);
        tutorialCards[19].SetActive(true);
        tutorialCards[20].SetActive(true);
    }


    IEnumerator NextStepWithDelay(float delay)
    {
        // PauseGame(false, 0);  // 恢復敵人和環境的正常狀態
        // StartCoroutine(HideMessageAfterDelay(1f));
        yield return new WaitForSeconds(delay); // 等待時間

        frequency = 0;

        // 等待後進行步驟過渡
        switch (currentStep)
        {
            case TutorialState.V_Hand1:
                currentStep = TutorialState.Hand2;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                StartCoroutine(NextStepWithDelay(5));

                tutorialCards[0].SetActive(false);
                tutorialCards[1].SetActive(false);
                tutorialCards[19].SetActive(false);
                tutorialCards[20].SetActive(false);
                tutorialCards[2].SetActive(true);
                break;

            case TutorialState.Hand2:
                currentStep = TutorialState.V_Vision1;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                // PauseGame(true, 3); // 進入教學時，先暫停敵人
                // ShowMessage("旋轉視野\n右腳往前踩、左腳往後踩 → 向右轉\n左腳往前踩、右腳往後踩 → 向左轉", 0);
                ShowTutorial(1);
                isVision1 = false;
                // turnHeadByThumbstick.enabled = true;
                // turnRobotByThumbstick.enabled = true;
                controlRobotByGyroscope.canTurn = true;

                tutorialBar.hp = 0;
                barCanMove = true;

                tutorialCards[2].SetActive(false);
                tutorialCards[3].SetActive(true);
                break;

            case TutorialState.V_Vision1:
                currentStep = TutorialState.V_Vision2;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                ShowTutorial(2);
                isVision2 = false;

                tutorialBar.hp = 0;
                barCanMove = true;

                tutorialCards[3].SetActive(false);
                tutorialCards[4].SetActive(true);
                break;

            case TutorialState.V_Vision2:
                currentStep = TutorialState.V_Forward;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                // PauseGame(true, 0); // 進入教學時，先暫停敵人
                // ShowMessage("移動\n雙腳往前踩 → 前進\n雙腳往後踩 → 後退", 1);
                ShowTutorial(3);
                // moveForwardByThumbstick.enabled = true;
                controlRobotByGyroscope.canMove = true;
                isForwarded = false;

                tutorialBar.hp = 0;
                barCanMove = true;
                tutorialBar.maxHp = (PlayerRobot.position - Enemy.position).magnitude - vRRig_Test.leftHand.distanceThreshold;
                
                tutorialCards[4].SetActive(false);
                tutorialCards[5].SetActive(true);
                break;

            case TutorialState.V_Forward:
                currentStep = TutorialState.Punch1;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                ShowTutorial(4);
                StartCoroutine(NextStepWithDelay(5));
                
                tutorialCards[5].SetActive(false);
                tutorialCards[6].SetActive(true);
                break;

            case TutorialState.Punch1:
                currentStep = TutorialState.V_Punch2;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                // PauseGame(true, 0);
                // ShowMessage("距離敵人夠\"近\"，營幕提示揮拳時，往前揮出！", 4);
                vRRig_Test.leftHand.attackMode = true;
                vRRig_Test.rightHand.attackMode = true;
                isPunched = false;

                tutorialBar.hp = 0;
                barCanMove = true;

                tutorialCards[6].SetActive(false);
                tutorialCards[7].SetActive(true);
                tutorialCards[8].SetActive(true);
                tutorialCards[19].SetActive(true);
                tutorialCards[20].SetActive(true);
                break;

            case TutorialState.V_Punch2:
                currentStep = TutorialState.V_Backward;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                // PauseGame(true, 0);
                ShowTutorial(5);
                controlRobotByGyroscope.canBackward = true;
                isBackwarded = false;

                tutorialBar.hp = 0;
                barCanMove = true;
                
                tutorialCards[7].SetActive(false);
                tutorialCards[8].SetActive(false);
                tutorialCards[19].SetActive(false);
                tutorialCards[20].SetActive(false);
                tutorialCards[9].SetActive(true);
                break;

            case TutorialState.V_Backward:
                currentStep = TutorialState.Switch1;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                // PauseGame(true, 0);
                // ShowMessage("現在按下任一發光按鈕切換成槍擊模式！", 2);
                ShowTutorial(6);
                StartCoroutine(NextStepWithDelay(5));
                
                tutorialCards[9].SetActive(false);
                tutorialCards[10].SetActive(true);
                break;

            case TutorialState.Switch1:
                currentStep = TutorialState.Switch2;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                StartCoroutine(NextStepWithDelay(5));
                
                tutorialCards[10].SetActive(false);
                tutorialCards[11].SetActive(true);
                break;

            case TutorialState.Switch2:
                currentStep = TutorialState.V_Switch3;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                switchMode_L.canSwitch = true;
                switchMode_R.canSwitch = true;
                isSwitched = false;
                controllerBtnEmmision.SetBlinking(true);

                tutorialBar.hp = 0;
                barCanMove = true;
                                
                tutorialCards[11].SetActive(false);                
                tutorialCards[12].SetActive(true);
                tutorialCards[13].SetActive(true);
                tutorialCards[19].SetActive(true);
                tutorialCards[20].SetActive(true);

                tutorialCards[17].SetActive(true);
                tutorialCards[18].SetActive(true);
                break;

            case TutorialState.V_Switch3:
                currentStep = TutorialState.Shoot1;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                ShowTutorial(7);
                StartCoroutine(NextStepWithDelay(5));

                tutorialCards[12].SetActive(false);
                tutorialCards[13].SetActive(false);
                tutorialCards[19].SetActive(false);
                tutorialCards[20].SetActive(false);
                tutorialCards[14].SetActive(true);

                tutorialCards[17].SetActive(false);
                tutorialCards[18].SetActive(false);
                break;

            case TutorialState.Shoot1:
                currentStep = TutorialState.V_Shoot2;
                StartCoroutine(PlayTutorialAudio((int)currentStep, 0));
                // PauseGame(true, 0);
                // ShowMessage("伸出手臂，瞄準藍色準星，按下扳機射擊！", 3);
                arduino.canShoot = true;
                isShooted = false;

                tutorialBar.hp = 0;
                barCanMove = true;

                tutorialCards[14].SetActive(false);
                tutorialCards[15].SetActive(true);
                tutorialCards[16].SetActive(true);
                tutorialCards[19].SetActive(true);
                tutorialCards[20].SetActive(true);
                break;

            case TutorialState.V_Shoot2:
                currentStep = TutorialState.Finish;
                // ShowMessage("教學完成！開始戰鬥吧！", 5);
                // PauseGame(false, 0); // 教學結束，恢復敵人
                bulletScript.damage = 20;
                attackedTimeline.Play();

                tutorialCards[15].SetActive(false);
                tutorialCards[16].SetActive(false);
                tutorialCards[19].SetActive(false);
                tutorialCards[20].SetActive(false);
                break;

        }
    }





    void ShowTutorial(int videoIndex)
    {
        // screenRawImage.texture = tutorialTextures[imageIndex];
        videoPlayer.Stop();
        videoPlayer.clip = tutorialVideoClips[videoIndex];
        videoPlayer.Play();
    }

    IEnumerator PlayTutorialAudio(int clipIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // 停止當前播放
        }
    
        audioSource.clip = tutorialAudioClips[clipIndex]; // 換音訊
        audioSource.Play();         // 播放新的 clip

    }


    // void ShowMessage(string message, int imageIndex)
    // {
    //     Vector3 targetPos = new Vector3(0, -33, 0);
    //     Vector3 targetRot = new Vector3(40, -0, 0);
    //     Vector3 targetScale = new Vector3(0.4f, 0.4f, 0.4f);
    //     if (shrinkCoroutine != null)
    //     {
    //         StopCoroutine(shrinkCoroutine);
    //     }
    //     shrinkCoroutine = StartCoroutine(ShrinkTutorial(5f, targetPos, targetRot, targetScale, 3f));

    //     tutorialUI.SetActive(true);
    //     tutorialObjects.transform.localPosition = startPos;
    //     tutorialObjects.transform.localEulerAngles = startRot;
    //     tutorialObjects.transform.localScale = startScale;
    //     tutorialText.text = message;
    //     // tutorialImage.sprite = tutorialSprites[imageIndex];
    //     AudioSource.PlayClipAtPoint(tutorialAlertClip, tutorialUI.transform.position);
    //     // StartCoroutine(HideMessageAfterDelay(7f)); // 顯示訊息 3 秒
    // }

    // IEnumerator HideMessageAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     tutorialUI.SetActive(false);
    //     tutorialObjects.transform.localPosition = new Vector3(0, 0, 0);
    //     tutorialObjects.transform.localEulerAngles = new Vector3(0, 0, 0);
    //     tutorialObjects.transform.localScale = new Vector3(1, 1, 1);
    // }

    // IEnumerator ShrinkTutorial(float delay, Vector3 targetPos, Vector3 targetRot, Vector3 targetScale, float duration)
    // {
    //     yield return new WaitForSeconds(delay);
    //     float elapsedTime = 0f;

    //     if (currentStep == TutorialState.Finish)
    //     {
    //         Destroy(tutorialUI);
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         while (elapsedTime < duration)
    //         {
    //             tutorialObjects.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
    //             tutorialObjects.transform.localEulerAngles = Vector3.Lerp(startRot, targetRot, elapsedTime / duration);
    //             tutorialObjects.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
    //             elapsedTime += Time.deltaTime; // 隨時間累加
    //             yield return null; // 等待下一幀
    //         }
    //     }

    //     tutorialObjects.transform.localPosition = targetPos;
    //     tutorialObjects.transform.localEulerAngles = targetRot;
    //     tutorialObjects.transform.localScale = targetScale;
    // }

    void PauseGame(bool pause, float delay)
    {
        new WaitForSeconds(delay);
        // 讓敵人完全停止或恢復
        if (enemy != null)
        {
            if (enemy.TryGetComponent(out NavMeshAgent agent))
            {
                agent.isStopped = pause; // 停止 AI
            }

            if (enemy.TryGetComponent(out Animator anim))
            {
                anim.speed = pause ? 0 : 1; // 停止或恢復動畫
            }
        }

        // 環境變灰
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = pause ? -40 : 0;
        }
    }





    void PlayFinishTutorSound()
    {
        // 音效提示完成教學
        if (completeTutorAudioSource.isPlaying)
            completeTutorAudioSource.Stop();
        completeTutorAudioSource.Play();
    }


    public void OnPlayerHand()
    {
        if (currentStep == TutorialState.V_Hand1)
        {
            StartCoroutine(NextStepWithDelay(0f));
            PlayFinishTutorSound();
        }
    }

    public void OnPlayerVision1()
    {
        if (currentStep == TutorialState.V_Vision1)
        {
            StartCoroutine(NextStepWithDelay(0f));
            PlayFinishTutorSound();
        }
    }

    public void OnPlayerVision2()
    {
        if (currentStep == TutorialState.V_Vision2)
        {
            StartCoroutine(NextStepWithDelay(0f));
            PlayFinishTutorSound();
        }
    }

    public void OnPlayerForward()
    {
        if (currentStep == TutorialState.V_Forward)
        {
            StartCoroutine(NextStepWithDelay(0f));
            PlayFinishTutorSound();
        }
    }

    public void OnPlayerPunch()
    {
        if (currentStep == TutorialState.V_Punch2)
        {
            StartCoroutine(NextStepWithDelay(0f));
            PlayFinishTutorSound();
        }
    }

    public void OnPlayerBackward()
    {
        if (currentStep == TutorialState.V_Backward)
        {
            // 延遲後結束教學
            StartCoroutine(NextStepWithDelay(0f));
            PlayFinishTutorSound();
        }
    }
    public void OnPlayerSwitch()
    {
        if (currentStep == TutorialState.V_Switch3)
        {
            controllerBtnEmmision.SetBlinking(false);
            StartCoroutine(NextStepWithDelay(0f));
            PlayFinishTutorSound();
        }
    }
    public void OnPlayerShoot()
    {
        if (currentStep == TutorialState.V_Shoot2)
        {
            // 延遲後進入防禦教學
            StartCoroutine(NextStepWithDelay(0f));
            PlayFinishTutorSound();
        }
    }
    public void OnSkipTutorial()
    {
        StartCoroutine(NextStepWithDelay(0f));
    }





    // Update is called once per frame
    void Update()
    {
        if (cameraVisibilityWithViewport_L.isInView || cameraVisibilityWithViewport_R.isInView)
        {
            if (currentStep == TutorialState.V_Hand1)
            {
                frequency = frequency + Time.deltaTime;
                if (barCanMove)
                {
                    tutorialBar.maxHp = 4;
                    tutorialBar.hp = frequency;
                }
                if (!isHand && frequency >= 4 && !audioSource.isPlaying)
                {
                    isHand = true;
                    OnPlayerHand();
                    frequency = 0;
                    barCanMove = false;
                }
            }
        }

        if (controlRobotByGyroscope.gyState == ControlRobotByGyroscope.GyState.Left && currentStep == TutorialState.V_Vision1)
        // if (turnRobotByThumbstick.turnL && currentStep == TutorialState.V_Vision1)
        {
            frequency = frequency + Time.deltaTime;
            if (barCanMove)
            {
                tutorialBar.maxHp = 1.5f;
                tutorialBar.hp = frequency;
            }

            if (!isVision1 && frequency >= 1.5f && !audioSource.isPlaying)
            {
                isVision1 = true;
                OnPlayerVision1();
                frequency = 0;
                barCanMove = false;
            }
        }

        if (controlRobotByGyroscope.gyState == ControlRobotByGyroscope.GyState.Right && currentStep == TutorialState.V_Vision2)
        // if (turnRobotByThumbstick.turnR && currentStep == TutorialState.V_Vision2)
        {
            frequency = frequency + Time.deltaTime;
            if (barCanMove)
            {
                tutorialBar.maxHp = 1.5f;
                tutorialBar.hp = frequency;
            }

            if (!isVision2 && frequency >= 1.5f && !audioSource.isPlaying)
            {
                isVision2 = true;
                OnPlayerVision2();
                frequency = 0;
                barCanMove = false;
            }
        }

        if (currentStep == TutorialState.V_Forward)
        {
            if (barCanMove)
            {
                tutorialBar.hp = tutorialBar.maxHp - ((PlayerRobot.position - Enemy.position).magnitude - vRRig_Test.leftHand.distanceThreshold);
            }

            if (!isForwarded && (PlayerRobot.position - Enemy.position).magnitude < vRRig_Test.leftHand.distanceThreshold && !audioSource.isPlaying)
            {
                Debug.Log(vRRig_Test.leftHand.distance);
                Debug.Log(vRRig_Test.leftHand.distanceThreshold);
                isForwarded = true;
                OnPlayerForward();
                barCanMove = false;
            }
        }

        if (vRRig_Test.leftHand.attacking || vRRig_Test.rightHand.attacking)
        {
            if (barCanMove)
            {
                tutorialBar.maxHp = 3;
                tutorialBar.hp = vRRig_Test.leftHand.punchCount + vRRig_Test.rightHand.punchCount;
            }

            if (!isPunched && (vRRig_Test.leftHand.punchCount + vRRig_Test.rightHand.punchCount) >= 3 && !audioSource.isPlaying)
            {
                isPunched = true;
                OnPlayerPunch();
            }
        }

        if (controlRobotByGyroscope.gyState == ControlRobotByGyroscope.GyState.Back && currentStep == TutorialState.V_Backward)
        // if (moveForwardByThumbstick.thumbstickY < -0.5 && currentStep == TutorialState.V_Backward)
        {
            frequency = frequency + Time.deltaTime;
            if (barCanMove)
            {
                tutorialBar.maxHp = 4;
                tutorialBar.hp = frequency;
            }

            if (!isBackwarded && frequency >= 4 && !audioSource.isPlaying)
            {
                isBackwarded = true;
                OnPlayerBackward();
                frequency = 0;
                barCanMove = false;
            }
        }

        if (switchMode_L.gunMode || switchMode_R.gunMode)
        {
            if (!isSwitched && !audioSource.isPlaying)
            {
                isSwitched = true;
                OnPlayerSwitch();
            }
        }

        if ((arduino.bulletCount_L + arduino.bulletCount_R) > 0)
        {
            if (barCanMove)
            {
                tutorialBar.maxHp = 4;
                tutorialBar.hp = arduino.bulletCount_L + arduino.bulletCount_R;
            }
            if (!isShooted && arduino.bulletCount_L + arduino.bulletCount_R >= 4 && !audioSource.isPlaying)
            {
                isShooted = true;
                OnPlayerShoot();
                barCanMove = false;
            }
        }



        if (Input.GetKeyDown("k"))
        {
            OnSkipTutorial();
        }

        if (Input.GetKeyDown("l"))
        {
            Debug.Log(currentStep);
        }
    }
}
