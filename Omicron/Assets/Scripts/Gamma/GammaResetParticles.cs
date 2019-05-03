using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaResetParticles : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    private GammaParticle _gammaParticle;

    private void Start()
    {
        _gammaParticle = GetComponent<GammaParticle>();
    }

    public void ResetParticle()
    {
        // Resets position of all magnets already in the puzzle (Not placeable magnets)
        transform.position = _spawnPoint.position;                                                   // Set there positions to their respective spawn point positions
        _gammaParticle.Temperature = _gammaParticle.OriginalTemperature;
        _gammaParticle.SetupTemperatureState(_gammaParticle.Temperature);
        _gammaParticle.Setup();
    }
}
