using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeWhenWalk : MonoBehaviour
{
    public Animator animator_robot;
    public float amplitude = 0.2f;
    public float smoothTime = 0.2f;

    private float maxY;
    private float minY;
    private Vector3 velocity = Vector3.zero;
    private Vector3 originalLocalPos;
    private Vector3 maxLocalPos;
    private Vector3 minLocalPos;
    private float positive = 1;

    void Start()
    {
        // 記錄初始位置
        originalLocalPos = transform.localPosition;
        maxY = transform.localPosition.y + amplitude;
        minY = transform.localPosition.y - amplitude;
        maxLocalPos = new Vector3(transform.localPosition.x, maxY, transform.localPosition.z);
        minLocalPos = new Vector3(transform.localPosition.x, minY, transform.localPosition.z);
    }

    void Update()
    {
        // 判斷目標是否在移動
        if (animator_robot.GetBool("isMoving"))
        {
            if (transform.localPosition.y > maxY - amplitude/10)
            {
                positive = -1;
            }else if (transform.localPosition.y < minY + amplitude/10)
            {
                positive = 1;
            }
            if (positive == 1)
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, maxLocalPos, ref velocity, smoothTime);
                // transform.localPosition = Vector3.Lerp(transform.localPosition, maxLocalPos, smoothTime);
            }else if (positive == -1)
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, minLocalPos, ref velocity, smoothTime);
                // transform.localPosition = Vector3.Lerp(transform.localPosition, minLocalPos, smoothTime);
            }
        }else
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, originalLocalPos, ref velocity, smoothTime);
        }
    }
}
