using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeEnemyVisibility : MonoBehaviour
{
    public Camera specificCamera; // 特定相机
    public GameObject[] cubeEnemys; // 需要检测的对象
    public bool isInView = false;


    public GameObject closestEnemy = null;
    public float closestDistanceSqr = Mathf.Infinity;

    List<GameObject> visibleEnemies = new List<GameObject>();

    void Update()
    {
        if (cubeEnemys[0] != null)
        {
            foreach (GameObject cubeEnemy in cubeEnemys)
            {
                // 将对象的世界坐标转换为视口坐标
                Vector3 viewportPoint = specificCamera.WorldToViewportPoint(cubeEnemy.transform.position);
                // 检查是否在相机视野内
                bool enemyIsInView = viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
                if (enemyIsInView)
                    visibleEnemies.Add(cubeEnemy);
            }
            if (visibleEnemies != null && visibleEnemies.Count > 0)
            {
                isInView = true;
                Vector3 currentPosition = transform.position;
                foreach (GameObject visibleEnemy in visibleEnemies)
                {
                    Vector3 directionToEnemy = visibleEnemy.transform.position - currentPosition;
                    float dSqrToEnemy = directionToEnemy.sqrMagnitude;

                    if (dSqrToEnemy < closestDistanceSqr)
                    {
                        closestDistanceSqr = dSqrToEnemy;
                        closestEnemy = visibleEnemy;
                    }
                }
            }
            else
            {
                isInView = false;
            }
        }
    }
}
