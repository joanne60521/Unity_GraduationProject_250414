using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScaleUp : MonoBehaviour
{
    public bool scaleUp;
    [SerializeField]private  float scaleSize = 2f;
    [SerializeField]private  float delay = 2.5f;

    private Vector3 oriScale;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        oriScale = rectTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (scaleUp)
        {
            if (oriScale != Vector3.zero)
            {
                rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, oriScale * scaleSize, Time.deltaTime * delay);
            }
            else
            {
                rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, oriScale + new Vector3(1, 1, 1) * scaleSize, Time.deltaTime * delay);
            }
        }else
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, oriScale, Time.deltaTime * delay);
        }
    }
}
