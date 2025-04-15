using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpotControl : MonoBehaviour
{
    [SerializeField] RectTransform miniMapRect; // 小地圖 RawImage 的 RectTransform
    [SerializeField] RectTransform enemyIcon;   // 敵人圖示
    [SerializeField] RectTransform playerIcon;
    [SerializeField] Camera miniMapCamera;      // 小地圖 Camera
    [SerializeField] Transform enemyTarget;     // 敵人 Transform
    [SerializeField] Transform playerTarget;
    [SerializeField] float miniMapBoundThickness = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 世界座標轉成 Viewport 座標（0~1）
        Vector3 viewportPos = miniMapCamera.WorldToViewportPoint(enemyTarget.position);

        // 2. Viewport 轉換成 anchoredPosition（UI 座標）
        Vector2 mapSize = miniMapRect.sizeDelta;

        // anchoredPosition 是以中心為 (0,0)，所以要 -0.5 再乘大小
        Vector2 enemyLocalPos = new Vector2(
            (viewportPos.x - 0.5f) * mapSize.x,
            (viewportPos.y - 0.5f) * mapSize.y
        );

        // 3. 設定邊界（限制圖示不超出小地圖 Rect）
        float halfWidth = mapSize.x / 2f * (1 - miniMapBoundThickness);
        float halfHeight = mapSize.y / 2f * (1 - miniMapBoundThickness);

        bool clamped = false;

        if (Mathf.Abs(enemyLocalPos.x) > halfWidth)
        {
            enemyLocalPos.x = Mathf.Sign(enemyLocalPos.x) * halfWidth;
            clamped = true;
        }

        if (Mathf.Abs(enemyLocalPos.y) > halfHeight)
        {
            enemyLocalPos.y = Mathf.Sign(enemyLocalPos.y) * halfHeight;
            clamped = true;
        }

        // 4. 更新 icon 位置
        enemyIcon.anchoredPosition = enemyLocalPos;

        // // 5. 可選：如果在邊界，旋轉箭頭指向敵人方向
        // if (clamped)
        // {
        //     Vector3 dir = (enemyTarget.position - miniMapCamera.transform.position).normalized;
        //     float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        //     enemyIcon.localRotation = Quaternion.Euler(0, 0, -angle);
        // }
        // else
        // {
        //     // 在小地圖內就不旋轉
        //     enemyIcon.localRotation = Quaternion.identity;
        // }


        // player icon pos
        Vector3 playerViewportPos = miniMapCamera.WorldToViewportPoint(playerTarget.position);
        Vector2 playerLocalPos = new Vector2(
            (playerViewportPos.x - 0.5f) * mapSize.x,
            (playerViewportPos.y - 0.5f) * mapSize.y
        );
        playerIcon.anchoredPosition = playerLocalPos;
        playerIcon.localRotation = Quaternion.Euler(0, 0, -playerTarget.eulerAngles.y);
    }
}
