using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonParticleAttach : MonoBehaviour
{
    private EpsilonLevelManager _epsilonManager;

    private void OnEnable() 
    {
        Setup();
        _epsilonManager.OnParticleAttach += ParticleAttach;
    }

    private void OnDisable() 
    {
        _epsilonManager.OnParticleAttach -= ParticleAttach;
    }

    private void Setup()
    {
        _epsilonManager = GetComponent<EpsilonLevelManager>();
    }

    private void ParticleAttach(GameObject particle)
    {
        if (_epsilonManager.NumQuarksUsed < _epsilonManager.MaxQuarks)
        {
            // If a particle is already attached, destroy it
            if (_epsilonManager.IsParticleAttached)
            {
                GameObject oldParticle = GameManager.Instance.RemoteSpawnTrans.GetChild(0).gameObject;
                Destroy(oldParticle);
            }

            // Set is particle attached boolean to true
            _epsilonManager.IsParticleAttached = true;
            Debug.Log("Number quarks used: " + _epsilonManager.NumQuarksUsed);
            // Instantiate the particle
            GameObject p = Instantiate(particle, GameManager.Instance.RemoteSpawnTrans.position, Quaternion.identity);
            // Sets its parent transform to be the spawn point transform on the front of the remote
            p.transform.SetParent(GameManager.Instance.RemoteSpawnTrans);
            // Cache the currently attached particle
            _epsilonManager.CurrentAttachedParticle = p;
        }
    }
}
