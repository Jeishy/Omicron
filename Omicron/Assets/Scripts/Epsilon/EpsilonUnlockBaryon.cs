using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonUnlockBaryon : MonoBehaviour
{
    [SerializeField] EpsilonNucleus _epsilonNucleus;            // The nucleus used to create the desired particle that corresponds to the right baryon
    [SerializeField] private Transform _spawnPoint;

    private bool _isBaryonUnlocked;
    private MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _isBaryonUnlocked = false;
        // Hide baryon's mesh renderer
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_epsilonNucleus.IsParticleCreated && !_isBaryonUnlocked)
        {
            _isBaryonUnlocked = true;
            // Re-enable mesh renderer
            _meshRenderer.enabled = true;
            transform.position = _spawnPoint.position;
        }
    }
}
