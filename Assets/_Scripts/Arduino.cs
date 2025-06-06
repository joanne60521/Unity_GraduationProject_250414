using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.IO;
using UnityEngine.InputSystem;
using TMPro;
using System.Runtime.Remoting;

public class Arduino : MonoBehaviour
{
    public SerialPort sp3 = new SerialPort("COM3", 9600);

    public InputActionReference rightActivateValueReference;
    public InputActionReference leftActivateValueReference;
    private float rightActivateValue;
    private float leftActivateValue;
    private float nextTimeToFire = 0f;
    public float fireRate = 0.3f;
    private bool triggerPressed;
    private bool triggerPressed1;
    public CameraShakeWhenFire cameraShakeWhenFire;
    public GunFire gunFire_L;
    public GunFire gunFire_R;
    public int bulletCount_L = 0;
    public int bulletCount_R = 0;
    public TextMeshProUGUI BulletNum_L;
    public TextMeshProUGUI BulletNum_R;
    public TextMeshProUGUI MaxBulletNum_L;
    public TextMeshProUGUI MaxBulletNum_R;
    public SwitchMode_edit switchMode_L;
    public SwitchMode_edit switchMode_R;
    public VRRig_test vRRig_Test;
    public float brakeDelay = 0.2f;
    public bool canShoot = true;
    private float brakeWriteCD = 0.1f;
    public float nextTimeToWriteBrake;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            sp3.Open();
            Debug.Log("sp3 is opened");
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
        }
        MaxBulletNum_L.text = "/ " + switchMode_L.maxBullet.ToString();
        BulletNum_L.text = switchMode_L.maxBullet.ToString();
        MaxBulletNum_R.text = "/ " + switchMode_R.maxBullet.ToString();
        BulletNum_R.text = switchMode_R.maxBullet.ToString();

        nextTimeToWriteBrake = Time.deltaTime + brakeWriteCD;
    }

    // Update is called once per frame
    void Update()
    {
        // right hand shoot
        if(switchMode_R.gunMode && canShoot)   //if(sp3.IsOpen && turnOnLight.gunBool) 
        {
            rightActivateValue = rightActivateValueReference.action.ReadValue<float>();
            leftActivateValue = leftActivateValueReference.action.ReadValue<float>();

            if (rightActivateValue > 0.5 && Time.time >= nextTimeToFire && bulletCount_R < switchMode_R.maxBullet)
            {
                nextTimeToFire = Time.time + fireRate;
                if (!triggerPressed)
                {
                    if (sp3.IsOpen)
                    {
                        sp3.Write("1");  // shoot
                        Debug.Log("sp3.Write: 1");
                        // sp3.Write("2");
                    }
                    gunFire_R.Shoot();
                    bulletCount_R++;
                    BulletNum_R.text = (switchMode_R.maxBullet - bulletCount_R).ToString();
                    cameraShakeWhenFire.TriggerShake();
                    triggerPressed = true;
                }
            }
            if (rightActivateValue == 0)
            {
                triggerPressed = false;
            }
        }

        // left hand shoot
        if(switchMode_L.gunMode && canShoot)   //if(sp3.IsOpen && turnOnLight.gunBool) 
        {
            leftActivateValue = leftActivateValueReference.action.ReadValue<float>();

            if (leftActivateValue > 0.5 && Time.time >= nextTimeToFire && bulletCount_L < switchMode_L.maxBullet)
            {
                nextTimeToFire = Time.time + fireRate;
                if (!triggerPressed1)
                {
                    if (sp3.IsOpen)
                    {
                        sp3.Write("4");  // shoot
                        Debug.Log("sp3.Write: 4");
                    }
                    gunFire_L.Shoot();
                    bulletCount_L++;
                    BulletNum_L.text = (switchMode_L.maxBullet - bulletCount_L).ToString();
                    cameraShakeWhenFire.TriggerShake();
                    triggerPressed1 = true;
                }
            }
            if (rightActivateValue == 0)
            {
                triggerPressed1 = false;
            }
        }


        if (vRRig_Test.rightHand.brake && Time.time >= nextTimeToWriteBrake)
        {
            if (sp3.IsOpen)
            {
                sp3.Write("2");  // right hand punch
                Debug.Log("sp3.Write: 2");
            }
            nextTimeToWriteBrake = Time.time + brakeWriteCD;
        }
        if (vRRig_Test.leftHand.brake && Time.time >= nextTimeToWriteBrake)
        {
            if (sp3.IsOpen)
            {
                sp3.Write("3");  // left hand punch
                Debug.Log("sp3.Write: 3");
            }
            nextTimeToWriteBrake = Time.time + brakeWriteCD;
        }
        if (Input.GetKey("b"))
        {
            if (sp3.IsOpen)
            {
                vRRig_Test.rightHand.brake = false;
                vRRig_Test.leftHand.brake = false;
            }
        }


        if (Input.GetKeyDown("a") && Time.time >= nextTimeToFire && bulletCount_R < switchMode_R.maxBullet && switchMode_R.gunMode && canShoot)  // right gun
        {
            gunFire_R.Shoot();
            if (sp3.IsOpen)
                {
                    sp3.Write("1");  // shoot
                    Debug.Log("sp3.Write: 1");
                }
            bulletCount_R++;
            BulletNum_R.text = (switchMode_R.maxBullet - bulletCount_R).ToString();
            cameraShakeWhenFire.TriggerShake();

        }
        if (Input.GetKeyDown("z") && Time.time >= nextTimeToFire && bulletCount_L < switchMode_L.maxBullet && switchMode_L.gunMode && canShoot)  // left gun
        {
            gunFire_L.Shoot();
            if (sp3.IsOpen)
                {
                    sp3.Write("4");  // shoot
                    Debug.Log("sp3.Write: 4");
                }
            bulletCount_L++;
            BulletNum_L.text = (switchMode_L.maxBullet - bulletCount_L).ToString();
            cameraShakeWhenFire.TriggerShake();

        }
    }


    void OnDestroy()
    {
        if (sp3 != null && sp3.IsOpen)
        {
            sp3.Close();
        }
        
    }
    void OnApplicationQuit()
    {
        if (sp3 != null && sp3.IsOpen)
        {
            sp3.Close();
        }
    }
}

