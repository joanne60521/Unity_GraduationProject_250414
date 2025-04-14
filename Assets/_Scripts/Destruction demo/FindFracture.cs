using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFracture : MonoBehaviour
{
    // Update is called once per frame
    public GameObject fragments;
    void Update()
    {
        //GameObject.Find("Fragment");
        Debug.Log(GameObject.Find("Fragment"));
    }
}
