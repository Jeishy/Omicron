using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaResetPuzzle : MonoBehaviour
{
    private GammaLevelManager _gammaManager;
    
    private void OnEnable()
    {
        Setup();
        _gammaManager.OnPuzzleRestart += Reset;
    }

    private void OnDisable()
    {
        _gammaManager.OnPuzzleRestart -= Reset;
    }

    private void Setup()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
    }

    private void Reset()
    {
        // Play puzzle failed sound
        AudioManager.Instance.Play("PuzzleFail");
        // Find all active particles and reset them
        foreach (GammaParticle particles in _gammaManager.AllParticlesInPuzzle)
        {
            particles.GetComponent<GammaResetParticles>().ResetParticle();
        }
    }
}
