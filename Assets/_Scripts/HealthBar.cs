using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;
    public Target target;
    private float hp;
    [HideInInspector] public float maxHp;
    [SerializeField] private float hurtSpeed = 0.005f;

    private void Start()
    {
        maxHp = target.health;
        hp = maxHp;
    }

    private void Update()
    {
        hp = target.health;
        hpImage.fillAmount = hp / maxHp;

        if(hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }


}
