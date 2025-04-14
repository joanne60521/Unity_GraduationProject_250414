using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGameControl : MonoBehaviour
{
    public InputActionReference leftSelectValueReference;
    public ResetCamera resetCamera;
    public float leftSelectValue;
    public bool startGame = false;
    public Arduino arduino;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftSelectValue = leftSelectValueReference.action.ReadValue<float>();
        
        // Start game
        if (Input.GetKeyDown("space") && !startGame)
        {
            startGame = true;
        }
        
        if (Input.GetKeyDown("r"))
        {
            arduino.sp3.Close();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown("c") || leftSelectValue == 1f)
        {
            resetCamera.ResetMainCamPos();
        }
    }
}
