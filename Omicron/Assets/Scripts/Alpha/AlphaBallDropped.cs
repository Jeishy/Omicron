﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBallDropped : MonoBehaviour
{
    private AlphaLevelManager alphaLevelManager;
    [SerializeField] private Transform ball;
    [SerializeField] private float maxBallDistance; // The max distance that the ball can go away from the player before being considered "dropped"
    private Transform oculusGoTrans;

    private void OnEnable()
    {
        Setup();
        alphaLevelManager.OnBallDropped += Dropped;
    }

    private void OnDisable()
    {
        alphaLevelManager.OnBallDropped -= Dropped;
    }

    private void Setup()
    {
        alphaLevelManager = GetComponent<AlphaLevelManager>();
        oculusGoTrans = GameObject.FindGameObjectWithTag("OculusRemote").transform;  
    }

    private void Dropped()
    {
        // Run some shader code and wait till finished
        // Spawn ball at position of remote
        Vector3 ballPos = ball.position;                                        // Cache balls position
        Vector3 playerPos = oculusGoTrans.position;                             // Cache OVRCameraRig's position
        float distanceFromPlayerToBall = Vector3.Distance(playerPos, ballPos);  // Find distance between the two

        // If the ball goes beyond set distance from the player then
        // reset the balls position
        if (distanceFromPlayerToBall > maxBallDistance)
        {
            alphaLevelManager.ResetBallPosition();
        }
    }
}
