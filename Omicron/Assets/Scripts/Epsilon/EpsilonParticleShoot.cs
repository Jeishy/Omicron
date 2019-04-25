using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonParticleShoot : MonoBehaviour
{
    [SerializeField] private float _shotSpeed;

    private EpsilonLevelManager _epsilonManager;

    private void OnEnable() 
    {
        Setup();
        _epsilonManager.OnParticleShoot += ParticleShoot;
    }

    private void OnDisable() 
    {
        _epsilonManager.OnParticleShoot -= ParticleShoot;
    }

    private void Setup()
    {
        _epsilonManager = GetComponent<EpsilonLevelManager>();
    }

    private void ParticleShoot()
    {
        // Set is particle attached boolean to false
        _epsilonManager.IsParticleAttached = false;
        // Increment quarks used variable
        _epsilonManager.NumQuarksUsed++;
        Rigidbody particleRB = _epsilonManager.CurrentAttachedParticle.GetComponent<Rigidbody>();
        // Get the remote's transform
        Transform ovrRemote = _epsilonManager.OVRRemote;
        // Detach particle from spawn point's transform
        _epsilonManager.ParticleSpawnTrans.DetachChildren();
        // Shoot particle in direction the remote is aimed towards
        particleRB.velocity = ovrRemote.forward * _shotSpeed;
    }
}
