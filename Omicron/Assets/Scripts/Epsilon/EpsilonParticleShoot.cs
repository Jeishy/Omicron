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
        // Renable collider on particle
        Collider particleCol = _epsilonManager.CurrentAttachedParticle.GetComponent<Collider>();
        particleCol.enabled = true;
        // Attach particle to current puzzle transform
        Transform particleTrans = _epsilonManager.CurrentAttachedParticle.transform;
        particleTrans.SetParent(GameManager.Instance.FindActivePuzzle().transform);
        // Set is particle attached boolean to false
        _epsilonManager.IsParticleAttached = false;
        Rigidbody particleRB = _epsilonManager.CurrentAttachedParticle.GetComponent<Rigidbody>();
        // Get the remote's transform
        Transform ovrRemote = _epsilonManager.OVRRemote;
        // Detach particle from spawn point's transform
        _epsilonManager.ParticleSpawnTrans.DetachChildren();
        // Shoot particle in direction the remote is aimed towards
        particleRB.velocity = ovrRemote.forward * _shotSpeed;

        if (particleCol.CompareTag("ShelfQuark"))
        // Increment number of quarks used variable
            _epsilonManager.NumQuarksUsed++;
        else if (particleCol.CompareTag("ShelfBaryon"))
            _epsilonManager.NumBaryonsUsed++;
    }
}
