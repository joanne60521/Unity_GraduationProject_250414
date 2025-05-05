using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEnemyTrigger : MonoBehaviour
{
    public bool moveToEnemy = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: " + other.gameObject.name);
        moveToEnemy = true;
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit: " + other.gameObject.name);
        moveToEnemy = false;
    }
}
