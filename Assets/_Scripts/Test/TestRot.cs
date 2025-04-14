using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRot : MonoBehaviour
{
    public Vector3 positionA; // 原始位置
    public GameObject cubeeRed;
    public GameObject cubeeBlue;



    void Start()
    {
        // 定义围绕 Y 轴旋转 90 度的四元数
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        
        // 旋转 positionA 相对于 Y=0 軸
        Vector3 rotatedPositionA = rotation * positionA;
        
        Instantiate(cubeeRed, positionA, transform.rotation);
        Instantiate(cubeeBlue, rotatedPositionA, transform.rotation);
    }
}
