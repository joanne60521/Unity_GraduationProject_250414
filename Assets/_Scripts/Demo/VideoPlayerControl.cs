using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.UI;

public class VideoPlayerControl : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public VideoClip CockpitView;
    public VideoClip RobotView;
    public VideoClip AttackMode;
    public VideoClip AttackPunch;
    public VideoClip GunAim;
    public VideoClip GunFire;

    public RawImage rawImage;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = CockpitView;
    }

    void Update()
    {
        if (videoPlayer.clip == CockpitView && Input.GetKeyDown(KeyCode.A))
        {
            videoPlayer.Play();
            Invoke("ToRobotView", 1);
        }
        else if (videoPlayer.clip == RobotView && Input.GetKeyDown(KeyCode.A))
        {
            videoPlayer.Play();
            Invoke("ToAttackMode", 1);

        }
        else if (videoPlayer.clip == AttackMode && Input.GetKeyDown(KeyCode.A))
        {
            videoPlayer.Play();
            Invoke("ToAttackPunch", 1);
        }
        else if (videoPlayer.clip == AttackPunch && Input.GetKeyDown(KeyCode.A))
        {
            videoPlayer.Play();
            Invoke("ToGunAim", 1);
        }
        else if (videoPlayer.clip == GunAim && Input.GetKeyDown(KeyCode.A))
        {
            videoPlayer.Play();
            Invoke("ToGunFire", 1);
        }
        else if (videoPlayer.clip == GunFire && Input.GetKeyDown(KeyCode.A))
        {
            // videoPlayer.Play();
            // Invoke("ToCockpitView", 5);
            Destroy(rawImage);
        }
    }

    public void ToRobotView()
    {
        videoPlayer.clip = RobotView;
        Debug.Log("cockpit view change to robot view");

    }

    public void ToAttackMode()
    {
        videoPlayer.clip = AttackMode;
        Debug.Log("cockpit view change to AttackMode");

    }

    public void ToAttackPunch()
    {
        videoPlayer.clip = AttackPunch;
        Debug.Log("cockpit view change to AttackPunch");

    }

    public void ToGunAim()
    {
        videoPlayer.clip = GunAim;
        Debug.Log("cockpit view change to GunAim");

    }

    public void ToGunFire()
    {
        videoPlayer.clip = GunFire;
        Debug.Log("cockpit view change to GunFire");

    }

    public void ToCockpitView()
    {
        videoPlayer.clip = CockpitView;
        Debug.Log("cockpit view change to CockpitView");

    }
}
