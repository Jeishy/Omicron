using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaSetHotAndColdParticlesLists : MonoBehaviour
{
    private GammaLevelManager _gammaManager;

    private void OnEnable()
    {
        Setup();
        _gammaManager.OnPuzzleStart += GetParticles;
    }

    private void OnDisable()
    {
        _gammaManager.OnPuzzleStart -= GetParticles;
    }

    private void Setup()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
    }

    private void GetParticles()
    {
        // If either of the lists are already populated, clear both of them
        if(_gammaManager.HotParticlesInPuzzle.Count > 0 || _gammaManager.ColdParticlesInPuzzle.Count > 0)
        {
            _gammaManager.HotParticlesInPuzzle.Clear();
            _gammaManager.ColdParticlesInPuzzle.Clear();
        }
        else
        {
            // Populate the hot and cold particles lists
            _gammaManager.SetHotParticlesInPuzzle();
            _gammaManager.SetColdParticlesInPuzzle();
        }
    }
}
