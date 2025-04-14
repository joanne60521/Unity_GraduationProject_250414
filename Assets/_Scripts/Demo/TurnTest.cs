using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTest : MonoBehaviour
{
    public float rotationSpeed = 3.0f;
    public float verticalRotationLimit = 90.0f;

    private float verticalRotation = 0.0f;

    void Update()
    {
        // 获取游戏手柄的输入
        float horizontalRotation = Input.GetAxis("Horizontal") * rotationSpeed;
        float verticalRotationInput = Input.GetAxis("Vertical") * rotationSpeed;

        // 根据输入旋转相机
        transform.Rotate(Vector3.up, horizontalRotation);
        verticalRotation -= verticalRotationInput;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        transform.localRotation = Quaternion.Euler(verticalRotation, transform.localEulerAngles.y, 0);
    }
}
