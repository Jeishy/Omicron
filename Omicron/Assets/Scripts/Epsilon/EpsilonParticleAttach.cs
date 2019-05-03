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
        if ((_epsilonManager.NumQuarksUsed < _epsilonManager.MaxQuarks && particle.GetComponent<Collider>().CompareTag("ShelfQuark")) || 
                    (_epsilonManager.NumBaryonsUsed < _epsilonManager.MaxBaryons && particle.GetComponent<Collider>().CompareTag("ShelfBaryon")))
        {
            // Play particle pickup sound
            AudioManager.Instance.Play("ParticlePickup");
            // If a particle is already attached, destroy it
            if (_epsilonManager.IsParticleAttached)
            {
                _epsilonManager.CurrentAttachedParticle = null;
                GameObject oldParticle = GameManager.Instance.RemoteSpawnTrans.GetChild(0).gameObject;
                Destroy(oldParticle);
            }
            
            // Set is particle attached boolean to true
            _epsilonManager.IsParticleAttached = true;
            // Instantiate the particle
            GameObject p = Instantiate(particle, GameManager.Instance.RemoteSpawnTrans.position, Quaternion.identity);
            // Sets its parent transform to be the spawn point transform on the front of the remote
            p.transform.SetParent(GameManager.Instance.RemoteSpawnTrans);
            // Disable collider on particle
            Collider particleCol = p.GetComponent<Collider>();
            particleCol.enabled = false;
            // Cache the currently attached particle
            _epsilonManager.CurrentAttachedParticle = p;
        }
    }
}
