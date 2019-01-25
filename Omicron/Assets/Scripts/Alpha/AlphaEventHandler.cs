using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaEventHandler : MonoBehaviour
{
    private AlphaLevelManager alphaLevelManager;

    // Start is called before the first frame upda te
    private void Start()
    {
        alphaLevelManager = GetComponent<AlphaLevelManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        alphaLevelManager.BallDropped();
        Input();
    }

    private void Input()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            alphaLevelManager.BallShot();
        }
    }

}
