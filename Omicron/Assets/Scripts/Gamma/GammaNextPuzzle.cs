﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GammaNextPuzzle : MonoBehaviour
{
    [HideInInspector] public int HotParticlesInCorrectChamber;                          // The number of hot particles in the right chamber
    [HideInInspector] public int ColdParticlesInCorrectChamber;                         // The number of cold particles in the right chamber

    [SerializeField] private float _waitTimeTillPuzzleCompletion;                        // The time taken till the puzzle is completed
    //[SerializeField] private Text _debugText;

    private GammaLevelManager _gammaManager;

    private void Start()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
    }

    private void Update()
    {
        CheckIfParticlesInCorrectChamber();
        if (_gammaManager.IsPuzzleCompleted)
        {
            StartCoroutine(NextPuzzle());
            Debug.Log("Going to next puzzle");
        }
    }

    private void CheckIfParticlesInCorrectChamber()
    {
        // If all the Is particle in chamber booleans are set to true, set the Is puzzle completed boolean to true
        // Else break out of the loop and set it to false
        foreach (GammaParticle particle in _gammaManager.AllParticlesInPuzzle)
        {   
            if (particle.IsParticleInCorrectChamber)
            {
                _gammaManager.IsPuzzleCompleted = true;
            }
            else
            {
                _gammaManager.IsPuzzleCompleted = false;
                break;
            }
        }
    }

    private IEnumerator NextPuzzle()
    {
        yield return new WaitForSeconds(_waitTimeTillPuzzleCompletion);
        if (!_gammaManager.IsPuzzleCompleted) yield break;
        _gammaManager.IsPuzzleCompleted = false;
        GameManager.Instance.NextPuzzle();
    }
}
