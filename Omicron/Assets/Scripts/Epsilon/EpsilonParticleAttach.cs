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
        if (_epsilonManager.IsParticleAttached)
        {
            // Instantiate the particle
            GameObject p = Instantiate(particle, GameManager.Instance.RemoteSpawnTrans.position, Quaternion.identity);
            // Sets its parent transform to be the spawn point transform on the front of the remote
            p.transform.SetParent(GameManager.Instance.RemoteSpawnTrans);
            // Cache the currently attached particle
            _epsilonManager.CurrentAttachedParticle = particle;
        }
    }
}
