using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaResetPuzzle : MonoBehaviour
{
    private GammaLevelManager _gammaManager;
    private GammaNextPuzzle _gammaNextPuzzle;

    private void OnEnable()
    {
        Setup();
        _gammaManager.OnPuzzleRestart += Reset;
    }

    private void OnDisable()
    {
        _gammaManager.OnPuzzleRestart -= Reset;
    }

    private void Setup()
    {
        _gammaManager = GetComponent<GammaLevelManager>();
        _gammaNextPuzzle = GetComponent<GammaNextPuzzle>();
    }

    private void Reset()
    {
        //Debug.Log("Hot particles: " + _gammaManager.HotParticlesInPuzzle + " || Cold particles: " + _gammaManager.ColdParticlesInPuzzle);
        // Reset hot and col particles in correct chamber counters
        _gammaManager.HotParticlesInPuzzle = 0;
        _gammaManager.ColdParticlesInPuzzle = 0;
        _gammaNextPuzzle.HotParticlesInCorrectChamber = 0;
        _gammaNextPuzzle.ColdParticlesInCorrectChamber = 0;

        // Iterate through all hot and cold chambers and set their counters to 0
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        GammaHotChamber[] hotChambers = activePuzzle.GetComponentsInChildren<GammaHotChamber>();
        foreach (GammaHotChamber hotChamber in hotChambers)
        {
            hotChamber.HotParticlesInChamber = 0;
        }

        GammaColdChamber[] coldChambers = activePuzzle.GetComponentsInChildren<GammaColdChamber>();
        foreach (GammaColdChamber coldChamber in coldChambers)
        {
            coldChamber.ColdParticlesInChamber = 0;
        }

        // Find all active particles and reset them
        foreach (GammaParticle particles in _gammaManager.ParticlesInPuzzle)
        {
            particles.GetComponent<GammaResetParticles>().ResetParticle();
        }
    }
}
