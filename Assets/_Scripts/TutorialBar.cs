using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;
    public float hp = 1;
    public float maxHp = 1;
    [SerializeField] private float hurtSpeed = 0.005f;

    private void Start()
    {

    }

    private void Update()
    {
        hpImage.fillAmount = hp / maxHp;

        if(hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount += hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }
}
