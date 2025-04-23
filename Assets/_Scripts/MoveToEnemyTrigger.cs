using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEnemyTrigger : MonoBehaviour
{
    public bool moveToEnemy = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        moveToEnemy = true;
    }
}
