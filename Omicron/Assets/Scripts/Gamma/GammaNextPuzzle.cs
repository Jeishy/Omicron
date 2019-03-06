using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GammaNextPuzzle : MonoBehaviour
{
    [HideInInspector] public int HotParticlesInCorrectChamber;                       // The number of hot particles in the right chamber
    [HideInInspector] public int ColdParticlesInCorrectChamber;                      // The number of cold particles in the right chamber

    [SerializeField] private float waitTimeTillPuzzleCompletion;    // The time taken till the puzzle is completed
    //[SerializeField] private Text _debugText;

    private GammaLevelManager _gammaManager;

    private void Start()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
    }

    private void Update()
    {
        // If the number of chambers in the puzzle are more than one then check if particles are in correct chambers
        if (CheckNumberOfChambers() > 1)
        {
            Debug.Log("Hot particles in correct chamber: " + CheckNumberOfHotParticles());
            Debug.Log("Cold particles in correct chamber: " + CheckNumberOfColdParticles());
            // If the number of particles in the correct chambers are at the correct amount, go to the next puzzle
            if (_gammaManager.HotParticlesInPuzzle == CheckNumberOfHotParticles() && _gammaManager.ColdParticlesInPuzzle == CheckNumberOfColdParticles())
            {
                Debug.Log("Current hot particles: " + _gammaManager.HotParticlesInPuzzle);
                Debug.Log("Current cold  particles: " + _gammaManager.ColdParticlesInPuzzle);
                StartCoroutine(WaitForNextPuzzle());
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
        int hotParticles= 0;
        // Get the currenct active puzzle
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
        // Get the currenct active puzzle
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        // Find the number of cold chambers in the puzzle
        GammaColdChamber[] coldChambers = activePuzzle.GetComponentsInChildren<GammaColdChamber>();
        foreach (GammaColdChamber coldChamber in coldChambers)
        {
            // For every cold particle in the right chamber, add one to the total of cold particles in the right chambers
            coldParticles += coldChamber.ColdParticlesInChamber;
        }
        return coldParticles;
    }

    private IEnumerator WaitForNextPuzzle()
    {
        // Wait specified amount of seconds before checking if all particles are in the right chambers again
        yield return new WaitForSeconds(waitTimeTillPuzzleCompletion);
        if (_gammaManager.HotParticlesInPuzzle == CheckNumberOfHotParticles() && _gammaManager.ColdParticlesInPuzzle == CheckNumberOfColdParticles())
        {
            Debug.Log("Going to next puzzle");
            GameManager.Instance.NextPuzzle();
            // Trigger OnPuzzleStart event
            _gammaManager.PuzzleStart();
            // reset the counters for the next puzzle
            HotParticlesInCorrectChamber = 0;
            ColdParticlesInCorrectChamber = 0;
  
            Debug.Log(_gammaManager.ParticlesInPuzzle.Count);
            // Set IsParticleStateChanged to false before entering the next puzzle
            /*foreach (GammaParticle particle in _gammaManager.ParticlesInPuzzle)
            {
                particle.Setup();
            }*/
        }
    }
}
