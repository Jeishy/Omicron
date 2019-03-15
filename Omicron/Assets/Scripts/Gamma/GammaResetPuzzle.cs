using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaResetPuzzle : MonoBehaviour
{
    private GammaLevelManager _gammaManager;
    
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
    }

    private void Reset()
    {
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
        foreach (GammaParticle particles in _gammaManager.AllParticlesInPuzzle)
        {
            particles.GetComponent<GammaResetParticles>().ResetParticle();
        }
    }
}
