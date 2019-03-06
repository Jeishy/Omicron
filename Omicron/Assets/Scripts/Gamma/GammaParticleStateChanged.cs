using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaParticleStateChanged : MonoBehaviour
{
    private GammaLevelManager _gammaManager;

    private void OnEnable()
    {
        Setup();
        _gammaManager.OnParticleStateChange += ParticleStateChanged;
    }

    private void OnDisable()
    {
        _gammaManager.OnParticleStateChange -= ParticleStateChanged;
    }

    private void Setup()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
    }

    private void ParticleStateChanged(bool isHot)
    {
        // Increase or decrease the number of hot or cold particles in the puzzle according to the new state of the particle
        if (isHot)
        {
            _gammaManager.HotParticlesInPuzzle++;
            _gammaManager.ColdParticlesInPuzzle--;
        }
        else
        {
            _gammaManager.HotParticlesInPuzzle--;
            _gammaManager.ColdParticlesInPuzzle++;
        }
    }
}
