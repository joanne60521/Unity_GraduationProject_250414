using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineControl : MonoBehaviour
{
    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            playableDirector.Play();
        }
    }
}
