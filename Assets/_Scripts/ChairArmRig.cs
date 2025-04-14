using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public class ChairArmRig_Map
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}



public class ChairArmRig : MonoBehaviour
{
    public ChairArmRig_Map leftHand;
    public ChairArmRig_Map rightHand;

    void Start()
    {
        
    }

    void Update()
    {
        leftHand.Map();
        rightHand.Map();
    }
}
