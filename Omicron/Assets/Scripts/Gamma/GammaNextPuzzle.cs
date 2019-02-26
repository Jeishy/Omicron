using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaNextPuzzle : MonoBehaviour
{
    [SerializeField] private float waitTimeTillPuzzleCompletion;    // The time taken till the puzzle is completed

    private GammaLevelManager gammaManager;
    private int hotParticlesInCorrectChamber;                       // The number of hot particles in the right chamber
    private int coldParticlesInCorrectChamber;                      // The number of cold particles in the right chamber

    private void Start()
    {
        gammaManager = GetComponent<GammaLevelManager>();
    }

    private void Update()
    {
        // If the number of chambers in the puzzle are more than one then check if particles are in correct chambers
        if (CheckNumberOfChambers() > 1)
        {
            // If the number of particles in the correct chambers are at the correct amount, go to the next puzzle
            if (gammaManager.HotParticlesInPuzzle == CheckNumberOfHotParticles() && gammaManager.ColdParticlesInPuzzle == CheckNumberOfColdParticles())
            {
                StartCoroutine(WaitForNextPuzzle());
                // reset the counters for the next puzzle
                hotParticlesInCorrectChamber = 0;
                coldParticlesInCorrectChamber = 0;
            }
        }
    }

    private int CheckNumberOfChambers()
    {
        // Find the active puzzle
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        // Get the number of hot chambers
        int numOfHotChambers = activePuzzle.GetComponentsInChildren<GammaHotChamber>().Length;
        // Get the number of cold chambers
        int numOfColdChambers = activePuzzle.GetComponentsInChildren<GammaColdChamber>().Length;
        // Get total number of chambers in puzzle
        int numOfChambers = numOfHotChambers + numOfColdChambers;
        return numOfChambers;
    }

    private int CheckNumberOfHotParticles()
    {
        int hotParticlesInCorrectChamber = 0;
        // Get the currenct active puzzle
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        // Find the number of hot chambers in the puzzle
        GammaHotChamber[] hotChambers = activePuzzle.GetComponentsInChildren<GammaHotChamber>();
        foreach(GammaHotChamber hotChamber in hotChambers)
        {
            // For every hot particle in the right chamber, add one to the total of hot particles in the right chambers
            hotParticlesInCorrectChamber += hotChamber.HotParticlesInChamber;
        }
        return hotParticlesInCorrectChamber;
    }

    private int CheckNumberOfColdParticles()
    {
        int coldParticlesInCorrectChamber = 0;
        // Get the currenct active puzzle
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        // Find the number of cold chambers in the puzzle
        GammaColdChamber[] coldChambers = activePuzzle.GetComponentsInChildren<GammaColdChamber>();
        foreach (GammaColdChamber coldChamber in coldChambers)
        {
            // For every cold particle in the right chamber, add one to the total of cold particles in the right chambers
            coldParticlesInCorrectChamber += coldChamber.ColdParticlesInChamber;
        }
        return coldParticlesInCorrectChamber;
    }

    private IEnumerator WaitForNextPuzzle()
    {
        // Wait specified amount of seconds before checking if all particles are in the right chambers again
        yield return new WaitForSeconds(waitTimeTillPuzzleCompletion);
        if (gammaManager.HotParticlesInPuzzle == CheckNumberOfHotParticles() && gammaManager.ColdParticlesInPuzzle == CheckNumberOfColdParticles())
            GameManager.Instance.NextPuzzle();
    }
}
