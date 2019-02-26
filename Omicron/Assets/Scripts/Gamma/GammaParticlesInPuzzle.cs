using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaParticlesInPuzzle : MonoBehaviour
{
    private GammaLevelManager gammaManager;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnPuzzleStart += GetParticles;
    }

    private void OnDisable()
    {
        gammaManager.OnPuzzleStart -= GetParticles;
    }

    private void Setup()
    {
        gammaManager = GetComponent<GammaLevelManager>();
    }

    private void GetParticles()
    {
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        GammaParticle[] particles = activePuzzle.GetComponentsInChildren<GammaParticle>();
        
        foreach (GammaParticle particle in particles)
        {
            if (particle.IsHot)
            {
                gammaManager.HotParticlesInPuzzle++;
            }
            else
            {
                gammaManager.ColdParticlesInPuzzle++;
            }
        }
    }
}
