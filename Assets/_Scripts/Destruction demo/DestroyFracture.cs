using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyFracture : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(IsTriggerOn(7f));
    }
    void Update()
    {
        if (Input.GetKeyDown("p")){
            StartCoroutine(IsTriggerOn(0f));
        }
    }

    public IEnumerator IsTriggerOn(float delay)
    {
        yield return new WaitForSeconds(delay);

        MeshCollider col = gameObject.GetComponent<MeshCollider>();
            if (col != null)
            {
                col.isTrigger = true;
                Debug.Log($"Set isTrigger = true on: {gameObject.name}");
            }
        
        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.down * 2f);
        }
    }
}