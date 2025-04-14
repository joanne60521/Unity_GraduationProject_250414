using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisibilityWithViewport : MonoBehaviour
{
    public Camera specificCamera; // 特定相机
    public GameObject targetObject; // 需要检测的对象
    public bool isInView = false;

    void Update()
    {
        if (targetObject != null)
        {
            // 将对象的世界坐标转换为视口坐标
            Vector3 viewportPoint = specificCamera.WorldToViewportPoint(targetObject.transform.position);

            // 检查是否在相机视野内
            isInView = viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
        }
    }
}
