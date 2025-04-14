using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnTrigger_Demo : MonoBehaviour
{
    public string colTag = "////////";
    public TextMeshProUGUI mytext;
    public VRRig_test VRRig_test;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(colTag))
        {
            mytext.text = "> attack mode";
            if (colTag == "leftCollider")
            {
                VRRig_test.leftHand.attackMode = true;
            }
            if (colTag == "rightCollider")
            {
                VRRig_test.rightHand.attackMode = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == colTag)
        {
            if (!VRRig_test.leftHand.attacking)
            {
                mytext.text = "> normal mode";
                VRRig_test.leftHand.attackMode = false;
            }
            if (!VRRig_test.rightHand.attacking)
            {
                mytext.text = "> normal mode";
                VRRig_test.rightHand.attackMode = false;
            }
        }
    }
}
