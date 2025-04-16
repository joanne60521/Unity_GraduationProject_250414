using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindChild : MonoBehaviour
{
    public int childCount;
    public GameObject parentGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            GetChildCount();
        }
    }

    void GetChildCount()
    {
        childCount = parentGameObject.transform.childCount;
        for (int i = 0; i < parentGameObject.transform.childCount; i++)
        {
            Transform child = parentGameObject.transform.GetChild(i);
            Debug.Log("Child: " + child.name);
        }
    }
}
