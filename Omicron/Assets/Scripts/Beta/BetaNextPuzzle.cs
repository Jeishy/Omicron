﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaNextPuzzle : MonoBehaviour
{
    [SerializeField] private BetaLevelManager betaLevelManager;
    [SerializeField] private Text debugText;
    private GameManager gameManager;
    private BetaSetMaxMagnets betaSetMaxMagnets;
    private float triggerTime;
    [SerializeField] private float maxTriggerDuration;
    [SerializeField] private float minimumVel;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("SouthMagnet"))
        {
            triggerTime = Time.time;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("SouthMagnet"))
        {
            float colVel = col.GetComponent<Rigidbody>().velocity.magnitude;
            
            if (triggerTime + maxTriggerDuration < Time.time && colVel <= minimumVel)
            {
                // Note: Add UI here for countdown if so desired
                WaitForNextPuzzle();
            }
        }
    }

    private void WaitForNextPuzzle()
    {
        gameManager.NextPuzzle();
        betaSetMaxMagnets = GameObject.Find(gameManager.FindActivePuzzle().name).GetComponent<BetaSetMaxMagnets>();   
        betaLevelManager.MaxPlaceableMagnets = betaSetMaxMagnets.maxMagnets;
        betaLevelManager.Reset();
        betaLevelManager.MagnetAttach();
    }
}