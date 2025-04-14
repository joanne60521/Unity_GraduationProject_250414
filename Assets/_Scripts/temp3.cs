using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp3 : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("damage");
        Debug.Log("damage");
    }
}
