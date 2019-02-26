using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaClearParticlesList : MonoBehaviour
{
    private GammaLevelManager gammaManager;

    private void OnEnable()
    {
        gammaManager = GetComponent<GammaLevelManager>();
        GameManager.Instance.OnNextPuzzle += ClearParticleslList;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnNextPuzzle -= ClearParticleslList;
    }

    private void ClearParticleslList()
    {
        // Clear the particles from the list before adding new particles to the list
        gammaManager.ParticlesInPuzzle.Clear();
    }
}
