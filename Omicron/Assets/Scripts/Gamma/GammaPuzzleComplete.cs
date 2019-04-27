using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaPuzzleComplete : MonoBehaviour
{
    private GammaLevelManager _gammaManager;

    private void OnEnable() 
    {
        Setup();
        _gammaManager.OnPuzzleComplete += PuzzleComplete;
    }

    private void OnDisable() 
    {
        _gammaManager.OnPuzzleComplete -= PuzzleComplete;
    }

    private void Setup()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
    }

    private void PuzzleComplete()
    {
        // Go to the next puzzle
        GameManager.Instance.NextPuzzle();
        // Set the new particles list
        SetAllParticleList();
    }

    private void SetAllParticleList()
    {
        // Clear list if anything is in it
        _gammaManager.AllParticlesInPuzzle.Clear();
        // Find the current active puzzle
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        // Get an array of the particles in the puzzle
        GammaParticle[] particles = activePuzzle.GetComponentsInChildren<GammaParticle>();
        foreach (GammaParticle particle in particles)
        {
            // Add them to the ParticlesInPuzzle list in the GammaLevelManager class
            _gammaManager.AllParticlesInPuzzle.Add(particle);
        }
    }
}
