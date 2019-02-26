using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaParticleChangedState : MonoBehaviour
{
    private GammaLevelManager gammaManager;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnParticleStateChange += Check
    }

    private void OnDisable()
    {
        
    }

    private void Setup()
    {
        gammaManager = GetComponent<GammaLevelManager>();
    }

    private void CheckParticleStates()
    {
        foreach(GammaParticle particle in gammaManager.ParticlesInPuzzle)
        {
            if (particle.IsHot)
            {

            }
            else if part
        }
    }
}
