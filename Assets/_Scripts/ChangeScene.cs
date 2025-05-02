using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeScene : MonoBehaviour
{
    public int loadSceneInt = 0;
    public GameObject[] noPostProcess;
    public Volume globalVolume; // 拖入場景中的 Global Volume
    private ColorAdjustments colorAdjustments;
    public float fadeDuration = 1f; // 淡入淡出時間

    void Start()
    {
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 0; // 預設為正常顏色
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        for (int i = 0; i < noPostProcess.Length; i ++)
        {
            noPostProcess[i].layer = LayerMask.NameToLayer("Default");
        }
        StartCoroutine(FadeFromBlack());
    }

    IEnumerator FadeFromBlack()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float lerpedExposure = Mathf.Lerp(0, -20, t / fadeDuration);
            colorAdjustments.postExposure.value = lerpedExposure;
            yield return null;
        }
        SceneManager.LoadScene(loadSceneInt);
    }
}
