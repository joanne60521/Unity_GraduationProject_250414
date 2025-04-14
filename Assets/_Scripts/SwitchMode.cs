using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SwitchMode : MonoBehaviour
{
    public bool gunMode = true;
    public bool gunMode1 = true;
    public GunFire gunFire;
    public GunFire gunFire1;
    public VRRig_test vRRig_Test;
    // public SkinnedMeshRenderer skinnedMeshRenderer_gun;
    public InputActionReference rightSelectReference;
    public InputActionReference rightSelectReference1;
    public bool rightSelect = false;
    public InputActionReference leftSelectReference;
    public InputActionReference leftSelectReference1;
    public bool leftSelect = false;
    [SerializeField]private bool pressedR = false;
    [SerializeField]private bool pressedL = false;
    public GameObject gunCrosshair;
    public GameObject gunCrosshair1;
    public LineRenderer laserLine;
    public LineRenderer laserLine1;
    public Animator animator;
    public Animator animator1;
    public bool canSwitch = true;
    private AudioSource audioSource;
    public int maxBullet = 20;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SwitchGunMode();
        SwitchGunMode1();
    }

    // Update is called once per frame
    void Update()
    {
        // rightSelect = rightSelectReference.action.IsPressed();
        // leftSelect = leftSelectReference.action.IsPressed();
        if (rightSelectReference.action.IsPressed() || rightSelectReference1.action.IsPressed())
            rightSelect = true;
        else
            rightSelect = false;

        if (leftSelectReference.action.IsPressed() || leftSelectReference1.action.IsPressed())
            leftSelect = true;
        else
            leftSelect = false;


        if (Input.GetKeyDown("g"))
            rightSelect = true;
        if (Input.GetKeyDown("f"))
            leftSelect = true;


        if (canSwitch)
        {
            if (rightSelect && !pressedR)
            {
                if (!pressedR)
                SwitchGunMode();
                pressedR = true;
            }
            if (!rightSelect)
            {
                pressedR = false;
            }
            if (leftSelect && !pressedL)
            {
                SwitchGunMode1();
                pressedL = true;
            }
            if (!leftSelect)
            {
                pressedL = false;
            }
        }
    }

    public void SwitchGunMode()
    {
        gunMode = !gunMode;
        gunFire.enabled = gunMode;
        // skinnedMeshRenderer_gun.enabled = gunMode;
        gunCrosshair.SetActive(gunMode);
        laserLine.enabled = gunMode;
        vRRig_Test.rightHand.attackMode = !gunMode;
        animator.SetBool("gunMode", gunMode);
        audioSource.Play();
    }

    public void SwitchGunMode1()
    {
        gunMode1 = !gunMode1;
        gunFire1.enabled = gunMode1;
        // skinnedMeshRenderer_gun.enabled = gunMode;
        gunCrosshair1.SetActive(gunMode1);
        laserLine1.enabled = gunMode1;
        vRRig_Test.leftHand.attackMode = !gunMode1;
        animator1.SetBool("gunMode", gunMode1);
        audioSource.Play();
    }
}
