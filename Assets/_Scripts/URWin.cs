using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URWin : MonoBehaviour
{
    public Target target;
    public Target target1;
    private RectTransform rectTransform;
    private bool showed = false;
    public float showDuration = 3;
    private float unshowTime;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!showed)
        {
            if (target.died && target1.died)
            {
                Debug.Log("Win");
                rectTransform.localScale = new Vector3(1, 1, 1);
                showed = true;
                unshowTime = Time.time + showDuration;
            }
        }else if (Time.time > unshowTime)
        {
            rectTransform.localScale = Vector3.zero;
        }
    }
}
