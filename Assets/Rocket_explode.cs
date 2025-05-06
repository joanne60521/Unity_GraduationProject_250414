using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Rocket_explode : MonoBehaviour
{
    public Transform explosionPrefab;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        //AudioSource sound = gameObject.GetComponent<AudioSource>();
        //sound.Play();
        Instantiate(explosionPrefab, position, rotation);
        Destroy(gameObject);
    }
}
