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

    private void ParticleStateChanged(GammaParticle particle)
    {
        // Add particle to the relevant list after change
        if (particle.IsHot)
        {
            _gammaManager.ColdParticlesInPuzzle.Remove(particle);
            _gammaManager.HotParticlesInPuzzle.Add(particle);
        }
        else
        {
            _gammaManager.ColdParticlesInPuzzle.Add(particle);
            _gammaManager.HotParticlesInPuzzle.Remove(particle);

        }
    }
}
