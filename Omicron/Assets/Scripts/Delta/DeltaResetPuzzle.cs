using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaResetPuzzle : MonoBehaviour
{
    private DeltaLevelManager _deltaManager;

    private void OnEnable() 
    {
        Setup();
        _deltaManager.OnPuzzleReset += ResetPuzzle;
    }

    private void OnDisable() 
    {
        _deltaManager.OnPuzzleReset -= ResetPuzzle;
    }

    private void Setup()
    {
        _deltaManager = GetComponent<DeltaLevelManager>();
    }

    private void ResetPuzzle()
    {
        
        // Destroy all photons in the puzzle, if there are any
        GameObject[] photonsInPuzzle = GameObject.FindGameObjectsWithTag("Photon");
        if (photonsInPuzzle.Length > 0)
        {
            foreach (GameObject photon in photonsInPuzzle)
            {
                // Play particle effect in central photon particle effect script
                Destroy(photon);
            }
        }
        // Reset number of photons shot
        _deltaManager.PhotonsShot = 0;
        // Reset nu,ber of photons in goal
        _deltaManager.PhotonsInGoal = 0;
        // Set is photon attached bool to false
        _deltaManager.IsPhotonAttached = false;
        // Attach a new photon
        _deltaManager.PhotonAttach();
        // Get all goals in the puzzle
        GameObject[] goals = GameObject.FindGameObjectsWithTag("DeltaGoal");
        // Reset the emission colour on all goal gameobjects and has photon hit goal booleans
        foreach (GameObject goal in goals)
        {
            // Resetting colour
            Color origColour = goal.GetComponent<DeltaGoal>().OriginalColour;
            goal.GetComponent<MeshRenderer>().material.SetColor("_EmissionColour", origColour);
            // Resetting has photon hit goal bool
            DeltaGoal deltaGoal = goal.GetComponent<DeltaGoal>();
            deltaGoal.HasPhotonHit = false;
        }
    }
}
