using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaParticlesInPuzzleList : MonoBehaviour
{
    private GammaLevelManager gammaManager;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnPuzzleStart += GetParticlesList;
    }

    private void OnDisable()
    {
        gammaManager.OnPuzzleStart -= GetParticlesList;
    }

    private void Setup()
    {
        gammaManager = GetComponent<GammaLevelManager>();
    }

    private void GetParticlesList()
    {
        // Find the current active puzzle
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        // Get an array of the particles in the puzzle
        GammaParticle[] particles = activePuzzle.GetComponentsInChildren<GammaParticle>();
        foreach (GammaParticle particle in particles)
        {
            // Add them to the ParticlesInPuzzle list in the GammaLevelManager class
            gammaManager.ParticlesInPuzzle.Add(particle);
        }
    }
}
