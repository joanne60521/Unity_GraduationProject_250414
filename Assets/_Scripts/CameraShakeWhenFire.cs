using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeWhenFire : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.03f;

    private Vector3 originalPosition;
    private float remainingShakeTime;


    
    public Animator animator_robot;
    public float amplitude = 1f;
    public float smoothTime = 0.4f;

    private float maxY;
    private float minY;
    private Vector3 velocity = Vector3.zero;
    private Vector3 originalLocalPos;
    private Vector3 maxLocalPos;
    private Vector3 minLocalPos;
    private float positive = 1;

    void OnEnable()
    {
        // Save the initial camera position
        originalPosition = transform.localPosition;

        // 記錄初始位置
        originalLocalPos = transform.localPosition;
        maxY = transform.localPosition.y + amplitude;
        minY = transform.localPosition.y - amplitude;
        maxLocalPos = new Vector3(transform.localPosition.x, maxY, transform.localPosition.z);
        minLocalPos = new Vector3(transform.localPosition.x, minY, transform.localPosition.z);
    }

    public void TriggerShake(float duration = 0, float magnitude = 0)
    {
        remainingShakeTime = duration > 0 ? duration : shakeDuration;
        shakeMagnitude = magnitude > 0 ? magnitude : shakeMagnitude;
    }

    public void SignalTriggerShake()
    {
        TriggerShake(2f, 0.7f);
    }

    public void SignalTriggerShakeJet()
    {
        TriggerShake(1f, 0.7f);
    }
    public void SignalTriggerShakeJetSmaller()
    {
        TriggerShake(1f, 0.3f);
    }

    void Update()
    {
        if (remainingShakeTime > 0)
        {
            // Randomly shake the camera around its original position
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            remainingShakeTime -= Time.deltaTime;
        }
        else
        {
            // Reset to the original position
            // transform.localPosition = originalPosition;
            // 判斷目標是否在移動
            if (animator_robot.GetBool("isMoving"))
            {
                if (transform.localPosition.y > maxY - amplitude / 10)
                {
                    positive = -1;
                }else if (transform.localPosition.y < minY + amplitude / 10)
                {
                    positive = 1;
                }
                if (positive == 1)
                {
                    transform.localPosition = Vector3.SmoothDamp(transform.localPosition, maxLocalPos, ref velocity, smoothTime);
                    // SmoothTime越小越快
                    // transform.localPosition = Vector3.Lerp(transform.localPosition, maxLocalPos, smoothTime);
                }else if (positive == -1)
                {
                    transform.localPosition = Vector3.SmoothDamp(transform.localPosition, minLocalPos, ref velocity, smoothTime);
                    // transform.localPosition = Vector3.Lerp(transform.localPosition, minLocalPos, smoothTime);
                }
            }else
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, originalLocalPos, ref velocity, smoothTime);
                // transform.localPosition = Vector3.Lerp(transform.localPosition, originalLocalPos, smoothTime);
            }
        }
    }
}
