using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GammaNextPuzzle : MonoBehaviour
{
    [HideInInspector] public int HotParticlesInCorrectChamber;                          // The number of hot particles in the right chamber
    [HideInInspector] public int ColdParticlesInCorrectChamber;                         // The number of cold particles in the right chamber

    [SerializeField] private float waitTimeTillPuzzleCompletion;                        // The time taken till the puzzle is completed
    //[SerializeField] private Text _debugText;

    private GammaLevelManager _gammaManager;

    private void Start()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
    }

    private void Update()
    {
        //Debug.Log("Hot particles in correct chamber: " + CheckNumberOfHotParticles());
        //Debug.Log("Cold particles in correct chamber: " + CheckNumberOfColdParticles());
        // If the number of particles in the correct chambers are at the correct amount, go to the next puzzle
    }

    private void CheckParticlesInCorrectChamber()
    {
        foreach (GammaParticle particle in _gammaManager.AllParticlesInPuzzle)
        {
            if (!particle.IsParticleInCorrectChamber)
            {
                _gammaManager.IsPuzzleCompleted = false;
                break;
            }
        }
    }

    /*private int CheckNumberOfHotParticles()
    {
        int hotParticles = 0;
        // Get the current active puzzle
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        // Find the number of hot chambers in the puzzle
        GammaHotChamber[] hotChambers = activePuzzle.GetComponentsInChildren<GammaHotChamber>();
        foreach(GammaHotChamber hotChamber in hotChambers)
        {
            // For every hot particle in the right chamber, add one to the total of hot particles in the right chambers
            hotParticles += hotChamber.HotParticlesInChamber;
        }
        return hotParticles;
    }

    private int CheckNumberOfColdParticles()
    {
        int coldParticles = 0;
        // Get the current active puzzle
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        // Find the number of cold chambers in the puzzle
        GammaColdChamber[] coldChambers = activePuzzle.GetComponentsInChildren<GammaColdChamber>();
        foreach (GammaColdChamber coldChamber in coldChambers)
        {
            // For every cold particle in the right chamber, add one to the total of cold particles in the right chambers
            coldParticles += coldChamber.ColdParticlesInChamber;
        }
        return coldParticles;
    }*/

    /*private IEnumerator WaitForNextPuzzle()
    {
        
    }*/
}
