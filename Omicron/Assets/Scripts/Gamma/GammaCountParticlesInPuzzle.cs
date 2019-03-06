using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaCountParticlesInPuzzle : MonoBehaviour
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
        // Reset the hot and cold particles in puzzle counter
        _gammaManager.HotParticlesInPuzzle = 0;
        _gammaManager.ColdParticlesInPuzzle = 0;

        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        GammaParticle[] particles = activePuzzle.GetComponentsInChildren<GammaParticle>();
        
        foreach (GammaParticle particle in particles)
        {
            //Debug.Log(particle.gameObject + "'s temperature state is " + particle.IsHot);
            if (particle.IsHot)
            {
                _gammaManager.HotParticlesInPuzzle++;
            }
            else
            {
                _gammaManager.ColdParticlesInPuzzle++;
            }
        }
    }
}
