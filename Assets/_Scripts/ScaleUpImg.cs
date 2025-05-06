using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class ScaleUpImg : MonoBehaviour
{
    public VRRig_test vRRig_Test;
    public bool hideImg = false;
    private RectTransform rectTransform;
    private UnityEngine.UI.Image image;
    private Vector3 oriScale;
    [SerializeField]private  float delay = 2.5f;
    [SerializeField]private  float scaleSize = 2f;

    public CubeEnemyVisibility cubeEnemyVisibility;
    public Transform playerOriginMainCam;
    private float distanceThreshold = 20f;
    public bool imgScaleUp = false;
    public GameObject cubeEnemy;
    public ScaleUp scaleUp;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        distanceThreshold = vRRig_Test.leftHand.distanceThreshold;

        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<UnityEngine.UI.Image>();
        if (hideImg)
        {
            image.enabled = false;
        }
        oriScale = rectTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (cubeEnemy != null)
        {
            distance = (cubeEnemy.transform.position - playerOriginMainCam.transform.position).magnitude;
            if (distance < distanceThreshold && cubeEnemyVisibility.isInView)
            {
                imgScaleUp = true;
            }else
            {
                imgScaleUp = false;
            }
        }else
        {
            imgScaleUp = false;
        }
        
        if (imgScaleUp)
        {
            if (hideImg)
            {
                image.enabled = false;
            }
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, oriScale * scaleSize, Time.deltaTime * delay);
            scaleUp.scaleUp = true;
        }else
        {
            if (hideImg)
            {
                image.enabled = false;
            }
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, oriScale, Time.deltaTime * delay);
            scaleUp.scaleUp = false;
        }
    }
}
