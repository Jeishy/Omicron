using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaEventHandler : MonoBehaviour
{
    private AlphaLevelManager alphaLevelManager;

    private void Start()
    {
        alphaLevelManager = GetComponent<AlphaLevelManager>();
    }

    private void Update()
    {
        // Runs every frame to check if ball is dropped
        alphaLevelManager.BallDropped();
        // Runs input handling function every fram
        Input();
    }

    private void Input()
    {
        // If Oculus Go Remote's trigger is squeezed
        // shot the ball
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            alphaLevelManager.BallShot();
        }
    }

}
