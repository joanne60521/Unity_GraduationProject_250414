using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillationMotion : MonoBehaviour
{
    public RectTransform arrowUI;
    public float amplitude = 30f;       // 推動距離
    public float speed = 2f;            // 頻率
    private Vector3 startLocalPos;

    // Start is called before the first frame update
    void Start()
    {
        startLocalPos = arrowUI.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        Vector3 direction = transform.right; // 若不是箭頭所在物件方向，請手動指定
        arrowUI.localPosition = startLocalPos + direction * offset;
    }
}
