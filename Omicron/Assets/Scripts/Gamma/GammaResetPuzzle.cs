using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaResetPuzzle : MonoBehaviour
{
    private GammaLevelManager gammaManager;
    private GammaNextPuzzle gammaNextPuzzle;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnPuzzleReset += ResetPuzzle;
    }

    private void OnDisable()
    {
        gammaManager.OnPuzzleReset -= ResetPuzzle;
    }

    private void Setup()
    {
        gammaManager = GetComponent<GammaLevelManager>();
        gammaNextPuzzle = GetComponent<GammaNextPuzzle>();
    }

    private void ResetPuzzle()
    {
        gammaNextPuzzle.HotParticlesInCorrectChamber = 0;
        gammaNextPuzzle.ColdParticlesInCorrectChamber = 0;
    }
}
