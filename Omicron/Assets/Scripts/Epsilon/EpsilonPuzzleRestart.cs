using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonPuzzleRestart : MonoBehaviour
{
    private EpsilonLevelManager _epsilonManager;
    private EpsilonCheckPuzzleComplete _epsilonCheckPuzzle;

    private void OnEnable() 
    {
        Setup();
        _epsilonManager.OnPuzzleRestart += PuzzleRestart;
    }

    private void OnDisable() 
    {
        _epsilonManager.OnPuzzleRestart -= PuzzleRestart;
    }

    private void Setup()
    {
        _epsilonManager = GetComponent<EpsilonLevelManager>();
        _epsilonCheckPuzzle = GetComponent<EpsilonCheckPuzzleComplete>();
    }

    private void PuzzleRestart()
    {
        GameObject[] nuclei = GameObject.FindGameObjectsWithTag("Nucleus");
        // If puzzle hasnt been completed already, restart puzzle
        if (!_epsilonCheckPuzzle.IsPuzzleCompleted)
        {
            // Delete all quarks and baryons in nuclei in puzzle
            foreach (GameObject nucleus in nuclei)
            {
                EpsilonNucleus epsilonNucleus = nucleus.GetComponent<EpsilonNucleus>();
                // Delete all quarks and baryons in a nucleus
                epsilonNucleus.DestroyParticlesInNucleus(epsilonNucleus.EpsilonQuarksInNucleus);
                epsilonNucleus.DestroyParticlesInNucleus(epsilonNucleus.EpsilonBaryonsInNucleus);
                // Clear both lists
                epsilonNucleus.EpsilonBaryonsInNucleus.Clear();
                epsilonNucleus.EpsilonQuarksInNucleus.Clear();

                // Reset is particle created bool to false it is true
                if (epsilonNucleus.IsParticleCreated)
                    epsilonNucleus.IsParticleCreated = false;
            }

            // Set quarks and baryons used to 0
            _epsilonManager.NumQuarksUsed = 0;
            _epsilonManager.NumBaryonsUsed = 0;
        }
    }
}
