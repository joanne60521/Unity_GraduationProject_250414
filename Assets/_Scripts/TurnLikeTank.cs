using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLikeTank : MonoBehaviour
{
    [SerializeField] float turnValue = 20;
    public Transform leftFront;
    public Transform leftBack;
    public Transform rightFront;
    public Transform rightBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("u"))  // 左腳前踩
        {
            if (Input.GetKey("i"))
            {
                Debug.Log("前進");
            }else if (Input.GetKey("k"))
            {
                Debug.Log("左前右後");
                transform.RotateAround(transform.position, Vector3.up, turnValue * Time.deltaTime);  // 左輪向前右輪向後，機甲中心為原點右轉
            }else
            {
                Debug.Log("左前");
                // transform.eulerAngles += new Vector3(turnValue * Time.deltaTime * 1, 0, 0);  // 左輪向前右輪不動，右輪後方為原點右轉
                transform.RotateAround(rightBack.position, Vector3.up, turnValue * Time.deltaTime);
            }
        }
    }
}
