using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonUnlockBaryon : MonoBehaviour
{
    [SerializeField] EpsilonNucleus _epsilonNucleus;            // The nucleus used to create the desired particle that corresponds to the right baryon

    private bool _isBaryonUnlocked;
    private MeshRenderer _meshRenderer;
    private Collider _col;

    // Start is called before the first frame update
    void Start()
    {
        _isBaryonUnlocked = false;
        // Hide baryon's mesh renderer and disable collider
        _meshRenderer = GetComponent<MeshRenderer>();
        _col = GetComponent<Collider>();
        _meshRenderer.enabled = false;
        _col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_epsilonNucleus.IsParticleCreated && !_isBaryonUnlocked)
        {
            _isBaryonUnlocked = true;
            // Re-enable mesh renderer and collider
            _meshRenderer.enabled = true;
            _col.enabled = true;
        }
    }
}
