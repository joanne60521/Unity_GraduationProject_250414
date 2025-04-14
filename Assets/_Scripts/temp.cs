using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    public bool collide;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        collide = true;
    }

    void OnCollisionExit(Collision collision)
    {
        collide = false;
    }
}
