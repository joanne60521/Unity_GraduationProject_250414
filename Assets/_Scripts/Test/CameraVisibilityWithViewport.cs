using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisibilityWithViewport : MonoBehaviour
{
    public Camera specificCamera; // 特定相机
    public GameObject targetObject; // 需要检测的对象
    public bool isInView = false;
    public float height;

    void Update()
    {
        if (targetObject != null)
        {
            // 因為 cockpit 的 mesh 沒有閉合，不能用 mesh collider，所以 raycast 不能用
            // // Step 1: 判斷是否在視口內
            // Vector3 viewportPoint = specificCamera.WorldToViewportPoint(targetObject.transform.position);
            // bool inViewport = viewportPoint.z > 0 &&
            //                 viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
            //                 viewportPoint.y >= 0 && viewportPoint.y <= 1;

            // isInView = false;

            // if (inViewport)
            // {
            //     Vector3 dir = targetObject.transform.position - specificCamera.transform.position;
            //     float distance = dir.magnitude;
            //     int obstructionMask = LayerMask.GetMask("Default", "Cockpit", "RobotHand");

            //     // Step 2: 判斷是否有遮擋
            //     if (Physics.Raycast(specificCamera.transform.position, dir, out RaycastHit hit, distance, obstructionMask))
            //     {
            //         Debug.Log(hit.transform.gameObject.name);
            //         if (hit.transform == targetObject || hit.transform.IsChildOf(targetObject.transform))
            //         {
            //             isInView = true;
            //         }
            //     }
            // }

            if (targetObject.transform.position.y >= height)
            {
                isInView = true;
            }
            else
            {
                isInView = false;
            }
        }
    }
}
