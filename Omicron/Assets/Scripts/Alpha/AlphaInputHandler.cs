using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaInputHandler : MonoBehaviour
{
    private AlphaLevelManager _alphaLevelManager;

    private void Start()
    {
        _alphaLevelManager = GetComponent<AlphaLevelManager>();
    }

    private void Update()
    {
        // Runs every frame to check if ball is dropped
        _alphaLevelManager.BallDropped();
        // Runs input handling function every frame
        Input();
    }

    private void Input()
    {
        // If Oculus Go Remote's trigger is squeezed
        // shot the ball
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            _alphaLevelManager.BallShot();
        }
    }
}
