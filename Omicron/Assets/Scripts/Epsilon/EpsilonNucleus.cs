using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonNucleus : MonoBehaviour
{
    [SerializeField] private LayerMask _particleLayerMask;

    private List<GameObject> _epsilonQuarks;
    private List<GameObject> _epsilonNucleiParticles;

    private void Start() 
    {
        // Clear the lists
        _epsilonNucleiParticles.Clear();
        _epsilonQuarks.Clear();
    }

    private void OnTriggerEnter(Collider col)
    {
        // Check if particle has entered the nucleus
        if (col.gameObject.layer == _particleLayerMask.value)
        {
            if (col.CompareTag("Quark"))
            {
                // Add the quark to the list of quarks in the nucleus
                _epsilonQuarks.Add(col.gameObject);
            }
            // else if (col.CompareTag("NucleiParticle"))
            // {

            // }
        }
    }
}
