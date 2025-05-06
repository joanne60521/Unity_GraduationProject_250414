using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Playables;

public class Jet_shooting : MonoBehaviour
{
    // Start is called before the first frame update    
    public GameObject theBullet;
    public Transform barrelEnd;
    public VisualEffect Muzzleflash;

    public int bulletSpeed;
    public float despawnTime = 3.0f;
    //public AudioClip Gun_noises;
    //public AudioClip Hit_sound;

    public bool shootAble = true;
    public float waitBeforeNextShot = 0.25f;

    private void Update ()
    {
        if (Input.GetKey (KeyCode.Space))
        {
            if (shootAble)
            {
                shootAble = false;
                Shoot ();
                StartCoroutine (ShootingYield ());
            }
        }
    }

    IEnumerator ShootingYield ()
    {
        yield return new WaitForSeconds (waitBeforeNextShot);
        shootAble = true;
    }
    void Shoot ()
    {
        AudioSource sound = gameObject.GetComponent<AudioSource>();
        sound.Play();
        var bullet = Instantiate (theBullet, barrelEnd.position, barrelEnd.rotation);
        bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * bulletSpeed;
    
        Muzzleflash.Play();
        Destroy (bullet, despawnTime);
        
    }

    public void TriggerShoot()
    {
        if (shootAble)
            {
                shootAble = false;
                Shoot ();
                StartCoroutine (ShootingYield ());
            }
    }

}
