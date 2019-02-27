using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaParticleStateChanged : MonoBehaviour
{
    private GammaLevelManager gammaManager;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnParticleStateChange += ParticleStateChanged;
    }

    private void OnDisable()
    {
        gammaManager.OnParticleStateChange -= ParticleStateChanged;
    }

    private void Setup()
    {
        gammaManager = GetComponent<GammaLevelManager>();
    }

    private void ParticleStateChanged(bool isHot)
    {
        // Increase or decrease the number of hot or cold particles in the puzzle according to the new state of the particle
        if (isHot)
        {
            gammaManager.HotParticlesInPuzzle++;
            gammaManager.ColdParticlesInPuzzle--;
            Debug.Log("New hot particles amount: " + gammaManager.HotParticlesInPuzzle + " || New cold particles amount: " + gammaManager.ColdParticlesInPuzzle);
        }
        else
        {
            gammaManager.HotParticlesInPuzzle--;
            gammaManager.ColdParticlesInPuzzle++;
            Debug.Log("New cold particles amount: " + gammaManager.HotParticlesInPuzzle + " || New hot particles amount: " + gammaManager.ColdParticlesInPuzzle);
        }
    }
}
