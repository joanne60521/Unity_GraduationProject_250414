using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Playables;

public class WinOrLose : MonoBehaviour
{
    public Target target;
    public Target target1;
    public PlayableDirector winTimeline;
    public PlayableDirector loseTimeline;
    public CountdownTimer countdownTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.died && target1.died)
        {
            winTimeline.Play();
            countdownTimer.enabled = false;
        }
    }
}
