using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PressToStart : MonoBehaviour
{
    public InputActionReference leftActivateValueReference;
    public InputActionReference rightActivateValueReference;
    public InputActionReference leftSelectValueReference;
    public InputActionReference rightSelectValueReference;
    public InputActionReference leftPrimaryBtnReference;
    public InputActionReference rightPrimaryBtnReference;
    public InputActionReference leftSecondaryBtnReference;
    public InputActionReference rightSecondaryBtnReference;
    // public Volume globalVolume; // 拖入場景中的 Global Volume
    private ColorAdjustments colorAdjustments;

    public TextMeshProUGUI textLogo;
    public TextMeshProUGUI textPressToStart;
    public AudioSource audioSourcePress;

    public float fadeDuration = 1f; // 淡入淡出時間
    private bool pressed = false;
 
    // Start is called before the first frame update
    void Start()
    {
        // if (globalVolume.profile.TryGet(out colorAdjustments))
        // {
        //     colorAdjustments.postExposure.value = -30f;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(!pressed)
        {
            if(
                leftActivateValueReference.action.ReadValue<float>() > 0.5
                || rightActivateValueReference.action.ReadValue<float>() > 0.5
                || leftSelectValueReference.action.ReadValue<float>() > 0.5
                || rightSelectValueReference.action.ReadValue<float>() > 0.5
                || leftPrimaryBtnReference.action.IsPressed()
                || rightPrimaryBtnReference.action.IsPressed()
                || leftSecondaryBtnReference.action.IsPressed()
                || rightSecondaryBtnReference.action.IsPressed()
                || UnityEngine.Input.GetKeyDown("s")
            )
            {
                pressed = true;
                audioSourcePress.Play();
                StartCoroutine(FadeOut());
            }
        }
    }

    public IEnumerator FadeOut()
    {
        Color originColor = textLogo.color;
        Color originColor1 = textPressToStart.color;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            textLogo.color = new Color(originColor.r, originColor.g, originColor.b, alpha);
            textPressToStart.color = new Color(originColor1.r, originColor1.g, originColor1.b, alpha);
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}
