using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    private float nextTimeTo = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTimeTo)
        {
            nextTimeTo = Time.time + 3;

            GameObject[] fractureObjects = GameObject.FindGameObjectsWithTag("Fractures");

            foreach (GameObject obj in fractureObjects)
            {
                if (obj.GetComponent<DestroyFracture>() == null)
                {
                    obj.AddComponent<DestroyFracture>();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Destroy: " + other.gameObject.name);
        Destroy(other.gameObject);      
    }
}
