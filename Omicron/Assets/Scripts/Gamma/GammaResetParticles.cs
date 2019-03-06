using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaResetParticles : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private GammaLevelManager gammaManager;
    private GammaParticle gammaParticle;

    private void OnEnable()
    {
        Setup();
        gammaManager.OnPuzzleReset += ResetParticles;
    }

    private void OnDisable()
    {
        gammaManager.OnPuzzleReset -= ResetParticles;
    }

    private void Setup()
    {
        gammaManager = GameObject.Find("GammaLevelManager").GetComponent<GammaLevelManager>();
        gammaParticle = GetComponent<GammaParticle>();
    }

    private void ResetParticles()
    {
        float speed = gammaParticle.InitialSpeed;
        MeshRenderer particleMeshRenderer = GetComponent<MeshRenderer>();
        // Resets position of all magnets already in the puzzle (Not placeable magnets)
        transform.position = spawnPoint.position;                                                   // Set there positions to their respective spawn point positions
        GetComponent<Rigidbody>().velocity = gammaParticle.ParticleDirection * speed;                             // Set their velocities to their original velocities
        particleMeshRenderer.material.color = gammaParticle.OriginalColour;
        gammaParticle.Temperature = gammaParticle.OriginalTemperature;
        gammaParticle.IsParticleStateChanged = false;
        gammaParticle.CheckTemperatureState(gammaParticle.Temperature);
        gammaParticle.ChangeTemperature(gammaParticle.CanTemperatureIncrease);
        gammaParticle.TemperatureTime = 0;
    }
}
