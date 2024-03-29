﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStandardGravity : MonoBehaviour {
    [SerializeField] private float _gravity;
    private float Gravity { get{ return _gravity; } }

    private AlphaLevelManager alphaLevelManager;

    private void Start()
    {
        alphaLevelManager = GetComponent<AlphaLevelManager>();
    }

    // Update is called once per frame
    private void Update ()
    {
        // If gravity hasnt been changed, implement standard gravity
        if (alphaLevelManager.IsGravityChanged != true)
            StandardGravity();
	}

    private void StandardGravity()
    {
        // Set gravity to specified lateral gravity 
        Physics.gravity = new Vector3(0, Gravity, 0);
    }
}
