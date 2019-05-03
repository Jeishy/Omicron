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
            if (nuclei.Length > 0)
            {
                // Delete all quarks ons in nuclei in puzzle
                foreach (GameObject nucleus in nuclei)
                {
                    EpsilonNucleus epsilonNucleus = nucleus.GetComponent<EpsilonNucleus>();
                    EpsilonAtomNucleus epsilonAtomNucleus = nucleus.GetComponent<EpsilonAtomNucleus>();

                    if (epsilonNucleus != null)
                    {
                        // Delete all quarks in a nucleus
                        if (epsilonNucleus.EpsilonQuarksInNucleus.Count > 0)
                            epsilonNucleus.DestroyParticlesInNucleus(epsilonNucleus.EpsilonQuarksInNucleus);
                        if (epsilonNucleus.EpsilonBaryonsInNucleus.Count > 0)
                            epsilonNucleus.DestroyParticlesInNucleus(epsilonAtomNucleus.EpsilonBaryonsInNucleus);

                        // Clear list
                        epsilonNucleus.EpsilonQuarksInNucleus.Clear();
                        epsilonNucleus.EpsilonBaryonsInNucleus.Clear();

                        // Reset is particle created bool to false it is true
                        if (epsilonNucleus.IsParticleCreated)
                            epsilonNucleus.IsParticleCreated = false;
                    }
                    else if(epsilonAtomNucleus != null)
                    {
                        // Delete all quarks in a nucleus
                        if (epsilonAtomNucleus.EpsilonQuarksInNucleus.Count > 0)
                            epsilonAtomNucleus.DestroyParticlesInNucleus(epsilonAtomNucleus.EpsilonQuarksInNucleus);
                        if (epsilonAtomNucleus.EpsilonBaryonsInNucleus.Count > 0)
                            epsilonAtomNucleus.DestroyParticlesInNucleus(epsilonAtomNucleus.EpsilonBaryonsInNucleus);

                        // Clear list
                        epsilonAtomNucleus.EpsilonQuarksInNucleus.Clear();
                        epsilonAtomNucleus.EpsilonBaryonsInNucleus.Clear();

                        // Reset is particle created bool to false it is true
                        if (epsilonAtomNucleus.IsParticleCreated)
                            epsilonAtomNucleus.IsParticleCreated = false;
                    }
                }
            }
            // Set quarks and baryons used to 0
            _epsilonManager.NumQuarksUsed = 0;
            _epsilonManager.NumBaryonsUsed = 0;
        }
    }
}
