﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonAtomNucleus : MonoBehaviour
{
    [HideInInspector] public bool IsParticleCreated;                                                               // A bool for flagging if  the desured particle in this nucleus has been created
    [HideInInspector] public List<EpsilonBaryon> EpsilonBaryonsInNucleus = new List<EpsilonBaryon>();              // A list of all baryons in the nuclues


    private int _chargeInNucleus;
    private EpsilonLevelManager _epsilonManager;

    private void Start() 
    {
        // Set charge in nucleus to be neutral (to 0)
        _chargeInNucleus = 0;
        _epsilonManager = GameObject.Find("EpsilonLevelManager").GetComponent<EpsilonLevelManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        // Check if a particle has entered the nucleus
        if (col.gameObject.layer == 17)
        {
            // Set layer of particle to default so that it cannot be targetted
            col.gameObject.layer = 0;
            // Play particle enter sound
            AudioManager.Instance.Play("ParticleEnter");
            // Get reference to the particles epsilon particle component
            EpsilonParticle _epsilonParticle = col.gameObject.GetComponent<EpsilonParticle>();
             if (col.CompareTag("ShelfBaryon"))
            {
                // Add the quark to the list of quarks in the nucleus
                EpsilonBaryon epsilonBaryon = col.gameObject.GetComponent<EpsilonBaryon>();
                EpsilonBaryonsInNucleus.Add(epsilonBaryon);
            }

            // Set Nucleus centre trans to transform of nucleus
            _epsilonParticle.NucleusCentreTrans = transform;
            // Set HasEnteredNucleus bool to true
            _epsilonParticle.HasEnteredNucleus = true;
        }
    }

    private void CheckBaryonsInNucleus()
    {
        _chargeInNucleus = 0;
        foreach (EpsilonBaryon baryon in EpsilonBaryonsInNucleus)
        {
            // Get the sum charge of the nucleus
            _chargeInNucleus += baryon.Charge;
        }

        if (_chargeInNucleus == 18)
        {
            // Destroy all quarks in nucleus
            StartCoroutine(WaitToDestroyParticlesInNucleus(EpsilonBaryonsInNucleus));
            // Spawn atom
            IsParticleCreated = true;
        }
    }

    public IEnumerator WaitToDestroyParticlesInNucleus(List<EpsilonBaryon> baryons)
    {
        yield return new WaitForSeconds(_epsilonManager.TimeTillParticlesDestroyed);
        // Destroy all baryons in the baryons in nucleus list
        foreach (EpsilonBaryon baryon in baryons)
        {
            GameObject baryonGO = baryon.gameObject;
            Destroy(baryonGO);
        }
    }

    public void DestroyParticlesInNucleus(List<EpsilonBaryon> baryons)
    {
        // Destroy all baryons in the baryons in nucleus list
        foreach (EpsilonBaryon baryon in baryons)
        {
            GameObject baryonGO = baryon.gameObject;
            Destroy(baryonGO);
        }

        // Clear the epsilon quarks in nucles list when the puzzle has been completed
        EpsilonBaryonsInNucleus.Clear();
    }
}