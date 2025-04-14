using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchMode_edit : MonoBehaviour
{
    public bool isLeft;
    public bool gunMode = true;
    public GunFire gunFire;
    public VRRig_test vRRig_Test;
    // public SkinnedMeshRenderer skinnedMeshRenderer_gun;
    public InputActionReference vrInputRef;
    public InputActionReference vrInputRef2;
    public bool vrInput = false;
    [SerializeField]private bool pressed = false;
    public GameObject gunCrosshair;
    public LineRenderer laserLine;
    public Animator animator;
    public bool canSwitch = true;
    private AudioSource audioSource;
    public int maxBullet = 20;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SwitchGunMode();
    }

    // Update is called once per frame
    void Update()
    {
        // rightSelect = rightSelectReference.action.IsPressed();
        // leftSelect = leftSelectReference.action.IsPressed();
        if (vrInputRef.action.IsPressed() || vrInputRef2.action.IsPressed())
            vrInput = true;
        else
            vrInput = false;


        if (Input.GetKeyDown("g"))
            vrInput = true;


        if (canSwitch)
        {
            if (vrInput && !pressed)
            {
                if (!pressed)
                SwitchGunMode();
                pressed = true;
            }
            if (!vrInput)
            {
                pressed = false;
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
        if (isLeft)
            vRRig_Test.leftHand.attackMode = !gunMode;
        else
            vRRig_Test.rightHand.attackMode = !gunMode;
        animator.SetBool("gunMode", gunMode);
        audioSource.Play();
    }
}
